using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace OTATools
{
    /// <summary>
    /// OTA升级包管理类
    /// </summary>
    class OTAManager
    {

        private static Dictionary<string, DeviceInfo> devices=new Dictionary<string,DeviceInfo>();
        private static Dictionary<string, UpdateFileInfo> updateFiles=new Dictionary<string,UpdateFileInfo>();
        private MessageHandler handler;
        private Form form;

        public static Dictionary<string, DeviceInfo> DeviceList
        {
            get
            {
                return devices;
            }
        }

        public static DeviceInfo GetDevice(string id)
        {

            if (devices == null)
            {
                return null;
            }
            DeviceInfo device = null;
            devices.TryGetValue(id, out device);
            return device;
        }


        public static Dictionary<string, UpdateFileInfo> UpdateFileList
        {
            get
            {
                return updateFiles;
            }
        }

        public static UpdateFileInfo GetUpdateInfo(string Id)
        {
            if (updateFiles == null || updateFiles.Count == 0)
            {
                return null;
            }
            UpdateFileInfo updateFileInfo = null;
            updateFiles.TryGetValue(Id, out updateFileInfo);
            return updateFileInfo;

        }

        public static bool IsUpdateFileExist(string fileName, ref string fileId)
        {
            if (updateFiles == null || updateFiles.Count == 0)
            {
                return false;
            }

            foreach (UpdateFileInfo updateFile in updateFiles.Values)
            {
                if (updateFile.FileName.Equals(fileName))
                {
                    fileId = updateFile.Id;
                    return true;
                }

            }
            return false;
        }



        public OTAManager(MessageHandler handler, Form form)
        {
            this.handler = handler;
            this.form = form;

        }

        public void LoadDeviceList()
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.GetDeviceList));
            t.Start(null);
        }

        public void Login(string serverHost, int port, string userName, string password)
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.Login));
            object[] param = new object[4];
            param[0] = serverHost;
            param[1] = port;
            param[2] = userName;
            param[3] = password;
            t.Start(param);
        }


        public void LoadUpdateFileList(string modelId)
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.GetUpdateFileList));
            t.Start(modelId);
        }



        public void UploadFile(string localFile, string updateFileName, string modelId, bool delayed)
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.UploadFile));
            string[] param = new string[4];
            param[0] = localFile;
            param[1] = updateFileName;
            param[2] = modelId;
            param[3] = delayed.ToString();
            t.Start(param);
        }

        public void GetUpdateFileInfo(string fileName)
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.GetUpdateFileInfo));
            t.Start(fileName);
        }

        public void DeleteUpdateFile(string modelId, string updateFileName)
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.DeleteUpdateFile));
            string[] param = new string[2];
            param[0] = modelId;
            param[1] = updateFileName;
            t.Start(param);
        }


        public void DeleteDeviceDirectory(string modelId)
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.DeleteDeviceDirectoty));
            t.Start(modelId);
        }

        public void SaveUpdateFileList(string modelId)
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.SaveUpdateFileList));
            t.Start(modelId);
        }

        public void SaveDeviceList()
        {
            Thread t = new Thread(new ParameterizedThreadStart(this.SaveDeviceList));
            t.Start(null);
        }

        private void UploadFile(object o)
        {
            string[] param = (string[])o;
            string loacalFile = param[0];
            string updateFileName = param[1];
            string modelId = param[2];
            bool delayed = Convert.ToBoolean(param[3]);

            try
            {

                string ftpFileName = string.Format("{0}/{1}/update_files/{2}",Configurator.CONFIG_ROOT_DIR, modelId, updateFileName);
                if (delayed)
                {
                    Thread.Sleep(3000);
                }

               // FtpHelper.Instance.Upload(loacalFile, ftpFileName, handler, form);
                FtpHelperNew.Instance.Upload(loacalFile, ftpFileName, handler, form);

            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void DeleteUpdateFile(object o)
        {
            Messages.SendMessage(form, handler, Messages.MSG_DELETE_UPDATE_FILE_START, Resources.Strings.deleting);
            string[] param = (string[])o;
            string modelId = param[0];
            string fileName = param[1];
            string remoteFileName = string.Format("{0}/{1}/update_files/{2}",Configurator.CONFIG_ROOT_DIR, modelId, fileName);
           // bool result = FtpHelper.Instance.Delete(remoteFileName,false);
            bool result = FtpHelperNew.Instance.Delete(remoteFileName, false);
            if (result == true)
            {
                Messages.SendMessage(form, handler, Messages.MSG_DELETE_UPDATE_FILE_SUCCESS, Resources.Strings.deleteSuccess);
            }
            else
            {
                Messages.SendMessage(form, handler, Messages.MSG_DELETE_UPDATE_FILE_FAIL, Resources.Strings.deleteFail);
            }
        }

        private void DeleteDeviceDirectoty(object o)
        {
            Messages.SendMessage(form, handler, Messages.MSG_DELETE_DEVICE_DIRECTORY_START, Resources.Strings.deleting);

            string directoty = string.Format("{0}/{1}",Configurator.CONFIG_ROOT_DIR, o);
            Thread.Sleep(1000);
            bool result = true;// FtpHelperNew.Instance.Delete(directoty, true);
            if (result == true)
            {
                Messages.SendMessage(form, handler, Messages.MSG_DELETE_DEVICE_DIRECTORY_SUCCESS, Resources.Strings.deleteSuccess);
            }
            else
            {
                Messages.SendMessage(form, handler, Messages.MSG_DELETE_DEVICE_DIRECTORY_FAIL, Resources.Strings.deleteFail);
            }

        }


        private void GetUpdateFileInfo(object o)
        {
            Messages.SendMessage(form, handler, Messages.MSG_GET_FILE_INFO_START, Resources.Strings.gettingUpdateFileInfo);
            string fileName = o.ToString();

            FileInfo file = new FileInfo(fileName);

            if (!file.Exists)
            {
                Messages.SendMessage(form, handler, Messages.MSG_GET_FILE_INFO_FAIL,Resources.Strings.fileNotExist);
                return;
            }

            bool additional = false;
            string buildNumber = "";
            long buildTime = Utils.GetUpdateFileBuildTime(fileName,ref additional,ref buildNumber);

            if (buildTime <= 0)
            {
               // Messages.SendMessage(form, handler, Messages.MSG_GET_FILE_INFO_FAIL, "无效的升级包文件");
               // return;
            }
            String md5 = Utils.GetFileMD5(fileName);
            if (string.IsNullOrEmpty(md5))
            {
                Messages.SendMessage(form, handler, Messages.MSG_GET_FILE_INFO_FAIL, Resources.Strings.getMd5Fial);
                return;
            }

            UpdateFileInfo updateFileInfo = new UpdateFileInfo();
            updateFileInfo.Md5 = md5;
            updateFileInfo.FileSize = file.Length;
            updateFileInfo.Version = buildTime;
           // updateFileInfo.Version = buildTime;
            updateFileInfo.Additional = additional;
            updateFileInfo.BuildNumber = buildNumber;
            Messages.SendMessage(form, handler, Messages.MSG_GET_FILE_INFO_SUCCESS, updateFileInfo);
        }

        private void GetDeviceList(object o)
        {
            string error = null;
            Messages.SendMessage(form, handler, Messages.MSG_GET_DEVICES_START, Resources.Strings.gettingDeviceList);
           // devices = new Dictionary<string, DeviceInfo>();
            devices.Clear();
            try
            {
               // byte[] data = FtpHelper.Instance.DownloadFile(string.Format("{0}/devices.db",Configurator.CONFIG_ROOT_DIR), ref error);
                byte[] data = FtpHelperNew.Instance.DownloadFile(string.Format("{0}/devices.db", Configurator.CONFIG_ROOT_DIR), ref error);
                if (data != null)
                {
                    string deciphered = Utils.GetStringFromMixData(data);
                    StringReader sr = new StringReader(deciphered);
                    while (true)
                    {
                        string line = sr.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (line == "")
                        {
                            continue;
                        }
                        DeviceInfo device = DeviceInfo.Pase(line);
                      
                        devices[device.Id]= device;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                devices.Clear();
            }
            Messages.SendMessage(form, handler, Messages.MSG_GET_DEVICES_SUCCESS, null);
        }


        private void SaveDeviceList(object o)
        {
            string error = null;
            Messages.SendMessage(form, handler, Messages.MSG_SAVE_DEVICES_START, Resources.Strings.savingDeviceList);

            if (devices == null)
            {
                error = Resources.Strings.Parameterscannotempty;
                Messages.SendMessage(form, handler, Messages.MSG_SAVE_DEVICES_FAIL, error);
                return;
            }
            bool reult = false;
            try
            {
                StringBuilder sbJson = new StringBuilder();
                if (devices != null && devices.Count > 0)
                {
                    foreach (DeviceInfo device in devices.Values)
                    {
                        sbJson.AppendFormat("{0}\n", device.ToString());
                    }
                }
                byte[] buff = Utils.GetDataAfterMix(sbJson.ToString());
                //reult = FtpHelper.Instance.Upload(buff,  Configurator.CONFIG_ROOT_DIR+"/devices.db", ref error);
                reult = FtpHelperNew.Instance.Upload(buff, Configurator.CONFIG_ROOT_DIR + "/devices.db", ref error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }

            if (reult == true)
            {
                Messages.SendMessage(form, handler, Messages.MSG_SAVE_DEVICES_SUCCESS, null);
            }
            else
            {
                Messages.SendMessage(form, handler, Messages.MSG_SAVE_DEVICES_FAIL, error);
            }
        }

        private void Login(object o)
        {
            object[] param = (object[])o;
            string serverHost = param[0].ToString();
            int port = Convert.ToInt32(param[1]);
            string userName = param[2].ToString();
            string password = param[3].ToString();
            string error = null;

            Messages.SendMessage(form, handler, Messages.MSG_LOGIN_START, Resources.Strings.logging);
            bool result =   FtpHelperNew.Instance.Connect(serverHost, port, userName, password, ref error);
            
           
            if (result==true)
            {
                if (form.IsHandleCreated)
                {
                    Messages.SendMessage(form, handler, Messages.MSG_LOGIN_SUCCESS, null);
                }
            }
            else
            {
                Messages.SendMessage(form, handler, Messages.MSG_LOGIN_FAIL, error);
            }

        }

        private void GetUpdateFileList(object o)
        {
            Messages.SendMessage(form, handler, Messages.MSG_GET_UPDATE_FILE_LIST_START, null);
            string error = null;
            string listFileName = string.Format("{0}/{1}/files.db",Configurator.CONFIG_ROOT_DIR, o);
            updateFiles.Clear();
            try
            {
                    //byte[] data = FtpHelper.Instance.DownloadFile(listFileName, ref error);
                    byte[] data = FtpHelperNew.Instance.DownloadFile(listFileName, ref error);
                    string deciphered = Utils.GetStringFromMixData(data);

                    StringReader sr = new StringReader(deciphered);
                    while (true)
                    {
                        string line = sr.ReadLine();

                        if (line == null)
                        {
                            break;
                        }
                        if (line == "")
                        {
                            continue;
                        }
                        UpdateFileInfo updateFile = UpdateFileInfo.Pase(line);
                        updateFiles[updateFile.Id] = updateFile;
                    }
  
            }
            catch (Exception ex)
            {
                error = ex.Message;
                updateFiles.Clear();
            }
   
            Messages.SendMessage(form, handler, Messages.MSG_GET_UPDATE_FILE_LIST_SUCCESS, updateFiles);
        }

        private void SaveUpdateFileList(object o)
        {
            string error = null;
            Messages.SendMessage(form, handler, Messages.MSG_SAVE_UPDATE_FILE_LIST_START, Resources.Strings.savingUpdateList);
            if (updateFiles == null)
            {
                error = Resources.Strings.Parameterscannotempty;
                Messages.SendMessage(form, handler, Messages.MSG_SAVE_UPDATE_FILE_LIST_FAIL, error);
                return;
            }

            bool result = false;
            try
            {

                StringBuilder sbJson = new StringBuilder();


                if (updateFiles != null && updateFiles.Count > 0)
                {
                    foreach (UpdateFileInfo updateFile in updateFiles.Values)
                    {
                        sbJson.AppendFormat("{0}\n", updateFile.ToString());
                    }
                }

                byte[] buff = Utils.GetDataAfterMix(sbJson.ToString());

                string directory = string.Format("{0}/{1}",Configurator.CONFIG_ROOT_DIR,  o.ToString());

                string fileName = string.Format("{0}/files.db", directory);
               // result= FtpHelper.Instance.Upload(buff, fileName,ref error);
                result = FtpHelperNew.Instance.Upload(buff, fileName, ref error);

            }
            catch (Exception ex)
            {
                error = ex.Message;

            }

            if (result == true)
            {
                Messages.SendMessage(form, handler, Messages.MSG_SAVE_UPDATE_FILE_LIST_SUCCESS, null);
            }
            else
            {
                Messages.SendMessage(form, handler, Messages.MSG_SAVE_UPDATE_FILE_LIST_FAIL, error);
            }
        }
    }
}
