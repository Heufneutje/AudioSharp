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

        // Interface
        public bool ShowTrayIcon { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool AlwaysOnTop { get; set; }
        public bool StartMinimized { get; set; }

        // Hotkeys
        public Dictionary<HotkeyUtils.HotkeyType, Tuple<Key, ModifierKeys>> GlobalHotkeys { get; set; }
    }
}
