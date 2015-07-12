using System;
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
