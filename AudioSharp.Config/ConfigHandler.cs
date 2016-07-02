using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using AudioSharp.Utils;
using Newtonsoft.Json;

namespace AudioSharp.Config
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
                RecordingPrefix = "Recording{n}",
                NextRecordingNumber = 1,
                AutoIncrementRecordingNumber = true,
                OutputFormat = "wav",
                ShowTrayIcon = true,
                GlobalHotkeys = new Dictionary<HotkeyUtils.HotkeyType, Tuple<Key, ModifierKeys>>(),
                RecordingSettingsPanelVisible = true,
                RecordingOutputPanelVisible = true
            };
        }
    }
}
