using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using SimListener;
using SimListView;
using SimRedis;
using System.Diagnostics;

namespace Broadcast
{
    public partial class Form1 : Form
    {
        private static EventLogSettings myEventLogSettings = new EventLogSettings
        {
            SourceName = _sourceName,
            LogName = _logName
        };

        public Process? process = null;
        public Yaml? yaml = null;
        private const string _sourceName = "Simulator Service";
        private const string _logName = "Application";
        private List<string> Parameters = new List<string>();
        private string projectDirectory = Path.Combine(Environment.CurrentDirectory, "data");
        private string configDirectory = Path.Combine(Environment.CurrentDirectory, "settings");
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

            simConnection = new SimListener.Connect();
            simConnection.SimConnected += SimListener_SimConnected;
            simConnection.SimDataRecieved += SimListener_SimDataRecieved;
            simConnection.Enabled = false; // Initially disabled
                                           // simulatorOn.Checked = false; // Initially disabled
                                           // simulatorOn.Enabled = false; // Disable until Simulator is configured

            // Fix for CS1503: Convert port.Text (string) to an integer using int.Parse
            redisConnection = new SimRedis.SimRedis();
            redisConnection.DataRecieved += SimRedis_RedisDataRecieved;
            redisConnection.OnConnected += SimRedis_OnConnected;
            redisConnection.OnDisconnected += SimRedis_OnDisconnected;
            redisConnection.Enabled = false; // Enable Redis connection
            EnableRedis.Checked = false; // Initially enabled

            btnPurge.Enabled = false; // Initially disabled
            server.Text = redisConnection.server; // Default server    
            port.Text = redisConnection.port.ToString(); // Default port

            if (displayData is not null) displayData.ItemChanged += SimulatorData_ItemChanged;
        }
        private void SimulatorData_ItemChanged(object? sender, ItemData e)
        {
            redisConnection.write(e.key, e.index, e.value);
        }
        private void SimRedis_OnDisconnected(object? sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SimRedis_OnDisconnected(sender, e)));
                return;
            }
            btnPurge.Enabled = false; // Disable purge button on disconnect
        }
        private void SimRedis_OnConnected(object? sender, EventArgs e)
        {
            if (InvokeRequired)
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

            foreach (Dictionary<string, string> aircraftData in e.AircraftData)
            {
                foreach (KeyValuePair<string, string> kvp in aircraftData)
                {
                    if (string.IsNullOrEmpty(kvp.Key) || string.IsNullOrEmpty(kvp.Value))
                    {
                        logger?.LogError("Received empty or null data from simulator.");
                        lastMessage.Text = "Received empty or null data from simulator.";
                        continue;
                    }
                    logger?.LogInformation($"Simulator data received -> {kvp.Key} = {kvp.Value}");
                    redisConnection.write(kvp.Key, kvp.Value);
                    displayData.setValue(kvp.Key, kvp.Value);
                }
            }
        }
        private void SimListener_SimConnected(object? sender, EventArgs e)
        {
            if (sender == null)
            {
                logger?.LogError("Simulator connection failed: sender is null");
                lastMessage.Text = "Simulator connection failed: sender is null";
                btnStart.Enabled = true;
                return;
            }
            if (sender is not Connect)
            {
                logger?.LogError("Simulator connection failed: sender is not Connect");
                lastMessage.Text = "Simulator connection failed: sender is not Connect";
                btnStart.Enabled = true;
                return;
            }
            Connect s = (Connect)sender;

            s.AddRequests(displayData.to_string());

            logger?.LogDebug("Simulator connected");
            lastMessage.Text = "Simulator connected";
            btnStart.Enabled = false;
        }
        private void EnableSimData(object sender, EventArgs e)
        {
            simConnection.Enabled = simData.Checked;
            logger?.LogInformation($"Simulator data enabled: {simConnection.Enabled}");
        }
        private void OnEnableRedis(object sender, EventArgs e)
        {
            redisConnection.Enabled = EnableRedis.Checked;
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
                displayData.clear();

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
                displayData.load(filePath);
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
            displayData.TestMode = simTest.Checked;
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox a = new AboutBox();
            a.Show();
        }
        private void loadForm(object sender, EventArgs e)
        {
            yaml = new Yaml(Path.Combine(configDirectory, "startup.yaml"));
            if (yaml == null || yaml.Files.Count == 0)
            {
                logger?.LogError("No YAML files found in the configuration directory.");
                lastMessage.Text = "No YAML files found in the configuration directory.";
            }
            else
            {
                displayData.clear();
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
                btnStart.Enabled = false;
            }
            else if (yaml.CurrentMode == Yaml.Mode.Simulator)
            {
                simData.Checked = true;
                btnStart.Enabled = true;
            }
            else if (yaml.CurrentMode == Yaml.Mode.Test)
            {
                simTest.Checked = true;
                btnStart.Enabled = false;
            }
            this.WindowState = FormWindowState.Minimized;
        }
        private void onPurgeRedis(object sender, MouseEventArgs e)
        {
            redisConnection.Purge();
        }
        public static void StartSimulator(string Command)
        {
            if (string.IsNullOrEmpty(Command))
            {
                throw new ArgumentException("Command cannot be null or empty.", nameof(Command));
            }

            ProcessStartInfo ProcessInfo = new ProcessStartInfo("cmd.exe", "/C " + Command)
            {
                CreateNoWindow = true,
                UseShellExecute = true
            };

            Process? Process = System.Diagnostics.Process.Start(ProcessInfo);
            if (Process == null)
            {
                throw new InvalidOperationException("Failed to start the process.");
            }
        }
        private void btnStartSimulator(object sender, EventArgs e)
        {
            try
            {
                if( checkProcess())
                {
                    logger?.LogInformation("Simulator is already running.");
                    lastMessage.Text = "Simulator is already running.";
                    btnStart.Enabled = false;
                    return;
                }

                Form1.StartSimulator(yaml?.SimulatorCommand ?? "");
                btnStart.Enabled = false;
                lastMessage.Text = "Initiated Flight Simulator Start";
            }
            catch (Exception)
            {
                logger?.LogError("Failed to start the simulator. Please check the command.");
                lastMessage.Text = "Failed to start the simulator. Please check the command.";
            }

        }


        private bool checkProcess()
        {
            SimProcess simProcess = new SimProcess();
            return simProcess.GetProcess("FlightSimulator2024");
        }
    }
}
