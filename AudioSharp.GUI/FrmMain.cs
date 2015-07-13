using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AudioSharp.Config;
using AudioSharp.Core;
using AudioSharp.Translations;
using NAudio.CoreAudioApi;

namespace AudioSharp.GUI
{
    public partial class FrmMain : AudioSharpForm
    {
        #region Fields & Properties
        private AudioRecorder _AudioRecorder;
        private TimeSpan _Timer;
        private Configuration _Config;
        private NotifyIcon _TrayIcon;
        private string NextRecordingPath
        {
            get
            {
                return string.Format("{0}.{1}", Path.Combine(_Config.RecordingsFolder, _Config.RecordingPrefix + _Config.NextRecordingNumber.ToString("D4")), _Config.OutputFormat);
            }
        }
        #endregion

        #region Constructors
        public FrmMain()
        {
            InitializeComponent();
            _Config = ConfigHandler.ReadConfig();
            InitAudioDevices();
            InitTrayIcon();
        }
        #endregion

        #region Form Events
        private void FrmMain_Load(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!btnRecord.Enabled)
            {
                if (MessageBox.Show(Messages.GUIStopRecording, Messages.GUIStopRecordingTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                UpdateRecordingNumber();
            }

            _AudioRecorder.Dispose();
            _TrayIcon.Dispose();
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && _Config.MinimizeToTray)
                Hide();
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
                UpdateGUIState(true);
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
            UpdateGUIState(false);
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
                _AudioRecorder.SelectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar = trckVolume.Value / 50.0f;
                lblVolumePercentage.Text = string.Format("{0} %", (float)trckVolume.Value / 50 * 100);
            }
        }
        #endregion

        #region Menu Events
        private void menuButtonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuButtonRecordings_Click(object sender, EventArgs e)
        {
            using (FrmRecordingList recordingsForm = new FrmRecordingList(_Config.RecordingsFolder))
            {
                TopMost = false;
                recordingsForm.ShowDialog();
                TopMost = _Config.AlwaysOnTop;
            }
        }

        private void menuButtonSettings_Click(object sender, EventArgs e)
        {
            string oldOutputFormat = _Config.OutputFormat;
            using (FrmSettings settingsFrm = new FrmSettings(_Config))
            {
                TopMost = false;
                if (settingsFrm.ShowDialog() == DialogResult.OK)
                {
                    _Config = settingsFrm.Config;
                    ApplySettings();
                    if (oldOutputFormat != _Config.OutputFormat)
                        InitAudioDevices();
                }
            }
        }

        private void menuButtonAbout_Click(object sender, EventArgs e)
        {
            using (FrmAbout aboutFrm = new FrmAbout())
            {
                aboutFrm.ShowDialog();
            }
        }
        #endregion

        #region Context Menu Events
        private void _TrayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Show();
                if (WindowState == FormWindowState.Minimized)
                    WindowState = FormWindowState.Normal;
            }
        }
        #endregion

        #region Helper Functions
        private void InitAudioDevices()
        {
            if (_AudioRecorder != null)
            {
                _AudioRecorder.Dispose();
                _AudioRecorder = null;
            }

            switch (_Config.OutputFormat)
            {
                case "wav":
                    _AudioRecorder = new WaveRecorder();
                    break;
                case "mp3":
                    _AudioRecorder = new MP3Recorder();
                    break;
            }

            cbInputDevices.Items.Clear();
            cbInputDevices.Items.AddRange(_AudioRecorder.Devices.ToArray());
            cbInputDevices.SelectedIndex = _AudioRecorder.DefaultDeviceNumber;
        }

        private void InitTrayIcon()
        {
            _TrayIcon = new NotifyIcon();
            _TrayIcon.Text = Text;
            _TrayIcon.Icon = Properties.Resources.icon_default;
            _TrayIcon.ContextMenuStrip = contextMenuStrip;
            _TrayIcon.MouseClick += _TrayIcon_MouseClick;
        }

        private void UpdateGUIState(bool recording)
        {
            btnRecord.Enabled = !recording;
            contextButtonRecord.Enabled = !recording;
            menuButtonRecord.Enabled = !recording;
            btnStop.Enabled = recording;
            contextButtonStop.Enabled = recording;
            menuButtonStop.Enabled = recording;
            cbInputDevices.Enabled = !recording;
            menuButtonSettings.Enabled = !recording;

            Icon = recording ? Properties.Resources.icon_recording : Properties.Resources.icon_default;
            _TrayIcon.Icon = recording ? Properties.Resources.icon_recording : Properties.Resources.icon_default;
            statusStrip.BackColor = recording ? Color.Red : Color.DodgerBlue;
            statusLabel.Text = recording ? "Recording in progress..." : "Ready";

            if (recording)
                _TrayIcon.Text += " [RECORDING IN PROGRESS]";
            else
                _TrayIcon.Text = "AudioSharp";
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

        private void ApplySettings()
        {
            txtOutputFile.Text = NextRecordingPath;
            _TrayIcon.Visible = _Config.ShowTrayIcon;
            TopMost = _Config.AlwaysOnTop;
        }
        #endregion
    }
}
