using SimListener;
using System.Diagnostics;
using YamlDotNet.Serialization;

namespace Broadcast
{
    public class Yaml
    {
        public List<string> Files
        {
            get
            {
                if (Data == null)
                {
                    return new List<string>();
                }
                return Data.Load;
            }
        }
        public Mode CurrentMode
        {
            get
            {
                if (Data == null)
                {
                    return Mode.Manual;
                }
                return Data.Mode;
            }
        }
        public bool RedisEnabled
        {
            get
            {
                if (Data == null)
                {
                    return false;
                }
                return Data.Redis;
            }
        }

        public bool SimulatorAutostart
        {
            get
            {
                if (Data == null || Data.simulator == null)
                {
                    return false;
                }
                return Data.simulator.autostart;
            }
        }
        public string SimulatorCommand
        {
            get
            {
                if (Data == null || Data.simulator == null)
                {
                    return "";
                }
                return Data.simulator.command;
            }
        }
        private DataDefinition? Data { get;  set; }
        public enum Mode
        {
            Test,
            Simulator,
            Manual
        }
        private class Simulator
        {
            public bool autostart { get; set; } = false;
            public string command { get; set; } = "";
        }
        private class DataDefinition
        {
            public List<string> Load { get; set; } = new List<string>() ;
            public Mode Mode { get; set; } = Mode.Manual;
            public bool Redis { get; set; } = false;
            public Simulator simulator { get; set; } = new Simulator() ;
        }
        public Yaml(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified YAML file does not exist.", filePath);
            }
            // Load the YAML file
            string yaml = File.ReadAllText(filePath);

            Data = Deserializer(yaml);

        }
        private DataDefinition Deserializer(string Data)
        {
            var deserializer = new DeserializerBuilder()
                .WithCaseInsensitivePropertyMatching()
                .Build();

            return deserializer.Deserialize<DataDefinition>(Data.ToString());
        }
    }
}
