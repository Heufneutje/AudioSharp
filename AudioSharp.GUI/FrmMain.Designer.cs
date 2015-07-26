namespace AudioSharp.GUI
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.cbInputDevices = new System.Windows.Forms.ComboBox();
            this.gbRecordingSettings = new System.Windows.Forms.GroupBox();
            this.lblVolumePercentage = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.trckVolume = new System.Windows.Forms.TrackBar();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.lblInputDevice = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.gbRecording = new System.Windows.Forms.GroupBox();
            this.lblTimer = new System.Windows.Forms.Label();
            this.prbVolume = new System.Windows.Forms.ProgressBar();
            this.timerVAMeter = new System.Windows.Forms.Timer(this.components);
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuButtonRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.menuButtonStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuButtonExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuButtonRecordings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuButtonSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuButtonAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextButtonRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.contextButtonStop = new System.Windows.Forms.ToolStripMenuItem();
            this.contextButtonExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.driveSpaceLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerSpaceCheck = new System.Windows.Forms.Timer(this.components);
            this.gbRecordingSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckVolume)).BeginInit();
            this.gbRecording.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbInputDevices
            // 
            this.cbInputDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInputDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbInputDevices.FormattingEnabled = true;
            this.cbInputDevices.Location = new System.Drawing.Point(139, 23);
            this.cbInputDevices.Name = "cbInputDevices";
            this.cbInputDevices.Size = new System.Drawing.Size(415, 21);
            this.cbInputDevices.TabIndex = 1;
            this.cbInputDevices.SelectedIndexChanged += new System.EventHandler(this.cbInputDevices_SelectedIndexChanged);
            // 
            // gbRecordingSettings
            // 
            this.gbRecordingSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecordingSettings.Controls.Add(this.lblVolumePercentage);
            this.gbRecordingSettings.Controls.Add(this.lblVolume);
            this.gbRecordingSettings.Controls.Add(this.trckVolume);
            this.gbRecordingSettings.Controls.Add(this.txtOutputFile);
            this.gbRecordingSettings.Controls.Add(this.lblOutputFile);
            this.gbRecordingSettings.Controls.Add(this.lblInputDevice);
            this.gbRecordingSettings.Controls.Add(this.cbInputDevices);
            this.gbRecordingSettings.Location = new System.Drawing.Point(12, 27);
            this.gbRecordingSettings.Name = "gbRecordingSettings";
            this.gbRecordingSettings.Size = new System.Drawing.Size(560, 110);
            this.gbRecordingSettings.TabIndex = 2;
            this.gbRecordingSettings.TabStop = false;
            this.gbRecordingSettings.Text = "Recording Settings";
            // 
            // lblVolumePercentage
            // 
            this.lblVolumePercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVolumePercentage.AutoSize = true;
            this.lblVolumePercentage.Location = new System.Drawing.Point(518, 54);
            this.lblVolumePercentage.Name = "lblVolumePercentage";
            this.lblVolumePercentage.Size = new System.Drawing.Size(24, 13);
            this.lblVolumePercentage.TabIndex = 8;
            this.lblVolumePercentage.Text = "0 %";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(15, 54);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(72, 13);
            this.lblVolume.TabIndex = 7;
            this.lblVolume.Text = "Input Volume:";
            // 
            // trckVolume
            // 
            this.trckVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trckVolume.AutoSize = false;
            this.trckVolume.Enabled = false;
            this.trckVolume.Location = new System.Drawing.Point(139, 51);
            this.trckVolume.Maximum = 50;
            this.trckVolume.Name = "trckVolume";
            this.trckVolume.Size = new System.Drawing.Size(373, 23);
            this.trckVolume.TabIndex = 6;
            this.trckVolume.ValueChanged += new System.EventHandler(this.trckVolume_ValueChanged);
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutputFile.Location = new System.Drawing.Point(139, 80);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.ReadOnly = true;
            this.txtOutputFile.Size = new System.Drawing.Size(415, 20);
            this.txtOutputFile.TabIndex = 4;
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.Location = new System.Drawing.Point(15, 83);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(61, 13);
            this.lblOutputFile.TabIndex = 3;
            this.lblOutputFile.Text = "Output File:";
            // 
            // lblInputDevice
            // 
            this.lblInputDevice.AutoSize = true;
            this.lblInputDevice.Location = new System.Drawing.Point(15, 26);
            this.lblInputDevice.Name = "lblInputDevice";
            this.lblInputDevice.Size = new System.Drawing.Size(71, 13);
            this.lblInputDevice.TabIndex = 2;
            this.lblInputDevice.Text = "Input Device:";
            // 
            // btnRecord
            // 
            this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecord.Image = global::AudioSharp.GUI.Properties.Resources.record;
            this.btnRecord.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRecord.Location = new System.Drawing.Point(366, 231);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(100, 25);
            this.btnRecord.TabIndex = 4;
            this.btnRecord.Text = "&Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.Image = global::AudioSharp.GUI.Properties.Resources.stop;
            this.btnStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStop.Location = new System.Drawing.Point(472, 231);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 25);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "&Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // gbRecording
            // 
            this.gbRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRecording.Controls.Add(this.lblTimer);
            this.gbRecording.Controls.Add(this.prbVolume);
            this.gbRecording.Location = new System.Drawing.Point(12, 143);
            this.gbRecording.Name = "gbRecording";
            this.gbRecording.Size = new System.Drawing.Size(560, 81);
            this.gbRecording.TabIndex = 6;
            this.gbRecording.TabStop = false;
            this.gbRecording.Text = "Recording";
            // 
            // lblTimer
            // 
            this.lblTimer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(233, 16);
            this.lblTimer.Name = "lblTimer";
            this.lblTimer.Size = new System.Drawing.Size(96, 25);
            this.lblTimer.TabIndex = 1;
            this.lblTimer.Text = "00:00:00";
            // 
            // prbVolume
            // 
            this.prbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbVolume.Location = new System.Drawing.Point(6, 50);
            this.prbVolume.Name = "prbVolume";
            this.prbVolume.Size = new System.Drawing.Size(548, 23);
            this.prbVolume.TabIndex = 0;
            // 
            // timerVAMeter
            // 
            this.timerVAMeter.Enabled = true;
            this.timerVAMeter.Interval = 10;
            this.timerVAMeter.Tick += new System.EventHandler(this.timerVAMeter_Tick);
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuTools,
            this.menuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(584, 24);
            this.menuStrip.TabIndex = 7;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuButtonRecord,
            this.menuButtonStop,
            this.toolStripSeparator1,
            this.menuButtonExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "File";
            // 
            // menuButtonRecord
            // 
            this.menuButtonRecord.Image = global::AudioSharp.GUI.Properties.Resources.record;
            this.menuButtonRecord.Name = "menuButtonRecord";
            this.menuButtonRecord.Size = new System.Drawing.Size(134, 22);
            this.menuButtonRecord.Text = "Record";
            this.menuButtonRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // menuButtonStop
            // 
            this.menuButtonStop.Enabled = false;
            this.menuButtonStop.Image = global::AudioSharp.GUI.Properties.Resources.stop;
            this.menuButtonStop.Name = "menuButtonStop";
            this.menuButtonStop.Size = new System.Drawing.Size(134, 22);
            this.menuButtonStop.Text = "Stop";
            this.menuButtonStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(131, 6);
            // 
            // menuButtonExit
            // 
            this.menuButtonExit.Image = ((System.Drawing.Image)(resources.GetObject("menuButtonExit.Image")));
            this.menuButtonExit.Name = "menuButtonExit";
            this.menuButtonExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuButtonExit.Size = new System.Drawing.Size(134, 22);
            this.menuButtonExit.Text = "Exit";
            this.menuButtonExit.Click += new System.EventHandler(this.menuButtonExit_Click);
            // 
            // menuTools
            // 
            this.menuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuButtonRecordings,
            this.menuButtonSettings});
            this.menuTools.Name = "menuTools";
            this.menuTools.Size = new System.Drawing.Size(47, 20);
            this.menuTools.Text = "Tools";
            // 
            // menuButtonRecordings
            // 
            this.menuButtonRecordings.Image = global::AudioSharp.GUI.Properties.Resources.Folder;
            this.menuButtonRecordings.Name = "menuButtonRecordings";
            this.menuButtonRecordings.Size = new System.Drawing.Size(142, 22);
            this.menuButtonRecordings.Text = "Recordings...";
            this.menuButtonRecordings.Click += new System.EventHandler(this.menuButtonRecordings_Click);
            // 
            // menuButtonSettings
            // 
            this.menuButtonSettings.Image = global::AudioSharp.GUI.Properties.Resources.Compile;
            this.menuButtonSettings.Name = "menuButtonSettings";
            this.menuButtonSettings.Size = new System.Drawing.Size(142, 22);
            this.menuButtonSettings.Text = "Settings...";
            this.menuButtonSettings.Click += new System.EventHandler(this.menuButtonSettings_Click);
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuButtonAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(44, 20);
            this.menuHelp.Text = "Help";
            // 
            // menuButtonAbout
            // 
            this.menuButtonAbout.Image = global::AudioSharp.GUI.Properties.Resources.Info;
            this.menuButtonAbout.Name = "menuButtonAbout";
            this.menuButtonAbout.Size = new System.Drawing.Size(116, 22);
            this.menuButtonAbout.Text = "About...";
            this.menuButtonAbout.Click += new System.EventHandler(this.menuButtonAbout_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextButtonRecord,
            this.contextButtonStop,
            this.contextButtonExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(112, 70);
            // 
            // contextButtonRecord
            // 
            this.contextButtonRecord.Image = global::AudioSharp.GUI.Properties.Resources.record;
            this.contextButtonRecord.Name = "contextButtonRecord";
            this.contextButtonRecord.Size = new System.Drawing.Size(111, 22);
            this.contextButtonRecord.Text = "Record";
            this.contextButtonRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // contextButtonStop
            // 
            this.contextButtonStop.Enabled = false;
            this.contextButtonStop.Image = global::AudioSharp.GUI.Properties.Resources.stop;
            this.contextButtonStop.Name = "contextButtonStop";
            this.contextButtonStop.Size = new System.Drawing.Size(111, 22);
            this.contextButtonStop.Text = "Stop";
            this.contextButtonStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // contextButtonExit
            // 
            this.contextButtonExit.Image = global::AudioSharp.GUI.Properties.Resources.Application_cancel;
            this.contextButtonExit.Name = "contextButtonExit";
            this.contextButtonExit.Size = new System.Drawing.Size(111, 22);
            this.contextButtonExit.Text = "Exit";
            this.contextButtonExit.Click += new System.EventHandler(this.menuButtonExit_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.DodgerBlue;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.driveSpaceLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 264);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(584, 22);
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusLabel.ForeColor = System.Drawing.Color.White;
            this.statusLabel.Margin = new System.Windows.Forms.Padding(0, 3, 10, 2);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(82, 17);
            this.statusLabel.Text = "Status: Ready";
            // 
            // driveSpaceLabel
            // 
            this.driveSpaceLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.driveSpaceLabel.ForeColor = System.Drawing.Color.White;
            this.driveSpaceLabel.Name = "driveSpaceLabel";
            this.driveSpaceLabel.Size = new System.Drawing.Size(82, 17);
            this.driveSpaceLabel.Text = "Free space: ??";
            // 
            // timerSpaceCheck
            // 
            this.timerSpaceCheck.Enabled = true;
            this.timerSpaceCheck.Interval = 60000;
            this.timerSpaceCheck.Tick += new System.EventHandler(this.timerSpaceCheck_Tick);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 286);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.gbRecording);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.gbRecordingSettings);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MinimumSize = new System.Drawing.Size(600, 325);
            this.Name = "FrmMain";
            this.Text = "AudioSharp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.gbRecordingSettings.ResumeLayout(false);
            this.gbRecordingSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckVolume)).EndInit();
            this.gbRecording.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbInputDevices;
        private System.Windows.Forms.GroupBox gbRecordingSettings;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label lblOutputFile;
        private System.Windows.Forms.Label lblInputDevice;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.GroupBox gbRecording;
        private System.Windows.Forms.Label lblTimer;
        private System.Windows.Forms.ProgressBar prbVolume;
        private System.Windows.Forms.Timer timerVAMeter;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuTools;
        private System.Windows.Forms.ToolStripMenuItem menuButtonSettings;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuButtonAbout;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar trckVolume;
        private System.Windows.Forms.Label lblVolumePercentage;
        private System.Windows.Forms.ToolStripMenuItem menuButtonExit;
        private System.Windows.Forms.ToolStripMenuItem menuButtonRecordings;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem contextButtonRecord;
        private System.Windows.Forms.ToolStripMenuItem contextButtonStop;
        private System.Windows.Forms.ToolStripMenuItem contextButtonExit;
        private System.Windows.Forms.ToolStripMenuItem menuButtonRecord;
        private System.Windows.Forms.ToolStripMenuItem menuButtonStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel driveSpaceLabel;
        private System.Windows.Forms.Timer timerSpaceCheck;
    }
}

