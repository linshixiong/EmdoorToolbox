using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

namespace OTATools
{
    class FtpHelperNew
    {
        private static FtpHelperNew instance;
        private string host;
        private int port;
        private string username;
        private string password;
      
        private long remainderByteCount;
        private long uploadByteCountPerSecond;
        private long uploadSpeed;
        private int remainderTime;
        private System.Timers.Timer timer;
        public static FtpHelperNew Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FtpHelperNew();
                }
                return instance;
            }
        }

        private FtpHelperNew()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 500;

            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (timer.Interval == 500)
            {
                uploadSpeed = uploadByteCountPerSecond * 2;
            }
            else
            {
                uploadSpeed = uploadByteCountPerSecond / 5;
            }
            uploadByteCountPerSecond = 0;
            if (uploadSpeed > 0)
            {
                remainderTime = (int)(remainderByteCount / (uploadSpeed * 60));
            }
            timer.Interval = 5000;
        }
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string Host
        {
            get
            {
                return host;
            }
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get
            {
                return port;
            }
        }

        /// <summary>
        /// 登录帐号
        /// </summary>
        public string UserName
        {
            get
            {
                return username;
            }
        }

        /// <summary>
        /// 服务器密码
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
        }

        public bool Connected
        {
            get
            {
                return true;
            }
        }


        private FtpWebRequest CreateFtpWebRequest(string remoteFileName,string method)
        {

            string path = string.Format("ftp://{0}:{1}/{2}", host, port,remoteFileName);
            FtpWebRequest   ftpWebRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            ftpWebRequest.UseBinary = true;
            // ftp用户名和密码
            ftpWebRequest.Credentials = new NetworkCredential(username, password);

            ftpWebRequest.Method = method;

            return ftpWebRequest;
        }

        public bool Connect(string host, int port, string username, string password, ref string error)
        {

            // 根据uri创建FtpWebRequest对象
            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;


            try
            {
                FtpWebRequest ftpWebRequest = CreateFtpWebRequest("", WebRequestMethods.Ftp.ListDirectory);
                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
          
        }


        public byte[] DownloadFile(string remoteFileName, ref string error)
        {
            try
            {
                FtpWebRequest ftpWebRequest = CreateFtpWebRequest(remoteFileName, WebRequestMethods.Ftp.DownloadFile);
                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();
                MemoryStream ms = new MemoryStream();
                Stream ftpStream = response.GetResponseStream();

                long cl = response.ContentLength;

                int bufferSize = 2048;

                int readCount;

                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);

                while (readCount > 0)
                {
                    ms.Write(buffer, 0, readCount);

                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();

                ms.Close();

                response.Close();

                return ms.ToArray();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public bool Upload(byte[] data, string remoteFileName, ref string error)
        {
            try
            {
                string directoryName = Path.GetDirectoryName(remoteFileName);
                CreateDirectory(directoryName, true);

                FtpWebRequest ftpWebRequest = CreateFtpWebRequest(remoteFileName, WebRequestMethods.Ftp.UploadFile);
                ftpWebRequest.ContentLength = data.Length;

                Stream strm = ftpWebRequest.GetRequestStream();

                strm.Write(data, 0, data.Length);
                strm.Close();

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            return true;
        }


        public void Upload(string localFileName, string remoteFileName, MessageHandler handler, Form form)
        {
           
            Messages.SendMessage(form, handler, Messages.MSG_UPLOAD_START, null);


            //Thread.Sleep(5000);
            Console.WriteLine("upload restart");
           // string directoryName = Path.GetDirectoryName(remoteFileName);
            Stream strm = null;
            FileStream fs = null;

           
            bool isSuspend = false;
            try
            {
                FileInfo localFile = new FileInfo(localFileName);
                if (!localFile.Exists)
                {
                    Messages.SendMessage(form, handler, Messages.MSG_UPLOAD_END, null);
                    return;
                }
                string directoryName = Path.GetDirectoryName(remoteFileName);
                CreateDirectory(directoryName, true);
            
                fs = localFile.OpenRead();
                int buffLength = 2048;
  
                byte[] buff = new byte[buffLength];

                string path = string.Format("ftp://{0}:{1}/{2}", host, port, remoteFileName);
                long remoteLength =  GetFileSize(remoteFileName);
                fs.Seek(remoteLength, SeekOrigin.Begin);
                FtpWebRequest ftpWebRequest = CreateFtpWebRequest(remoteFileName, WebRequestMethods.Ftp.AppendFile);
 
                strm = ftpWebRequest.GetRequestStream();
                long finish = remoteLength;
                float progress = 0f;
                long localLength = localFile.Length;
                remainderByteCount = localLength - finish;

                uploadByteCountPerSecond = 0;
                timer.Start();
               
                while (true)
                {
                    int count = fs.Read(buff, 0, buffLength);
                    if (count == 0)
                    {
                        break;
                    }

                    try
                    {
                        strm.Write(buff, 0, count);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine("upload suspend:" + ex.Message);
                        isSuspend = true;
                        break;

                    }
                 
                    finish += count;
                    uploadByteCountPerSecond += count;
                    remainderByteCount -= count;
                    progress = (float)finish / (float)localLength;
                    float percent = progress * 100;

                    object[] result = new object[3];
                    result[0] = percent;
                    result[1] = uploadSpeed;
                    result[2] = remainderTime;
                    form.Invoke(handler, Messages.MSG_UPLOAD_PROGRESS_CHANGE, result);
                }

                if (!isSuspend)
                {
                    Messages.SendMessage(form, handler, Messages.MSG_UPLOAD_END, null);
                }
                else
                {


                    Messages.SendMessage(form, handler, Messages.MSG_UPLOAD_SUSPEND, null);


                }

            }
           
            catch (Exception ex)
            {


               // MessageBox.Show(ex.Message);
          
            }
               
            finally
            {

                Console.WriteLine("upload stop");
                fs.Close();
                timer.Stop();
                try
                {
                    strm.Close();
                }
                catch (Exception)
                {
                }
                
            }


        }

        public bool CreateDirectory(string directory, bool recursive)
        {
                /*
                if (IsDirectoryExist(directory))
                {
                    return true;
                }*/
                string[] directoryTree = directory.Split('\\');


                if (directoryTree == null || directoryTree.Length == 0)
                {
                    return false;
                }

                directory = "";
                for (int i = 0; i < directoryTree.Length; i++)
                {

                    directory +=string.Format("{0}",i==0?directoryTree[i]:"\\"+ directoryTree[i]);
                    /*
                    bool isExist = IsDirectoryExist(directory);
                    if (isExist)
                    {
                        continue;
                    }*/
                    FtpWebRequest ftpWebRequest = CreateFtpWebRequest(directory, WebRequestMethods.Ftp.MakeDirectory);

                    try
                    {
                        FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();

                        response.Close();
                    }
                    catch (Exception)
                    {
                    }
                }

                return true;
      
        }


        public bool IsDirectoryExist(string name)
        {
            try
            {
                FtpWebRequest ftpWebRequest = CreateFtpWebRequest(name, WebRequestMethods.Ftp.ListDirectoryDetails);
                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();

                Stream stream = response.GetResponseStream();
                long length = stream.Length;
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public long GetFileSize(string remoteFileName)
        {
            try
            {
                FtpWebRequest ftpWebRequest = CreateFtpWebRequest(remoteFileName, WebRequestMethods.Ftp.GetFileSize);

                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();

                long length=  response.ContentLength;
                response.Close();

                return length;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strFileName">待删除文件名或目录</param>
        public bool Delete(string remoteFileName, bool isDirectory)
        {
            try
            {
                FtpWebRequest ftpWebRequest = CreateFtpWebRequest(remoteFileName,isDirectory?WebRequestMethods.Ftp.RemoveDirectory:WebRequestMethods.Ftp.DeleteFile);

                FtpWebResponse response = (FtpWebResponse)ftpWebRequest.GetResponse();


                response.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }





}
