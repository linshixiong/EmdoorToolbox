using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using System.Windows.Forms;


namespace OTATools 
{
    public class FtpHelper
    {
        private Socket socketControl;
        private static int BUFFER_SIZE = 1024;
        private static FtpHelper instance;
        private string host;
        private int port;
        private string username;
        private string password;
        private System.Timers.Timer timer;
        private System.Timers.Timer timerKeepAlive;
        private long uploadByteCountPerSecond;
        private long uploadSpeed;
        private long remainderByteCount;
        private int remainderTime;
        private List<string> listFiles = new List<string>();
        private List<string> listDirs = new List<string>();
        public static FtpHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FtpHelper();
                }
                return instance;
            }
        }

        private FtpHelper()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 500;
            
            timer.AutoReset = true;
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);

            timerKeepAlive = new System.Timers.Timer();
            timerKeepAlive.Interval = 60000;
            timerKeepAlive.AutoReset = true;
            timerKeepAlive.Elapsed += new ElapsedEventHandler(timerKeepAlive_Elapsed);
        }

        /// <summary>
        /// 保持连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timerKeepAlive_Elapsed(object sender, ElapsedEventArgs e)
        {
            string replyStr = null;
            try
            {
                int replyCode = SendCommand("NOOP ", ref replyStr);
            }
            catch (Exception)
            {

            }
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
                return socketControl==null?false:socketControl.Connected;
            }
        }

        public bool Connect(string host, int port, string username, string password, ref string error)
        {

            this.host = host;
            this.port = port;
            this.username = username;
            this.password = password;

            string replyStr = null;
            int replyCode = 0;
            try
            {
                if (socketControl != null && socketControl.Connected)
                {
                    DisConnect();
                }
                socketControl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socketControl.Connect(host, port);
                replyCode = ReadReply(ref replyStr);
                replyCode = SendCommand("USER " + username, ref replyStr);
                if (!(replyCode == 331 || replyCode == 230))
                {
                    error = replyStr;
                    DisConnect();
                    return false;
                }
                if (replyCode != 230)
                {
                    replyCode = SendCommand("PASS " + password, ref replyStr);
                    if (!(replyCode == 230 || replyCode == 202))
                    {
                        error = replyStr;
                        DisConnect();
                        return false;
                    }
                }

                this.SetTransferType(TransferType.Binary);
                timerKeepAlive.Start();

            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            return true;
        }

        public void DisConnect()
        {
            if (socketControl != null)
            {
                string result = null;
                int code = SendCommand("QUIT", ref result);
                socketControl.Close();
                socketControl = null;
            }
            timerKeepAlive.Stop();
        }




        public string[] GetFileList(string directory)
        {
            //建立进行数据连接的socket
            Socket socketData = CreateDataSocket();
            string strReply = null;
            int iReplyCode = SendCommand("LIST " + directory, ref strReply);
            if (iReplyCode != 150)
            {
                throw new IOException(strReply);
            }

            //获得结果
            String strMsg = "";
            byte[] buffer = new byte[1024];
            while (true)
            {
                int iBytes = socketData.Receive(buffer, buffer.Length, 0);
                strMsg += Encoding.ASCII.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                {
                    break;
                }
            }
            strMsg = strMsg.Replace("\r", "");
            char[] seperator = { '\n' };
            string[] strsFileList = strMsg.Split(seperator);
            socketData.Close();//数据socket关闭时也会有返回码
            if (iReplyCode != 226)
            {
                iReplyCode = ReadReply(ref strReply);
                if (iReplyCode != 226)
                {
                    throw new IOException(strReply.Substring(4));
                }
            }

            

            foreach (string str in strsFileList)
            {
                if (str.Length > 0)
                {

                    string new_str = str.Substring(56);

                    if (!new_str.Equals(".."))
                    {
                        
                        if (str[0] == 'd')
                        {
                            listDirs.Add(directory + "/" + new_str);
                            GetFileList(directory + "/" + new_str);
                        }
                        else
                        {
                            listFiles.Add(directory + "/" + new_str);
                        }
                    }
                }
            }
            return strsFileList;

          
        }

        public long GetFileSize(string remoteFileName)
        {
            string strReply = null;
            long lSize = 0;
            try
            {
                int iReplyCode = SendCommand("SIZE " + remoteFileName, ref strReply);
                if (iReplyCode == 213)
                {
                    lSize = Int64.Parse(strReply);
                }
                else
                {
                    lSize = 0;
                }
            }
            catch (Exception)
            {
                lSize = 0;
            }
            return lSize;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strFileName">待删除文件名或目录</param>
        public bool Delete(string strFileName,bool isDirectory)
        {
            string strReply = null;
            int iReplyCode = 0;
            if (isDirectory)
            {
                iReplyCode = SendCommand("RMD " + strFileName, ref strReply);
            }
            else
            {
                iReplyCode = SendCommand("DELE " + strFileName, ref strReply);
            }
            if (iReplyCode != 250)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool Delete(string directory)
        {
            listFiles.Clear();
            listDirs.Clear();
            listDirs.Add(directory);
            GetFileList(directory);

            
            int count= listFiles.Count;
            for (int i = listFiles.Count - 1; i >= 0; i--)
            {
                Delete(listFiles[i], false);
            }
            for (int i = listDirs.Count - 1; i >= 0; i--)
            {
                Delete(listDirs[i], true);
            }
            return true;
        }

        public bool CreateDirectory(string directory, bool recursive)
        {
            string strReply = null;
            string[] dirs = null;
            
            if (recursive)
            {
               dirs = directory.Replace("\\", "/").Split('/');
            }
            else
            {
                dirs = new string[] { directory };
            }
            if (dirs.Length == 0)
            {
                return false;
            }
            string current = null;
            foreach (string dir in dirs)
            {
                try
                {
                    current = current == null ? dir : current + "/" + dir;
                    int iReplyCode = SendCommand("MKD " + current, ref strReply);
                    if (iReplyCode != 257)
                    {
                    }
                }
                catch (Exception)
                {
                    return false;
                }

            }
            return true;
        }

        /*
        public bool IsDirectoryExist(string directory)
        {
            try
            {
                Socket socketData = CreateDataSocket();

                string strReply = null;
                string parent = Path.GetDirectoryName(directory);
                int iReplyCode = SendCommand("NLST " + parent, ref strReply);
                if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226))
                {
                    return false;
                }
                string strMsg = "";
                byte[] buffer = new byte[BUFFER_SIZE];
                while (true)
                {
                    int count = socketData.Receive(buffer, buffer.Length, 0);
                    strMsg += Encoding.ASCII.GetString(buffer, 0, count);
                    if (count <= buffer.Length)
                    {
                        break;
                    }
                }
                socketData.Close();
                if (iReplyCode != 226)
                {
                    iReplyCode = ReadReply(ref strReply);
                }
                strMsg = strMsg.Replace("\r", "");
                char[] seperator = { '\n' };
                string[] strsFileList = strMsg.Split(seperator);
                if (strsFileList != null && strsFileList.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
  
        }


   */

        public byte[] DownloadFile(string remoteFileName,ref string error)
        {
          
            string strReply = null;
            int iReplyCode = 0;
            byte[] buff = new byte[BUFFER_SIZE];
            MemoryStream ms = new MemoryStream();
            Socket socketData = null;
            byte[] result = null;
            bool received = false;
            try
            {
                socketData = CreateDataSocket();
                iReplyCode = SendCommand("RETR " + remoteFileName, ref strReply);
                if (!(iReplyCode == 150 || iReplyCode == 125 || iReplyCode == 226 || iReplyCode == 250))
                {
                    error = strReply;
                    return null;
                }
                
                long total = 0;
                received = true;
                while (true)
                {
                    int count = socketData.Receive(buff, buff.Length, 0);
                    total += count;
                    ms.Write(buff, 0, count);
                    if (count <buff.Length)
                    {
                        break;
                    }
    
                }
                result = ms.ToArray();
                
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                ms.Close();

                if (socketData!=null&&socketData.Connected)
                {
                    socketData.Close();
                }

                if (received)
                {
                    iReplyCode = ReadReply(ref strReply);
                    /*
                    if (!(iReplyCode == 226 || iReplyCode == 250))
                    {
                       // return null;
                    }*/
                }
            }
            return result;
        }


        public bool Upload(byte[] data, string remoteFileName,ref string error)
        {
            int iReplyCode = 0;
            string strReply = null;
            Socket socketData=null;
            bool sent = false;
            try
            {
                string directoryName = Path.GetDirectoryName(remoteFileName);
                CreateDirectory(directoryName, true);
                socketData = CreateDataSocket();
                iReplyCode = SendCommand("STOR " + remoteFileName, ref strReply);
                if (!(iReplyCode == 125 || iReplyCode == 150))
                {
                    error = strReply;
                    return false;
                }
                
                socketData.Send(data);
                sent = true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            finally
            {
                if (socketData!=null&& socketData.Connected)
                {
                    socketData.Close();
                }
                if (sent)
                {
                    iReplyCode = ReadReply(ref strReply);
                    if (!(iReplyCode == 226 || iReplyCode == 250))
                    {
                        error = strReply; 
                    }
                }
            }
            return true;
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
            remainderTime = (int)(remainderByteCount / (uploadSpeed*60));
            timer.Interval = 5000;
        }

        public void Upload(string localFileName, string remoteFileName, MessageHandler handler, Form form)
        {

            Messages.SendMessage(form, handler, Messages.MSG_UPLOAD_START, null);
            string directoryName = Path.GetDirectoryName(remoteFileName);
            Socket socketData = null;
            int iReplyCode = 0;
            string strReply = null;
            FileStream input=null;
            bool sent = false;
            try
            {
                FileInfo localFile = new FileInfo(localFileName);
                if (!localFile.Exists)
                {
                    Messages.SendMessage(form, handler, Messages.MSG_UPLOAD_END, null);
                    return;
                }
                directoryName = Path.GetDirectoryName(remoteFileName);
                CreateDirectory(directoryName, true);

                long finish = GetFileSize(remoteFileName);
                socketData = CreateDataSocket();
                iReplyCode = SendCommand("APPE " + remoteFileName, ref strReply);
                if (!(iReplyCode == 125 || iReplyCode == 150))
                {
                    return;
                }
             
                input = localFile.OpenRead();

                input.Seek(finish, SeekOrigin.Begin);
                float progress = 0f;
                long localLength = input.Length;
                remainderByteCount = localLength - finish;
                byte[] buffer = new byte[BUFFER_SIZE];

                sent = true;
                uploadByteCountPerSecond = 0;
                timer.Start();
                while (true)
                {
                    int count = input.Read(buffer, 0, buffer.Length);
                    if (count <= 0)
                    {
                        break;
                    }
                    socketData.Send(buffer, count, 0);
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

            }
            catch (Exception ex)
            {
                
            }

            finally
            {
                timer.Stop();
                input.Close();
                if (socketData!=null&& socketData.Connected)
                {
                    socketData.Close();
                }
                if (sent)
                {
                    iReplyCode = ReadReply(ref strReply);
                    if (!(iReplyCode == 226 || iReplyCode == 250))
                    {
                      
                    }
                }
                Messages.SendMessage(form, handler, Messages.MSG_UPLOAD_END, null);
            }
          
        }

     


        private int SendCommand(string strCommand, ref string replyString)
        {
            Byte[] cmdBytes = Encoding.ASCII.GetBytes((strCommand + "\r\n").ToCharArray());
            socketControl.Send(cmdBytes, cmdBytes.Length, 0);
            return ReadReply(ref replyString);
        }

        private int ReadReply(ref string replyString)
        {
            replyString = ReadLine();
            int iReplyCode = Int32.Parse(replyString.Substring(0, 3));
            replyString = replyString.Substring(4).Trim('\r');
            return iReplyCode;
        }


        /// <summary>
        /// 建立进行数据连接的socket
        /// </summary>
        /// <returns>数据连接socket</returns>
        private Socket CreateDataSocket()
        {
            string strReply = null;
            int iReplyCode = SendCommand("PASV", ref strReply);
            if (iReplyCode != 227)
            {
                throw new IOException(strReply);
            }
            int index1 = strReply.IndexOf('(');
            int index2 = strReply.IndexOf(')');
            string ipData =
             strReply.Substring(index1 + 1, index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for (int i = 0; i < len && partCount <= 6; i++)
            {
                char ch = Char.Parse(ipData.Substring(i, 1));
                if (Char.IsDigit(ch))
                    buf += ch;
                else if (ch != ',')
                {
                    throw new IOException("Malformed PASV strReply: " +
                     strReply);
                }
                if (ch == ',' || i + 1 == len)
                {
                    try
                    {
                        parts[partCount++] = Int32.Parse(buf);
                        buf = "";
                    }
                    catch (Exception)
                    {
                        throw new IOException("Malformed PASV strReply: " +
                         strReply);
                    }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." +
             parts[2] + "." + parts[3];
            int port = (parts[4] << 8) + parts[5];
            Socket s = new
             Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new
             IPEndPoint(IPAddress.Parse(ipAddress), port);
            try
            {
                s.Connect(ep);
            }
            catch (Exception)
            {
                throw new IOException("Can't connect to remote server");
            }
            return s;
        }


        /// <summary>
        /// 读取Socket返回的所有字符串
        /// </summary>
        /// <returns>包含应答码的字符串行</returns>
        private string ReadLine()
        {
            string strMsg = null;
            Byte[] buffer = new Byte[BUFFER_SIZE];
            while (true)
            {
                int iBytes = socketControl.Receive(buffer, buffer.Length, 0);
                strMsg += Encoding.ASCII.GetString(buffer, 0, iBytes);
                if (iBytes < buffer.Length)
                {
                    break;
                }
            }
            char[] seperator = { '\n' };
            string[] mess = strMsg.Split(seperator);
            if (strMsg.Length > 2)
            {
                strMsg = mess[mess.Length - 2];
            }
            else
            {
                strMsg = mess[0];
            }
            if (!strMsg.Substring(3, 1).Equals(" "))//返回字符串正确的是以应答码(如220开头,后面接一空格,再接问候字符串)
            {
                return ReadLine();
            }
            return strMsg;
        }


        /// <summary>
        /// 设置传输模式
        /// </summary>
        /// <param name="ttType">传输模式</param>
        public void SetTransferType(TransferType ttType)
        {
            string replyString = null;
            int replyCode = 0;
            if (ttType == TransferType.Binary)
            {
                replyCode = SendCommand("TYPE I", ref replyString);//binary类型传输
            }
            else
            {
                replyCode = SendCommand("TYPE A", ref replyString);//ASCII类型传输
            }
            if (replyCode != 200)
            {
                throw new IOException(replyString);
            }

        }


        /// <summary>
        /// 传输模式:二进制类型、ASCII类型
        /// </summary>
        public enum TransferType
        {
            /// <summary>
            /// Binary
            /// </summary>
            Binary,
            /// <summary>
            /// ASCII
            /// </summary>
            ASCII
        };


    }
}
