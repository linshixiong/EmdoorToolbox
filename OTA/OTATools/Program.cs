using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OTATools
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string [] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args != null && args.Length > 0)
            {
                if("en-US".Equals(args[0])){
                    SetLang("en-US");
                }
            }
            Application.Run(new Login());
        }


        public static void SetLang(string lang)
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
        }
    }
}
