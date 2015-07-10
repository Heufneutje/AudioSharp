namespace HeufyAudio.GUI
{
    partial class FrmRecordingList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvRecordings = new System.Windows.Forms.ListView();
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnConvertToMp3 = new System.Windows.Forms.Button();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnPlay = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // lvRecordings
            // 
            this.lvRecordings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvRecordings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmName,
            this.clmType,
            this.clmDate,
            this.clmSize});
            this.lvRecordings.FullRowSelect = true;
            this.lvRecordings.GridLines = true;
            this.lvRecordings.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvRecordings.HideSelection = false;
            this.lvRecordings.Location = new System.Drawing.Point(43, 12);
            this.lvRecordings.MultiSelect = false;
            this.lvRecordings.Name = "lvRecordings";
            this.lvRecordings.ShowGroups = false;
            this.lvRecordings.Size = new System.Drawing.Size(647, 414);
            this.lvRecordings.TabIndex = 4;
            this.lvRecordings.UseCompatibleStateImageBehavior = false;
            this.lvRecordings.View = System.Windows.Forms.View.Details;
            this.lvRecordings.DoubleClick += new System.EventHandler(this.lvRecordings_DoubleClick);
            // 
            // clmName
            // 
            this.clmName.Text = "Name";
            this.clmName.Width = 111;
            // 
            // clmType
            // 
            this.clmType.Text = "Type";
            this.clmType.Width = 107;
            // 
            // clmDate
            // 
            this.clmDate.Text = "Date";
            this.clmDate.Width = 128;
            // 
            // clmSize
            // 
            this.clmSize.Text = "Size";
            this.clmSize.Width = 196;
            // 
            // btnConvertToMp3
            // 
            this.btnConvertToMp3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvertToMp3.Image = global::HeufyAudio.GUI.Properties.Resources.Right;
            this.btnConvertToMp3.Location = new System.Drawing.Point(12, 105);
            this.btnConvertToMp3.Name = "btnConvertToMp3";
            this.btnConvertToMp3.Size = new System.Drawing.Size(25, 25);
            this.btnConvertToMp3.TabIndex = 3;
            this.toolTip.SetToolTip(this.btnConvertToMp3, "Convert to MP3\r\n\r\nConvert the currently selected recording to an MP3 file.");
            this.btnConvertToMp3.UseVisualStyleBackColor = true;
            this.btnConvertToMp3.Click += new System.EventHandler(this.btnConvertToMp3_Click);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenFolder.Image = global::HeufyAudio.GUI.Properties.Resources.Folder;
            this.btnOpenFolder.Location = new System.Drawing.Point(12, 12);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(25, 25);
            this.btnOpenFolder.TabIndex = 0;
            this.toolTip.SetToolTip(this.btnOpenFolder, "Open folder\r\n\r\nOpen the folder where the recordings are stored in Windows Explore" +
        "r.");
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Changed);
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Created);
            this.fileSystemWatcher.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Deleted);
            this.fileSystemWatcher.Renamed += new System.IO.RenamedEventHandler(this.fileSystemWatcher_Renamed);
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 100;
            this.toolTip.AutoPopDelay = 2000;
            this.toolTip.InitialDelay = 100;
            this.toolTip.ReshowDelay = 20;
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::HeufyAudio.GUI.Properties.Resources.play_16;
            this.btnPlay.Location = new System.Drawing.Point(12, 43);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(25, 25);
            this.btnPlay.TabIndex = 1;
            this.toolTip.SetToolTip(this.btnPlay, "Play recording\r\n\r\nPlay the currently selected recording.");
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::HeufyAudio.GUI.Properties.Resources.Trash;
            this.btnDelete.Location = new System.Drawing.Point(12, 74);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 25);
            this.btnDelete.TabIndex = 2;
            this.toolTip.SetToolTip(this.btnDelete, "Delete recording\r\n\r\nDelete the currently selected recording.");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // FrmRecordingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 438);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnConvertToMp3);
            this.Controls.Add(this.lvRecordings);
            this.Name = "FrmRecordingList";
            this.Text = "Recordings";
            this.Load += new System.EventHandler(this.FrmRecordingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvRecordings;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.ColumnHeader clmType;
        private System.Windows.Forms.ColumnHeader clmDate;
        private System.Windows.Forms.ColumnHeader clmSize;
        private System.Windows.Forms.Button btnConvertToMp3;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Button btnDelete;
    }
}