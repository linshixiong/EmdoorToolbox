using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Ionic.Zip;

namespace OTATools
{
    class Utils
    {
        //默认密钥向量
        private const string sKey = "A3F2569DESJEIWBCJOTY45DYQWF68H1Y";   //矢量，矢量可以为空  
        private const string sIV = "qcDY6X+aPLw=";   //构造一个对称算法  
        private static SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider(); 
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptDES(string encryptString)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);    //指定加密的运算模式  
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;    //获取或设置加密算法的填充模式  
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
            byt = Encoding.UTF8.GetBytes(encryptString);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());  
        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptDES(string decryptString)
        {
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
            byt = Convert.FromBase64String(decryptString);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray()); 
        }


        public static string GetStringFromMixData(byte[] buff)
        {
            if (buff == null)
            {
                return null;
            }
            for (int index = 0; index < buff.Length; index++)
            {
                byte b = buff[index];

                buff[index] = b -= 128;
            }
            string str = Encoding.ASCII.GetString(buff);
            str = DecryptDES(str);
            return str;
        }

        public static byte[] GetDataAfterMix(string input)
        {
            input = EncryptDES(input);
            byte[] buff = Encoding.ASCII.GetBytes(input);

            for (int index = 0; index < buff.Length; index++)
            {
                byte b = buff[index];

                buff[index] = b += 128;
                char c = (char)buff[index];
            }
            return buff;
        }

         /// <summary>
        /// 实现对一个文件md5的读取，path为文件路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileMD5(string fileName)
        {
            try
            {
                FileStream get_file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                
                System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash_byte = get_md5.ComputeHash(get_file);
                get_file.Close();
                string result = System.BitConverter.ToString(hash_byte);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static string GetRandomString(int length)
        {
            return System.Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
        }


        public static byte[] SerialObject(Object o)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
             formatter.Serialize(ms,o);

            byte[] buff=new byte[ms.Length];
            ms.Seek(0, SeekOrigin.Begin);
            int count= ms.Read(buff, 0, buff.Length);
            ms.Close();
            return buff;
        }


        public static string GetReadableFileSize(long size)
        {
            float newSize = (float)size / 1024f;

            if (newSize < 1024)
            {
                return string.Format("{0}KB", newSize.ToString("0.0"));
            }
            newSize = newSize / 1024f;
           
            return string.Format("{0}MB",newSize.ToString("0.0"));
        }

        public static DateTime ConvertToLocalTime(long utc)
        {
            DateTime dtZone = new DateTime(1970, 1, 1, 0, 0, 0);
            dtZone = dtZone.AddSeconds(utc); 
           
            return dtZone.ToLocalTime();
        }

        public static long GetCurrentTimeSeconds()
        {
            DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
            return (long) (javaSpan.TotalMilliseconds/1000);
        }



        public static long GetUpdateFileBuildTime(string fileName, ref bool isAdditional,ref string buildNumber)
        {
            ZipFile zipFile=null;
            try
            {
                zipFile = ZipFile.Read(fileName);
            }
            catch (Exception ex)
            {
                return -1;
            }
             ZipEntry e=null;
             isAdditional = false;
            if (zipFile.ContainsEntry("system/build.prop"))
            {
               e= zipFile["system/build.prop"];
            }
            else if(zipFile.ContainsEntry("system/etc/version"))
            {
                isAdditional = true;
                e = zipFile["system/etc/version"];
            }


            if (e != null)
            {
                MemoryStream ms = new MemoryStream();
                e.Extract(ms);
                string result = System.Text.Encoding.Default.GetString(ms.ToArray());
                ms.Close();
                long utc = -1;
                if (!string.IsNullOrEmpty(result))
                {
                    StringReader sr = new StringReader(result);
                    while (true)
                    {
                        string line = sr.ReadLine();
                        if (line == null)
                        {
                            break;
                        }
                        if (line.StartsWith("#") || !line.Contains("="))
                        {
                            continue;
                        }

                        int index = line.IndexOf('=');

                        string key = line.Substring(0, index);
                        string value = line.Substring(index + 1);
                        if (key.Equals("ro.build.display.id"))
                        {
                            buildNumber = value;
                            continue;
                        }

                        if (key.Equals("ro.build.date.utc"))
                        {
                            utc = Convert.ToInt64(value);
                            continue;
                        }

                        if (key.Equals("ota.version"))
                        {
                            utc = Convert.ToInt64(value);
                            continue;
                        }


   
                    }
                }

                return utc;
                

            }
            else
            {
                return -1;
            }

           
        }
    }
}
