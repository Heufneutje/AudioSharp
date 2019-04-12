using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AudioSharp.Config;
using AudioSharp.Core;
using AudioSharp.GUI.CustomControls;
using AudioSharp.Translations;
using AudioSharp.Utils;

namespace AudioSharp.GUI.Wpf
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        #region Fields & Properties
        private ObservableCollection<LAMEPresetWrapper> _presetValues;
        private ObservableCollection<LAMEPresetWrapper> _abrValues;
        private ObservableCollection<LAMEPresetWrapper> _vbrValues;

        public Configuration Config { get; private set; }
        #endregion

        #region Constructor
        public SettingsWindow(Configuration currentConfig)
        {
            Config = CloneUtils.DeepCopy(currentConfig);
            Config.StartTracking();
            InitializeComponent();
            DataContext = Config;

            if (currentConfig.PromptForFileName)
                promptFileNameRadioButton.IsChecked = true;
            else
                generateFileNameRadioButton.IsChecked = true;

            _presetValues = LAMEPresetWrapper.GetPresetValues();
            _abrValues = LAMEPresetWrapper.GetAbrValues();
            _vbrValues = LAMEPresetWrapper.GetVbrValues();

            if (_presetValues.Any(x => x.LamePreset == Config.MP3EncodingPreset))
            {
                Config.MP3EncodingPresets = _presetValues;
                presetBitrateRadioButton.IsChecked = true;
            }
            else if (_abrValues.Any(x => x.LamePreset == Config.MP3EncodingPreset))
            {
                Config.MP3EncodingPresets = _abrValues;
                averageBitRateRadioButton.IsChecked = true;
            }
            else if (_vbrValues.Any(x => x.LamePreset == Config.MP3EncodingPreset))
            {
                Config.MP3EncodingPresets = _vbrValues;
                variableBitRateRadioButton.IsChecked = true;
            }

            foreach (KeyValuePair<HotkeyType, Tuple<Key, ModifierKeys>> hotkey in currentConfig.GlobalHotkeys)
            {
                FillHotkeyField(hotkey.Key, hotkey.Value.Item1, hotkey.Value.Item2);
            }

            SetPreview();

            SourceInitialized += delegate { this.HideMinimizeAndMaximizeButtons(); };
        }
        #endregion

        #region Control Events
        private void recordingFolderTextBox_ButtonClick(object sender, RoutedEventArgs e)
        {
            using (System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;

                Config.RecordingsFolder = fbd.SelectedPath;
                GuiHelper.UpdateTextBoxBindingTarget(recordingFolderTextBox);
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Config.RecordingsFolder))
            {
                MessageBox.Show(Messages.GUIErrorRecordingsFolder, Messages.GUIErrorCommon, MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Config.PromptForFileName = promptFileNameRadioButton.IsChecked ?? false;
            ConfigHandler.SaveConfig(Config);
            DialogResult = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void control_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            SetPreview();
        }
        
        private void generateFileNameRadioButton_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool isChecked = generateFileNameRadioButton.IsChecked ?? false;
            recordingPrefixTextBox.IsEnabled = isChecked;
            recordingNumberIntegerUpDown.IsEnabled = isChecked;
            autoIncrementCheckBox.IsEnabled = isChecked;
            nextRecordingPreviewTextBox.IsEnabled = isChecked;
            recordingPrefixLabel.IsEnabled = isChecked;
            nextRecordingNumberLabel.IsEnabled = isChecked;
            nextRecordingPreviewLabel.IsEnabled = isChecked;
            formatInfoImage.IsEnabled = isChecked;
        }

        private void showTrayIconCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool isChecked = showTrayIconCheckBox.IsChecked ?? false;
            minimizeToTrayCheckBox.IsEnabled = isChecked;
            closeToTrayCheckBox.IsEnabled = isChecked;
            if (!isChecked)
            {
                Config.MinimizeToTray = false;
                Config.CloseToTray = false;
                GuiHelper.UpdateToggleButtonBindingTarget(minimizeToTrayCheckBox);
                GuiHelper.UpdateToggleButtonBindingTarget(closeToTrayCheckBox);
            }
        }

        private void hotkeyTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.System)
                key = e.SystemKey;

            if (HotkeyUtils.IllegalHotkeys.Contains(key))
                return;

            HotkeyType hotkeyType = (HotkeyType)Convert.ToInt32(((ButtonTextBox)e.Source).Tag);
            if (key == Key.Escape)
            {
                ClearHotkey(hotkeyType);
            }
            else
            {
                FillHotkeyField(hotkeyType, key, Keyboard.Modifiers);
                Config.GlobalHotkeys[hotkeyType] = new Tuple<Key, ModifierKeys>(key, Keyboard.Modifiers);
            }
        }

        private void hotkeyTextBox_ButtonClick(object sender, RoutedEventArgs e)
        {
            ClearHotkey((HotkeyType)Convert.ToInt32(((ButtonTextBox)e.Source).Tag));
        }

        private void PresetBitrateRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Config.MP3EncodingPresets = _presetValues;
            if (!_presetValues.Any(x => x.LamePreset == Config.MP3EncodingPreset))
                Config.MP3EncodingPreset = LAMEPresetWrapper.GetDefaultPresetValue().LamePreset;
        }

        private void AverageBitRateRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Config.MP3EncodingPresets = _abrValues;
            if (!_abrValues.Any(x => x.LamePreset == Config.MP3EncodingPreset))
                Config.MP3EncodingPreset = LAMEPresetWrapper.GetDefaultAbrValue().LamePreset;
        }

        private void VariableBitRateRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            Config.MP3EncodingPresets = _vbrValues;
            if (!_vbrValues.Any(x => x.LamePreset == Config.MP3EncodingPreset))
                Config.MP3EncodingPreset = LAMEPresetWrapper.GetDefaultVbrValue().LamePreset;
        }

        #endregion

        #region Helper Methods
        private void SetPreview()
        {
            if (string.IsNullOrEmpty(Config.RecordingsFolder) || Config.RecordingPrefix == null || Config.RecordingPrefix.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
                nextRecordingPreviewTextBox.Text = "<invalid path>";
            else
                nextRecordingPreviewTextBox.Text = $"{Path.Combine(Config.RecordingsFolder, FormattingUtils.FormatString(Config.RecordingPrefix, Config.NextRecordingNumber))}.{Config.OutputFormat}";
        }

        private void ClearHotkey(HotkeyType hotkeyType)
        {
            FillHotkeyField(hotkeyType, Key.None, ModifierKeys.None);
            if (Config.GlobalHotkeys.ContainsKey(hotkeyType))
                Config.GlobalHotkeys.Remove(hotkeyType);
        }

        private void FillHotkeyField(HotkeyType hotkeyType, Key hotkey, ModifierKeys modifiers)
        {
            if (hotkey == Key.None || HotkeyUtils.IllegalHotkeys.Contains(hotkey))
                GetHotkeyTextBox(hotkeyType).Text = string.Empty;
            else if (modifiers == ModifierKeys.None)
                GetHotkeyTextBox(hotkeyType).Text = hotkey.ToString();
            else
                GetHotkeyTextBox(hotkeyType).Text = string.Format("{0} + {1}", modifiers, hotkey);
        }

        private TextBox GetHotkeyTextBox(HotkeyType hotkeyType)
        {
            switch (hotkeyType)
            {
                case HotkeyType.StartRecording:
                    return hotkeyRecordTextBox;
                case HotkeyType.PauseRecording:
                    return hotkeyPauseTextBox;
                case HotkeyType.StopRecording:
                    return hotkeyStopTextBox;
                case HotkeyType.MuteUnmute:
                    return hotkeyMuteUnmuteTextBox;
                default:
                    return null;
            }
        }
        #endregion
    }
}
