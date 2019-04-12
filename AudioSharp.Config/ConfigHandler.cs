using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using AudioSharp.Utils;

namespace AudioSharp.Config
{
    public class ConfigHandler
    {
        public static void SaveConfig(Configuration config)
        {
            string json = JsonUtils.SerializeObject(config);
            File.WriteAllText(Path.Combine(FileUtils.AppDataFolder, "settings.json"), json);
        }

        public static Configuration ReadConfig()
        {
            string path = Path.Combine(FileUtils.AppDataFolder, "settings.json");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                Configuration config = JsonUtils.DeserializeObject<Configuration>(json);

                if (config.MP3EncodingPreset == 0)
                    config.MP3EncodingPreset = 1001;

                return config;
            }

            return new Configuration()
            {
                RecordingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                RecordingPrefix = "Recording{n}",
                NextRecordingNumber = 1,
                AutoIncrementRecordingNumber = true,
                OutputFormat = "wav",
                ShowTrayIcon = true,
                GlobalHotkeys = new Dictionary<HotkeyType, Tuple<Key, ModifierKeys>>(),
                RecordingSettingsPanelVisible = true,
                RecordingOutputPanelVisible = true,
                CheckForUpdates = true
            };
        }
    }
}
