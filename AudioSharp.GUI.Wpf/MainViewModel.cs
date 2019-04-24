using System.Diagnostics;
using AudioSharp.Core;

namespace AudioSharp.GUI.Wpf
{
    public class MainViewModel
    {
        private MainWindow _mainWindow;

        public DelegateCommand RecordCommand { get; private set; }
        public DelegateCommand PauseCommand { get; private set; }
        public DelegateCommand StopCommand { get; private set; }
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand IssueTrackerCommand { get; private set; }
        public DelegateCommand SourceCodeCommand { get; private set; }
        public DelegateCommand AboutCommand { get; private set; }

        public MainViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            RecordCommand = new DelegateCommand(StartRecording, CanRecord);
            PauseCommand = new DelegateCommand(PauseRecording, CanPauseOrStop);
            StopCommand = new DelegateCommand(StopRecording, CanPauseOrStop);
            ExitCommand = new DelegateCommand(Exit);
            IssueTrackerCommand = new DelegateCommand(ShowIssueTracker);
            SourceCodeCommand = new DelegateCommand(ShowSourceCode);
            AboutCommand = new DelegateCommand(ShowAboutDialog);
        }

        private bool CanRecord(object parameter)
        {
            return _mainWindow.RecordingState == RecordingState.Stopped;
        }

        private bool CanPauseOrStop(object parameter)
        {
            return _mainWindow.RecordingState != RecordingState.Stopped;
        }

        private void StartRecording(object parameter)
        {
            _mainWindow.StartRecording();
            UpdateCommands();
        }

        private void PauseRecording(object parameter)
        {
            _mainWindow.PauseRecording();
            UpdateCommands();
        }

        private void StopRecording(object parameter)
        {
            _mainWindow.StopRecording();
            UpdateCommands();
        }

        private void Exit(object parameter)
        {
            _mainWindow.Close();
        }

        private void ShowIssueTracker(object parameter)
        {
            Process.Start("https://github.com/Heufneutje/AudioSharp/issues");
        }

        private void ShowSourceCode(object paramter)
        {
            Process.Start("https://github.com/Heufneutje/AudioSharp");
        }

        private void ShowAboutDialog(object parameter)
        {
            AboutWindow aboutWindow = new AboutWindow();
            _mainWindow.Topmost = false;
            aboutWindow.ShowDialog();
            _mainWindow.ResetTopMost();
        }

        private void UpdateCommands()
        {
            RecordCommand.OnCanExecuteChanged();
            PauseCommand.OnCanExecuteChanged();
            StopCommand.OnCanExecuteChanged();
        }
    }
}
