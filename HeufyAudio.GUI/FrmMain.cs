using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HeufyAudio.Core;
using HeufyAudio.Translations;
using NAudio.CoreAudioApi;

namespace HeufyAudio.GUI
{
    public partial class FrmMain : Form
    {
        #region Fields & Properties
        private AudioRecorder _AudioRecorder;
        private TimeSpan _Timer;
        #endregion

        #region Constructors
        public FrmMain()
        {
            InitializeComponent();
            _AudioRecorder = new AudioRecorder();
            cbInputDevices.Items.AddRange(_AudioRecorder.Devices.ToArray());
            cbInputDevices.SelectedIndex = _AudioRecorder.DefaultDeviceNumber;
        }
        #endregion

        #region Form Events
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _AudioRecorder.Dispose();
        }
        #endregion

        #region Control Events
        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (cbInputDevices.SelectedItem == null)
            {
                MessageBox.Show(Messages.GUIErrorInputDevice, Messages.GUIErrorInputDeviceTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (File.Exists(txtOutputFile.Text))
            {
                if (MessageBox.Show(Messages.GUIQuestionOverwriteRecording, Messages.GUIQuestionOverwriteRecordingTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }

            try
            {
                _AudioRecorder.StartRecording(txtOutputFile.Text);
                UpdateButtonStates(true);
                _Timer = new TimeSpan();
                lblTimer.Text = _Timer.ToString();
                timerClock.Start();
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Messages.GUIErrorOutputFile, Messages.GUIErrorOutputFileTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            UpdateButtonStates(false);
            _AudioRecorder.StopRecording();
            timerClock.Stop();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Wave File (*.wav)|*.wav";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            txtOutputFile.Text = sfd.FileName;
        }

        private void timerVAMeter_Tick(object sender, EventArgs e)
        {
            if (cbInputDevices.SelectedItem != null)
            {
                prbVolume.Value = (int)Math.Round(_AudioRecorder.SelectedDevice.AudioMeterInformation.MasterPeakValue * 100);
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            _Timer = _Timer.Add(TimeSpan.FromSeconds(1));
            lblTimer.Text = _Timer.ToString();
        }

        private void cbInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInputDevices.SelectedItem != null)
            {
                _AudioRecorder.SelectedDevice = (MMDevice)cbInputDevices.SelectedItem;
                trckVolume.Enabled = true;
                trckVolume.Value = Convert.ToInt32(_AudioRecorder.SelectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100 / 2);
            }
        }

        private void trckVolume_ValueChanged(object sender, EventArgs e)
        {
            if (_AudioRecorder.SelectedDevice != null)
            {
                _AudioRecorder.SelectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar = (float)trckVolume.Value / 50.0f;
                lblVolumePercentage.Text = String.Format("{0} %", (float)trckVolume.Value / 50 * 100);
            }
        }
        #endregion

        #region Menu Events
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Helper Functions
        private void UpdateButtonStates(bool recording)
        {
            btnRecord.Enabled = !recording;
            btnStop.Enabled = recording;
            btnBrowse.Enabled = !recording;
            txtOutputFile.ReadOnly = recording;
            cbInputDevices.Enabled = !recording;
        }
        #endregion
    }
}
