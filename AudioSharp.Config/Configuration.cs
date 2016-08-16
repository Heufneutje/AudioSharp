using System;
using System.Collections.Generic;
using System.Windows.Input;
using AudioSharp.Utils;

namespace AudioSharp.Config
{
    public class Configuration
    {
        // Recording
        public bool PromptForFileName { get; set; }
        public string RecordingsFolder { get; set; }
        public string RecordingPrefix { get; set; }
        public int NextRecordingNumber { get; set; }
        public bool AutoIncrementRecordingNumber { get; set; }
        public string OutputFormat { get; set; }

        // General Settings
        public bool ShowTrayIcon { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool CloseToTray { get; set; }
        public bool AlwaysOnTop { get; set; }
        public bool StartMinimized { get; set; }
        public bool CheckForUpdates { get; set; }

        // Hotkeys
        public Dictionary<HotkeyUtils.HotkeyType, Tuple<Key, ModifierKeys>> GlobalHotkeys { get; set; }

        // View
        public bool RecordingSettingsPanelVisible { get; set; }
        public bool RecordingOutputPanelVisible { get; set; }

        // Window
        public Dictionary<string, Tuple<double, double>> WindowSizes { get; set; }

        public Configuration()
        {
            WindowSizes = new Dictionary<string, Tuple<double, double>>();
            WindowSizes.Add("mainWindow", new Tuple<double, double>(600, 315));
        }

        public Tuple<double, double> GetWindowSize(string windowName)
        {
            if (!WindowSizes.ContainsKey(windowName))
                return null;

            return WindowSizes[windowName];
        }

        public void SetWindowSize(string windowName, double width, double height)
        {
            if (!WindowSizes.ContainsKey(windowName))
                WindowSizes.Add(windowName, new Tuple<double, double>(width, height));
            else
                WindowSizes[windowName] = new Tuple<double, double>(width, height);
        }
    }
}
