using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AudioSharp.Utils;

namespace AudioSharp.Config
{
    public class Configuration
    {
        // General
        public string RecordingsFolder { get; set; }
        public string RecordingPrefix { get; set; }
        public int NextRecordingNumber { get; set; }
        public bool AutoIncrementRecordingNumber { get; set; }
        public string OutputFormat { get; set; }

        // Interface
        public bool ShowTrayIcon { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool AlwaysOnTop { get; set; }

        // Hotkeys
        public Dictionary<HotkeyUtils.HotkeyType, Tuple<Keys, Keys>> GlobalHotkeys;
    }
}
