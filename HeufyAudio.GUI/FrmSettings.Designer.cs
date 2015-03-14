namespace HeufyAudio.GUI
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
            this.grpGeneral = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.spinCurrentRecording = new System.Windows.Forms.NumericUpDown();
            this.configurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtRecordingPrefix = new System.Windows.Forms.TextBox();
            this.txtRecordingsFolder = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNextRecording = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.grpGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinCurrentRecording)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.configurationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpGeneral
            // 
            this.grpGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGeneral.Controls.Add(this.btnBrowse);
            this.grpGeneral.Controls.Add(this.txtNextRecording);
            this.grpGeneral.Controls.Add(this.label4);
            this.grpGeneral.Controls.Add(this.label3);
            this.grpGeneral.Controls.Add(this.label2);
            this.grpGeneral.Controls.Add(this.label1);
            this.grpGeneral.Controls.Add(this.spinCurrentRecording);
            this.grpGeneral.Controls.Add(this.txtRecordingPrefix);
            this.grpGeneral.Controls.Add(this.txtRecordingsFolder);
            this.grpGeneral.Location = new System.Drawing.Point(12, 12);
            this.grpGeneral.Name = "grpGeneral";
            this.grpGeneral.Size = new System.Drawing.Size(534, 127);
            this.grpGeneral.TabIndex = 0;
            this.grpGeneral.TabStop = false;
            this.grpGeneral.Text = "General";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Next recording number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Recording prefix:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Recordings folder:";
            // 
            // spinCurrentRecording
            // 
            this.spinCurrentRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spinCurrentRecording.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.configurationBindingSource, "CurrentRecordingNumber", true));
            this.spinCurrentRecording.Location = new System.Drawing.Point(145, 71);
            this.spinCurrentRecording.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinCurrentRecording.Name = "spinCurrentRecording";
            this.spinCurrentRecording.Size = new System.Drawing.Size(378, 20);
            this.spinCurrentRecording.TabIndex = 3;
            // 
            // configurationBindingSource
            // 
            this.configurationBindingSource.DataSource = typeof(HeufyAudio.Config.Configuration);
            this.configurationBindingSource.CurrentItemChanged += new System.EventHandler(this.configurationBindingSource_CurrentItemChanged);
            // 
            // txtRecordingPrefix
            // 
            this.txtRecordingPrefix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordingPrefix.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configurationBindingSource, "RecordingPrefix", true));
            this.txtRecordingPrefix.Location = new System.Drawing.Point(145, 45);
            this.txtRecordingPrefix.Name = "txtRecordingPrefix";
            this.txtRecordingPrefix.Size = new System.Drawing.Size(378, 20);
            this.txtRecordingPrefix.TabIndex = 2;
            // 
            // txtRecordingsFolder
            // 
            this.txtRecordingsFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordingsFolder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.configurationBindingSource, "RecordingsFolder", true));
            this.txtRecordingsFolder.Location = new System.Drawing.Point(145, 20);
            this.txtRecordingsFolder.Name = "txtRecordingsFolder";
            this.txtRecordingsFolder.Size = new System.Drawing.Size(308, 20);
            this.txtRecordingsFolder.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(390, 145);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Save";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(471, 145);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Next recording:";
            // 
            // txtNextRecording
            // 
            this.txtNextRecording.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNextRecording.Location = new System.Drawing.Point(145, 97);
            this.txtNextRecording.Name = "txtNextRecording";
            this.txtNextRecording.ReadOnly = true;
            this.txtNextRecording.Size = new System.Drawing.Size(378, 20);
            this.txtNextRecording.TabIndex = 4;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(459, 18);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(64, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // FrmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 175);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpGeneral);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSettings";
            this.Text = "Settings";
            this.grpGeneral.ResumeLayout(false);
            this.grpGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinCurrentRecording)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.configurationBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpGeneral;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown spinCurrentRecording;
        private System.Windows.Forms.TextBox txtRecordingPrefix;
        private System.Windows.Forms.TextBox txtRecordingsFolder;
        private System.Windows.Forms.BindingSource configurationBindingSource;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtNextRecording;
        private System.Windows.Forms.Label label4;
    }
}