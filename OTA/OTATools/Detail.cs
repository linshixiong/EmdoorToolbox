using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OTATools
{
    public partial class Detail : Form
    {
        private string modelId;
        private string updateId;

        public Detail(string modelId)
        {
            this.modelId = modelId;
            this.updateId = null;
            InitializeComponent();
        }

        public Detail(string modelId, string updateId)
        {
            this.modelId = modelId;
            this.updateId = updateId;
            InitializeComponent();
        }

        private void Detail_Load(object sender, EventArgs e)
        {
            DeviceInfo device= OTAManager.GetDevice(modelId);
            richTextBoxDetail.AppendText( Resources.Strings.deviceInfo+"\n");
            richTextBoxDetail.AppendText(string.Format(Resources.Strings.deviceId, device.Id) + "\n");
            richTextBoxDetail.AppendText(string.Format(Resources.Strings.deviceModel, device.Name) + "\n");
            richTextBoxDetail.AppendText(string.Format(Resources.Strings.clientName, device.Client) + "\n");

            richTextBoxDetail.AppendText(string.Format(Resources.Strings.createTime, Utils.ConvertToLocalTime(device.CreateTime)).ToString() + "\n");
            richTextBoxDetail.AppendText(string.Format(Resources.Strings.description, device.Description) + "\n");


            if (!string.IsNullOrEmpty(updateId))
            {

                UpdateFileInfo updateFileInfo = OTAManager.GetUpdateInfo(updateId);
                richTextBoxDetail.AppendText( "\n"+Resources.Strings.updateFileInfo + "\n");
                richTextBoxDetail.AppendText(string.Format(Resources.Strings.updateFileId, updateFileInfo.Id) + "\n");
                richTextBoxDetail.AppendText(string.Format(Resources.Strings.fileName, updateFileInfo.FileName) + "\n");
                richTextBoxDetail.AppendText(string.Format(Resources.Strings.summaryInfo, updateFileInfo.Description) + "\n");
                richTextBoxDetail.AppendText(string.Format(Resources.Strings.fileSize, updateFileInfo.FileSize) + "\n");
                richTextBoxDetail.AppendText(string.Format(Resources.Strings.md5, updateFileInfo.Md5) + "\n");
                richTextBoxDetail.AppendText(string.Format(Resources.Strings.releaseTime, Utils.ConvertToLocalTime(updateFileInfo.ReleaseTime)).ToString() + "\n");
                richTextBoxDetail.AppendText(string.Format(Resources.Strings.version, updateFileInfo.Version) + "\n");

                if (!string.IsNullOrEmpty(updateFileInfo.ChangeLog))
                {
                    richTextBoxDetail.AppendText( "\n"+Resources.Strings.changeLog + "\n");
                    richTextBoxDetail.AppendText(updateFileInfo.ChangeLog + "\n");
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
