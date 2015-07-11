using System;
using System.IO;
using HeufyAudio.Utils;
using Newtonsoft.Json;

namespace HeufyAudio.Config
{
    public class ConfigHandler
    {
        public static void SaveConfig(Configuration config)
        {
            string json = JsonConvert.SerializeObject(config);
            File.WriteAllText(Path.Combine(FileUtils.AppDataFolder, "settings.json"), json);
        }

        public static Configuration ReadConfig()
        {
            string path = Path.Combine(FileUtils.AppDataFolder, "settings.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<Configuration>(json);
            }

            return new Configuration()
            {
                RecordingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                RecordingPrefix = "Recording",
                NextRecordingNumber = 1,
                AutoIncrementRecordingNumber = true,
                OutputFormat = "wav"
            };
        }
    }
}
