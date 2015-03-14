using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeufyAudio.Config;

namespace HeufyAudio.GUI
{
    public partial class FrmSettings : Form
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
            configurationBindingSource.DataSource = currentConfig;
        }
        #endregion

        #region Control Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            configurationBindingSource.EndEdit();
            ConfigHandler.SaveConfig((Configuration)configurationBindingSource.Current);
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtRecordingsFolder.Text = fbd.SelectedPath;
            }
        }
        #endregion

        #region DataSource Events
        private void configurationBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            Configuration config = (Configuration)configurationBindingSource.Current;
            if (String.IsNullOrEmpty(config.RecordingsFolder) || String.IsNullOrEmpty(config.RecordingPrefix))
                txtNextRecording.Text = "<invalid path>";
            else
                txtNextRecording.Text = String.Format("{0}.wav", 
                    Path.Combine(config.RecordingsFolder, config.RecordingPrefix + config.CurrentRecordingNumber.ToString("D3")));
        }
        #endregion
    }
}
