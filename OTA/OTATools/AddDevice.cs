using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OTATools
{
    public partial class AddDevice : Form
    {

        private DeviceInfo device;      

        public AddDevice()
        {
            InitializeComponent();
            device = new DeviceInfo();
            device.Id = "device_" + Utils.GetRandomString(16);
            textBoxModelId.Text = device.Id;
        }

      

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(device.Id))
            {
                MessageBox.Show(Resources.Strings.invalidID);
                return;
            }
            if (OTAManager.DeviceList.ContainsKey(device.Id))
            {
                MessageBox.Show(Resources.Strings.modelExisting);
                return;
            }

            device.Name = txtDeviceName.Text;
            device.Client = txtClientName.Text;
            device.Description = txtDescription.Text;
            device.FWListFile = string.Format("{0}/{1}/files.db",Configurator.CONFIG_ROOT_DIR, device.Id);
            device.CreateTime = Utils.GetCurrentTimeSeconds();
            device.Enable = checkBoxOTAEnable.Checked;

            OTAManager.DeviceList.Add(device.Id, device);
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void AddDevice_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void txtDeviceName_TextChanged(object sender, EventArgs e)
        {
            btnAdd.Enabled = txtDeviceName.Text.Trim().Length > 0;
           
        }

        private void textBoxModelId_TextChanged(object sender, EventArgs e)
        {

            
        }



        private void buttonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = Resources.Strings.fileFilter;
            fileDialog.AddExtension = true;
            fileDialog.DefaultExt = "cfg";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {

                Dictionary<string, string> configs = Configurator.Instance.ReadConfigs(fileDialog.FileName);

                string deviceId = null;
                
                configs.TryGetValue(Configurator.CONFIG_KEY_MODEL_ID, out deviceId);

                device.Id = deviceId;
                textBoxModelId.Text = deviceId;


            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
