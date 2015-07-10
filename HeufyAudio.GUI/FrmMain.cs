using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HeufyAudio.Config;
using HeufyAudio.Core;
using HeufyAudio.Translations;
using NAudio.CoreAudioApi;

namespace HeufyAudio.GUI
{
    public partial class FrmMain : HeufyAudioBaseForm
    {
        #region Fields & Properties
        private AudioRecorder _AudioRecorder;
        private TimeSpan _Timer;
        private Configuration _Config;
        private string NextRecordingPath
        {
            get
            {
                return string.Format("{0}.wav", Path.Combine(_Config.RecordingsFolder, _Config.RecordingPrefix + _Config.NextRecordingNumber.ToString("D4")));
            }
        }
        #endregion

        #region Constructors
        public FrmMain()
        {
            InitializeComponent();
            _Config = ConfigHandler.ReadConfig();
            _AudioRecorder = new WaveRecorder();
            cbInputDevices.Items.AddRange(_AudioRecorder.Devices.ToArray());
            cbInputDevices.SelectedIndex = _AudioRecorder.DefaultDeviceNumber;
        }
        #endregion

        #region Form Events
        private void FrmMain_Load(object sender, EventArgs e)
        {
            txtOutputFile.Text = NextRecordingPath;
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnRecord.Enabled)
            {
                if (MessageBox.Show(Messages.GUIStopRecording, Messages.GUIStopRecordingTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    e.Cancel = true;

                UpdateRecordingNumber();
            }

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
                _AudioRecorder.StartRecording(NextRecordingPath);
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
            UpdateRecordingNumber();
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

        private void recordingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRecordingList recordingsForm = new FrmRecordingList(_Config.RecordingsFolder);
            recordingsForm.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmSettings settingsFrm = new FrmSettings(_Config))
            {
                if (settingsFrm.ShowDialog() == DialogResult.OK)
                {
                    _Config = settingsFrm.Config;
                    txtOutputFile.Text = NextRecordingPath;
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAbout aboutFrm = new FrmAbout())
            {
                aboutFrm.ShowDialog();
            }
        }
        #endregion

        #region Helper Functions
        private void UpdateButtonStates(bool recording)
        {
            btnRecord.Enabled = !recording;
            btnStop.Enabled = recording;
            cbInputDevices.Enabled = !recording;
        }

        private void UpdateRecordingNumber()
        {
            if (!_Config.AutoIncrementRecordingNumber)
                return;

            if (_Config.NextRecordingNumber < 9999)
                _Config.NextRecordingNumber++;
            else
                _Config.NextRecordingNumber = 1;

            txtOutputFile.Text = NextRecordingPath;
            ConfigHandler.SaveConfig(_Config);
        }
        #endregion
    }
}
