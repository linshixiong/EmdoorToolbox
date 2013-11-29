using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;

namespace IMEI_Reader
{
    public partial class InputDialog : Form
    {
        private int source;

        public InputDialog(int source)
        {
            this.source = source;
            InitializeComponent();

            switch (source)
            {
                case CodeType.TYPE_SN:
                   
                    this.Text = "SN";
                    this.textBoxInput.MaxLength = 32;
                    break;
                case CodeType.TYPE_IMEI:
                    this.Text = "IMEI";
                    this.textBoxInput.MaxLength = 15;
                    break;
                case CodeType.TYPE_IMEI2:
                    this.Text = "IMEI2";
                    this.textBoxInput.MaxLength = 15;
                    break;
                case CodeType.TYPE_WIFI_MAC:
                    this.Text = "WIFI地址";
                    this.textBoxInput.MaxLength = 12;
                    break;
                case CodeType.TYPE_BT_MAC:

                    this.Text = "蓝牙地址";
                    this.textBoxInput.MaxLength = 12;
                    break;

                default:
 
                    break;
            }

            this.groupBoxTitle.Text = this.Text;
        }

        public String Input
        {
            get
            {
                return textBoxInput.Text;
            }
        }

        private void InputDialog_Activated(object sender, EventArgs e)
        {
            this.textBoxInput.Focus();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            bool valid = false;
            TextBox textBox = (TextBox)sender;
            int tag = Convert.ToInt32(textBox.Tag);

            if (textBox.Text.Trim().Length > 0)
            {
                switch (source)
                {
                    case CodeType.TYPE_SN:
                        valid = Util.IsValidSN(textBox.Text.Trim());
                        break;
                    case CodeType.TYPE_IMEI:
                    case CodeType.TYPE_IMEI2:
                        valid = Util.IsValidIMEI(textBox.Text.Trim());
                        break;
                    case CodeType.TYPE_WIFI_MAC:
                    case CodeType.TYPE_BT_MAC:
                        valid = Util.IsValidMAC(textBox.Text.Trim());
                        break;
                }

                textBox.BackColor = valid ? Color.White : Color.Red;
            }
            else
            {
                textBox.BackColor = Color.White;
            }
            
            buttonOK.Enabled = valid;
        }


        private void textBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            

            TextBox textBox = (TextBox)sender;

            int maxLength = 0;

            if (source == CodeType.TYPE_IMEI || source == CodeType.TYPE_IMEI2)
            {
                maxLength = 15;
            }
            else if (source == CodeType.TYPE_SN)
            {
                maxLength = 32;
            }
            else
            {
                maxLength = 12;
            }


            if (textBox.Text.Length >= maxLength && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

            else if (e.KeyChar != '\b')
            {
                if (source == CodeType.TYPE_IMEI || source == CodeType.TYPE_IMEI2)
                {
                    e.Handled = !Char.IsDigit(e.KeyChar);
                }
                else if (source == CodeType.TYPE_SN)
                {
                    e.Handled = !Char.IsLetterOrDigit(e.KeyChar);
                }
                else
                {
                    e.Handled = "0123456789ABCDEF".IndexOf(char.ToUpper(e.KeyChar)) < 0;
                }

            }

        }

        private void textBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter&&buttonOK.Enabled )
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
