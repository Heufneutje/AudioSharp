using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HeufyAudio.Config
{
    public class ConfigHandler
    {
        public static void SaveConfig(Configuration config)
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HeufyAudioRecorder");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            string json = JsonConvert.SerializeObject(config);
            File.WriteAllText(Path.Combine(dir, "settings.json"), json);
        }

        public static Configuration ReadConfig()
        {
            string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HeufyAudioRecorder");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            if (File.Exists(Path.Combine(dir, "settings.json")))
            {
                string json = File.ReadAllText(Path.Combine(dir, "settings.json"));
                return JsonConvert.DeserializeObject<Configuration>(json);
            }

            return new Configuration()
            {
                RecordingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                RecordingPrefix = "Recording"
            };
        }
    }
}
