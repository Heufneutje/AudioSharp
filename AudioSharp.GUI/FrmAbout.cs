using System;
using System.IO;
using System.Windows.Forms;

namespace AudioSharp.GUI
{
    public partial class FrmAbout : AudioSharpForm
    {
        public FrmAbout()
        {
            InitializeComponent();
        }

        private void FrmAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = string.Format("AudioSharp {0}", Application.ProductVersion);
            rtbLicense.Text = Properties.Resources.Licenses;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
