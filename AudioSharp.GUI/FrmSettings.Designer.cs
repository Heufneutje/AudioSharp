namespace AudioSharp.GUI
{
    partial class FrmSettings
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
            this.configurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageRecording = new System.Windows.Forms.TabPage();
            this.grpDefaultLocation = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRecordingsFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.grpOutputFormat = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbOutputFormat = new System.Windows.Forms.ComboBox();
            this.grpFileDestination = new System.Windows.Forms.GroupBox();
            this.chkAutoIncrementRecording = new System.Windows.Forms.CheckBox();
            this.txtNextRecording = new System.Windows.Forms.TextBox();
            this.lblNextRecordingPreview = new System.Windows.Forms.Label();
            this.lblNextRecordingNumber = new System.Windows.Forms.Label();
            this.lblRecordingPrefix = new System.Windows.Forms.Label();
            this.spinNextRecording = new System.Windows.Forms.NumericUpDown();
            this.txtRecordingPrefix = new System.Windows.Forms.TextBox();
            this.tabPageInterface = new System.Windows.Forms.TabPage();
            this.grpMainInterface = new System.Windows.Forms.GroupBox();
            this.chkAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.grpTray = new System.Windows.Forms.GroupBox();
            this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.chkShowTrayIcon = new System.Windows.Forms.CheckBox();
            this.tabPageHotkeys = new System.Windows.Forms.TabPage();
            this.grpHotkeys = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHotkeyStop = new System.Windows.Forms.TextBox();
            this.txtHotkeyRecord = new System.Windows.Forms.TextBox();
            this.rbGenerateAutomatically = new System.Windows.Forms.RadioButton();
            this.rbPromptForFilename = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.configurationBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageRecording.SuspendLayout();
            this.grpDefaultLocation.SuspendLayout();
            this.grpOutputFormat.SuspendLayout();
            this.grpFileDestination.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNextRecording)).BeginInit();
            this.tabPageInterface.SuspendLayout();
            this.grpMainInterface.SuspendLayout();
            this.grpTray.SuspendLayout();
            this.tabPageHotkeys.SuspendLayout();
            this.grpHotkeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // configurationBindingSource
            // 
            this.configurationBindingSource.DataSource = typeof(AudioSharp.Config.Configuration);
            this.configurationBindingSource.CurrentItemChanged += new System.EventHandler(this.configurationBindingSource_CurrentItemChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 301);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 33);
            this.panel1.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(524, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(605, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageRecording);
            this.tabControl.Controls.Add(this.tabPageInterface);
            this.tabControl.Controls.Add(this.tabPageHotkeys);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(687, 334);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageRecording
            // 
            this.tabPageRecording.Controls.Add(this.grpDefaultLocation);
            this.tabPageRecording.Controls.Add(this.grpOutputFormat);
            this.tabPageRecording.Controls.Add(this.grpFileDestination);
            this.tabPageRecording.Location = new System.Drawing.Point(4, 22);
            this.tabPageRecording.Name = "tabPageRecording";
            this.tabPageRecording.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageRecording.Size = new System.Drawing.Size(679, 308);
            this.tabPageRecording.TabIndex = 0;
            this.tabPageRecording.Text = "Recording";
            this.tabPageRecording.UseVisualStyleBackColor = true;
            // 
            // grpDefaultLocation
            // 
            this.grpDefaultLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDefaultLocation.Controls.Add(this.label1);
            this.grpDefaultLocation.Controls.Add(this.txtRecordingsFolder);
            this.grpDefaultLocation.Controls.Add(this.btnBrowse);
            this.grpDefaultLocation.Location = new System.Drawing.Point(9, 66);
            this.grpDefaultLocation.Name = "grpDefaultLocation";
            this.grpDefaultLocation.Size = new System.Drawing.Size(662, 54);
            this.grpDefaultLocation.TabIndex = 1;
            this.grpDefaultLocation.TabStop = false;
            this.grpDefaultLocation.Text = "Default file destination";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Recordings folder:";
            // 
            // txtRecordingsFolder
            // 
            this.txtRecordingsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordingsFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configurationBindingSource, "RecordingsFolder", true));
            this.txtRecordingsFolder.Location = new System.Drawing.Point(157, 24);
            this.txtRecordingsFolder.Name = "txtRecordingsFolder";
            this.txtRecordingsFolder.Size = new System.Drawing.Size(414, 20);
            this.txtRecordingsFolder.TabIndex = 0;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(577, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(64, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // grpOutputFormat
            // 
            this.grpOutputFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpOutputFormat.Controls.Add(this.label5);
            this.grpOutputFormat.Controls.Add(this.cbOutputFormat);
            this.grpOutputFormat.Location = new System.Drawing.Point(8, 6);
            this.grpOutputFormat.Name = "grpOutputFormat";
            this.grpOutputFormat.Size = new System.Drawing.Size(663, 53);
            this.grpOutputFormat.TabIndex = 0;
            this.grpOutputFormat.TabStop = false;
            this.grpOutputFormat.Text = "Format";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Output format:";
            // 
            // cbOutputFormat
            // 
            this.cbOutputFormat.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", this.configurationBindingSource, "OutputFormat", true));
            this.cbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOutputFormat.FormattingEnabled = true;
            this.cbOutputFormat.Items.AddRange(new object[] {
            "wav",
            "mp3"});
            this.cbOutputFormat.Location = new System.Drawing.Point(157, 20);
            this.cbOutputFormat.Name = "cbOutputFormat";
            this.cbOutputFormat.Size = new System.Drawing.Size(61, 21);
            this.cbOutputFormat.TabIndex = 0;
            this.cbOutputFormat.SelectedValueChanged += new System.EventHandler(this.cbOutputFormat_SelectedValueChanged);
            // 
            // grpFileDestination
            // 
            this.grpFileDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFileDestination.Controls.Add(this.rbGenerateAutomatically);
            this.grpFileDestination.Controls.Add(this.rbPromptForFilename);
            this.grpFileDestination.Controls.Add(this.chkAutoIncrementRecording);
            this.grpFileDestination.Controls.Add(this.txtNextRecording);
            this.grpFileDestination.Controls.Add(this.lblNextRecordingPreview);
            this.grpFileDestination.Controls.Add(this.lblNextRecordingNumber);
            this.grpFileDestination.Controls.Add(this.lblRecordingPrefix);
            this.grpFileDestination.Controls.Add(this.spinNextRecording);
            this.grpFileDestination.Controls.Add(this.txtRecordingPrefix);
            this.grpFileDestination.Location = new System.Drawing.Point(8, 126);
            this.grpFileDestination.Name = "grpFileDestination";
            this.grpFileDestination.Size = new System.Drawing.Size(663, 147);
            this.grpFileDestination.TabIndex = 2;
            this.grpFileDestination.TabStop = false;
            this.grpFileDestination.Text = "File name";
            // 
            // chkAutoIncrementRecording
            // 
            this.chkAutoIncrementRecording.AutoSize = true;
            this.chkAutoIncrementRecording.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.configurationBindingSource, "AutoIncrementRecordingNumber", true));
            this.chkAutoIncrementRecording.Location = new System.Drawing.Point(224, 93);
            this.chkAutoIncrementRecording.Name = "chkAutoIncrementRecording";
            this.chkAutoIncrementRecording.Size = new System.Drawing.Size(97, 17);
            this.chkAutoIncrementRecording.TabIndex = 4;
            this.chkAutoIncrementRecording.Text = "Auto-increment";
            this.chkAutoIncrementRecording.UseVisualStyleBackColor = true;
            // 
            // txtNextRecording
            // 
            this.txtNextRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNextRecording.Location = new System.Drawing.Point(157, 117);
            this.txtNextRecording.Name = "txtNextRecording";
            this.txtNextRecording.ReadOnly = true;
            this.txtNextRecording.Size = new System.Drawing.Size(485, 20);
            this.txtNextRecording.TabIndex = 5;
            this.txtNextRecording.TabStop = false;
            // 
            // lblNextRecordingPreview
            // 
            this.lblNextRecordingPreview.AutoSize = true;
            this.lblNextRecordingPreview.Location = new System.Drawing.Point(29, 120);
            this.lblNextRecordingPreview.Name = "lblNextRecordingPreview";
            this.lblNextRecordingPreview.Size = new System.Drawing.Size(119, 13);
            this.lblNextRecordingPreview.TabIndex = 11;
            this.lblNextRecordingPreview.Text = "Next recording preview:";
            // 
            // lblNextRecordingNumber
            // 
            this.lblNextRecordingNumber.AutoSize = true;
            this.lblNextRecordingNumber.Location = new System.Drawing.Point(29, 93);
            this.lblNextRecordingNumber.Name = "lblNextRecordingNumber";
            this.lblNextRecordingNumber.Size = new System.Drawing.Size(117, 13);
            this.lblNextRecordingNumber.TabIndex = 9;
            this.lblNextRecordingNumber.Text = "Next recording number:";
            // 
            // lblRecordingPrefix
            // 
            this.lblRecordingPrefix.AutoSize = true;
            this.lblRecordingPrefix.Location = new System.Drawing.Point(29, 68);
            this.lblRecordingPrefix.Name = "lblRecordingPrefix";
            this.lblRecordingPrefix.Size = new System.Drawing.Size(87, 13);
            this.lblRecordingPrefix.TabIndex = 8;
            this.lblRecordingPrefix.Text = "Recording prefix:";
            // 
            // spinNextRecording
            // 
            this.spinNextRecording.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.configurationBindingSource, "NextRecordingNumber", true));
            this.spinNextRecording.Location = new System.Drawing.Point(157, 91);
            this.spinNextRecording.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinNextRecording.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNextRecording.Name = "spinNextRecording";
            this.spinNextRecording.Size = new System.Drawing.Size(61, 20);
            this.spinNextRecording.TabIndex = 3;
            this.spinNextRecording.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtRecordingPrefix
            // 
            this.txtRecordingPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordingPrefix.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configurationBindingSource, "RecordingPrefix", true));
            this.txtRecordingPrefix.Location = new System.Drawing.Point(157, 65);
            this.txtRecordingPrefix.Name = "txtRecordingPrefix";
            this.txtRecordingPrefix.Size = new System.Drawing.Size(485, 20);
            this.txtRecordingPrefix.TabIndex = 2;
            // 
            // tabPageInterface
            // 
            this.tabPageInterface.Controls.Add(this.grpMainInterface);
            this.tabPageInterface.Controls.Add(this.grpTray);
            this.tabPageInterface.Location = new System.Drawing.Point(4, 22);
            this.tabPageInterface.Name = "tabPageInterface";
            this.tabPageInterface.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInterface.Size = new System.Drawing.Size(679, 308);
            this.tabPageInterface.TabIndex = 1;
            this.tabPageInterface.Text = "Interface";
            this.tabPageInterface.UseVisualStyleBackColor = true;
            // 
            // grpMainInterface
            // 
            this.grpMainInterface.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMainInterface.Controls.Add(this.chkAlwaysOnTop);
            this.grpMainInterface.Location = new System.Drawing.Point(8, 6);
            this.grpMainInterface.Name = "grpMainInterface";
            this.grpMainInterface.Size = new System.Drawing.Size(663, 46);
            this.grpMainInterface.TabIndex = 1;
            this.grpMainInterface.TabStop = false;
            this.grpMainInterface.Text = "Main interface";
            // 
            // chkAlwaysOnTop
            // 
            this.chkAlwaysOnTop.AutoSize = true;
            this.chkAlwaysOnTop.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.configurationBindingSource, "AlwaysOnTop", true));
            this.chkAlwaysOnTop.Location = new System.Drawing.Point(8, 19);
            this.chkAlwaysOnTop.Name = "chkAlwaysOnTop";
            this.chkAlwaysOnTop.Size = new System.Drawing.Size(217, 17);
            this.chkAlwaysOnTop.TabIndex = 2;
            this.chkAlwaysOnTop.Text = "Always show AudioSharp window on top";
            this.chkAlwaysOnTop.UseVisualStyleBackColor = true;
            // 
            // grpTray
            // 
            this.grpTray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTray.Controls.Add(this.chkMinimizeToTray);
            this.grpTray.Controls.Add(this.chkShowTrayIcon);
            this.grpTray.Location = new System.Drawing.Point(8, 58);
            this.grpTray.Name = "grpTray";
            this.grpTray.Size = new System.Drawing.Size(663, 71);
            this.grpTray.TabIndex = 0;
            this.grpTray.TabStop = false;
            this.grpTray.Text = "System tray";
            // 
            // chkMinimizeToTray
            // 
            this.chkMinimizeToTray.AutoSize = true;
            this.chkMinimizeToTray.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.configurationBindingSource, "MinimizeToTray", true));
            this.chkMinimizeToTray.Enabled = false;
            this.chkMinimizeToTray.Location = new System.Drawing.Point(7, 42);
            this.chkMinimizeToTray.Name = "chkMinimizeToTray";
            this.chkMinimizeToTray.Size = new System.Drawing.Size(98, 17);
            this.chkMinimizeToTray.TabIndex = 1;
            this.chkMinimizeToTray.Text = "Minimize to tray";
            this.chkMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // chkShowTrayIcon
            // 
            this.chkShowTrayIcon.AutoSize = true;
            this.chkShowTrayIcon.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.configurationBindingSource, "ShowTrayIcon", true));
            this.chkShowTrayIcon.Location = new System.Drawing.Point(7, 19);
            this.chkShowTrayIcon.Name = "chkShowTrayIcon";
            this.chkShowTrayIcon.Size = new System.Drawing.Size(96, 17);
            this.chkShowTrayIcon.TabIndex = 0;
            this.chkShowTrayIcon.Text = "Show tray icon";
            this.chkShowTrayIcon.UseVisualStyleBackColor = true;
            this.chkShowTrayIcon.CheckedChanged += new System.EventHandler(this.chkShowTrayIcon_CheckedChanged);
            // 
            // tabPageHotkeys
            // 
            this.tabPageHotkeys.Controls.Add(this.grpHotkeys);
            this.tabPageHotkeys.Location = new System.Drawing.Point(4, 22);
            this.tabPageHotkeys.Name = "tabPageHotkeys";
            this.tabPageHotkeys.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHotkeys.Size = new System.Drawing.Size(679, 308);
            this.tabPageHotkeys.TabIndex = 2;
            this.tabPageHotkeys.Text = "Hotkeys";
            this.tabPageHotkeys.UseVisualStyleBackColor = true;
            // 
            // grpHotkeys
            // 
            this.grpHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHotkeys.Controls.Add(this.label7);
            this.grpHotkeys.Controls.Add(this.label6);
            this.grpHotkeys.Controls.Add(this.txtHotkeyStop);
            this.grpHotkeys.Controls.Add(this.txtHotkeyRecord);
            this.grpHotkeys.Location = new System.Drawing.Point(8, 6);
            this.grpHotkeys.Name = "grpHotkeys";
            this.grpHotkeys.Size = new System.Drawing.Size(663, 78);
            this.grpHotkeys.TabIndex = 0;
            this.grpHotkeys.TabStop = false;
            this.grpHotkeys.Text = "Global hotkeys";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Stop recording:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Start recording:";
            // 
            // txtHotkeyStop
            // 
            this.txtHotkeyStop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotkeyStop.Location = new System.Drawing.Point(157, 47);
            this.txtHotkeyStop.Name = "txtHotkeyStop";
            this.txtHotkeyStop.ReadOnly = true;
            this.txtHotkeyStop.Size = new System.Drawing.Size(485, 20);
            this.txtHotkeyStop.TabIndex = 1;
            this.txtHotkeyStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotkeyStop_KeyDown);
            // 
            // txtHotkeyRecord
            // 
            this.txtHotkeyRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHotkeyRecord.Location = new System.Drawing.Point(157, 20);
            this.txtHotkeyRecord.Name = "txtHotkeyRecord";
            this.txtHotkeyRecord.ReadOnly = true;
            this.txtHotkeyRecord.Size = new System.Drawing.Size(485, 20);
            this.txtHotkeyRecord.TabIndex = 0;
            this.txtHotkeyRecord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtHotkeyRecord_KeyDown);
            // 
            // rbGenerateAutomatically
            // 
            this.rbGenerateAutomatically.AutoSize = true;
            this.rbGenerateAutomatically.Checked = true;
            this.rbGenerateAutomatically.Location = new System.Drawing.Point(13, 42);
            this.rbGenerateAutomatically.Name = "rbGenerateAutomatically";
            this.rbGenerateAutomatically.Size = new System.Drawing.Size(186, 17);
            this.rbGenerateAutomatically.TabIndex = 1;
            this.rbGenerateAutomatically.TabStop = true;
            this.rbGenerateAutomatically.Text = "Automatically generate a file name";
            this.rbGenerateAutomatically.UseVisualStyleBackColor = true;
            this.rbGenerateAutomatically.CheckedChanged += new System.EventHandler(this.rbGenerateAutomatically_CheckedChanged);
            // 
            // rbPromptForFilename
            // 
            this.rbPromptForFilename.AutoSize = true;
            this.rbPromptForFilename.Location = new System.Drawing.Point(13, 19);
            this.rbPromptForFilename.Name = "rbPromptForFilename";
            this.rbPromptForFilename.Size = new System.Drawing.Size(178, 17);
            this.rbPromptForFilename.TabIndex = 0;
            this.rbPromptForFilename.Text = "Prompt for a file name every time";
            this.rbPromptForFilename.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 334);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 260);
            this.Name = "FrmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.configurationBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageRecording.ResumeLayout(false);
            this.grpDefaultLocation.ResumeLayout(false);
            this.grpDefaultLocation.PerformLayout();
            this.grpOutputFormat.ResumeLayout(false);
            this.grpOutputFormat.PerformLayout();
            this.grpFileDestination.ResumeLayout(false);
            this.grpFileDestination.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNextRecording)).EndInit();
            this.tabPageInterface.ResumeLayout(false);
            this.grpMainInterface.ResumeLayout(false);
            this.grpMainInterface.PerformLayout();
            this.grpTray.ResumeLayout(false);
            this.grpTray.PerformLayout();
            this.tabPageHotkeys.ResumeLayout(false);
            this.grpHotkeys.ResumeLayout(false);
            this.grpHotkeys.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpFileDestination;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblNextRecordingNumber;
        private System.Windows.Forms.Label lblRecordingPrefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown spinNextRecording;
        private System.Windows.Forms.TextBox txtRecordingPrefix;
        private System.Windows.Forms.TextBox txtRecordingsFolder;
        private System.Windows.Forms.BindingSource configurationBindingSource;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtNextRecording;
        private System.Windows.Forms.Label lblNextRecordingPreview;
        private System.Windows.Forms.CheckBox chkAutoIncrementRecording;
        private System.Windows.Forms.ComboBox cbOutputFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageRecording;
        private System.Windows.Forms.TabPage tabPageInterface;
        private System.Windows.Forms.GroupBox grpTray;
        private System.Windows.Forms.CheckBox chkMinimizeToTray;
        private System.Windows.Forms.CheckBox chkShowTrayIcon;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkAlwaysOnTop;
        private System.Windows.Forms.GroupBox grpMainInterface;
        private System.Windows.Forms.TabPage tabPageHotkeys;
        private System.Windows.Forms.GroupBox grpHotkeys;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHotkeyStop;
        private System.Windows.Forms.TextBox txtHotkeyRecord;
        private System.Windows.Forms.GroupBox grpOutputFormat;
        private System.Windows.Forms.GroupBox grpDefaultLocation;
        private System.Windows.Forms.RadioButton rbGenerateAutomatically;
        private System.Windows.Forms.RadioButton rbPromptForFilename;
    }
}