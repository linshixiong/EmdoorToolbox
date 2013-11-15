namespace OTATools
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textUserName = new System.Windows.Forms.TextBox();
            this.textServerHost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbRemember = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textPort = new System.Windows.Forms.TextBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.labelLoginResult = new System.Windows.Forms.Label();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonLoginCancel = new System.Windows.Forms.Button();
            this.panelLogin.SuspendLayout();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textPassword
            // 
            resources.ApplyResources(this.textPassword, "textPassword");
            this.textPassword.Name = "textPassword";
            this.textPassword.TextChanged += new System.EventHandler(this.textInput_TextChanged);
            // 
            // textUserName
            // 
            resources.ApplyResources(this.textUserName, "textUserName");
            this.textUserName.Name = "textUserName";
            this.textUserName.TextChanged += new System.EventHandler(this.textInput_TextChanged);
            // 
            // textServerHost
            // 
            resources.ApplyResources(this.textServerHost, "textServerHost");
            this.textServerHost.Name = "textServerHost";
            this.textServerHost.TextChanged += new System.EventHandler(this.textInput_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btnLogin
            // 
            resources.ApplyResources(this.btnLogin, "btnLogin");
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbRemember
            // 
            resources.ApplyResources(this.cbRemember, "cbRemember");
            this.cbRemember.Name = "cbRemember";
            this.cbRemember.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // textPort
            // 
            resources.ApplyResources(this.textPort, "textPort");
            this.textPort.Name = "textPort";
            this.textPort.TextChanged += new System.EventHandler(this.textInput_TextChanged);
            this.textPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPort_KeyPress);
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.labelLoginResult);
            this.panelLogin.Controls.Add(this.btnCancel);
            this.panelLogin.Controls.Add(this.textPort);
            this.panelLogin.Controls.Add(this.btnLogin);
            this.panelLogin.Controls.Add(this.cbRemember);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Controls.Add(this.textPassword);
            this.panelLogin.Controls.Add(this.label4);
            this.panelLogin.Controls.Add(this.textUserName);
            this.panelLogin.Controls.Add(this.label2);
            this.panelLogin.Controls.Add(this.textServerHost);
            this.panelLogin.Controls.Add(this.label3);
            resources.ApplyResources(this.panelLogin, "panelLogin");
            this.panelLogin.Name = "panelLogin";
            // 
            // labelLoginResult
            // 
            resources.ApplyResources(this.labelLoginResult, "labelLoginResult");
            this.labelLoginResult.ForeColor = System.Drawing.Color.Red;
            this.labelLoginResult.Name = "labelLoginResult";
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.labelStatus);
            this.panelProgress.Controls.Add(this.pictureBox1);
            this.panelProgress.Controls.Add(this.buttonLoginCancel);
            resources.ApplyResources(this.panelProgress, "panelProgress");
            this.panelProgress.Name = "panelProgress";
            // 
            // labelStatus
            // 
            resources.ApplyResources(this.labelStatus, "labelStatus");
            this.labelStatus.Name = "labelStatus";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // buttonLoginCancel
            // 
            resources.ApplyResources(this.buttonLoginCancel, "buttonLoginCancel");
            this.buttonLoginCancel.Name = "buttonLoginCancel";
            this.buttonLoginCancel.UseVisualStyleBackColor = true;
            this.buttonLoginCancel.Click += new System.EventHandler(this.buttonLoginCancel_Click);
            // 
            // Login
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLogin);
            this.Controls.Add(this.panelProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textUserName;
        private System.Windows.Forms.TextBox textServerHost;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbRemember;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textPort;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonLoginCancel;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelLoginResult;

    }
}