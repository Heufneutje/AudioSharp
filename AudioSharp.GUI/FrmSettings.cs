using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using AudioSharp.Config;
using AudioSharp.Utils;

namespace AudioSharp.GUI
{
    public partial class FrmSettings : AudioSharpForm
    {
        #region Fields & Properties
        public Configuration Config
        {
            get
            {
                configurationBindingSource.EndEdit();
                return (Configuration)configurationBindingSource.Current;
            }
        }
        #endregion

        #region Constructor
        public FrmSettings(Configuration currentConfig)
        {
            InitializeComponent();
            cbOutputFormat.SelectedIndex = 0;
            configurationBindingSource.DataSource = currentConfig;

            foreach (KeyValuePair<HotkeyUtils.HotkeyType, Tuple<Keys, Keys>> hotkey in currentConfig.GlobalHotkeys)
            {
                FillHotkeyField(hotkey.Key, hotkey.Value.Item1, hotkey.Value.Item2);
            }
        }
        #endregion

        #region Control Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            configurationBindingSource.EndEdit();
            ConfigHandler.SaveConfig((Configuration)configurationBindingSource.Current);
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtRecordingsFolder.Text = fbd.SelectedPath;
                    configurationBindingSource.EndEdit();
                }
            }
        }

        private void cbOutputFormat_SelectedValueChanged(object sender, EventArgs e)
        {
            configurationBindingSource.EndEdit();
        }

        private void chkShowTrayIcon_CheckedChanged(object sender, EventArgs e)
        {
            chkMinimizeToTray.Enabled = chkShowTrayIcon.Checked;
            if (!chkShowTrayIcon.Checked)
            {
                Configuration config = Config;
                config.MinimizeToTray = false;
                chkMinimizeToTray.Checked = false;
                configurationBindingSource.DataSource = config;
            }
        }

        private void txtHotkeyRecord_KeyDown(object sender, KeyEventArgs e)
        {
            HandleHotkey(HotkeyUtils.HotkeyType.StartRecording, e);
        }

        private void txtHotkeyStop_KeyDown(object sender, KeyEventArgs e)
        {
            HandleHotkey(HotkeyUtils.HotkeyType.StopRecording, e);
        }
        #endregion

        #region DataSource Events
        private void configurationBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            Configuration config = (Configuration)configurationBindingSource.Current;
            if (string.IsNullOrEmpty(config.RecordingsFolder) || string.IsNullOrEmpty(config.RecordingPrefix))
                txtNextRecording.Text = "<invalid path>";
            else
                txtNextRecording.Text = string.Format("{0}.{1}", 
                    Path.Combine(config.RecordingsFolder, config.RecordingPrefix + config.NextRecordingNumber.ToString("D4")), config.OutputFormat);
        }
        #endregion

        #region Custom Functions
        private void HandleHotkey(HotkeyUtils.HotkeyType hotkeyType, KeyEventArgs e)
        {
            if (HotkeyUtils.IllegalHotkeys.Contains(e.KeyCode))
                return;

            Configuration config = Config;
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
            {
                FillHotkeyField(hotkeyType, Keys.None, Keys.None);
                if (config.GlobalHotkeys.ContainsKey(hotkeyType))
                    config.GlobalHotkeys.Remove(hotkeyType);
            }
            else
            {
                FillHotkeyField(hotkeyType, e.KeyCode, e.Modifiers);
                config.GlobalHotkeys[hotkeyType] = new Tuple<Keys, Keys>(e.KeyCode, e.Modifiers);
            }
        }

        private void FillHotkeyField(HotkeyUtils.HotkeyType hotkeyType, Keys hotkey, Keys modifiers)
        {
            if (modifiers == Keys.None)
                GetHotkeyTextBox(hotkeyType).Text = hotkey.ToString();
            else
                GetHotkeyTextBox(hotkeyType).Text = string.Format("{0} + {1}", modifiers, hotkey);
        }

        private TextBox GetHotkeyTextBox(HotkeyUtils.HotkeyType hotkeyType)
        {
            switch (hotkeyType)
            {
                case HotkeyUtils.HotkeyType.StartRecording:
                    return txtHotkeyRecord;
                case HotkeyUtils.HotkeyType.StopRecording:
                    return txtHotkeyStop;
                default:
                    return null;
            }
        }
        #endregion
    }
}
