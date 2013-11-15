using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OTATools.Resources;
using System.Globalization;
using System.Threading;


namespace OTATools
{
    public partial class Login : Form
    {

        private MessageHandler mHandler;
        private  const int SCREEN_LOGIN=0;
        private  const int SCREEN_PROGRESS=1;
        private OTAManager ota;
        private bool isCanceled;

        public Login()
        {
            InitializeComponent();
            Init();
            mHandler = new MessageHandler(this.HandleMessge);
            ota = new OTAManager(mHandler, this);
        }

        private void Init()
        {

            cbRemember.Checked = Settings.Default.remember_login;
            if (cbRemember.Checked)
            {
                textServerHost.Text = Settings.Default.host;
                textPort.Text = Settings.Default.port.ToString();
                textUserName.Text = Settings.Default.user;
                textPassword.Text = Settings.Default.pwd;
            }
            else
            {
                textPort.Text = "21";
            }

        }

        public void HandleMessge(int msgId, object obj)
        {
            if (isCanceled)
            {
                return;
            }
            switch (msgId)
            {
                case Messages.MSG_LOGIN_START:
                    labelStatus.Text =Strings.logging;
                    SwitchScreen(SCREEN_PROGRESS);
                    break;
                case Messages.MSG_LOGIN_SUCCESS:

                    ota.LoadDeviceList();
                    break;
                case Messages.MSG_LOGIN_FAIL:
                    OnLoginFail(obj.ToString());
                    break;
                case Messages.MSG_GET_DEVICES_START:
                    labelStatus.Text = Strings.getting_devices;
                    break;
                case Messages.MSG_GET_DEVICES_SUCCESS:
                    ShowMainScreen();
                    break;
                case Messages.MSG_GET_DEVICES_FAIL:
                    OnLoginFail(obj.ToString());
                    break;
                default: 
                    break;
            }
        }

        private void SaveConfig()
        {
            Settings.Default.remember_login = cbRemember.Checked;

            if (cbRemember.Checked)
            {
                Settings.Default.host = textServerHost.Text.Trim();
                Settings.Default.port = Convert.ToInt32(textPort.Text.Trim());
                Settings.Default.user = textUserName.Text.Trim();
                Settings.Default.pwd = textPassword.Text;

            }
            Settings.Default.Save();
        }

        private void SwitchScreen(int screen)
        {
            switch (screen)
            {
                case SCREEN_LOGIN:
                    panelLogin.Visible = true;
                    panelProgress.Visible = false;
                    break;
                case SCREEN_PROGRESS:
                    panelLogin.Visible = false;
                    panelProgress.Visible = true;
                    break;
                default:
                    panelLogin.Visible = true;
                    panelProgress.Visible = false;
                    break;
            }
        }

        private void ShowMainScreen()
        {
            Main ota = new Main();
            this.Hide();
            ota.Show();
           
            
        }

        private void OnLoginFail(String msg)
        {
            if (!isCanceled)
            {
                SwitchScreen(SCREEN_LOGIN);
                this.Hide();

                MessageBox.Show(msg, Resources.Strings.loginFail, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                this.Show();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            SaveConfig();
            isCanceled = false;
            int port = Convert.ToInt32(textPort.Text.Trim());
           
            ota.Login(textServerHost.Text.Trim(),port,textUserName.Text.Trim(),textPassword.Text);
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLoginCancel_Click(object sender, EventArgs e)
        {
            isCanceled = true;
            SwitchScreen(SCREEN_LOGIN);
        }

        private void textPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textInput_TextChanged(object sender, EventArgs e)
        {
            if (textServerHost.Text.Trim().Length > 0 && textPort.Text.Trim().Length > 0 && textUserName.Text.Trim().Length > 0 && textPassword.Text.Length > 0)
            {
                btnLogin.Enabled = true;
            }
            else
            {
                btnLogin.Enabled = false;
            }
        }

    }
}
