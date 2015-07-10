using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using HeufyAudio.Core;
using HeufyAudio.Translations;
using Microsoft.VisualBasic.FileIO;

namespace HeufyAudio.GUI
{
    public partial class FrmRecordingList : HeufyAudioBaseForm
    {
        #region Fields & Properties
        private string _RecordingPath;
        private List<string> _Recordings;
        private FrmProgressDialog _ProgressDialog;
        #endregion

        #region Constructor
        public FrmRecordingList(string recordingPath)
        {
            InitializeComponent();
            _RecordingPath = recordingPath;
            fileSystemWatcher.Path = recordingPath;
            RefreshRecordings();
        }
        #endregion

        #region Form Events
        private void FrmRecordingList_Load(object sender, EventArgs e)
        {
            foreach (ColumnHeader column in lvRecordings.Columns)
            {
                column.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }
        #endregion

        #region Control Events
        private void lvRecordings_DoubleClick(object sender, EventArgs e)
        {
            PlaySelectedRecording();
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(_RecordingPath);
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (lvRecordings.SelectedItems.Count == 0)
            {
                MessageBox.Show(Messages.GUINoFileSelected, Messages.GUICommonError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PlaySelectedRecording();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lvRecordings.SelectedItems.Count == 0)
            {
                MessageBox.Show(Messages.GUINoFileSelected, Messages.GUICommonError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(Messages.GUIDefaultDelete, Messages.GUIDefaultDeleteTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                DeleteSelectedRecording();
        }

        private void btnConvertToMp3_Click(object sender, EventArgs e)
        {
            if (lvRecordings.SelectedItems.Count == 0)
            {
                MessageBox.Show(Messages.GUINoFileSelected, Messages.GUICommonError, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem selectedItem = lvRecordings.SelectedItems[0];
            string filePath = _Recordings[selectedItem.Index];

            if (filePath.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show(Messages.GUIConvertAlreadyMP3, Messages.GUIConvertTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _ProgressDialog = new FrmProgressDialog()
            {
                Task = "Converting WAV file to MP3...",
                Progress = 0,
                CanCancel = false
            };
            _ProgressDialog.Show();
            backgroundWorker.RunWorkerAsync(_Recordings[selectedItem.Index]);
        }

        private void convertor_ReportingProgress(object sender, ConvertorProgressEventArgs e)
        {
            backgroundWorker.ReportProgress(e.Progress);
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            RefreshRecordings();
        }

        private void fileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            RefreshRecordings();
        }

        private void fileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            RefreshRecordings();
        }

        private void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            RefreshRecordings();
        }
        #endregion

        #region BackgroundWorker Events
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            MP3Convertor convertor = new MP3Convertor();
            convertor.ReportingProgress += convertor_ReportingProgress;
            convertor.WaveToMP3((string)e.Argument);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _ProgressDialog.Progress = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _ProgressDialog.Close();

            if (e.Cancelled)
                return;

            if (e.Error != null)
            {
                string error = Messages.GUIConvertError + Environment.NewLine + e.Error.Message;
                MessageBox.Show(error, Messages.GUIConvertTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        #endregion

        #region Helper Functions
        private void RefreshRecordings()
        {
            _Recordings = new List<string>();
            lvRecordings.Items.Clear();
            string[] files = Directory.GetFiles(_RecordingPath);

            foreach (string filePath in files)
            {
                FileInfo info = new FileInfo(filePath);
                if (info.Extension.ToLower() != ".wav" && info.Extension.ToLower() != ".mp3")
                    continue;

                _Recordings.Add(filePath);
                string formattedDate = info.CreationTime.ToString("yyyy-MM-dd HH:mm");
                ListViewItem item = new ListViewItem(new[] { Path.GetFileNameWithoutExtension(filePath), info.Extension, formattedDate, BytesTostring(info.Length) });
                lvRecordings.Items.Add(item);
            }
        }

        private string BytesTostring(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0 B";
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return string.Format("{0} {1}", Math.Sign(byteCount) * num, suf[place]);
        }

        private void PlaySelectedRecording()
        {
            ListViewItem selectedItem = lvRecordings.SelectedItems[0];

            if (File.Exists(_Recordings[selectedItem.Index]))
                Process.Start(_Recordings[selectedItem.Index]);
        }

        private void DeleteSelectedRecording()
        {
            ListViewItem selectedItem = lvRecordings.SelectedItems[0];

            if (File.Exists(_Recordings[selectedItem.Index]))
                FileSystem.DeleteFile(_Recordings[selectedItem.Index], UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
        }
        #endregion
    }
}
