namespace IMEI_Reader
{
    partial class Writer
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoPoweroff = new System.Windows.Forms.CheckBox();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabelBt = new System.Windows.Forms.LinkLabel();
            this.linkLabelWifi = new System.Windows.Forms.LinkLabel();
            this.linkLabelIMEI = new System.Windows.Forms.LinkLabel();
            this.linkLabelSN = new System.Windows.Forms.LinkLabel();
            this.checkBoxAutoWrite = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxBt = new System.Windows.Forms.CheckBox();
            this.checkBoxWifi = new System.Windows.Forms.CheckBox();
            this.checkBoxIMEI = new System.Windows.Forms.CheckBox();
            this.checkBoxSN = new System.Windows.Forms.CheckBox();
            this.textBoxBt = new System.Windows.Forms.TextBox();
            this.textBoxWifi = new System.Windows.Forms.TextBox();
            this.textBoxIMEI = new System.Windows.Forms.TextBox();
            this.textBoxSN = new System.Windows.Forms.TextBox();
            this.buttonWrite = new System.Windows.Forms.Button();
            this.labelMsg = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.WriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PoweroffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RebootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoWriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoPoweroffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PrinterConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.USBConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxPrint = new System.Windows.Forms.CheckBox();
            this.labelPrintCount = new System.Windows.Forms.Label();
            this.textBoxPrintCount = new System.Windows.Forms.TextBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PrintAfterWriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelPrintCount);
            this.groupBox1.Controls.Add(this.textBoxPrintCount);
            this.groupBox1.Controls.Add(this.checkBoxPrint);
            this.groupBox1.Controls.Add(this.checkBoxAutoPoweroff);
            this.groupBox1.Controls.Add(this.pictureBox5);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.linkLabelBt);
            this.groupBox1.Controls.Add(this.linkLabelWifi);
            this.groupBox1.Controls.Add(this.linkLabelIMEI);
            this.groupBox1.Controls.Add(this.linkLabelSN);
            this.groupBox1.Controls.Add(this.checkBoxAutoWrite);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.checkBoxBt);
            this.groupBox1.Controls.Add(this.checkBoxWifi);
            this.groupBox1.Controls.Add(this.checkBoxIMEI);
            this.groupBox1.Controls.Add(this.checkBoxSN);
            this.groupBox1.Controls.Add(this.textBoxBt);
            this.groupBox1.Controls.Add(this.textBoxWifi);
            this.groupBox1.Controls.Add(this.textBoxIMEI);
            this.groupBox1.Controls.Add(this.textBoxSN);
            this.groupBox1.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 289);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择项";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // checkBoxAutoPoweroff
            // 
            this.checkBoxAutoPoweroff.AutoSize = true;
            this.checkBoxAutoPoweroff.Location = new System.Drawing.Point(163, 256);
            this.checkBoxAutoPoweroff.Name = "checkBoxAutoPoweroff";
            this.checkBoxAutoPoweroff.Size = new System.Drawing.Size(96, 18);
            this.checkBoxAutoPoweroff.TabIndex = 23;
            this.checkBoxAutoPoweroff.Text = "完成后关机";
            this.checkBoxAutoPoweroff.UseVisualStyleBackColor = true;
            this.checkBoxAutoPoweroff.CheckedChanged += new System.EventHandler(this.checkBoxPoweroff_CheckedChanged);
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.label5);
            this.panelProgress.Controls.Add(this.pictureBox1);
            this.panelProgress.Location = new System.Drawing.Point(282, 323);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(137, 39);
            this.panelProgress.TabIndex = 11;
            this.panelProgress.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(40, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 14);
            this.label5.TabIndex = 10;
            this.label5.Text = "正在写入...";
            // 
            // linkLabelBt
            // 
            this.linkLabelBt.AutoSize = true;
            this.linkLabelBt.Font = new System.Drawing.Font("黑体", 10F);
            this.linkLabelBt.Location = new System.Drawing.Point(419, 216);
            this.linkLabelBt.Name = "linkLabelBt";
            this.linkLabelBt.Size = new System.Drawing.Size(35, 14);
            this.linkLabelBt.TabIndex = 5;
            this.linkLabelBt.TabStop = true;
            this.linkLabelBt.Tag = "4";
            this.linkLabelBt.Text = "配置";
            this.linkLabelBt.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // linkLabelWifi
            // 
            this.linkLabelWifi.AutoSize = true;
            this.linkLabelWifi.Font = new System.Drawing.Font("黑体", 10F);
            this.linkLabelWifi.Location = new System.Drawing.Point(419, 157);
            this.linkLabelWifi.Name = "linkLabelWifi";
            this.linkLabelWifi.Size = new System.Drawing.Size(35, 14);
            this.linkLabelWifi.TabIndex = 5;
            this.linkLabelWifi.TabStop = true;
            this.linkLabelWifi.Tag = "3";
            this.linkLabelWifi.Text = "配置";
            this.linkLabelWifi.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // linkLabelIMEI
            // 
            this.linkLabelIMEI.AutoSize = true;
            this.linkLabelIMEI.Font = new System.Drawing.Font("黑体", 10F);
            this.linkLabelIMEI.Location = new System.Drawing.Point(419, 97);
            this.linkLabelIMEI.Name = "linkLabelIMEI";
            this.linkLabelIMEI.Size = new System.Drawing.Size(35, 14);
            this.linkLabelIMEI.TabIndex = 5;
            this.linkLabelIMEI.TabStop = true;
            this.linkLabelIMEI.Tag = "2";
            this.linkLabelIMEI.Text = "配置";
            this.linkLabelIMEI.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // linkLabelSN
            // 
            this.linkLabelSN.AutoSize = true;
            this.linkLabelSN.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabelSN.Location = new System.Drawing.Point(419, 37);
            this.linkLabelSN.Name = "linkLabelSN";
            this.linkLabelSN.Size = new System.Drawing.Size(35, 14);
            this.linkLabelSN.TabIndex = 5;
            this.linkLabelSN.TabStop = true;
            this.linkLabelSN.Tag = "1";
            this.linkLabelSN.Text = "配置";
            this.linkLabelSN.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // checkBoxAutoWrite
            // 
            this.checkBoxAutoWrite.AutoSize = true;
            this.checkBoxAutoWrite.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxAutoWrite.Location = new System.Drawing.Point(75, 256);
            this.checkBoxAutoWrite.Name = "checkBoxAutoWrite";
            this.checkBoxAutoWrite.Size = new System.Drawing.Size(82, 18);
            this.checkBoxAutoWrite.TabIndex = 4;
            this.checkBoxAutoWrite.Text = "自动烧写";
            this.checkBoxAutoWrite.UseVisualStyleBackColor = true;
            this.checkBoxAutoWrite.CheckedChanged += new System.EventHandler(this.checkBoxAutoWrite_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 10F);
            this.label4.Location = new System.Drawing.Point(6, 217);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "蓝牙地址";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 10F);
            this.label3.Location = new System.Drawing.Point(6, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 14);
            this.label3.TabIndex = 3;
            this.label3.Text = "WIFI地址";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 10F);
            this.label2.Location = new System.Drawing.Point(34, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "IMEI";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 10F);
            this.label1.Location = new System.Drawing.Point(41, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 14);
            this.label1.TabIndex = 3;
            this.label1.Text = "S/N";
            // 
            // checkBoxBt
            // 
            this.checkBoxBt.AutoSize = true;
            this.checkBoxBt.Checked = true;
            this.checkBoxBt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBt.Location = new System.Drawing.Point(398, 217);
            this.checkBoxBt.Name = "checkBoxBt";
            this.checkBoxBt.Size = new System.Drawing.Size(15, 14);
            this.checkBoxBt.TabIndex = 2;
            this.checkBoxBt.UseVisualStyleBackColor = true;
            this.checkBoxBt.CheckedChanged += new System.EventHandler(this.checkBoxBt_CheckedChanged);
            // 
            // checkBoxWifi
            // 
            this.checkBoxWifi.AutoSize = true;
            this.checkBoxWifi.Checked = true;
            this.checkBoxWifi.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxWifi.Location = new System.Drawing.Point(398, 157);
            this.checkBoxWifi.Name = "checkBoxWifi";
            this.checkBoxWifi.Size = new System.Drawing.Size(15, 14);
            this.checkBoxWifi.TabIndex = 2;
            this.checkBoxWifi.UseVisualStyleBackColor = true;
            this.checkBoxWifi.CheckedChanged += new System.EventHandler(this.checkBoxWifi_CheckedChanged);
            // 
            // checkBoxIMEI
            // 
            this.checkBoxIMEI.AutoSize = true;
            this.checkBoxIMEI.Checked = true;
            this.checkBoxIMEI.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIMEI.Location = new System.Drawing.Point(398, 97);
            this.checkBoxIMEI.Name = "checkBoxIMEI";
            this.checkBoxIMEI.Size = new System.Drawing.Size(15, 14);
            this.checkBoxIMEI.TabIndex = 2;
            this.checkBoxIMEI.UseVisualStyleBackColor = true;
            this.checkBoxIMEI.CheckedChanged += new System.EventHandler(this.checkBoxIMEI_CheckedChanged);
            // 
            // checkBoxSN
            // 
            this.checkBoxSN.AutoSize = true;
            this.checkBoxSN.Checked = true;
            this.checkBoxSN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSN.Location = new System.Drawing.Point(398, 37);
            this.checkBoxSN.Name = "checkBoxSN";
            this.checkBoxSN.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSN.TabIndex = 2;
            this.checkBoxSN.UseVisualStyleBackColor = true;
            this.checkBoxSN.CheckedChanged += new System.EventHandler(this.checkBoxSN_CheckedChanged);
            // 
            // textBoxBt
            // 
            this.textBoxBt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxBt.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxBt.Location = new System.Drawing.Point(75, 211);
            this.textBoxBt.MaxLength = 120;
            this.textBoxBt.Name = "textBoxBt";
            this.textBoxBt.Size = new System.Drawing.Size(317, 26);
            this.textBoxBt.TabIndex = 1;
            this.textBoxBt.TextChanged += new System.EventHandler(this.textBoxWifi_TextChanged);
            this.textBoxBt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxBt_KeyDown);
            this.textBoxBt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWifi_KeyPress);
            // 
            // textBoxWifi
            // 
            this.textBoxWifi.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxWifi.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxWifi.Location = new System.Drawing.Point(75, 151);
            this.textBoxWifi.MaxLength = 120;
            this.textBoxWifi.Name = "textBoxWifi";
            this.textBoxWifi.Size = new System.Drawing.Size(317, 26);
            this.textBoxWifi.TabIndex = 1;
            this.textBoxWifi.TextChanged += new System.EventHandler(this.textBoxWifi_TextChanged);
            this.textBoxWifi.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxWifi_KeyDown);
            this.textBoxWifi.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWifi_KeyPress);
            // 
            // textBoxIMEI
            // 
            this.textBoxIMEI.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxIMEI.Location = new System.Drawing.Point(75, 91);
            this.textBoxIMEI.MaxLength = 15;
            this.textBoxIMEI.Name = "textBoxIMEI";
            this.textBoxIMEI.Size = new System.Drawing.Size(317, 26);
            this.textBoxIMEI.TabIndex = 1;
            this.textBoxIMEI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxIMEI_KeyDown);
            // 
            // textBoxSN
            // 
            this.textBoxSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxSN.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSN.Location = new System.Drawing.Point(75, 31);
            this.textBoxSN.MaxLength = 32;
            this.textBoxSN.Name = "textBoxSN";
            this.textBoxSN.Size = new System.Drawing.Size(317, 26);
            this.textBoxSN.TabIndex = 0;
            this.textBoxSN.TextChanged += new System.EventHandler(this.textBoxSN_TextChanged);
            this.textBoxSN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSN_KeyDown);
            this.textBoxSN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSN_KeyPress);
            // 
            // buttonWrite
            // 
            this.buttonWrite.Font = new System.Drawing.Font("黑体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonWrite.Location = new System.Drawing.Point(425, 326);
            this.buttonWrite.Name = "buttonWrite";
            this.buttonWrite.Size = new System.Drawing.Size(80, 30);
            this.buttonWrite.TabIndex = 1;
            this.buttonWrite.Text = "烧写";
            this.buttonWrite.UseVisualStyleBackColor = true;
            this.buttonWrite.Click += new System.EventHandler(this.buttonWrite_Click);
            // 
            // labelMsg
            // 
            this.labelMsg.AutoSize = true;
            this.labelMsg.Font = new System.Drawing.Font("黑体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelMsg.Location = new System.Drawing.Point(12, 336);
            this.labelMsg.Name = "labelMsg";
            this.labelMsg.Size = new System.Drawing.Size(49, 14);
            this.labelMsg.TabIndex = 2;
            this.labelMsg.Text = "label5";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.OperatorToolStripMenuItem,
            this.OptionToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(517, 25);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileToolStripMenuItem
            // 
            this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitToolStripMenuItem});
            this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
            this.FileToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.FileToolStripMenuItem.Text = "文件";
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitToolStripMenuItem.Text = "退出";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // OperatorToolStripMenuItem
            // 
            this.OperatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.WriteToolStripMenuItem,
            this.PoweroffToolStripMenuItem,
            this.RebootToolStripMenuItem});
            this.OperatorToolStripMenuItem.Name = "OperatorToolStripMenuItem";
            this.OperatorToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.OperatorToolStripMenuItem.Text = "操作";
            // 
            // WriteToolStripMenuItem
            // 
            this.WriteToolStripMenuItem.Enabled = false;
            this.WriteToolStripMenuItem.Name = "WriteToolStripMenuItem";
            this.WriteToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.WriteToolStripMenuItem.Text = "烧写";
            // 
            // PoweroffToolStripMenuItem
            // 
            this.PoweroffToolStripMenuItem.Enabled = false;
            this.PoweroffToolStripMenuItem.Name = "PoweroffToolStripMenuItem";
            this.PoweroffToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.PoweroffToolStripMenuItem.Text = "关机";
            this.PoweroffToolStripMenuItem.Click += new System.EventHandler(this.PoweroffToolStripMenuItem_Click);
            // 
            // RebootToolStripMenuItem
            // 
            this.RebootToolStripMenuItem.Enabled = false;
            this.RebootToolStripMenuItem.Name = "RebootToolStripMenuItem";
            this.RebootToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.RebootToolStripMenuItem.Text = "重启";
            this.RebootToolStripMenuItem.Click += new System.EventHandler(this.RebootToolStripMenuItem_Click);
            // 
            // OptionToolStripMenuItem
            // 
            this.OptionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AutoWriteToolStripMenuItem,
            this.AutoPoweroffToolStripMenuItem,
            this.PrintAfterWriteToolStripMenuItem,
            this.PrinterConfigToolStripMenuItem,
            this.USBConfigToolStripMenuItem});
            this.OptionToolStripMenuItem.Name = "OptionToolStripMenuItem";
            this.OptionToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.OptionToolStripMenuItem.Text = "选项";
            // 
            // AutoWriteToolStripMenuItem
            // 
            this.AutoWriteToolStripMenuItem.CheckOnClick = true;
            this.AutoWriteToolStripMenuItem.Name = "AutoWriteToolStripMenuItem";
            this.AutoWriteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AutoWriteToolStripMenuItem.Text = "自动烧写";
            this.AutoWriteToolStripMenuItem.Click += new System.EventHandler(this.AutoWriteToolStripMenuItem_Click);
            // 
            // AutoPoweroffToolStripMenuItem
            // 
            this.AutoPoweroffToolStripMenuItem.CheckOnClick = true;
            this.AutoPoweroffToolStripMenuItem.Name = "AutoPoweroffToolStripMenuItem";
            this.AutoPoweroffToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.AutoPoweroffToolStripMenuItem.Text = "完成后关机";
            this.AutoPoweroffToolStripMenuItem.Click += new System.EventHandler(this.AutoPoweroffToolStripMenuItem_Click);
            // 
            // PrinterConfigToolStripMenuItem
            // 
            this.PrinterConfigToolStripMenuItem.Name = "PrinterConfigToolStripMenuItem";
            this.PrinterConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PrinterConfigToolStripMenuItem.Text = "打印机配置";
            this.PrinterConfigToolStripMenuItem.Click += new System.EventHandler(this.PrinterConfigToolStripMenuItem_Click);
            // 
            // USBConfigToolStripMenuItem
            // 
            this.USBConfigToolStripMenuItem.Name = "USBConfigToolStripMenuItem";
            this.USBConfigToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.USBConfigToolStripMenuItem.Text = "USB连接参数";
            this.USBConfigToolStripMenuItem.Click += new System.EventHandler(this.USBConfigToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem1});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.HelpToolStripMenuItem.Text = "帮助";
            this.HelpToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // AboutToolStripMenuItem1
            // 
            this.AboutToolStripMenuItem1.Name = "AboutToolStripMenuItem1";
            this.AboutToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.AboutToolStripMenuItem1.Text = "关于";
            this.AboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1_Click);
            // 
            // checkBoxPrint
            // 
            this.checkBoxPrint.AutoSize = true;
            this.checkBoxPrint.Checked = true;
            this.checkBoxPrint.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPrint.Location = new System.Drawing.Point(265, 256);
            this.checkBoxPrint.Name = "checkBoxPrint";
            this.checkBoxPrint.Size = new System.Drawing.Size(82, 18);
            this.checkBoxPrint.TabIndex = 24;
            this.checkBoxPrint.Text = "打印条码";
            this.checkBoxPrint.UseVisualStyleBackColor = true;
            this.checkBoxPrint.CheckedChanged += new System.EventHandler(this.checkBoxPrint_CheckedChanged);
            // 
            // labelPrintCount
            // 
            this.labelPrintCount.AutoSize = true;
            this.labelPrintCount.Location = new System.Drawing.Point(351, 256);
            this.labelPrintCount.Name = "labelPrintCount";
            this.labelPrintCount.Size = new System.Drawing.Size(70, 14);
            this.labelPrintCount.TabIndex = 26;
            this.labelPrintCount.Text = "打印数量:";
            this.labelPrintCount.Click += new System.EventHandler(this.label7_Click);
            // 
            // textBoxPrintCount
            // 
            this.textBoxPrintCount.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPrintCount.Location = new System.Drawing.Point(422, 251);
            this.textBoxPrintCount.Name = "textBoxPrintCount";
            this.textBoxPrintCount.Size = new System.Drawing.Size(22, 23);
            this.textBoxPrintCount.TabIndex = 25;
            this.textBoxPrintCount.Text = "3";
            this.textBoxPrintCount.TextChanged += new System.EventHandler(this.textBoxPrintCount_TextChanged);
            this.textBoxPrintCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPrintCount_KeyPress);
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::IMEI_Reader.Properties.Resources.Lading;
            this.pictureBox5.Location = new System.Drawing.Point(460, 211);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(25, 25);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 22;
            this.pictureBox5.TabStop = false;
            this.pictureBox5.Visible = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::IMEI_Reader.Properties.Resources.Lading;
            this.pictureBox4.Location = new System.Drawing.Point(460, 151);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(25, 25);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 22;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::IMEI_Reader.Properties.Resources.Lading;
            this.pictureBox3.Location = new System.Drawing.Point(460, 91);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(25, 25);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 22;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::IMEI_Reader.Properties.Resources.Lading;
            this.pictureBox2.Location = new System.Drawing.Point(460, 32);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 22;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::IMEI_Reader.Properties.Resources.Lading;
            this.pictureBox1.Location = new System.Drawing.Point(5, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // PrintAfterWriteToolStripMenuItem
            // 
            this.PrintAfterWriteToolStripMenuItem.CheckOnClick = true;
            this.PrintAfterWriteToolStripMenuItem.Name = "PrintAfterWriteToolStripMenuItem";
            this.PrintAfterWriteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.PrintAfterWriteToolStripMenuItem.Text = "打印条码";
            this.PrintAfterWriteToolStripMenuItem.Click += new System.EventHandler(this.PrintAfterWriteToolStripMenuItem_Click);
            // 
            // Writer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 366);
            this.Controls.Add(this.labelMsg);
            this.Controls.Add(this.buttonWrite);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Writer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "串号烧写器";
            this.Load += new System.EventHandler(this.Writer_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Writer_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxBt;
        private System.Windows.Forms.CheckBox checkBoxWifi;
        private System.Windows.Forms.CheckBox checkBoxIMEI;
        private System.Windows.Forms.CheckBox checkBoxSN;
        private System.Windows.Forms.TextBox textBoxBt;
        private System.Windows.Forms.TextBox textBoxWifi;
        private System.Windows.Forms.TextBox textBoxIMEI;
        private System.Windows.Forms.TextBox textBoxSN;
        private System.Windows.Forms.Button buttonWrite;
        private System.Windows.Forms.CheckBox checkBoxAutoWrite;
        private System.Windows.Forms.Label labelMsg;
        private System.Windows.Forms.LinkLabel linkLabelBt;
        private System.Windows.Forms.LinkLabel linkLabelWifi;
        private System.Windows.Forms.LinkLabel linkLabelIMEI;
        private System.Windows.Forms.LinkLabel linkLabelSN;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OperatorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem WriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PoweroffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RebootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem OptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AutoWriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PrinterConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem USBConfigToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxAutoPoweroff;
        private System.Windows.Forms.ToolStripMenuItem AutoPoweroffToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxPrint;
        private System.Windows.Forms.Label labelPrintCount;
        private System.Windows.Forms.TextBox textBoxPrintCount;
        private System.Windows.Forms.ToolStripMenuItem PrintAfterWriteToolStripMenuItem;
    }
}