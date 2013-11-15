using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace OTATools
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    public class Configurator
    {
        public static readonly string CONFIG_KEY_SERVER_HOST = "host";
        public static readonly string CONFIG_KEY_SERVER_PORT = "port";
        public static readonly string CONFIG_KEY_USERNAME = "user";
        public static readonly string CONFIG_KEY_PASSWORD = "pwd";
        public static readonly string CONFIG_KEY_MODEL_ID = "modelid";

        public static readonly string CONFIG_ROOT_DIR = "ota";


        private Dictionary<string, string> configs;

        private static Configurator instance;

        public static Configurator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configurator();
                }
                return instance;
            }
        }

        private Configurator()
        {
            configs = new Dictionary<string, string>();

        


        }




        public void SetConfig(string configkey,string value)
        {
            if (configs == null)
            {
                return;
            }

            configs[configkey] = value;
        }

        
        public void ExportConfig(string fileName)
        {
            if (configs != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string key in configs.Keys)
                {
                    string value = configs[key];
                    sb.AppendFormat("{0}={1}\n", key, value);
                }

                byte[] data = Utils.GetDataAfterMix(sb.ToString());

                File.WriteAllBytes(fileName, data);
            }
        }




        public Dictionary<string, string> ReadConfigs(string fileName)
        {

           // FileInfo file = new FileInfo(fileName);

            byte[] data= File.ReadAllBytes(fileName);

            String str= Utils.GetStringFromMixData(data);

            if (!string.IsNullOrEmpty(str))
            {
                Dictionary<string, string> configs = new Dictionary<string, string>();
                StringReader sr = new StringReader(str);

                while (true)
                {
                  string  line=  sr.ReadLine();
                  if (line == null)
                  {
                      break;
                  }

                  string[] keyValue = line.Split('=');
                  if (keyValue != null && keyValue.Length >= 2)
                  {
                      configs.Add(keyValue[0], keyValue[1]);
                  }
                
 
                }

                return configs;

            }
            return null;
        }
       

    }
}
