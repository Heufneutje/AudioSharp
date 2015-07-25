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
        private AudioRecorder _AudioRecorder;
        private TimeSpan _Timer;
        private Configuration _Config;
        private NotifyIcon _TrayIcon;
        private string NextRecordingPath
        {
            get
            {
                if (_Config.PromptForFileName)
                {
                    using (SaveFileDialog sfd = new SaveFileDialog() { InitialDirectory = _Config.RecordingsFolder })
                    {
                        switch (_Config.OutputFormat)
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
                    return string.Format("{0}.{1}", Path.Combine(_Config.RecordingsFolder, _Config.RecordingPrefix + _Config.NextRecordingNumber.ToString("D4")), _Config.OutputFormat);
                }
            }
        }
        private bool IsRecording
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
            _Config = ConfigHandler.ReadConfig();

            if (!Directory.Exists(_Config.RecordingsFolder))
            {
                _Config.RecordingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                ConfigHandler.SaveConfig(_Config);
            }

            InitAudioDevices();
            InitTrayIcon();
            HotkeyUtils.RegisterAllHotkeys(Handle, _Config.GlobalHotkeys);
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
            if (IsRecording)
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
            HotkeyUtils.UnregisterAllHotkeys(Handle);
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
            StartRecording();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopRecording();
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

        private void timerSpaceCheck_Tick(object sender, EventArgs e)
        {
            driveSpaceLabel.Text = string.Format("Free space: {0}", DriveSpaceUtils.GetAvailableDriveSpace(_Config.RecordingsFolder));
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
                HotkeyUtils.UnregisterAllHotkeys(Handle);
                if (settingsFrm.ShowDialog() == DialogResult.OK)
                {
                    _Config = settingsFrm.Config;
                    ApplySettings();
                    if (oldOutputFormat != _Config.OutputFormat)
                        InitAudioDevices();
                }
                HotkeyUtils.RegisterAllHotkeys(Handle, _Config.GlobalHotkeys);
            }
        }

        private void menuButtonAbout_Click(object sender, EventArgs e)
        {
            using (FrmAbout aboutFrm = new FrmAbout())
            {
                TopMost = false;
                aboutFrm.ShowDialog();
                TopMost = _Config.AlwaysOnTop;
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
            IEnumerable<HotkeyUtils.HotkeyType> matches = _Config.GlobalHotkeys.Where(x => x.Value.Item1 == key && x.Value.Item3 == modifier).Select(x => x.Key);

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
            if (IsRecording)
                return;

            if (cbInputDevices.SelectedItem == null)
            {
                MessageBox.Show(Messages.GUIErrorInputDevice, Messages.GUIErrorInputDeviceTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string recordingPath = NextRecordingPath;
            if (recordingPath == null)
                return;

            if (!_Config.PromptForFileName && File.Exists(recordingPath))
            {
                if (MessageBox.Show(Messages.GUIQuestionOverwriteRecording, Messages.GUIQuestionOverwriteRecordingTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            else if (_Config.PromptForFileName)
            {
                txtOutputFile.Text = recordingPath;
            }

            try
            {
                _AudioRecorder.StartRecording(recordingPath);
                UpdateGUIState(true);
                _Timer = new TimeSpan();
                lblTimer.Text = _Timer.ToString();
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

        private void StopRecording()
        {
            if (!IsRecording)
                return;

            UpdateGUIState(false);
            _AudioRecorder.StopRecording();
            timerClock.Stop();
            UpdateRecordingNumber();
        }

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
            _TrayIcon.Icon = Properties.Resources.AudioSharp_default32x32;
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

            Icon = recording ? Properties.Resources.AudioSharp_recording32x32 : Properties.Resources.AudioSharp_default32x32;
            _TrayIcon.Icon = recording ? Properties.Resources.AudioSharp_recording32x32 : Properties.Resources.AudioSharp_default32x32;
            statusStrip.BackColor = recording ? Color.Red : Color.DodgerBlue;
            statusLabel.Text = string.Format("Status: {0}", recording ? "Recording" : "Ready");

            if (recording)
                _TrayIcon.Text += " [RECORDING IN PROGRESS]";
            else
                _TrayIcon.Text = "AudioSharp";
        }

        private void UpdateRecordingNumber()
        {
            if (_Config.PromptForFileName)
            {
                txtOutputFile.Text = string.Empty;
                return;
            }
                
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
            if (_Config.PromptForFileName)
                txtOutputFile.Text = string.Empty;
            else
                txtOutputFile.Text = NextRecordingPath;
            _TrayIcon.Visible = _Config.ShowTrayIcon;
            TopMost = _Config.AlwaysOnTop;
        }
        #endregion
    }
}
