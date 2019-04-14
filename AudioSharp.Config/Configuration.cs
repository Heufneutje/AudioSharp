using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using AudioSharp.Core;
using AudioSharp.Utils;
using Newtonsoft.Json;

namespace AudioSharp.Config
{
    [Serializable]
    public class Configuration : BaseChangeTrackable, INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        // Recording
        public bool PromptForFileName { get; set; }
        public string RecordingsFolder { get; set; }
        public string RecordingPrefix { get; set; }
        public int NextRecordingNumber { get; set; }
        public bool AutoIncrementRecordingNumber { get; set; }
        public string OutputFormat { get; set; }

        private int _mp3EncodingPreset;
        public int MP3EncodingPreset
        {
            get
            {
                return _mp3EncodingPreset;
            }
            set
            {
                _mp3EncodingPreset = value;
                NotifyPropertyChanged(nameof(MP3EncodingPreset));
            }
        }

        [NonSerialized]
        private ObservableCollection<LAMEPresetWrapper> _mp3EncodingPresets;

        [JsonIgnore]
        [ChangeTrackingIgnore]
        public ObservableCollection<LAMEPresetWrapper> MP3EncodingPresets
        {
            get
            {
                return _mp3EncodingPresets;
            }
            set
            {
                _mp3EncodingPresets = value;
                NotifyPropertyChanged(nameof(MP3EncodingPresets));
            }
        }

        // General Settings
        public bool ShowTrayIcon { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool CloseToTray { get; set; }
        public bool AlwaysOnTop { get; set; }
        public bool StartMinimized { get; set; }
        public bool CheckForUpdates { get; set; }
        public bool RunAtStartup { get; set; }

        // Hotkeys
        public Dictionary<HotkeyType, Tuple<Key, ModifierKeys>> GlobalHotkeys { get; set; }

        // View
        public bool RecordingSettingsPanelVisible { get; set; } = true;
        public bool RecordingOutputPanelVisible { get; set; } = true;
        public bool StatusBarVisible { get; set; } = true;
        public bool StatusBarItemVisible { get; set; } = true;
        public bool FreeSpaceBarItemVisible { get; set; } = true;

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

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
