using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AudioSharp.Config;
using AudioSharp.Core;
using AudioSharp.Translations;
using AudioSharp.Utils;
using NAudio.CoreAudioApi;

namespace AudioSharp.GUI
{
    public partial class FrmMain : AudioSharpForm
    {
        #region Fields & Properties
        private AudioRecorder _audioRecorder;
        private TimeSpan _timer;
        private Configuration _config;
        private NotifyIcon _trayIcon;
        private string NextRecordingPath
        {
            get
            {
                if (_config.PromptForFileName)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog() { InitialDirectory = _config.RecordingsFolder })
                    {
                        switch (_config.OutputFormat)
                        {
                            case "wav":
                                sfd.Filter = "Wave Files (*.wav)|*.wav";
                                break;
                            case "mp3":
                                sfd.Filter = "MP3 Files (*.mp3)|*.mp3";
                                break;
                        }
                        if (sfd.ShowDialog() == DialogResult.OK)
                            return sfd.FileName;
                        else
                            return null;
                    }
                }
                else
                {
                    return string.Format("{0}.{1}", Path.Combine(_config.RecordingsFolder, _config.RecordingPrefix + _config.NextRecordingNumber.ToString("D4")), _config.OutputFormat);
                }
            }
        }
        private bool _IsRecording
        {
            get
            {
                return !btnRecord.Enabled;
            }
        }
        #endregion

        #region Constructors
        public FrmMain()
        {
            InitializeComponent();
            _config = ConfigHandler.ReadConfig();

            if (!Directory.Exists(_config.RecordingsFolder))
            {
                _config.RecordingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                ConfigHandler.SaveConfig(_config);
            }

            InitAudioDevices();
            InitTrayIcon();
            HotkeyUtils.RegisterAllHotkeys(Handle, _config.GlobalHotkeys);
            timerSpaceCheck_Tick(null, null);
        }
        #endregion

        #region Form Events
        private void FrmMain_Load(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_IsRecording)
            {
                if (MessageBox.Show(Messages.GUIStopRecording, Messages.GUIStopRecordingTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                UpdateRecordingNumber();
            }

            _audioRecorder.Dispose();
            _trayIcon.Dispose();
            HotkeyUtils.UnregisterAllHotkeys(Handle);
        }

        private void FrmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && _config.MinimizeToTray)
                Hide();
        }
        #endregion

        #region Control Events
        private void btnRecord_Click(object sender, EventArgs e)
        {
            StartRecording();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            PauseRecording();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopRecording();
        }

        private void timerVAMeter_Tick(object sender, EventArgs e)
        {
            if (cbInputDevices.SelectedItem != null)
                prbVolume.Value = (int)Math.Round(_audioRecorder.SelectedDevice.AudioMeterInformation.MasterPeakValue * 100);
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            _timer = _timer.Add(TimeSpan.FromSeconds(1));
            lblTimerValue.Text = _timer.ToString();

            if (_timer.Seconds % 2 == 0)
                lblFileSizeValue.Text = FileUtils.GetFileSize(txtOutputFile.Text);
        }

        private void timerSpaceCheck_Tick(object sender, EventArgs e)
        {
            driveSpaceLabel.Text = string.Format("Free space: {0}", DriveSpaceUtils.GetAvailableDriveSpace(_config.RecordingsFolder));
        }

        private void cbInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInputDevices.SelectedItem != null)
            {
                _audioRecorder.SelectedDevice = (MMDevice)cbInputDevices.SelectedItem;
                trckVolume.Enabled = true;
                trckVolume.Value = Convert.ToInt32(_audioRecorder.SelectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100 / 2);
            }
        }

        private void trckVolume_ValueChanged(object sender, EventArgs e)
        {
            if (_audioRecorder.SelectedDevice != null)
            {
                _audioRecorder.SelectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar = trckVolume.Value / 50.0f;
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
            using (FrmRecordingList recordingsForm = new FrmRecordingList(_config.RecordingsFolder))
            {
                TopMost = false;
                recordingsForm.ShowDialog();
                TopMost = _config.AlwaysOnTop;
            }
        }

        private void menuButtonSettings_Click(object sender, EventArgs e)
        {
            string oldOutputFormat = _config.OutputFormat;
            using (FrmSettings settingsFrm = new FrmSettings(_config))
            {
                TopMost = false;
                HotkeyUtils.UnregisterAllHotkeys(Handle);
                if (settingsFrm.ShowDialog() == DialogResult.OK)
                {
                    _config = settingsFrm.Config;
                    ApplySettings();
                    if (oldOutputFormat != _config.OutputFormat)
                        InitAudioDevices();
                }
                HotkeyUtils.RegisterAllHotkeys(Handle, _config.GlobalHotkeys);
            }
        }

        private void menuButtonAbout_Click(object sender, EventArgs e)
        {
            using (FrmAbout aboutFrm = new FrmAbout())
            {
                TopMost = false;
                aboutFrm.ShowDialog();
                TopMost = _config.AlwaysOnTop;
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

        #region Global Hotkeys
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg != 0x0312)
                return;
            
            Keys key = (Keys)(((int)m.LParam >> 16) & 0xFFFF);
            int modifier = (int)m.LParam & 0xFFFF;
            IEnumerable<HotkeyUtils.HotkeyType> matches = _config.GlobalHotkeys.Where(x => x.Value.Item1 == key && x.Value.Item3 == modifier).Select(x => x.Key);

            foreach (HotkeyUtils.HotkeyType hotkeyType in matches)
            {
                switch (hotkeyType)
                {
                    case HotkeyUtils.HotkeyType.StartRecording:
                        StartRecording();
                        break;
                    case HotkeyUtils.HotkeyType.StopRecording:
                        StopRecording();
                        break;
                }
            }
        }
        #endregion

        #region Helper Functions
        private void StartRecording()
        {
            if (_IsRecording)
                return;

            if (cbInputDevices.SelectedItem == null)
            {
                MessageBox.Show(Messages.GUIErrorInputDevice, Messages.GUIErrorInputDeviceTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string recordingPath = NextRecordingPath;
            if (recordingPath == null)
                return;

            if (!_config.PromptForFileName && File.Exists(recordingPath))
            {
                if (MessageBox.Show(Messages.GUIQuestionOverwriteRecording, Messages.GUIQuestionOverwriteRecordingTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            else if (_config.PromptForFileName)
            {
                txtOutputFile.Text = recordingPath;
            }

            try
            {
                _audioRecorder.StartRecording(recordingPath);
                UpdateGUIState(RecordingState.Started);
                _timer = new TimeSpan();
                lblTimerValue.Text = _timer.ToString();
                lblFileSizeValue.Text = "0 bytes";
                timerClock.Start();
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Messages.GUIErrorOutputFile, Messages.GUIErrorOutputFileTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(Messages.GUIErrorNoWriteAccess, Messages.GUIErrorNoWriteAccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PauseRecording()
        {
            if (!_IsRecording)
                return;

            if (_audioRecorder.IsPaused)
            {
                UpdateGUIState(RecordingState.Started);
                timerClock.Start();
            }
            else
            {
                UpdateGUIState(RecordingState.Paused);
                timerClock.Stop();
            }

            _audioRecorder.PauseRecording();
        }

        private void StopRecording()
        {
            if (!_IsRecording)
                return;

            UpdateGUIState(RecordingState.Stopped);
            _audioRecorder.StopRecording();
            timerClock.Stop();
            UpdateRecordingNumber();
        }

        private void InitAudioDevices()
        {
            if (_audioRecorder != null)
            {
                _audioRecorder.Dispose();
                _audioRecorder = null;
            }

            switch (_config.OutputFormat)
            {
                case "wav":
                    _audioRecorder = new WaveRecorder();
                    break;
                case "mp3":
                    _audioRecorder = new MP3Recorder();
                    break;
            }

            cbInputDevices.Items.Clear();
            cbInputDevices.Items.AddRange(_audioRecorder.Devices.ToArray());
            cbInputDevices.SelectedIndex = _audioRecorder.DefaultDeviceNumber;
        }

        private void InitTrayIcon()
        {
            _trayIcon = new NotifyIcon();
            _trayIcon.Text = Text;
            _trayIcon.Icon = Properties.Resources.AudioSharp_default32x32;
            _trayIcon.ContextMenuStrip = contextMenuStrip;
            _trayIcon.MouseClick += _TrayIcon_MouseClick;
        }

        private void UpdateGUIState(RecordingState status)
        {
            btnRecord.Enabled = status == RecordingState.Stopped;
            contextButtonRecord.Enabled = status == RecordingState.Stopped;
            menuButtonRecord.Enabled = status == RecordingState.Stopped;
            btnStop.Enabled = status != RecordingState.Stopped;
            contextButtonStop.Enabled = status != RecordingState.Stopped;
            menuButtonStop.Enabled = status != RecordingState.Stopped;
            cbInputDevices.Enabled = status == RecordingState.Stopped;
            menuButtonSettings.Enabled = status == RecordingState.Stopped;
            btnPause.Enabled = status != RecordingState.Stopped;
            contextButtonPause.Enabled = status != RecordingState.Stopped;
            menuButtonPause.Enabled = status != RecordingState.Stopped;

            FontStyle fontStyle = status != RecordingState.Stopped ? FontStyle.Bold : FontStyle.Regular;
            lblTimerValue.Font = new Font(lblTimerValue.Font, fontStyle);
            lblFileSizeValue.Font = new Font(lblFileSizeValue.Font, fontStyle);

            switch (status)
            {
                case RecordingState.Started:
                    Icon = Properties.Resources.AudioSharp_recording32x32;
                    _trayIcon.Icon = Properties.Resources.AudioSharp_recording32x32;
                    statusStrip.BackColor = Color.Red;
                    statusLabel.Text = "Status: Recording";
                    break;
                case RecordingState.Paused:
                    Icon = Properties.Resources.AudioSharp_paused32x32;
                    _trayIcon.Icon = Properties.Resources.AudioSharp_paused32x32;
                    statusStrip.BackColor = Color.Orange;
                    statusLabel.Text = "Status: Paused";
                    break;
                case RecordingState.Stopped:
                    Icon = Properties.Resources.AudioSharp_default32x32;
                    _trayIcon.Icon = Properties.Resources.AudioSharp_default32x32;
                    statusStrip.BackColor = Color.DodgerBlue;
                    statusLabel.Text = "Status: Ready";
                    break;
            }

            if (status == RecordingState.Stopped)
                _trayIcon.Text = "AudioSharp";
            else if (!_trayIcon.Text.Contains("[RECORDING IN PROGRESS]"))
                _trayIcon.Text += " [RECORDING IN PROGRESS]";
        }

        private void UpdateRecordingNumber()
        {
            if (_config.PromptForFileName)
            {
                txtOutputFile.Text = string.Empty;
                return;
            }
                
            if (!_config.AutoIncrementRecordingNumber)
                return;

            if (_config.NextRecordingNumber < 9999)
                _config.NextRecordingNumber++;
            else
                _config.NextRecordingNumber = 1;

            txtOutputFile.Text = NextRecordingPath;
            ConfigHandler.SaveConfig(_config);
        }

        private void ApplySettings()
        {
            if (_config.PromptForFileName)
                txtOutputFile.Text = string.Empty;
            else
                txtOutputFile.Text = NextRecordingPath;
            _trayIcon.Visible = _config.ShowTrayIcon;
            TopMost = _config.AlwaysOnTop;
        }
        #endregion
    }
}
