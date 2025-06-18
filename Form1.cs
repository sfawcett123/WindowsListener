using SimListener;
using System.Diagnostics;
using Test.Properties;

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
    }
}
