using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AudioSharp.Config;
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
        public Configuration Config { get; private set; }
        #endregion

        #region Constructor
        public SettingsWindow(Configuration currentConfig)
        {
            Config = currentConfig;
            InitializeComponent();
            DataContext = Config;

            if (currentConfig.PromptForFileName)
                promptFileNameRadioButton.IsChecked = true;
            else
                generateFileNameRadioButton.IsChecked = true;

            foreach (KeyValuePair<HotkeyUtils.HotkeyType, Tuple<Key, ModifierKeys>> hotkey in currentConfig.GlobalHotkeys)
            {
                FillHotkeyField(hotkey.Key, hotkey.Value.Item1, hotkey.Value.Item2);
            }

            SetPreview();
        }
        #endregion

        #region Control Events
        private void browseButton_Click(object sender, RoutedEventArgs e)
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
        }

        private void showTrayIconCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool isChecked = showTrayIconCheckBox.IsChecked ?? false;
            minimizeToTrayCheckBox.IsEnabled = isChecked;
            if (!isChecked)
            {
                Config.MinimizeToTray = false;
                GuiHelper.UpdateToggleButtonBindingTarget(minimizeToTrayCheckBox);
            }
        }

        private void hotkeyRecordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            HandleHotkey(HotkeyUtils.HotkeyType.StartRecording, e);
        }

        private void hotkeyStopTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            HandleHotkey(HotkeyUtils.HotkeyType.StopRecording, e);
        }
        #endregion

        #region Helper Methods
        private void SetPreview()
        {
            if (string.IsNullOrEmpty(Config.RecordingsFolder) || Config.RecordingPrefix == null)
                nextRecordingPreviewTextBox.Text = "<invalid path>";
            else
                nextRecordingPreviewTextBox.Text = $"{Path.Combine(Config.RecordingsFolder, Config.RecordingPrefix + Config.NextRecordingNumber.ToString("D4"))}.{Config.OutputFormat}";
        }

        private void HandleHotkey(HotkeyUtils.HotkeyType hotkeyType, KeyEventArgs e)
        {
            Key key = e.Key;
            if (key == Key.System)
                key = e.SystemKey;

            if (HotkeyUtils.IllegalHotkeys.Contains(key))
                return;

            Configuration config = Config;
            if (key == Key.Escape)
            {
                FillHotkeyField(hotkeyType, Key.None, ModifierKeys.None);
                if (config.GlobalHotkeys.ContainsKey(hotkeyType))
                    config.GlobalHotkeys.Remove(hotkeyType);
            }
            else
            {
                FillHotkeyField(hotkeyType, key, Keyboard.Modifiers);
                config.GlobalHotkeys[hotkeyType] = new Tuple<Key, ModifierKeys>(key, Keyboard.Modifiers);
            }
        }

        private void FillHotkeyField(HotkeyUtils.HotkeyType hotkeyType, Key hotkey, ModifierKeys modifiers)
        {
            if (hotkey == Key.None || HotkeyUtils.IllegalHotkeys.Contains(hotkey))
                GetHotkeyTextBox(hotkeyType).Text = string.Empty;
            else if (modifiers == ModifierKeys.None)
                GetHotkeyTextBox(hotkeyType).Text = hotkey.ToString();
            else
                GetHotkeyTextBox(hotkeyType).Text = string.Format("{0} + {1}", modifiers, hotkey);
        }

        private TextBox GetHotkeyTextBox(HotkeyUtils.HotkeyType hotkeyType)
        {
            switch (hotkeyType)
            {
                case HotkeyUtils.HotkeyType.StartRecording:
                    return hotkeyRecordTextBox;
                case HotkeyUtils.HotkeyType.StopRecording:
                    return hotkeyStopTextBox;
                default:
                    return null;
            }
        }
        #endregion
    }
}
