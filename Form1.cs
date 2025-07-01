using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using SimListener;
using SimListView;
using SimRedis;

namespace Broadcast
{
    public partial class Form1 : Form
    {
        private static EventLogSettings myEventLogSettings = new EventLogSettings
        {
            SourceName = _sourceName,
            LogName = _logName
        };
        private const string _sourceName = "Simulator Service";
        private const string _logName = "Application";
        List<string> Parameters = new List<string>();
        string projectDirectory = Path.Combine(Environment.CurrentDirectory, "data");
        string configDirectory = Path.Combine(Environment.CurrentDirectory, "settings");
        private ILoggerFactory factory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.AddDebug();
            builder.AddEventLog(myEventLogSettings);
        });

        private ILogger? logger = null;
        public Form1()
        {
            InitializeComponent();

            this.logger = factory.CreateLogger("Broadcast Listener");

            SimListener = new SimListener.Connect();
            SimListener.SimConnected += SimListener_SimConnected;
            SimListener.SimDataRecieved += SimListener_SimDataRecieved;
            SimListener.Enabled = false; // Initially disabled
                                         // simulatorOn.Checked = false; // Initially disabled
                                         // simulatorOn.Enabled = false; // Disable until Simulator is configured

            // Fix for CS1503: Convert port.Text (string) to an integer using int.Parse
            SimRedis = new SimRedis.SimRedis();
            SimRedis.DataRecieved += SimRedis_RedisDataRecieved;
            SimRedis.OnConnected += SimRedis_OnConnected;
            SimRedis.OnDisconnected += SimRedis_OnDisconnected;
            SimRedis.Enabled = false; // Enable Redis connection
            EnableRedis.Checked = false; // Initially enabled

            btnPurge.Enabled = false; // Initially disabled
            server.Text = SimRedis.server; // Default server    
            port.Text = SimRedis.port.ToString(); // Default port

            SimulatorData.ItemChanged += SimulatorData_ItemChanged;
        }

        private void SimulatorData_ItemChanged(object? sender, ItemData e)
        {
            SimRedis.write(e.key,e.index , e.value);
        }

        private void SimRedis_OnDisconnected(object? sender, EventArgs e)
        {
            if( InvokeRequired )
            {
                Invoke(new Action(() => SimRedis_OnDisconnected(sender, e)));
                return;
            }
            btnPurge.Enabled = false; // Disable purge button on disconnect
        }

        private void SimRedis_OnConnected(object? sender, EventArgs e)
        {
            if( InvokeRequired )
            {
                Invoke(new Action(() => SimRedis_OnConnected(sender, e)));
                return;
            }
            btnPurge.Enabled = true; // Enable purge button on connect  
        }

        private void SimRedis_RedisDataRecieved(object? sender, RedisDataEventArgs e)
        {
            foreach (KeyValuePair<string, string> kvp in e.AircraftData)
            {
                if (kvp.Key == null || kvp.Value == null)
                {
                    logger?.LogError("Received empty or null data from Redis.");
                    lastMessage.Text = "Received empty or null data from Redis.";
                    continue;
                }
                logger?.LogDebug($"Data {kvp.Key} = {kvp.Value}"); // Add unit if available
            }
        }
        private void SimListener_SimDataRecieved(object? sender, SimListener.SimulatorData e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SimListener_SimDataRecieved(sender, e)));
                return;
            }

            foreach (Dictionary<string, string> kvp in e.AircraftData)
            {
                if (kvp == null || kvp.Count == 0)
                {
                    logger?.LogError("Received empty or null data from simulator.");
                    lastMessage.Text = "Received empty or null data from simulator.";
                    continue;
                }
                foreach (KeyValuePair<string, string> item in kvp)
                {
                    if (item.Key == null || item.Value == null)
                    {
                        logger?.LogError("Received empty or null key/value from simulator.");
                        lastMessage.Text = "Received empty or null key/value from simulator.";
                        continue;
                    }
                    logger?.LogInformation($"Data {item.Key} = {item.Value}"); // Add unit if available
                    SimulatorData.setValue(item.Key, item.Value);
                }
            }

        }

        private void SimListener_SimConnected(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                logger?.LogError("Simulator connection failed: sender is null");
                lastMessage.Text = "Simulator connection failed: sender is null";
                return;
            }
            if (sender is not Connect)
            {
                logger?.LogError("Simulator connection failed: sender is not Connect");
                lastMessage.Text = "Simulator connection failed: sender is not Connect";
                return;
            }
            Connect s = (Connect)sender;

            s.AddRequests(SimulatorData.to_string());

            logger?.LogDebug("Simulator connected");
            lastMessage.Text = "Simulator connected";
        }

        private void EnableSimData(object sender, EventArgs e)
        {
            SimListener.Enabled = simData.Checked;
            logger?.LogInformation($"Simulator data enabled: {SimListener.Enabled}");
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
                Title = "Load Aircraft Data",
                Multiselect = true
            };

            if (openAircraftData.ShowDialog() == DialogResult.OK)
            {
                SimulatorData.clear();

                foreach (string filePath in openAircraftData.FileNames)
                {
                    if (File.Exists(filePath))
                    {
                        loadFile(filePath);

                    }
                }
            }
        }

        private void loadFile(string filePath)
        {
            simManual.Checked = true;

            if (string.IsNullOrEmpty(filePath))
            {
                // simulatorOn.Enabled = false;
                logger?.LogError("No file selected.");
                lastMessage.Text = "No file selected.";
                simData.Enabled = false;
                simManual.Enabled = false;
                simTest.Enabled = false;
                return;
            }
            try
            {
                logger?.LogInformation($"Aircraft data loaded from {filePath}");
                lastMessage.Text = $"Aircraft data loaded from {filePath}";
                SimulatorData.load(filePath);
                simData.Enabled = true;
                simManual.Enabled = true;
                simTest.Enabled = true;
            }
            catch (Exception ex)
            {
                logger?.LogError($"Error loading aircraft data: {ex.Message}");
                lastMessage.Text = $"Error loading aircraft data: {ex.Message}";
                simData.Enabled = false;
                simManual.Enabled = false;
                simTest.Enabled = false;
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

        private void loadForm(object sender, EventArgs e)
        {
            Yaml yaml = new Yaml(Path.Combine(configDirectory, "startup.yaml"));
            if (yaml == null || yaml.Files.Count == 0)
            {
                logger?.LogError("No YAML files found in the configuration directory.");
                lastMessage.Text = "No YAML files found in the configuration directory.";
            }
            else
            {
                SimulatorData.clear();
                foreach (string file in yaml.Files)
                {
                    string f = Path.Combine(projectDirectory, file);
                    loadFile(f);
                }
            }

            if (yaml == null)
            {
                logger?.LogError("YAML data is null.");
                lastMessage.Text = "YAML data is null.";
                return;
            }

            if (yaml.RedisEnabled)
            {
                EnableRedis.Checked = true;
            }
            else
            {
                EnableRedis.Checked = false;
            }

            if (yaml.CurrentMode == Yaml.Mode.Manual)
            {
                simManual.Checked = true;
            }
            else if (yaml.CurrentMode == Yaml.Mode.Simulator)
            {
                simData.Checked = true;
            }
            else if (yaml.CurrentMode == Yaml.Mode.Test)
            {
                simTest.Checked = true;
            }

        }

        private void onPurgeRedis(object sender, MouseEventArgs e)
        {
            SimRedis.Purge();
        }
    }
}
