namespace HeufyAudio.GUI
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbRecordingSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckVolume)).BeginInit();
            this.gbRecording.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbInputDevices
            // 
            this.cbInputDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbInputDevices.FormattingEnabled = true;
            this.cbInputDevices.Location = new System.Drawing.Point(139, 23);
            this.cbInputDevices.Name = "cbInputDevices";
            this.cbInputDevices.Size = new System.Drawing.Size(515, 21);
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
            this.gbRecordingSettings.Size = new System.Drawing.Size(660, 110);
            this.gbRecordingSettings.TabIndex = 2;
            this.gbRecordingSettings.TabStop = false;
            this.gbRecordingSettings.Text = "Recording Settings";
            // 
            // lblVolumePercentage
            // 
            this.lblVolumePercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVolumePercentage.AutoSize = true;
            this.lblVolumePercentage.Location = new System.Drawing.Point(618, 54);
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
            this.trckVolume.Size = new System.Drawing.Size(473, 23);
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
            this.txtOutputFile.Size = new System.Drawing.Size(515, 20);
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
            this.btnRecord.Location = new System.Drawing.Point(516, 230);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(75, 23);
            this.btnRecord.TabIndex = 4;
            this.btnRecord.Text = "&Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(597, 230);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
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
            this.gbRecording.Size = new System.Drawing.Size(660, 81);
            this.gbRecording.TabIndex = 6;
            this.gbRecording.TabStop = false;
            this.gbRecording.Text = "Recording";
            // 
            // lblTimer
            // 
            this.lblTimer.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimer.Location = new System.Drawing.Point(283, 16);
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
            this.prbVolume.Size = new System.Drawing.Size(648, 23);
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 265);
            this.Controls.Add(this.gbRecording);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.gbRecordingSettings);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(509, 263);
            this.Name = "FrmMain";
            this.Text = "Heufy Audio Recorder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gbRecordingSettings.ResumeLayout(false);
            this.gbRecordingSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trckVolume)).EndInit();
            this.gbRecording.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.TrackBar trckVolume;
        private System.Windows.Forms.Label lblVolumePercentage;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

