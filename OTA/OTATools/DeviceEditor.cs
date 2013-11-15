using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OTATools
{
    public partial class DeviceEditor : Form
    {
        private DeviceInfo device;

        public DeviceEditor(DeviceInfo device)
        {
            this.device = device;

            InitializeComponent();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DeviceEditor_Load(object sender, EventArgs e)
        {
            textBoxClientName.Text = device.Client;
            textBoxDescription.Text = device.Description;
            textBoxModelName.Text = device.Name;
            
        }

        private void buttonCommit_Click(object sender, EventArgs e)
        {
            device.Name = textBoxModelName.Text.Trim();
            device.Description = textBoxDescription.Text.Trim();
            device.Client = textBoxClientName.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void textBoxModelName_TextChanged(object sender, EventArgs e)
        {

            buttonCommit.Enabled = textBoxModelName.Text.Trim().Length > 0;
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
