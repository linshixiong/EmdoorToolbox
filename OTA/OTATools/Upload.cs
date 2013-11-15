using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace OTATools
{
    public partial class Upload : Form
    {
        private MessageHandler mHandler;
        private OTAManager ota;
        private string modelId;
        private UpdateFileInfo updateFile;
        private bool fileExist;


        public Upload(string modelId)
        {
            InitializeComponent();
            this.modelId = modelId;
            mHandler = new MessageHandler(this.HandleMessge);
            ota = new OTAManager(mHandler, this);
          
        }

        public void HandleMessge(int msgId, object obj)
        {
            switch (msgId)
            {
                case Messages.MSG_UPLOAD_START:
                    panelProgress.Visible = true;
                    panelFileBrowe.Visible = false;
                    btnUpload.Enabled = false;
                    break;

                case Messages.MSG_UPLOAD_SUSPEND:
                   
                    ota.UploadFile(txtUpdateFileName.Text, updateFile.FileName, modelId, true);
                    break;
                case Messages.MSG_UPLOAD_END:
                    panelProgress.Visible = false;
                    panelFileBrowe.Visible = true;
                    btnUpload.Enabled = true;
                    updateFile.ChangeLog = textBoxChangeLog.Text;
                    updateFile.Description = textBoxDescription.Text.Trim();
                    updateFile.Enable = true;
                    updateFile.ReleaseTime = Utils.GetCurrentTimeSeconds();
                    updateFile.Version = Convert.ToInt64( textBoxVersion.Text.Trim());
                    OTAManager.UpdateFileList[updateFile.Id] = updateFile;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
                case Messages.MSG_UPLOAD_PROGRESS_CHANGE:
                    object[] result = (object[])obj;
                    float percent = Convert.ToSingle(result[0]);
                    int speed = Convert.ToInt32(result[1]);
                    int remainderTime=Convert.ToInt32(result[2]);
                    progressBar1.Value = (int)percent;
                    label2.Text = String.Format(Resources.Strings.uploadSpeed, speed / 1024, percent.ToString("f2"), remainderTime==0?1:remainderTime);
                    break;

                case Messages.MSG_GET_FILE_INFO_START:
                    fileExist = false;
                    labelMsg.Text = Resources.Strings.gettingUpdateFileInfo;
                    break;

                case Messages.MSG_GET_FILE_INFO_SUCCESS:
                    updateFile = (UpdateFileInfo)obj;
                    if (updateFile == null)
                    {
                        labelMsg.Text = Resources.Strings.cannotGetUpdateFileInfo;
                        return;
                    }

                    string fileName = string.Format("update_{0}.zip", updateFile.Md5.ToLower());
                    string id = null;
                    if (OTAManager.IsUpdateFileExist(fileName, ref id))
                    {
                        updateFile = OTAManager.GetUpdateInfo(id);
                        fileExist = true;
                        btnUpload.Enabled = false;
                        labelMsg.Text = Resources.Strings.updateFileExist;
                    }
                    else
                    {
                        updateFile.Id = "update_" + Utils.GetRandomString(16);
                        updateFile.ModelId = modelId;
                        updateFile.FileName = fileName;
                        btnUpload.Enabled = true;
                        labelMsg.Text = "";
                        textBoxDescription.Focus();
   
                    }
                    
                    textBoxFileMD5.Text = updateFile.Md5;
                    textBoxFileSize.Text = updateFile.FileSize.ToString();
                    textBoxBuildNumber.Text = updateFile.BuildNumber;
                    if (updateFile.Version <= 0)
                    {
                        textBoxVersion.ReadOnly = false;
                        textBoxVersion.Text = Utils.GetCurrentTimeSeconds().ToString();
                    }
                    else
                    {
                        textBoxVersion.ReadOnly = true;
                        textBoxVersion.Text = updateFile.Version.ToString();
                    }
                    textBoxID.Text = updateFile.Id;
                    textBoxModelId.Text = modelId;
                   
                    

                    break;
                case Messages.MSG_GET_FILE_INFO_FAIL:
                    labelMsg.Text = obj.ToString();
                 
                    break;
            
            }
        }



        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = Resources.Strings.updateFileFilter;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtUpdateFileName.Text = fileDialog.FileName;
                ota.GetUpdateFileInfo(txtUpdateFileName.Text);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //bool overWrite = false;
            if(string.IsNullOrEmpty(textBoxVersion.Text.Trim()))
            {
                DialogResult result = MessageBox.Show(Resources.Strings.versionInvalid, Resources.Strings.cannotUpload, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
                return;
            }
            
            if (fileExist)
            {
                DialogResult result = MessageBox.Show(Resources.Strings.updateFileExist, Resources.Strings.cannotUpload, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);
               return;
            }
            if (!checkBox1.Checked)
            {
                ota.UploadFile(txtUpdateFileName.Text, updateFile.FileName, modelId, false);
            }
            else
            {
                Messages.SendMessage(this, mHandler, Messages.MSG_UPLOAD_END, null);
            }
        }

       

        private void Upload_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Upload_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }





    }
}
