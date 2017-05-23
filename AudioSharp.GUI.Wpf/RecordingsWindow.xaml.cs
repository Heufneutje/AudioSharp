using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using AudioSharp.Core;
using AudioSharp.Translations;
using AudioSharp.Utils;
using Microsoft.VisualBasic.FileIO;
using Xceed.Wpf.DataGrid;

namespace AudioSharp.GUI.Wpf
{
    /// <summary>
    /// Interaction logic for RecordingsWindow.xaml
    /// </summary>
    public partial class RecordingsWindow : Window
    {
        #region Fields & Properties
        private string _recordingPath;
        private FileSystemWatcher _watcher;
        private BackgroundWorker _convertWorker;
        #endregion

        #region Constructor
        public RecordingsWindow(string recordingPath)
        {
            InitializeComponent();
            _recordingPath = recordingPath;
            _watcher = new FileSystemWatcher();
            _watcher.Changed += Watcher_Changed;
            _watcher.Created += Watcher_Created;
            _watcher.Deleted += Watcher_Deleted;
            _watcher.Renamed += Watcher_Renamed;

            _watcher.Path = recordingPath;
            _watcher.EnableRaisingEvents = true;
            RefreshRecordings();
        }
        #endregion

        #region Events
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            Dispatcher.Invoke(RefreshRecordings);
        }

        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(RefreshRecordings);
        }

        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            Dispatcher.Invoke(RefreshRecordings);
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            // Make sure the file exists, as the Changed event is also fired before deleting a file.
            if (File.Exists(e.FullPath))
                Dispatcher.Invoke(RefreshRecordings);
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckSelection())
                PlaySelectedRecording();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckSelection())
                return;

            RecordingItem selectedItem = (RecordingItem)recordingsDataGrid.SelectedItem;
            FileSystem.DeleteFile(selectedItem.FullPath, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.DoNothing);
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckSelection())
                return;

            RecordingItem selectedItem = (RecordingItem)recordingsDataGrid.SelectedItem;
            if (selectedItem.FullPath.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(Messages.GUIConvertAlreadyMP3, Messages.GUIConvertTitle, MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            SetProgressVisibility(Visibility.Visible);
            convertButton.IsEnabled = false;

            _convertWorker = new BackgroundWorker() { WorkerReportsProgress = true };
            _convertWorker.DoWork += ConvertWorker_DoWork;
            _convertWorker.ProgressChanged += ConvertWorker_ProgressChanged;
            _convertWorker.RunWorkerCompleted += ConvertWorker_RunWorkerCompleted;

            _convertWorker.RunWorkerAsync(selectedItem.FullPath);
        }

        private void ConvertWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MP3Convertor convertor = new MP3Convertor();
            convertor.ReportingProgress += Convertor_ReportingProgress;
            convertor.WaveToMP3((string)e.Argument);
        }

        private void ConvertWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void ConvertWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SetProgressVisibility(Visibility.Hidden);
            convertButton.IsEnabled = true;
        }

        private void Convertor_ReportingProgress(object sender, ConvertorProgressEventArgs e)
        {
            _convertWorker.ReportProgress(e.Progress);
        }

        private void openFolderButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(_recordingPath);
        }

        private void recordingsDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!CheckSelection())
                return;

            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while (dep != null)
            {
                if (dep is CellContentPresenter || dep is DataCell)
                {
                    PlaySelectedRecording();
                    break;
                }
                dep = VisualTreeHelper.GetParent(dep);
            }
        }
        #endregion

        #region Helper Functions
        private void RefreshRecordings()
        {
            string[] files = Directory.GetFiles(_recordingPath);
            List<RecordingItem> recordings = new List<Utils.RecordingItem>();
            foreach (string filePath in files)
            {
                if (!File.Exists(filePath))
                    continue;

                FileInfo info = new FileInfo(filePath);
                if (info.Extension.ToLower() != ".wav" && info.Extension.ToLower() != ".mp3")
                    continue;

                string formattedDate = info.CreationTime.ToString("yyyy-MM-dd HH:mm");
                System.Drawing.Image image = FileUtils.GetSmallIcon(filePath);

                recordings.Add(new RecordingItem(Path.GetFileNameWithoutExtension(filePath), info.Extension, info.CreationTime, DriveSpaceUtils.BytesTostring(info.Length), image, filePath));
            }
            recordingsDataGrid.ItemsSource = recordings;
        }

        private void PlaySelectedRecording()
        {
            RecordingItem selectedItem = (RecordingItem)recordingsDataGrid.SelectedItem;
            if (File.Exists(selectedItem.FullPath))
                Process.Start(selectedItem.FullPath);
        }

        private bool CheckSelection()
        {
            if (recordingsDataGrid.SelectedItem == null)
            {
                MessageBox.Show(Messages.GUINoFileSelected, Messages.GUIErrorCommon, MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void SetProgressVisibility(Visibility visibility)
        {
            progressLabel.Visibility = visibility;
            progressBar.Visibility = visibility;
        }
        #endregion
    }
}
