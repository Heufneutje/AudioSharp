using System;
using System.Windows.Forms;

namespace HeufyAudio.GUI
{
    public partial class FrmAbout : HeufyAudioBaseForm
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = string.Format("Heufy Audio Recorder {0}", Application.ProductVersion);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
