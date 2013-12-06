using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using System.Threading;
using IMEI_Reader.Properties;

namespace IMEI_Reader
{
    public partial class Writer : Form
    {
        private MessageHandler mHandler;
        private DeviceDetector detetor;
        private int deviceCount;
        private bool isWriting;

        public Writer()
        {
            InitializeComponent();
            mHandler = new MessageHandler(this.HandleMessge);
            detetor = new DeviceDetector(mHandler, this);
            this.UpdateUI();
        }


        public void HandleMessge(int msgId, int requestCode, object obj)
        {
            string errorMsg = null;
            switch (msgId)
            {
                case Messages.MSG_UPDATE_DEVICE_COUNT:
                    this.deviceCount = Convert.ToInt32(obj);

                    this.UpdateUI();
                    break;
                case Messages.MSG_WRITE_START:
                    isWriting = true;
                    panelProgress.Visible = true;
                    checkBoxSN.Enabled = false;
                    checkBoxIMEI.Enabled = false;
                    checkBoxIMEI2.Enabled = false;
                    checkBoxWifi.Enabled = false;
                    checkBoxBt.Enabled = false;
                    buttonWrite.Enabled = false;
                    WriteToolStripMenuItem.Enabled = false;
                    PoweroffToolStripMenuItem.Enabled = false;
                    RebootToolStripMenuItem.Enabled = false;

                    break;
                case Messages.MSG_WRITE_STATE_CHANGE:
                    isWriting = true;
                    if (requestCode > 0)
                    {
                        HandleProgress(requestCode, Convert.ToInt32(obj));
                    }

                    break;
                case Messages.MSG_WRITE_SUCCESS:
                    isWriting = false;
                    panelProgress.Visible = false;
                    checkBoxSN.Enabled = true;
                    checkBoxIMEI.Enabled = true;
                    checkBoxIMEI2.Enabled = true;
                    checkBoxWifi.Enabled = true;
                    checkBoxBt.Enabled = true;
                    buttonWrite.Enabled = true;
                    WriteToolStripMenuItem.Enabled = true;
                    PoweroffToolStripMenuItem.Enabled = true;
                    RebootToolStripMenuItem.Enabled = true;
                    DoActionAfterWrite();
                    break;
                case Messages.MSG_WRITE_FAIL:
                    isWriting = false;
                    panelProgress.Visible = false;
                    checkBoxSN.Enabled = true;
                    checkBoxIMEI.Enabled = true;
                    checkBoxIMEI2.Enabled = true;
                    checkBoxWifi.Enabled = true;
                    checkBoxBt.Enabled = true;
                    buttonWrite.Enabled = true;
                    WriteToolStripMenuItem.Enabled = true;
                    PoweroffToolStripMenuItem.Enabled = true;
                    RebootToolStripMenuItem.Enabled = true;
                    errorMsg = obj.ToString();
                    MessageBox.Show(errorMsg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    break;
            }
        }


        private void HandleProgress(int requestCode, int progress)
        {

            PictureBox pictureBox = null;
            switch (requestCode)
            {
                case CodeType.TYPE_SN:
                    pictureBox = pictureBoxSN;
                    if (progress == 1)
                    {
                        Settings.Default.SN_CUR = Util.GetNextSN(textBoxSN.Text.Trim());
                        Settings.Default.Save();
                    }

                    break;
                case CodeType.TYPE_IMEI:
                    pictureBox = pictureBoxIMEI;
                    if (progress == 1)
                    {
                        Settings.Default.IMEI_CUR = Util.GetNextIMEI(textBoxIMEI.Text.Trim());
                        Settings.Default.Save();
                    }
                    break;
                case CodeType.TYPE_IMEI2:
                    pictureBox = pictureBoxIMEI2;
                    if (progress == 1)
                    {
                        Settings.Default.IMEI2_CUR = Util.GetNextIMEI(textBoxIMEI2.Text.Trim());
                        Settings.Default.Save();
                    }
                    break;
                case CodeType.TYPE_WIFI_MAC:
                    pictureBox = pictureBoxWIFI;
                    if (progress == 1)
                    {
                        Settings.Default.WIFI_CUR= Util.GetNextMAC(textBoxWifi.Text.Trim());
                        Settings.Default.Save();
                    }
                    break;
                case CodeType.TYPE_BT_MAC:
                    pictureBox = pictureBoxBT;
                    if (progress == 1)
                    {
                        Settings.Default.BT_CUR = Util.GetNextMAC(textBoxBt.Text.Trim());
                        Settings.Default.Save();
                    }
                    break;
            }
            if (pictureBox != null)
            {
                pictureBox.Visible = true;
                if (progress == 0)
                {
                    pictureBox.Image = Resources.Lading;
                }
                else if (progress == 1)
                {
                    pictureBox.Image = Resources.Tick;
                }
                else if (progress == 2)
                {
                    pictureBox.Image = Resources.Error;
                }
                pictureBox.Enabled = (progress == 2);

            }
           

        }

        private void UpdateUI()
        {
            updateInputBox();
            if (deviceCount <= 0)
            {
                labelMsg.Text = "请连接设备";
                labelMsg.ForeColor = Color.Red;
               
            }
            else if (deviceCount == 1)
            {
                labelMsg.Text = "设备已连接";
                labelMsg.ForeColor = Color.Green;
                
                if (checkBoxAutoWrite.Checked && !isWriting)
                {
                    CheckAndStartWrite();
                }
            }
            else
            {
                labelMsg.Text = "连接设备过多";
                labelMsg.ForeColor = Color.Red;
            }
            buttonWrite.Enabled = (deviceCount == 1);
            WriteToolStripMenuItem.Enabled = (deviceCount == 1);
            PoweroffToolStripMenuItem.Enabled = (deviceCount == 1);
            RebootToolStripMenuItem.Enabled = (deviceCount == 1);

            pictureBoxSN.Visible = false;
            pictureBoxIMEI.Visible = false;
            pictureBoxIMEI2.Visible = false;
            pictureBoxWIFI.Visible = false;
            pictureBoxBT.Visible = false;

        }

        private void textBoxSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxIMEI.Enabled && !textBoxIMEI.ReadOnly)
                {
                    textBoxIMEI.Focus();
                }
                else if (textBoxIMEI2.Enabled && !textBoxIMEI2.ReadOnly)
                {
                    textBoxIMEI2.Focus();
                }
                else if (textBoxWifi.Enabled && !textBoxWifi.ReadOnly)
                {
                    textBoxWifi.Focus();
                }
                else if (textBoxBt.Enabled && !textBoxBt.ReadOnly)
                {
                    textBoxBt.Focus();
                }
                else
                {
                    buttonWrite.Focus();
                }


            }
        }

        private void textBoxIMEI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxIMEI2.Enabled && !textBoxIMEI2.ReadOnly)
                {
                    textBoxIMEI2.Focus();
                }
                else if (textBoxWifi.Enabled && !textBoxWifi.ReadOnly)
                {
                    textBoxWifi.Focus();
                }
                else if (textBoxBt.Enabled && !textBoxBt.ReadOnly)
                {
                    textBoxBt.Focus();
                }
                else
                {
                    buttonWrite.Focus();
                }
            }
        }

        private void textBoxIMEI2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxWifi.Enabled && !textBoxWifi.ReadOnly)
                {
                    textBoxWifi.Focus();
                }
                else if (textBoxBt.Enabled && !textBoxBt.ReadOnly)
                {
                    textBoxBt.Focus();
                }
                else
                {
                    buttonWrite.Focus();
                }
            }
        }

        private void textBoxWifi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxBt.Enabled && !textBoxBt.ReadOnly)
                {
                    textBoxBt.Focus();
                }
                else
                {
                    buttonWrite.Focus();
                }
            }
        }

        private void textBoxBt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonWrite.Focus();
            }
        }



        private void CheckAndStartWrite()
        {
            isWriting = true;
            List<KeyValuePair<int, string>> codes = new List<KeyValuePair<int, string>>();
            int count = Settings.Default.PrintCount;
            if (checkBoxSN.Checked)
            {
                if (checkBoxScan.Checked)
                {
                    InputDialog dialog = new InputDialog(CodeType.TYPE_SN);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxSN.Text = dialog.Input;
                    }
                    else
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(textBoxSN.Text.Trim()))
                {
                    goto ERROR;
                }
                codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_SN, textBoxSN.Text.Trim()));
            }

            if (checkBoxIMEI.Checked)
            {
                if (checkBoxScan.Checked)
                {
                    InputDialog dialog = new InputDialog(CodeType.TYPE_IMEI);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxIMEI.Text = dialog.Input;
                    }
                    else
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(textBoxIMEI.Text.Trim()))
                {
                    goto ERROR;
                }
                codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_IMEI, textBoxIMEI.Text.Trim()));
            }
            if (checkBoxIMEI2.Checked)
            {
                if (checkBoxScan.Checked)
                {
                    InputDialog dialog = new InputDialog(CodeType.TYPE_IMEI2);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxIMEI2.Text = dialog.Input;
                    }
                    else
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(textBoxIMEI2.Text.Trim()))
                {
                    goto ERROR;
                }
                codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_IMEI2, textBoxIMEI2.Text.Trim()));

            }
            if (checkBoxWifi.Checked)
            {
                if (checkBoxScan.Checked)
                {
                    InputDialog dialog = new InputDialog(CodeType.TYPE_WIFI_MAC);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxWifi.Text = dialog.Input;
                    }
                    else
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(textBoxWifi.Text.Trim()))
                {
                    goto ERROR;
                }
                codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_WIFI_MAC, Util.GetMACToWrite(textBoxWifi.Text.Trim())));

            }

            if (checkBoxBt.Checked)
            {
                if (checkBoxScan.Checked)
                {
                    InputDialog dialog = new InputDialog(CodeType.TYPE_BT_MAC);
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        textBoxBt.Text = dialog.Input;
                    }
                    else
                    {
                        return;
                    }
                }
                if (string.IsNullOrEmpty(textBoxBt.Text.Trim()))
                {
                    goto ERROR;
                }
                codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_BT_MAC, Util.GetMACToWrite(textBoxBt.Text.Trim())));
            }
            pictureBoxSN.Visible = false;
            pictureBoxIMEI.Visible = false;
            pictureBoxIMEI2.Visible = false;
            pictureBoxWIFI.Visible = false;
            pictureBoxBT.Visible = false;

            Write(codes);
            return;

        ERROR:
            {
                MessageBox.Show("请确保选择的项中包含有效的数据！", "无效的数据", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isWriting = false;
            }
        }


        private void Write(List<KeyValuePair<int, string>> codes)
        {

            CmdExecutor executor = CmdExecutor.GetCmdExecutor(Settings.Default.DevicePlatform,mHandler,this);
            Thread thread = new Thread(new ParameterizedThreadStart(executor.StartExcuteWriteCmd));
            thread.Start(codes);

        }

        private void DoActionAfterWrite()
        {
            int action = checkBoxAutoPoweroff.Checked ? 1 : 0;

            switch (action)
            {
                case 1:
                    {
                        CmdExecutor executor = CmdExecutor.GetCmdExecutor(Settings.Default.DevicePlatform, mHandler, this);
                        Thread thread = new Thread(new ParameterizedThreadStart(executor.StartPowerOff));
                        thread.Start(null);
                    }
                    break;
                case 2:
                    {
                        CmdExecutor executor = CmdExecutor.GetCmdExecutor(Settings.Default.DevicePlatform, mHandler, this);
                        Thread thread = new Thread(new ParameterizedThreadStart(executor.StartReboot));
                        thread.Start("");
                    }
                    break;
                default:
                    return;
            }

        }

        private void checkBoxSN_CheckedChanged(object sender, EventArgs e)
        {
            textBoxSN.Enabled = checkBoxSN.Checked;
            textBoxSN.BackColor = checkBoxSN.Checked ? Color.White : System.Drawing.SystemColors.Control;
            //linkLabelSN.Visible = checkBoxSN.Checked;
            if (!checkBoxSN.Checked)
            {
                pictureBoxSN.Visible = false;
            }
            Settings.Default.WriteSNChecked = checkBoxSN.Checked;
            Settings.Default.Save();
        }

        private void checkBoxIMEI_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIMEI.Enabled = checkBoxIMEI.Checked;
            textBoxIMEI.BackColor = checkBoxIMEI.Checked ? Color.White : System.Drawing.SystemColors.Control;
            //linkLabelIMEI.Visible = checkBoxIMEI.Checked;
            if (!checkBoxIMEI.Checked)
            {
                pictureBoxIMEI.Visible = false;
            }
            Settings.Default.WriteIMEIChecked = checkBoxIMEI.Checked;
            Settings.Default.Save();
        }


        private void checkBoxIMEI2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxIMEI2.Enabled = checkBoxIMEI2.Checked;
            textBoxIMEI2.BackColor = checkBoxIMEI2.Checked ? Color.White : System.Drawing.SystemColors.Control;
            //linkLabelIMEI2.Visible = checkBoxIMEI2.Checked;
            if (!checkBoxIMEI2.Checked)
            {
                pictureBoxIMEI2.Visible = false;
            }
            Settings.Default.WriteIMEI2Checked = checkBoxIMEI2.Checked;
            Settings.Default.Save();
        }


        private void checkBoxWifi_CheckedChanged(object sender, EventArgs e)
        {
            textBoxWifi.Enabled = checkBoxWifi.Checked;
            textBoxWifi.BackColor = checkBoxWifi.Checked ? Color.White : System.Drawing.SystemColors.Control;
            //linkLabelWifi.Visible = checkBoxWifi.Checked;
            if (!checkBoxWifi.Checked)
            {
                pictureBoxWIFI.Visible = false;
            }
            Settings.Default.WriteWIFIChecked = checkBoxWifi.Checked;
            Settings.Default.Save();
        }

        private void checkBoxBt_CheckedChanged(object sender, EventArgs e)
        {
            textBoxBt.Enabled = checkBoxBt.Checked;
            textBoxBt.BackColor = checkBoxBt.Checked ? Color.White : System.Drawing.SystemColors.Control;
            //linkLabelBt.Visible = checkBoxBt.Checked;
            if (!checkBoxBt.Checked)
            {
                pictureBoxBT.Visible = false;
            }
            Settings.Default.WriteBTChecked = checkBoxBt.Checked;
            Settings.Default.Save();
        }

        private void Writer_FormClosed(object sender, FormClosedEventArgs e)
        {
            detetor.RemoveUSBEventWatcher();
            CmdExecutor.CleanUpAdbProcess();
            this.Dispose();
            Environment.Exit(0);
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int tag = Convert.ToInt32(((LinkLabel)sender).Tag);

            WriterConfig config = new WriterConfig();
            config.Top = this.Top + 70;
            config.Left = this.Left + 60;
            config.Tag = tag;
            if (config.ShowDialog() == DialogResult.OK)
            {
                this.updateInputBox();
            }

            setFocus();
        }


        private void updateInputBox()
        {
            textBoxSN.Text = Settings.Default.SN_CUR;
            textBoxIMEI.Text =  Settings.Default.IMEI_CUR;
            textBoxIMEI2.Text = Settings.Default.IMEI2_CUR;
            textBoxWifi.Text = Settings.Default.WIFI_CUR;
            textBoxBt.Text =  Settings.Default.BT_CUR;
        }


        private void buttonWrite_Click(object sender, EventArgs e)
        {

            this.CheckAndStartWrite();
        }

        private void Writer_Load(object sender, EventArgs e)
        {
            checkBoxSN.Checked = Settings.Default.WriteSNChecked;
            this.checkBoxIMEI.Checked = Settings.Default.DevicePlatform == 0 && Settings.Default.WriteIMEIChecked;
            this.checkBoxIMEI.Enabled = Settings.Default.DevicePlatform == 0;
            this.checkBoxIMEI2.Checked = Settings.Default.DevicePlatform == 0 && Settings.Default.WriteIMEI2Checked;
            this.checkBoxIMEI2.Enabled = Settings.Default.DevicePlatform == 0;
            checkBoxWifi.Checked = Settings.Default.WriteWIFIChecked;
            checkBoxBt.Checked = Settings.Default.WriteBTChecked;
            checkBoxScan.Checked = Settings.Default.ScanInput;
            checkBoxPrint.Checked = Settings.Default.PrintAfterWrite;
            checkBoxAutoPoweroff.Checked = Settings.Default.AutoPoweroff;
            AutoPoweroffToolStripMenuItem.Checked = Settings.Default.AutoPoweroff;
            checkBoxAutoWrite.Checked = Settings.Default.AutoWrite;
            AutoWriteToolStripMenuItem.Checked = Settings.Default.AutoWrite;
            PrintAfterWriteToolStripMenuItem.Checked = Settings.Default.PrintAfterWrite;
            textBoxPrintCount.Text = Settings.Default.PrintCount.ToString();
            pictureBoxSN.Tag = CodeType.TYPE_SN;
            pictureBoxIMEI.Tag = CodeType.TYPE_IMEI;
            pictureBoxIMEI2.Tag = CodeType.TYPE_IMEI2;
            pictureBoxWIFI.Tag = CodeType.TYPE_WIFI_MAC;
            pictureBoxBT.Tag = CodeType.TYPE_BT_MAC;



            updateInputBox();
        }

        private void textBoxSN_TextChanged(object sender, EventArgs e)
        {
            bool valid = textBoxSN.Text.Trim().Length == 0 || Util.IsValidSN(textBoxSN.Text.Trim());

            textBoxSN.BackColor = valid ? Color.White : Color.Red;

            textBoxSN.ForeColor = valid ? System.Drawing.Color.RoyalBlue : Color.Silver;
        }

        private void textBoxSN_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar != '\b' && e.KeyChar != 22 && e.KeyChar != 3)
            {

                e.Handled = !Char.IsLetterOrDigit(e.KeyChar);

            }
        }

        private void textBoxIMEI_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            bool valid = textBox.Text.Trim().Length == 0 || Util.IsValidIMEI(textBox.Text.Trim());

            textBox.BackColor = valid ? Color.White : Color.Red;

            textBox.ForeColor = valid ? System.Drawing.Color.RoyalBlue : Color.Silver;
        }

        private void textBoxMac_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            String text = textBox.Text.Trim().ToUpper().Replace(":", "").Replace("-", "");
            if (text.Length > 12)
            {
                text = text.Substring(0, 12);
            }

            textBox.Text = text;


            bool valid = text.Length == 0 || Util.IsValidMAC(text);

            textBox.BackColor = valid ? Color.White : Color.Red;

            textBox.ForeColor = valid ? System.Drawing.Color.RoyalBlue : Color.Silver;

        }

        private void textBoxWifi_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == '\b' || e.KeyChar == 22 || e.KeyChar == 3 || e.KeyChar == 24)
            {
                return;
            }
            else if (textBox.Text.Length >= 12)
            {
                e.Handled = true;
            }
            else
            {

                e.Handled = "0123456789ABCDEF".IndexOf(char.ToUpper(e.KeyChar)) < 0;


            }
        }


        private void checkBoxPoweroff_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AutoPoweroff = checkBoxAutoPoweroff.Checked;
            AutoPoweroffToolStripMenuItem.Checked = checkBoxAutoPoweroff.Checked;
            Settings.Default.Save();
        }


        private void checkBoxAutoWrite_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AutoWrite = checkBoxAutoWrite.Checked;
            AutoWriteToolStripMenuItem.Checked = checkBoxAutoWrite.Checked;
            Settings.Default.Save();
        }
        private void PoweroffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CmdExecutor executor = CmdExecutor.GetCmdExecutor(Settings.Default.DevicePlatform, mHandler, this);
            Thread thread = new Thread(new ParameterizedThreadStart(executor.StartPowerOff));
            thread.Start(null);
        }

        private void RebootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CmdExecutor executor = CmdExecutor.GetCmdExecutor(Settings.Default.DevicePlatform, mHandler, this);
            Thread thread = new Thread(new ParameterizedThreadStart(executor.StartReboot));
            thread.Start("");
        }

        private void AutoWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.AutoWrite = AutoWriteToolStripMenuItem.Checked;
            checkBoxAutoWrite.Checked = AutoWriteToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void AutoPoweroffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.AutoPoweroff = AutoPoweroffToolStripMenuItem.Checked;
            checkBoxAutoPoweroff.Checked = AutoPoweroffToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void PrinterConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrinterConfig config = new PrinterConfig();
            config.ShowDialog();
        }

        private void USBConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsbConfig config = new UsbConfig();
            config.StartPosition = FormStartPosition.CenterParent;

            if (config.ShowDialog() == DialogResult.OK)
            {
                this.checkBoxIMEI.Checked = Settings.Default.DevicePlatform == 0 && Settings.Default.IMEI_Selected;
                this.checkBoxIMEI.Enabled = Settings.Default.DevicePlatform == 0;
                textBoxIMEI.Enabled = checkBoxIMEI.Checked;
                textBoxIMEI.BackColor = checkBoxIMEI.Checked ? Color.White : System.Drawing.SystemColors.Control;

                this.checkBoxIMEI2.Checked = Settings.Default.DevicePlatform == 0 && Settings.Default.IMEI2_Selected;
                this.checkBoxIMEI2.Enabled = Settings.Default.DevicePlatform == 0;
                textBoxIMEI2.Enabled = checkBoxIMEI2.Checked;
                textBoxIMEI2.BackColor = checkBoxIMEI2.Checked ? Color.White : System.Drawing.SystemColors.Control;
                detetor.updateDeviceCount();

            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void checkBoxPrint_CheckedChanged(object sender, EventArgs e)
        {
            labelPrintCount.Visible = checkBoxPrint.Checked;
            textBoxPrintCount.Visible = checkBoxPrint.Checked;

            Settings.Default.PrintAfterWrite = checkBoxPrint.Checked;
            PrintAfterWriteToolStripMenuItem.Checked = checkBoxPrint.Checked;
            Settings.Default.Save();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.ShowDialog();
        }

        private void PrintAfterWriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.PrintAfterWrite = PrintAfterWriteToolStripMenuItem.Checked;
            checkBoxPrint.Checked = PrintAfterWriteToolStripMenuItem.Checked;
            Settings.Default.Save();
        }


        private void textBoxPrintCount_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPrintCount.Text.Length > 0)
            {
                Settings.Default.PrintCount = Convert.ToInt32(textBoxPrintCount.Text);
                Settings.Default.Save();
            }

        }

        private void textBoxPrintCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            int tag = Convert.ToInt32(((PictureBox)sender).Tag);

            List<KeyValuePair<int, string>> codes = new List<KeyValuePair<int, string>>();

            switch (tag)
            {
                case CodeType.TYPE_SN:
                    codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_SN, textBoxSN.Text.Trim()));
                    break;
                case CodeType.TYPE_IMEI:
                    codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_IMEI, textBoxIMEI.Text.Trim()));
                    break;
                case CodeType.TYPE_IMEI2:
                    codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_IMEI2, textBoxIMEI2.Text.Trim()));
                    break;
                case CodeType.TYPE_WIFI_MAC:
                    codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_WIFI_MAC, Util.GetMACToWrite(textBoxWifi.Text.Trim())));
                    break;
                case CodeType.TYPE_BT_MAC:
                    codes.Add(new KeyValuePair<int, string>(CodeType.TYPE_BT_MAC, Util.GetMACToWrite(textBoxBt.Text.Trim())));
                    break;
            }
            if (codes.Count > 0)
            {

                Write(codes);
            }

        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip((Control)sender, "点击重新烧写");
        }

        private void Writer_Activated(object sender, EventArgs e)
        {
            setFocus();
        }

        private void setFocus()
        {
            if (!textBoxSN.ReadOnly && textBoxSN.Enabled)
            {
                textBoxSN.Focus();
            }
            else if (!textBoxIMEI.ReadOnly && textBoxIMEI.Enabled)
            {
                textBoxIMEI.Focus();
            }
            else if (!textBoxIMEI2.ReadOnly && textBoxIMEI2.Enabled)
            {
                textBoxIMEI2.Focus();
            }
            else if (!textBoxWifi.ReadOnly && textBoxWifi.Enabled)
            {
                textBoxWifi.Focus();
            }
            else if (!textBoxBt.ReadOnly && textBoxBt.Enabled)
            {
                textBoxBt.Focus();
            }
        }

        private void checkBoxScan_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.ScanInput = checkBoxScan.Checked;
            Settings.Default.Save();
            updateInputBox();
        }




    }
}
