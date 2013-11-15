namespace OTATools
{
    partial class Upload
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upload));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelFileBrowe = new System.Windows.Forms.Panel();
            this.txtUpdateFileName = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFileMD5 = new System.Windows.Forms.TextBox();
            this.textBoxFileSize = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxBuildNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxVersion = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxModelId = new System.Windows.Forms.TextBox();
            this.labelMsg = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxChangeLog = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panelFileBrowe.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelFileBrowe);
            this.groupBox1.Controls.Add(this.panelProgress);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // panelFileBrowe
            // 
            this.panelFileBrowe.Controls.Add(this.txtUpdateFileName);
            this.panelFileBrowe.Controls.Add(this.btnBrowseFile);
            resources.ApplyResources(this.panelFileBrowe, "panelFileBrowe");
            this.panelFileBrowe.Name = "panelFileBrowe";
            // 
            // txtUpdateFileName
            // 
            resources.ApplyResources(this.txtUpdateFileName, "txtUpdateFileName");
            this.txtUpdateFileName.Name = "txtUpdateFileName";
            // 
            // btnBrowseFile
            // 
            resources.ApplyResources(this.btnBrowseFile, "btnBrowseFile");
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.label2);
            this.panelProgress.Controls.Add(this.progressBar1);
            resources.ApplyResources(this.panelProgress, "panelProgress");
            this.panelProgress.Name = "panelProgress";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // textBoxFileMD5
            // 
            resources.ApplyResources(this.textBoxFileMD5, "textBoxFileMD5");
            this.textBoxFileMD5.Name = "textBoxFileMD5";
            this.textBoxFileMD5.ReadOnly = true;
            // 
            // textBoxFileSize
            // 
            resources.ApplyResources(this.textBoxFileSize, "textBoxFileSize");
            this.textBoxFileSize.Name = "textBoxFileSize";
            this.textBoxFileSize.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btnUpload
            // 
            resources.ApplyResources(this.btnUpload, "btnUpload");
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxBuildNumber);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBoxVersion);
            this.groupBox2.Controls.Add(this.textBoxID);
            this.groupBox2.Controls.Add(this.textBoxModelId);
            this.groupBox2.Controls.Add(this.textBoxFileSize);
            this.groupBox2.Controls.Add(this.textBoxFileMD5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // textBoxBuildNumber
            // 
            resources.ApplyResources(this.textBoxBuildNumber, "textBoxBuildNumber");
            this.textBoxBuildNumber.Name = "textBoxBuildNumber";
            this.textBoxBuildNumber.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // textBoxVersion
            // 
            resources.ApplyResources(this.textBoxVersion, "textBoxVersion");
            this.textBoxVersion.Name = "textBoxVersion";
            this.textBoxVersion.ReadOnly = true;
            // 
            // textBoxID
            // 
            resources.ApplyResources(this.textBoxID, "textBoxID");
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.ReadOnly = true;
            // 
            // textBoxModelId
            // 
            resources.ApplyResources(this.textBoxModelId, "textBoxModelId");
            this.textBoxModelId.Name = "textBoxModelId";
            this.textBoxModelId.ReadOnly = true;
            // 
            // labelMsg
            // 
            resources.ApplyResources(this.labelMsg, "labelMsg");
            this.labelMsg.ForeColor = System.Drawing.Color.Red;
            this.labelMsg.Name = "labelMsg";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxChangeLog);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // textBoxChangeLog
            // 
            resources.ApplyResources(this.textBoxChangeLog, "textBoxChangeLog");
            this.textBoxChangeLog.Name = "textBoxChangeLog";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBoxDescription);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // textBoxDescription
            // 
            resources.ApplyResources(this.textBoxDescription, "textBoxDescription");
            this.textBoxDescription.Name = "textBoxDescription";
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Upload
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.labelMsg);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Upload";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Upload_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Upload_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.panelFileBrowe.ResumeLayout(false);
            this.panelFileBrowe.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxFileSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFileMD5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxModelId;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxVersion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxChangeLog;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Panel panelFileBrowe;
        private System.Windows.Forms.TextBox txtUpdateFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxBuildNumber;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}