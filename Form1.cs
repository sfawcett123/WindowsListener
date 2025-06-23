using SimListener;
using SimRedis;
using System.Diagnostics;

namespace Broadcast
{
    public partial class Form1 : Form
    {
        List<string> Parameters = new List<string>();
        string projectDirectory = Path.Combine(Environment.CurrentDirectory, "data");

        public Form1()
        {
            InitializeComponent();

            SimListener = new SimListener.Connect();
            SimListener.SimConnected += SimListener_SimConnected;
            SimListener.SimDataRecieved += SimListener_SimDataRecieved;
            SimListener.Enabled = false; // Initially disabled
                                         // simulatorOn.Checked = false; // Initially disabled
                                         // simulatorOn.Enabled = false; // Disable until Simulator is configured

            // Fix for CS1503: Convert port.Text (string) to an integer using int.Parse
            SimRedis = new SimRedis.SimRedis();
            SimRedis.RedisDataRecieved += SimRedis_RedisDataRecieved;
            SimRedis.Enabled = false; // Enable Redis connection
            EnableRedis.Checked = false; // Initially enabled

            server.Text = SimRedis.server; // Default server    
            port.Text = SimRedis.port.ToString(); // Default port
        }

        private void SimRedis_RedisDataRecieved(object? sender, RedisDataEventArgs e)
        {
            foreach (KeyValuePair<string, string> kvp in e.AircraftData)
            {
                if (kvp.Key == null || kvp.Value == null)
                {
                    Debug.WriteLine("Received empty or null data from Redis.");
                    lastMessage.Text = "Received empty or null data from Redis.";
                    continue;
                }
                Debug.WriteLine($"Data {kvp.Key} = {kvp.Value}"); // Add unit if available
            }
        }
        private void SimListener_SimDataRecieved(object? sender, SimListener.SimulatorData e)
        {
            foreach (Dictionary<string, string> kvp in e.AircraftData)
            {
                if (kvp == null || kvp.Count == 0)
                {
                    Debug.WriteLine("Received empty or null data from simulator.");
                    lastMessage.Text = "Received empty or null data from simulator.";
                    continue;
                }
            }
        }

        private void SimListener_SimConnected(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                Debug.WriteLine("Simulator connection failed: sender is null");
                lastMessage.Text = "Simulator connection failed: sender is null";
                return;
            }
            if (sender is not Connect)
            {
                Debug.WriteLine("Simulator connection failed: sender is not Connect");
                lastMessage.Text = "Simulator connection failed: sender is not Connect";
                return;
            }
            Connect s = (Connect)sender;

            s.AddRequests(SimulatorData.to_string());

            Debug.WriteLine("Simulator connected");
            lastMessage.Text = "Simulator connected";
        }
        private void PeriodicEvents_Tick(object sender, EventArgs e)
        {
            SimRedis.write("TEST", "123");
        }

        private void EnableSimData(object sender, EventArgs e)
        {
            //SimListener.Enabled = simulatorOn.Checked;
        }

        private void OnEnableRedis(object sender, EventArgs e)
        {
            SimRedis.Enabled = EnableRedis.Checked;
        }

        private void OnLoadDataButton(object sender, EventArgs e)
        {

            OpenFileDialog openAircraftData = new OpenFileDialog
            {
                InitialDirectory = projectDirectory,
                Filter = "Aircraft Data Files (*.yaml)|*.yaml;*.yml|All Files (*.*)|*.*",
                Title = "Load Aircraft Data"
            };

            if (openAircraftData.ShowDialog() == DialogResult.OK)
            {
                simManual.Checked = true;
                string filePath = openAircraftData.FileName;
                if (string.IsNullOrEmpty(filePath))
                {
                    // simulatorOn.Enabled = false;
                    Debug.WriteLine("No file selected.");
                    lastMessage.Text = "No file selected.";
                    simData.Enabled = false;
                    simManual.Enabled = false;
                    simTest.Enabled = false;
                    return;
                }
                try
                {
                    Debug.WriteLine($"Aircraft data loaded from {filePath}");
                    lastMessage.Text = $"Aircraft data loaded from {filePath}";
                    SimulatorData.load(filePath);
                    simData.Enabled = true;
                    simManual.Enabled = true;
                    simTest.Enabled = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error loading aircraft data: {ex.Message}");
                    lastMessage.Text = $"Error loading aircraft data: {ex.Message}";
                    simData.Enabled = false;
                    simManual.Enabled = false;
                    simTest.Enabled = false;
                }

            }
        }
        private void simTest_CheckedChanged(object sender, EventArgs e)
        {
            SimulatorData.TestMode = simTest.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }
    }
}
