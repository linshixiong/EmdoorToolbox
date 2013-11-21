using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Common;
using IMEI_Reader.Properties;

namespace IMEI_Reader
{
    public partial class WriterConfig : Form
    {

        private int source ;
        private int inputType;

        public WriterConfig()
        {
            InitializeComponent();

          
        }

        private void WriterConfig_Load(object sender, EventArgs e)
        {
            this.source = Convert.ToInt32(this.Tag);
            this.inputType = getInputType(source);

            initUI();
        }

        private void initUI()
        {
            int maxLength = 0;
            string title = "";
            switch (source)
            {
                case CodeType.TYPE_SN:
                    maxLength = 32;
                    title = "SN";
                    break;
                case CodeType.TYPE_IMEI:
                    title = "IMEI";
                    maxLength = 15;
                    break;
                case CodeType.TYPE_IMEI2:
                    title = "IMEI2";
                    maxLength = 15;
                    break;
                case CodeType.TYPE_WIFI_MAC:
                    maxLength = 12;
                    title = "WIFI地址";
                    break;
                case CodeType.TYPE_BT_MAC:
                    maxLength = 12;
                    title = "蓝牙地址";
                    break;

                default:
                    maxLength = 0;
                    break;
            }

            textBoxMin.MaxLength = maxLength;
            textBoxMax.MaxLength = maxLength;
            textBoxCurrent.MaxLength = maxLength;
            this.Text = title + this.Text;
            switch (inputType)
            {

                case 0:
                    radioButton_0.Checked = true;
                    break;
                case 1:
                    radioButton_1.Checked = true;

                    break;
                case 2:
                    radioButton_2.Checked = true;
                    break;
                default:
                    radioButton_0.Checked = true;
                    break;
            }

        }

        private string[] getSavedData()
        {
            string [] data=new string[3];
            switch (source)
            {
                case CodeType.TYPE_SN:
                    data[0] = Settings.Default.SN_MIN;
                    data[1] = Settings.Default.SN_MAX;
                    data[2] = Settings.Default.SN_CUR;
                    break;
                case CodeType.TYPE_IMEI:
                    data[0] = Settings.Default.IMEI_MIN;
                    data[1] = Settings.Default.IMEI_MAX;
                    data[2] = Settings.Default.IMEI_CUR;
                    break;
                case CodeType.TYPE_IMEI2:
                    data[0] = Settings.Default.IMEI2_MIN;
                    data[1] = Settings.Default.IMEI2_MAX;
                    data[2] = Settings.Default.IMEI2_CUR;
                    break;
                case CodeType.TYPE_WIFI_MAC:
                    data[0] = Settings.Default.WIFI_MIN;
                    data[1] = Settings.Default.WIFI_MAX;
                    data[2] = Settings.Default.WIFI_CUR;
                    break;
                case CodeType.TYPE_BT_MAC:
                    data[0] = Settings.Default.BT_MIN;
                    data[1] = Settings.Default.BT_MAX;
                    data[2] = Settings.Default.BT_CUR;
                    break;
                default:
                    break;
            }
            return data;
        }

        private void saveData(string min, string max, string current)
        {
            switch (source)
            {
                case CodeType.TYPE_SN:
                    Settings.Default.SN_MIN = min;
                    Settings.Default.SN_MAX = max;
                    Settings.Default.SN_CUR = current;
                    break;
                case CodeType.TYPE_IMEI:
                    Settings.Default.IMEI_MIN = min;
                    Settings.Default.IMEI_MAX = max;
                    Settings.Default.IMEI_CUR = current;
                    break;
                case CodeType.TYPE_IMEI2:
                    Settings.Default.IMEI2_MIN = min;
                    Settings.Default.IMEI2_MAX = max;
                    Settings.Default.IMEI2_CUR = current;
                    break;
                case CodeType.TYPE_WIFI_MAC:
                    Settings.Default.WIFI_MIN = min;
                    Settings.Default.WIFI_MAX = max;
                    Settings.Default.WIFI_CUR = current;
                    break;
                case CodeType.TYPE_BT_MAC:
                    Settings.Default.BT_MIN = min;
                    Settings.Default.BT_MAX = max;
                    Settings.Default.BT_CUR = current;
                    break;
                default:
                    break;
            }
            Settings.Default.Save();
        }

