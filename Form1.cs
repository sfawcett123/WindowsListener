using SimListener;
using System.Diagnostics;
using Test.Properties;
using SimRedis;

namespace Test
{
    public partial class Form1 : Form
    {
        List<string> Parameters = new List<string>();
        public Form1()
        {
            InitializeComponent();

            Parameters = new List<string>(Settings.Default.Parameters.Split(','));

            foreach (string parameter in Parameters)
            {
                string cleaned = parameter.Trim().Trim('"');
                SimulatorData.add_value(cleaned, "0", ""); // Initialize with default value
            }

            SimListener = new SimListener.Connect();
            SimListener.SimConnected += SimListener_SimConnected;
            SimListener.SimDataRecieved += SimListener_SimDataRecieved;
            SimListener.Enabled = false; // Initially disabled
            simulatorOn.Checked = false; // Initially disabled


            // Fix for CS1503: Convert port.Text (string) to an integer using int.Parse
            SimRedis = new SimRedis.SimRedis(server.Text, int.Parse(port.Text));
            SimRedis.RedisDataRecieved += SimRedis_RedisDataRecieved;
            SimRedis.Enabled = true; // Enable Redis connection
            EnableRedis.Checked = true; // Initially enabled
        }

        private void SimRedis_RedisDataRecieved(object? sender, RedisDataEventArgs e)
        {
            foreach (KeyValuePair<string, string> kvp in e.AircraftData)
            {
                if (kvp.Key == null || kvp.Value == null)
                {
                    Debug.WriteLine("Received empty or null data from Redis.");
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
                    continue;
                }
                foreach (string key in kvp.Keys)
                {
                    SimulatorData.add_value(key, kvp[key], kvp.ContainsKey("Unit") ? kvp["Unit"] : ""); // Add unit if available
                }
            }
        }

        private void SimListener_SimConnected(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                Debug.WriteLine("Simulator connection failed: sender is null");
                return;
            }
            if (sender is not Connect)
            {
                Debug.WriteLine("Simulator connection failed: sender is not Connect");
                return;
            }
            Connect s = (Connect)sender;

            s.AddRequests(SimulatorData.to_string());

            Debug.WriteLine("Simulator connected");
        }
        private void PeriodicEvents_Tick(object sender, EventArgs e)
        {
            SimRedis.write("TEST", "123");
        }

        private void EnableSimData(object sender, EventArgs e)
        {
            SimListener.Enabled = simulatorOn.Checked;
        }

        private void OnEnableRedis(object sender, EventArgs e)
        {
            SimRedis.Enabled = EnableRedis.Checked;
        }
    }
}
