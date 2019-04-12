using NAudio.Lame;
using System;
using System.Collections.ObjectModel;

namespace AudioSharp.Core
{
    [Serializable]
    public class LAMEPresetWrapper
    {
        private static readonly LAMEPresetWrapper _defaultAbr = new LAMEPresetWrapper("128 kbps", LAMEPreset.ABR_128);
        private static readonly LAMEPresetWrapper _defaultVbr = new LAMEPresetWrapper("V2; 170-210 kbps", LAMEPreset.V2);
        private static readonly LAMEPresetWrapper _defaultPreset = new LAMEPresetWrapper("Standard Quality; 170-210 kbps", LAMEPreset.STANDARD);

        public string Description { get; set; }
        public int LamePreset { get; set; }

        private LAMEPresetWrapper(string description, LAMEPreset preset)
        {
            Description = description;
            LamePreset = (int)preset;
        }

        public static ObservableCollection<LAMEPresetWrapper> GetAbrValues()
        {
            ObservableCollection<LAMEPresetWrapper> presets = new ObservableCollection<LAMEPresetWrapper>();
            presets.Add(new LAMEPresetWrapper("8 kbps", LAMEPreset.ABR_8));
            presets.Add(new LAMEPresetWrapper("16 kbps", LAMEPreset.ABR_16));
            presets.Add(new LAMEPresetWrapper("32 kbps", LAMEPreset.ABR_32));
            presets.Add(new LAMEPresetWrapper("48 kbps", LAMEPreset.ABR_48));
            presets.Add(new LAMEPresetWrapper("64 kbps", LAMEPreset.ABR_64));
            presets.Add(new LAMEPresetWrapper("96 kbps", LAMEPreset.ABR_96));
            presets.Add(_defaultAbr);
            presets.Add(new LAMEPresetWrapper("160 kbps", LAMEPreset.ABR_160));
            presets.Add(new LAMEPresetWrapper("256 kbps", LAMEPreset.ABR_256));
            presets.Add(new LAMEPresetWrapper("320 kbps", LAMEPreset.ABR_320));
            return presets;
        }

        public static ObservableCollection<LAMEPresetWrapper> GetVbrValues()
        {
            ObservableCollection<LAMEPresetWrapper> presets = new ObservableCollection<LAMEPresetWrapper>();
            presets.Add(new LAMEPresetWrapper("V9; 45-85 kbps", LAMEPreset.V9));
            presets.Add(new LAMEPresetWrapper("V8; 65-105 kbps", LAMEPreset.V8));
            presets.Add(new LAMEPresetWrapper("V7; 80-120 kbps", LAMEPreset.V7));
            presets.Add(new LAMEPresetWrapper("V6; 95-135 kbps", LAMEPreset.V6));
            presets.Add(new LAMEPresetWrapper("V5; 110-150 kbps", LAMEPreset.V5));
            presets.Add(new LAMEPresetWrapper("V4; 145-185 kbps", LAMEPreset.V4));
            presets.Add(new LAMEPresetWrapper("V3; 155-195 kbps", LAMEPreset.V3));
            presets.Add(_defaultVbr);
            presets.Add(new LAMEPresetWrapper("V1; 200-250 kbps", LAMEPreset.V1));
            presets.Add(new LAMEPresetWrapper("V0; 220-260 kbps", LAMEPreset.V0));
            return presets;
        }

        public static ObservableCollection<LAMEPresetWrapper> GetPresetValues()
        {
            ObservableCollection<LAMEPresetWrapper> presets = new ObservableCollection<LAMEPresetWrapper>();
            presets.Add(_defaultPreset);
            presets.Add(new LAMEPresetWrapper("Medium Quality; 145-185 kbps", LAMEPreset.MEDIUM));
            presets.Add(new LAMEPresetWrapper("Extreme Quality; 220-260 kbps", LAMEPreset.EXTREME));
            presets.Add(new LAMEPresetWrapper("Insane Quality; 320 kbps", LAMEPreset.INSANE));
            return presets;
        }

        public static LAMEPresetWrapper GetDefaultAbrValue()
        {
            return _defaultAbr;
        }

        public static LAMEPresetWrapper GetDefaultVbrValue()
        {
            return _defaultVbr;
        }

        public static LAMEPresetWrapper GetDefaultPresetValue()
        {
            return _defaultPreset;
        }
    }
}