        private int getInputType(int source)
        {
            switch (source)
            {
                case CodeType.TYPE_SN:
                    return Settings.Default.InputSNType;
                case CodeType.TYPE_IMEI:
                    return Settings.Default.InputIMEIType;
                case CodeType.TYPE_IMEI2:
                    return Settings.Default.InputIMEI2Type;
                case CodeType.TYPE_WIFI_MAC:
                    return Settings.Default.InputWIFIType;
                case CodeType.TYPE_BT_MAC:
                    return Settings.Default.InputBTType;
                default:
                    return Settings.Default.InputSNType;
            }
        }

        private void setInputType(int source, int inputType)
        {
            switch (source)
            {
                case CodeType.TYPE_SN:
                     Settings.Default.InputSNType=inputType;
                     break;
                case CodeType.TYPE_IMEI:
                      Settings.Default.InputIMEIType = inputType;
                      break;
                case CodeType.TYPE_IMEI2:
                      Settings.Default.InputIMEI2Type = inputType;
                      break;
                case CodeType.TYPE_WIFI_MAC:
                      Settings.Default.InputWIFIType = inputType;
                      break;
                case CodeType.TYPE_BT_MAC:
                      Settings.Default.InputBTType = inputType;
                      break;
                default:
                      //Settings.Default.InputSNType = inputType;
                      break;
                     

            }
            Settings.Default.Save();
        }


        private void buttonCancel_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            setInputType(source, inputType);
            saveData(textBoxMin.Text.Trim(), textBoxMax.Text.Trim(), textBoxCurrent.Text.Trim());
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            int maxLength = 0;

            if (source == CodeType.TYPE_IMEI||source==CodeType.TYPE_IMEI2)
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
                if (source == CodeType.TYPE_IMEI||source==CodeType.TYPE_IMEI2)
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


        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                this.inputType = Convert.ToInt32(radioButton.Tag);

                groupBox1.Visible = inputType == 1;
                groupBox2.Visible = inputType == 2;
                if (inputType == 0)
                {
                    buttonSave.Enabled = true;
                }
                else if (inputType == 1)
                {
                    buttonSave.Enabled = false;

                    string[] data = getSavedData();
                    textBoxMin.Text = data[0];
                    textBoxMax.Text = data[1];
                    textBoxCurrent.Text = data[2];

                }


            }
        }

        private void textBoxMin_TextChanged(object sender, EventArgs e)
        {
           
           textBoxCurrent.Text= textBoxMin.Text;
           checkInput((TextBox)sender);

        }

        private void textBoxMax_TextChanged(object sender, EventArgs e)
        {
            checkInput((TextBox)sender);
        }

        private void textBoxCurrent_TextChanged(object sender, EventArgs e)
        {
            checkInput((TextBox)sender);
        }

        private void checkInput(TextBox textBox)
        {
            bool valid = true;

            int tag = Convert.ToInt32(textBox.Tag);
            PictureBox pictureBox=null;
            switch (tag)
            {
                case 1:
                    pictureBox= pictureBoxMin ;
                    break;
                case 2:
                    pictureBox = pictureBoxMax;
                    break;
                case 3:
                    pictureBox = pictureBoxCurrent;
                    break;
            }

            if (textBox.Text.Trim().Length > 0)
            {
                switch (source)
                {
                    case CodeType.TYPE_SN:
                        valid = Util.IsValidSN(textBox.Text.Trim());
                        break;
                    case CodeType.TYPE_IMEI:
                    case  CodeType.TYPE_IMEI2:
                        valid = Util.IsValidIMEI(textBox.Text.Trim());
                        break;
                    case CodeType.TYPE_WIFI_MAC:
                    case CodeType.TYPE_BT_MAC:
                        valid = Util.IsValidMAC(textBox.Text.Trim());
                        break;
                }
               
                pictureBox.Image = valid ? Resources.Tick : Resources.Error;
                pictureBox.Tag = valid;
                
            }
            else
            {
                pictureBox.Image = Resources.Warning;
                pictureBox.Tag = false;
            }



            if (Convert.ToBoolean(pictureBoxMin.Tag) && Convert.ToBoolean(pictureBoxMax.Tag) && Convert.ToBoolean(pictureBoxCurrent.Tag) )
            {
                buttonSave.Enabled = true;
            }
            else
            {
                buttonSave.Enabled = false;
            }
           

        }

    }
}
