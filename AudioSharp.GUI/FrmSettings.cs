using System;
using System.IO;
using System.Windows.Forms;
using AudioSharp.Config;

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
                configurationBindingSource.EndEdit();
                Configuration config = (Configuration)configurationBindingSource.DataSource;
                config.MinimizeToTray = false;
                chkMinimizeToTray.Checked = false;
                configurationBindingSource.DataSource = config;
            }
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
