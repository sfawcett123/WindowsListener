using SimListener;
using System.Diagnostics;
using System.Reflection.Metadata;
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

            SimulatorData.Items.Clear();
            SimulatorData.Columns.Clear();
            SimulatorData.Columns.Add("Parameter", 200, HorizontalAlignment.Center);
            SimulatorData.Columns.Add("Value", 200, HorizontalAlignment.Center);
            SimulatorData.Columns.Add("Unit", 100, HorizontalAlignment.Center);

            foreach (string parameter in Parameters)
            {
                string cleaned = parameter.Trim().Trim('"');
                ListViewItem item = new ListViewItem(cleaned);
                item.SubItems.Add("0");
                SimulatorData.Items.Add(item);
            }

            listview_to_string();
     
            SimListener = new SimListener.Connect();
            SimListener.SimConnected += SimListener_SimConnected;
            SimListener.SimDataRecieved += SimListener_SimDataRecieved;
            
        }

        private void listview_add_value(string key, string value)
        {
            if (SimulatorData.InvokeRequired)
            {
                SimulatorData.Invoke(new Action(() => listview_add_value(key, value)));
                return;
            }

            foreach (ListViewItem item in SimulatorData.Items)
            {
                if (item.Text == key)
                {
                    item.SubItems[1].Text = value;
                    return; // Exit if the item already exists
                }
            }
            ListViewItem newItem = new ListViewItem(key);
            newItem.SubItems.Add(value);
            SimulatorData.Items.Add(newItem);
        }

        public List<string> listview_to_string()
        {
            if (SimulatorData.InvokeRequired)
            {
                return (List<string>)SimulatorData.Invoke(new Func<List<string>>(listview_to_string));
            }

            List<string> list = new List<string>();
            foreach (ListViewItem item in SimulatorData.Items)
            {
                list.Add(item.Text);
            }
            Debug.WriteLine(String.Join("\n", list));

            return list;
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
                    Debug.WriteLine($"{key} = {kvp[key]} ");
                    listview_add_value(key, kvp[key]);
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

            // Fixing the syntax error by correctly invoking the listview_to_string method
            s.AddRequests(listview_to_string());

            Debug.WriteLine("Simulator connected");
        }
    }
}
