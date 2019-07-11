using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using AudioSharp.Config;
using AudioSharp.Core;
using AudioSharp.GUI.CustomControls;
using AudioSharp.Translations;
using AudioSharp.Utils;
using AudioSharp.Utils.Update;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Win32;
using NAudio.CoreAudioApi;

namespace AudioSharp.GUI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields & Properties

        private bool _isSizeChanged;
        private bool _isLoading;
        public RecordingState RecordingState { get; private set; }

        private AudioRecorder _audioRecorder;
        private TimeSpan _timer;
        private Configuration _config;
        private DispatcherTimer _timerSpaceCheck;
        private DispatcherTimer _timerVAMeter;
        private DispatcherTimer _timerClock;

        private TaskbarIcon _taskbarIcon;

        private string NextRecordingPath
        {
            get
            {
                if (_config.PromptForFileName)
                {
                    SaveFileDialog sfd = new SaveFileDialog() { InitialDirectory = _config.RecordingsFolder };

                    switch (_config.OutputFormat)
                    {
                        case "wav":
                            sfd.Filter = "Wave Files (*.wav)|*.wav";
                            break;
                        case "mp3":
                            sfd.Filter = "MP3 Files (*.mp3)|*.mp3";
                            break;
                    }
                    if (sfd.ShowDialog() == true)
                        return sfd.FileName;
                    else
                        return null;
                }
                else
                {
                    return $"{Path.Combine(_config.RecordingsFolder, FormattingUtils.FormatString(_config.RecordingPrefix, _config.NextRecordingNumber))}.{_config.OutputFormat}";
                }
            }
        }
        #endregion Fields & Properties

        #region Constructors

        public MainWindow()
        {
            _isLoading = true;
            InitializeComponent();

            _config = ConfigHandler.ReadConfig();

            if (_config.StartMinimized)
            {
                WindowState = WindowState.Minimized;
                if (_config.MinimizeToTray)
                {
                    Hide();
                    InitTrayIcon();
                }
            }

            if (!Directory.Exists(_config.RecordingsFolder))
            {
                _config.RecordingsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                ConfigHandler.SaveConfig(_config);
            }

            InitAudioDevices();
            InitTimers();
            UpdateItemVisibility();

            RegisterHotkeys();
            HotkeyUtils.GlobalHoykeyPressed += HotkeyUtils_GlobalHoykeyPressed;
            _config.StartTrackingChanges();
        }

        #endregion Constructors

        #region Window Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplySettings();
            _isLoading = false;
            _isSizeChanged = false;

            if (_config.CheckForUpdates)
                CheckForUpdate(false);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (_config.CloseToTray && Visibility != Visibility.Hidden)
            {
                Hide();
                e.Cancel = true;
            }

            if (RecordingState != RecordingState.Stopped)
            {
                if (MessageBox.Show(Messages.GUIStopRecording, Messages.GUIStopRecordingTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    e.Cancel = true;
                    return;
                }

                UpdateRecordingNumber();
            }

            _audioRecorder.Dispose();

            if (_isSizeChanged)
                _config.SetWindowSize(Name, Width, Height);

            if (_isSizeChanged || _config.HasChanges())
                ConfigHandler.SaveConfig(_config);

            HotkeyUtils.UnregisterAllHotkeys(GuiHelper.GetWindowHandle(this));
            HotkeyUtils.GlobalHoykeyPressed -= HotkeyUtils_GlobalHoykeyPressed;
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized && _config.MinimizeToTray)
                Hide();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!_isLoading)
                _isSizeChanged = true;
        }

        #endregion Window Events

        #region Control Events
        private void muteToggleButton_CheckedChanged(object sender, RoutedEventArgs e)
        {
            bool isChecked = muteToggleButton.IsChecked ?? false;
            inputVolumeSlider.IsEnabled = !isChecked;
            inputVolumePercLabel.IsEnabled = !isChecked;

            if (_audioRecorder.SelectedDevice != null)
                _audioRecorder.SelectedDevice.AudioEndpointVolume.Mute = isChecked;

            if (isChecked)
            {
                muteToggleButton.ToolTip = FindResource("unmuteTooltip");
                ((AutoGreyableImage)muteToggleButton.Content).Source = (ImageSource)FindResource("muteImage");
            }
            else
            {
                muteToggleButton.ToolTip = isChecked ? FindResource("unmuteTooltip") : FindResource("muteTooltip");
                ((AutoGreyableImage)muteToggleButton.Content).Source = (ImageSource)FindResource("notmuteImage");
            }
        }

        private void timerVAMeter_Tick(object sender, EventArgs e)
        {
            if (devicesComboBox.SelectedItem != null)
                inputVolumeProgressBar.Value = (int)Math.Round(_audioRecorder.SelectedDevice.AudioMeterInformation.MasterPeakValue * 100);
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            _timer = _timer.Add(TimeSpan.FromSeconds(1));
            timerLabel.Content = _timer.ToString();

            if (_timer.Seconds % 2 == 0)
                fileSizeLabel.Content = FileUtils.GetFileSize(outputFileTextBox.Text);
        }

        private void timerSpaceCheck_Tick(object sender, EventArgs e)
        {
            driveSpaceBarItem.Content = $"Free space: {DriveSpaceUtils.GetAvailableDriveSpace(_config.RecordingsFolder)}";
        }

        private void devicesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (devicesComboBox.SelectedItem != null)
            {
                _audioRecorder.SelectedDevice = (MMDevice)devicesComboBox.SelectedItem;
                inputVolumeSlider.Value = Convert.ToInt32(_audioRecorder.SelectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100 / 2);
                muteToggleButton.IsChecked = _audioRecorder.SelectedDevice.AudioEndpointVolume.Mute;
            }
        }

        private void inputVolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_audioRecorder.SelectedDevice != null)
            {
                _audioRecorder.SelectedDevice.AudioEndpointVolume.MasterVolumeLevelScalar = (float)inputVolumeSlider.Value / 50.0f;
                inputVolumePercLabel.Content = $"{(int)((float)inputVolumeSlider.Value / 50 * 100)} %";
            }
        }

        private void HotkeyUtils_GlobalHoykeyPressed(HotkeyType hotkey)
        {
            switch (hotkey)
            {
                case HotkeyType.StartRecording:
                    StartRecording();
                    break;
                case HotkeyType.PauseRecording:
                    PauseRecording();
                    break;
                case HotkeyType.StopRecording:
                    StopRecording();
                    break;
                case HotkeyType.MuteUnmute:
                    muteToggleButton.IsChecked = !muteToggleButton.IsChecked;
                    break;
            }
        }

        #endregion Control Events

        #region Menu Events

        private void viewRecordingSettingPanelMenuItem_CheckedChanged(object sender, RoutedEventArgs e)
        {
            UpdateItemVisibility();
            _config.RecordingSettingsPanelVisible = viewRecordingSettingPanelMenuItem.IsChecked;
        }

        private void viewRecordingOutputPanelMenuItem_CheckedChanged(object sender, RoutedEventArgs e)
        {
            UpdateItemVisibility();
            _config.RecordingOutputPanelVisible = viewRecordingOutputPanelMenuItem.IsChecked;
        }

        private void viewStatusBarMenuItem_CheckedChanged(object sender, RoutedEventArgs e)
        {
            UpdateItemVisibility();
            _config.StatusBarVisible = viewStatusBarMenuItem.IsChecked;
        }

        private void viewStatusBarItemMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            UpdateItemVisibility();
            _config.StatusBarItemVisible = viewStatusBarItemMenuItem.IsChecked;
        }

        private void viewFreeSpaceMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            UpdateItemVisibility();
            _config.FreeSpaceBarItemVisible = viewFreeSpaceMenuItem.IsChecked;
        }

        private void recordingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            RecordingsWindow recordingsForm = new RecordingsWindow(_config.RecordingsFolder);
            Topmost = false;
            recordingsForm.ShowDialog();
            Topmost = _config.AlwaysOnTop;
        }

        private void settingsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow(_config);
            Topmost = false;

            IntPtr windowHandle = GuiHelper.GetWindowHandle(this);
            HotkeyUtils.UnregisterAllHotkeys(windowHandle);
            if (settingsWindow.ShowDialog() == true)
            {
                _config = settingsWindow.Config;
                ApplySettings();
                
                if (_config.HasPropertyChanged(nameof(_config.OutputFormat)) 
                    || _config.HasPropertyChanged(nameof(_config.MP3EncodingPreset)))
                    InitAudioDevices();
            }
            HotkeyUtils.RegisterAllHotkeys(windowHandle, _config.GlobalHotkeys);
        }

        private void checkForUpdatesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CheckForUpdate(true);
        }

        #endregion Menu Events

        #region Context Menu Events

        private void taskbarIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            Show();
            if (WindowState == WindowState.Minimized)
                WindowState = WindowState.Normal;
            Activate();
        }

        #endregion Context Menu Events

        #region Helper Functions

        public void StartRecording()
        {
            if (RecordingState != RecordingState.Stopped)
                return;

            if (devicesComboBox.SelectedItem == null)
            {
                MessageBox.Show(Messages.GUIErrorInputDevice, Messages.GUIErrorInputDeviceTitle, MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string recordingPath = NextRecordingPath;
            if (recordingPath == null)
                return;

            if (!_config.PromptForFileName && File.Exists(recordingPath))
            {
                if (MessageBox.Show(Messages.GUIQuestionOverwriteRecording, Messages.GUIQuestionOverwriteRecordingTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;
            }
            else if (_config.PromptForFileName)
            {
                outputFileTextBox.Text = recordingPath;
            }

            try
            {
                _audioRecorder.StartRecording(recordingPath);
                UpdateGUIState(RecordingState.Started);
                _timer = new TimeSpan();
                timerLabel.Content = _timer.ToString();
                fileSizeLabel.Content = "0 bytes";
                _timerClock.Start();
            }
            catch (ArgumentException)
            {
                MessageBox.Show(Messages.GUIErrorOutputFile, Messages.GUIErrorOutputFileTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show(Messages.GUIErrorNoWriteAccess, Messages.GUIErrorNoWriteAccessTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void PauseRecording()
        {
            if (RecordingState == RecordingState.Stopped)
                return;

            if (_audioRecorder.IsPaused)
            {
                UpdateGUIState(RecordingState.Started);
                _timerClock.Start();
            }
            else
            {
                UpdateGUIState(RecordingState.Paused);
                _timerClock.Stop();
            }

            _audioRecorder.PauseRecording();
        }

        public void StopRecording()
        {
            if (RecordingState == RecordingState.Stopped)
                return;

            if (_audioRecorder.IsPaused)
                PauseRecording();

            UpdateGUIState(RecordingState.Stopped);
            _audioRecorder.StopRecording();
            _timerClock.Stop();
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
                    _audioRecorder = new MP3Recorder(_config.MP3EncodingPreset);
                    break;
            }

            devicesComboBox.Items.Clear();
            foreach (MMDevice device in _audioRecorder.Devices)
                devicesComboBox.Items.Add(device);

            devicesComboBox.SelectedIndex = _audioRecorder.DefaultDeviceNumber;
        }

        private void InitTimers()
        {
            _timerClock = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            _timerVAMeter = new DispatcherTimer() { Interval = new TimeSpan(10), IsEnabled = true };
            _timerSpaceCheck = new DispatcherTimer() { Interval = new TimeSpan(0, 1, 0), IsEnabled = true };

            _timerClock.Tick += timerClock_Tick;
            _timerVAMeter.Tick += timerVAMeter_Tick;
            _timerSpaceCheck.Tick += timerSpaceCheck_Tick;
            timerSpaceCheck_Tick(null, null);
        }

        private void UpdateGUIState(RecordingState status)
        {
            RecordingState = status;
            checkForUpdatesMenuItem.IsEnabled = status == RecordingState.Stopped;

            recordThumbButtonInfo.ImageSource = status == RecordingState.Stopped ? (ImageSource)FindResource("recordThumb") : (ImageSource)FindResource("recordThumbTransparent");
            stopThumbButtonInfo.ImageSource = status != RecordingState.Stopped ? (ImageSource)FindResource("stopThumb") : (ImageSource)FindResource("stopThumbTransparent");
            pauseThumbButtonInfo.ImageSource = status != RecordingState.Stopped ? (ImageSource)FindResource("pauseThumb") : (ImageSource)FindResource("pauseThumbTransparent");

            FontWeight fontWeight = status != RecordingState.Stopped ? FontWeights.Bold : FontWeights.Normal;
            timerLabel.FontWeight = fontWeight;
            fileSizeLabel.FontWeight = fontWeight;

            switch (status)
            {
                case RecordingState.Started:
                    Icon = (ImageSource)FindResource("recordIcon");
                    if (_taskbarIcon != null)
                        _taskbarIcon.IconSource = (ImageSource)FindResource("recordTrayIcon");
                    statusBar.Background = Brushes.Red;
                    statusBarItem.Content = "Recording";
                    break;
                case RecordingState.Paused:
                    Icon = (ImageSource)FindResource("pauseIcon");
                    if (_taskbarIcon != null)
                        _taskbarIcon.IconSource = (ImageSource)FindResource("pauseTrayIcon");
                    statusBar.Background = Brushes.Orange;
                    statusBarItem.Content = "Paused";
                    break;
                case RecordingState.Stopped:
                    Icon = (ImageSource)FindResource("defaultIcon");
                    if (_taskbarIcon != null)
                        _taskbarIcon.IconSource = (ImageSource)FindResource("defaultTrayIcon");
                    statusBar.Background = Brushes.DodgerBlue;
                    statusBarItem.Content = "Ready";
                    break;
            }
        }

        private void UpdateRecordingNumber()
        {
            if (_config.PromptForFileName)
            {
                outputFileTextBox.Text = string.Empty;
                return;
            }

            if (!_config.AutoIncrementRecordingNumber)
                return;

            if (_config.NextRecordingNumber < 9999)
                _config.NextRecordingNumber++;
            else
                _config.NextRecordingNumber = 1;

            outputFileTextBox.Text = NextRecordingPath;
            ConfigHandler.SaveConfig(_config);
        }

        private void ApplySettings()
        {
            if (_config.PromptForFileName)
                outputFileTextBox.Text = string.Empty;
            else
                outputFileTextBox.Text = NextRecordingPath;

            InitTrayIcon();
            Topmost = _config.AlwaysOnTop;

            Tuple<double, double> windowSize = _config.GetWindowSize(Name);
            Width = windowSize.Item1;
            Height = windowSize.Item2;

            viewRecordingSettingPanelMenuItem.IsChecked = _config.RecordingSettingsPanelVisible;
            viewRecordingOutputPanelMenuItem.IsChecked = _config.RecordingOutputPanelVisible;
            viewStatusBarMenuItem.IsChecked = _config.StatusBarVisible;
            viewStatusBarItemMenuItem.IsChecked = _config.StatusBarItemVisible;
            viewFreeSpaceMenuItem.IsChecked = _config.FreeSpaceBarItemVisible;

            if (_config.RunAtStartup)
            {
                try
                {
                    ShortcutUtils.AddStartupShortcut();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Messages.GUIErrorAddingStartupShortcut, Environment.NewLine, ex.Message), Messages.GUIErrorCommon, MessageBoxButton.OK, MessageBoxImage.Error);
                    _config.RunAtStartup = false;
                    ConfigHandler.SaveConfig(_config);
                }
            }
            else
            {
                try
                {
                    ShortcutUtils.RemoveStartupShortcut();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(Messages.GUIErrorRemovingStartupShortcut, Environment.NewLine, ex.Message), Messages.GUIErrorCommon, MessageBoxButton.OK, MessageBoxImage.Error);
                    _config.RunAtStartup = true;
                    ConfigHandler.SaveConfig(_config);
                }
            }
        }

        private void InitTrayIcon()
        {
            if (_config.ShowTrayIcon && _taskbarIcon == null)
            {
                _taskbarIcon = new TaskbarIcon()
                {
                    IconSource = (ImageSource)FindResource("defaultTrayIcon"),
                    ToolTipText = "AudioSharp",
                    ContextMenu = (ContextMenu)FindResource("contextMenu"),
                    Visibility = Visibility.Visible
                };
                _taskbarIcon.TrayLeftMouseDown += taskbarIcon_TrayLeftMouseDown;
            }
            else if (!_config.ShowTrayIcon && _taskbarIcon != null)
            {
                _taskbarIcon.Dispose();
                _taskbarIcon = null;
            }
        }

        private void RegisterHotkeys()
        {
            if (!HotkeyUtils.RegisterAllHotkeys(GuiHelper.GetWindowHandle(this), _config.GlobalHotkeys))
                MessageBox.Show(Messages.GUIErrorHotkeys, Messages.GUIErrorCommon, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void UpdateItemVisibility()
        {
            recordingSettingsGroup.Visibility = viewRecordingSettingPanelMenuItem.IsChecked ? Visibility.Visible : Visibility.Hidden;
            recordingOutputGroup.Visibility = viewRecordingOutputPanelMenuItem.IsChecked ? Visibility.Visible : Visibility.Hidden;
            statusBar.Visibility = viewStatusBarMenuItem.IsChecked ? Visibility.Visible : Visibility.Hidden;
            statusSeparator.Visibility = viewFreeSpaceMenuItem.IsChecked && viewStatusBarItemMenuItem.IsChecked ? Visibility.Visible : Visibility.Collapsed;
            statusBarItem.Visibility = viewStatusBarItemMenuItem.IsChecked ? Visibility.Visible : Visibility.Collapsed;
            driveSpaceBarItem.Visibility = viewFreeSpaceMenuItem.IsChecked ? Visibility.Visible : Visibility.Collapsed;

            if (recordingSettingsGroup.Visibility == Visibility.Hidden && recordingOutputGroup.Visibility == Visibility.Hidden)
            {
                MinWidth = 270;
                MinHeight = 110;
            }
            else
            {
                MinWidth = 600;
                if (recordingSettingsGroup.Visibility == Visibility.Hidden && recordingOutputGroup.Visibility == Visibility.Visible)
                {
                    recordingOutputGroup.Margin = new Thickness(10, 23, 10, -1);
                    MinHeight = 240;
                }
                else if (recordingSettingsGroup.Visibility == Visibility.Visible && recordingOutputGroup.Visibility == Visibility.Hidden)
                {
                    MinHeight = 190;
                }
                else if (recordingSettingsGroup.Visibility == Visibility.Visible && recordingOutputGroup.Visibility == Visibility.Visible)
                {
                    recordingOutputGroup.Margin = new Thickness(10, 101, 10, -1);
                    MinWidth = 600;
                    MinHeight = 315;
                }
            }

            int yPosition = 5;
            if (!viewStatusBarMenuItem.IsChecked)
            {
                MinHeight -= 22;
                yPosition -= 22;
            }

            buttonGrid.Margin = new Thickness(0, 0, 0, yPosition);
        }

        private void CheckForUpdate(bool shouldPopUp)
        {
            checkForUpdatesMenuItem.IsEnabled = false;
            statusBarItem.Content = "Checking for updates";

            BackgroundWorker updateCheckerBackgroundWorker = new BackgroundWorker();
            UpdateHelper updateHelper = new UpdateHelper();
            updateCheckerBackgroundWorker.DoWork += (sender, args) =>
            {
                args.Result = updateHelper.CheckForUpdate();
            };
            updateCheckerBackgroundWorker.RunWorkerCompleted += (sender, args) =>
            {
                UpdateCheckResult result = (UpdateCheckResult)args.Result;
                if (result.ResultType == UpdateResultType.UpdateAvailable)
                {
                    if (MessageBox.Show(result.Message, result.MessageTitle, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        DownloadUpdate(updateHelper, result.DownloadUrl);
                        return;
                    }
                }
                else
                {
                    if (shouldPopUp)
                        MessageBox.Show(result.Message, result.MessageTitle, MessageBoxButton.OK, result.ResultType == UpdateResultType.NoUpdateAvailable ? MessageBoxImage.Information : MessageBoxImage.Error);
                }

                FinishUpdateCheck();
                updateHelper.Dispose();
            };
            updateCheckerBackgroundWorker.RunWorkerAsync();
        }

        private void DownloadUpdate(UpdateHelper updateHelper, string downloadUrl)
        {
            statusBarItem.Content = "Downloading update files";
            progressSeparator.Visibility = viewStatusBarMenuItem.IsChecked || viewFreeSpaceMenuItem.IsChecked ? Visibility.Visible : Visibility.Collapsed;
            progressBar.Visibility = Visibility.Visible;

            updateHelper.ProgressChanged += (sender, args) =>
            {
                progressBar.Value = args.ProgressPercentage;
            };
            updateHelper.DownloadCompleted += (sender, args) =>
            {
                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.Message);
                }
                else
                {
                    try
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(updateHelper.InstallerFilePath);
                        psi.Arguments = $"\"/S /UPDATELOCATION={updateHelper.GetApplicationDirectory()}\"";
                        updateHelper.Dispose();
                        Process.Start(psi);
                        Application.Current.Shutdown();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format(Messages.GUIErrorRunningInstaller, ex.Message), Messages.GUIErrorCommon, MessageBoxButton.OK, MessageBoxImage.Error);
                        updateHelper.Dispose();
                        FinishUpdateCheck();
                    }
                }
            };
            updateHelper.DownloadUpdate(downloadUrl);
        }

        private void FinishUpdateCheck()
        {
            checkForUpdatesMenuItem.IsEnabled = true;
            statusBarItem.Content = "Ready";
            progressSeparator.Visibility = Visibility.Collapsed;
            progressBar.Visibility = Visibility.Collapsed;
        }

        public void ResetTopMost()
        {
            Topmost = _config.AlwaysOnTop;
        }

        #endregion Helper Functions
    }
}
