using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OTATools
{
    public partial class ExportConfig : Form
    {
        private string modelId;

        public ExportConfig(string modelId)
        {
            InitializeComponent();
            this.modelId = modelId;
          
        }

        public ExportConfig()
        {
            InitializeComponent();
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ExportConfig_Load(object sender, EventArgs e)
        {
            textBoxHost.Text = FtpHelperNew.Instance.Host;
            textBoxPort.Text = FtpHelperNew.Instance.Port.ToString();
            textBoxPassword.Text = FtpHelperNew.Instance.Password;
            textBoxUserName.Text = FtpHelperNew.Instance.UserName;
            textBoxModelId.Text = this.modelId;
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = Resources.Strings.fileFilter;
            file.AddExtension = true;
            file.DefaultExt = "cfg";
            file.FileName = "ota.cfg";
            if (file.ShowDialog() == DialogResult.OK)
            {
                Configurator.Instance.SetConfig(Configurator.CONFIG_KEY_SERVER_HOST, textBoxHost.Text);
                Configurator.Instance.SetConfig(Configurator.CONFIG_KEY_SERVER_PORT, textBoxPort.Text);
                Configurator.Instance.SetConfig(Configurator.CONFIG_KEY_USERNAME,textBoxUserName.Text);
                Configurator.Instance.SetConfig(Configurator.CONFIG_KEY_PASSWORD, textBoxPassword.Text);
                Configurator.Instance.SetConfig(Configurator.CONFIG_KEY_MODEL_ID, textBoxModelId.Text);

                Configurator.Instance.ExportConfig(file.FileName);
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
