using System;
using System.Collections.Generic;
using System.Text;
using Managed.Adb;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Common
{
    public abstract class CmdExecutor
    {
        private static readonly string adbFilePath = string.Format("{0}adb.exe", AppDomain.CurrentDomain.BaseDirectory);
        private MessageHandler handler;
        private Form form;
        protected bool isCanceled = false;

        public static CmdExecutor GetCmdExecutor(int platform, MessageHandler handler, Form form)
        {
            switch (platform)
            {
                case 0:
                    return new MarvellCmdExecutor(handler,form);
                case 1:
                    return new AmlogicCmdExecutor(handler, form);
                case 2:
                    return new RockchipsCmdExecutor(handler, form);
                default:
                    return new MarvellCmdExecutor(handler, form);
            }
        }

        public CmdExecutor(MessageHandler handler, Form form)
        {
            this.handler = handler;
            this.form = form;
        }

        /// <summary>
        ///启动adb进程
        /// </summary>
        public static bool StartAdbProcess()
        {
            bool result = true;
            try
            {
                if (Util.IsProcessOpen("adb"))
                {
                    result = true;
                }
                else
                {
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = adbFilePath;
                    p.StartInfo.Arguments = "devices";
                    p.Start();
                    try
                    {
                        AndroidDebugBridge.Initialize(true);
                        AndroidDebugBridge.CreateBridge();
                        AndroidDebugBridge.Instance.Start();
                    }
                    catch (Exception)
                    {
                        CleanUpAdbProcess();
                        result = false;
                    }
                }


            }
            catch (Exception e)
            {
                result = false;
            }
            return result;

        }

        /// <summary>
        /// 结束adb进程
        /// </summary>
        public static void CleanUpAdbProcess()
        {
            AndroidDebugBridge.Instance.Stop();

            Process[] procs = Process.GetProcessesByName("adb");
            foreach (var item in procs)
            {
                try
                {
                    item.Kill();
                }
                catch { }
            }
        }

        public void StartExcuteWriteCmd(object o)
        {
            DateTime dtStart = DateTime.Now;
            List<KeyValuePair<int, string>> cmds = (List<KeyValuePair<int, string>>)o;
            if (cmds == null || cmds.Count == 0)
            {
                return;
            }
            form.Invoke(handler, Messages.MSG_WRITE_START, 0, null);

            bool success = true;
            string error_msg = null;
            int error_code = 0;
            if (!StartAdbProcess())
            {
                error_msg = "adb进程启动失败";
                error_code = -1;
                success = false;
            }
            if (isCanceled)
            {
                goto END;
            }
            if (isCanceled)
            {
                goto END;
            }
            if (success)
            {
                form.Invoke(handler, Messages.MSG_WRITE_STATE_CHANGE, 0, "正在检查设备...");
                bool ok = CheckAdbShell();

                if (isCanceled)
                {
                    goto END;
                }
                if (!ok)
                {
                    error_msg = "无法执行adb命令";
                    error_code = -2;
                    success = false;
                }
                else
                {
                    form.Invoke(handler, Messages.MSG_WRITE_STATE_CHANGE, 0, "正在读取序列号...");
                    string cmdResult = null;
                    Dictionary<int, string> results = new Dictionary<int, string>();
                    foreach (KeyValuePair<int, string> item in cmds)
                    {

                        int cmd = item.Key;
                        string value = item.Value;
                        bool result = false;
                        form.Invoke(handler, Messages.MSG_WRITE_STATE_CHANGE, cmd, 0);
                        switch (cmd)
                        {
                            case CodeType.TYPE_SN:
                                result = ExcuteSNWriteCmd(out cmdResult, value);
                                break;
                            case CodeType.TYPE_IMEI:
                                result = ExcuteIMEIWriteCmd(out cmdResult, value, 0);
                                break;
                            case CodeType.TYPE_IMEI2:
                                result = ExcuteIMEIWriteCmd(out cmdResult, value, 1);
                                break;
                            case CodeType.TYPE_WIFI_MAC:
                                result = ExcuteWifiAddressWriteCmd(out cmdResult, value);
                                break;
                            case CodeType.TYPE_BT_MAC:
                                result = ExcuteBtAddressWriteCmd(out cmdResult, value);
                                break;
                            default:
                                break;
                        }
                        form.Invoke(handler, Messages.MSG_WRITE_STATE_CHANGE, cmd, result ? 1 : 2);

                    }


                    form.Invoke(handler, Messages.MSG_WRITE_SUCCESS, 0, results);

                    DateTime dtStop = DateTime.Now;

                    TimeSpan ts = dtStop - dtStart;

                    Console.WriteLine("写入{0}项完成，耗时{1}毫秒", cmds.Count, ts.TotalMilliseconds);

                }
            }

        END:
            if (!success && !isCanceled)
            {
                form.Invoke(handler, Messages.MSG_WRITE_FAIL, error_code, error_msg);
            }

        }

        public void StartExcuteReadCmd(object o)
        {
            DateTime dtStart = DateTime.Now;

            List<int> cmds = (List<int>)o;
            if (cmds == null || cmds.Count == 0)
            {
                return;
            }
            form.Invoke(handler, Messages.MSG_READ_START, 0, null);

            bool success = true;
            string error_msg = null;
            int error_code = 0;
            if (!StartAdbProcess())
            {
                error_msg = "adb进程启动失败";
                error_code = -1;
                success = false;
            }
            if (isCanceled)
            {
                goto END;
            }
            if (isCanceled)
            {
                goto END;
            }
            if (success)
            {
                form.Invoke(handler, Messages.MSG_READ_STATE_CHANGE, 0, "正在检查设备...");

                bool ok = CheckAdbShell();

                if (isCanceled)
                {
                    goto END;
                }

                if (!ok)
                {
                    error_msg = "无法执行adb命令";
                    error_code = -2;
                    success = false;

                }

                else
                {
                    form.Invoke(handler, Messages.MSG_READ_STATE_CHANGE, 0, "正在读取序列号...");
                    string cmdResult = null;
                    Dictionary<int, string> results = new Dictionary<int, string>();
                    foreach (int cmd in cmds)
                    {
                        switch (cmd)
                        {
                            case CodeType.TYPE_SN:
                                string sn = ExcuteSNReadCmd(out cmdResult);
                                results.Add(CodeType.TYPE_SN, sn);
                                break;
                            case CodeType.TYPE_IMEI:
                                string imei = ExcuteIMEIReadCmd(0, out cmdResult);
                                results.Add(CodeType.TYPE_IMEI, imei);
                                break;
                            case CodeType.TYPE_IMEI2:
                                string imei2 = ExcuteIMEIReadCmd(1, out cmdResult);
                                results.Add(CodeType.TYPE_IMEI2, imei2);
                                break;
                            case CodeType.TYPE_WIFI_MAC:
                                string wifi = ExcuteWifiAddressReadCmd(out cmdResult);
                                results.Add(CodeType.TYPE_WIFI_MAC, wifi);
                                break;
                            case CodeType.TYPE_BT_MAC:
                                string bt = ExcuteBtAddressReadCmd(out cmdResult);
                                results.Add(CodeType.TYPE_BT_MAC, bt);
                                break;
                            case CodeType.TYPE_SW_VERSION:
                                string version = ExcuteSWVersionReadCmd(out cmdResult);
                                results.Add(CodeType.TYPE_SW_VERSION, version);
                                break;
                            default:
                                break;
                        }

                    }


                    form.Invoke(handler, Messages.MSG_READ_SUCCESS, 0, results);
                    DateTime dtStop = DateTime.Now;

                    TimeSpan ts = dtStop - dtStart;

                    Console.WriteLine("读取{0}项完成，耗时{1}毫秒", cmds.Count, ts.TotalMilliseconds);
                }
            }

        END:
            if (!success && !isCanceled)
            {
                form.Invoke(handler, Messages.MSG_READ_FAIL, error_code, error_msg);
            }

        }

        public void StartExcuteAdbCmd(object o)
        {

            string cmd = o.ToString();
            bool success = true;
            string error_msg = null;
            int error_code = 0;
            if (!StartAdbProcess())
            {
                error_msg = "adb进程启动失败";
                error_code = -1;
                success = false;
            }
            if (isCanceled)
            {
                goto END;
            }

            if (isCanceled)
            {
                goto END;
            }
            if (success)
            {
                form.Invoke(handler, Messages.MSG_WRITE_STATE_CHANGE, 0, "正在检查设备...");

                bool ok = CheckAdbShell();
                if (isCanceled)
                {
                    goto END;
                }
                if (!ok)
                {
                    error_msg = "无法执行adb命令";
                    error_code = -2;
                    success = false;
                }
                else
                {
                    ExcuteAdbCmd(out error_msg, cmd);

                }
            }

        END:
            if (!success && !isCanceled)
            {

            }


        }

        public void StartReboot(object o)
        {
            string into = o.ToString();
            bool success = true;
            string error_msg = null;
            int error_code = 0;
            if (!StartAdbProcess())
            {
                error_msg = "adb进程启动失败";
                error_code = -1;
                success = false;
            }
            if (isCanceled)
            {
                goto END;
            }

            if (isCanceled)
            {
                goto END;
            }
            if (success)
            {
                form.Invoke(handler, Messages.MSG_WRITE_STATE_CHANGE, 0, "正在检查设备...");

                bool ok = CheckAdbShell();
                if (isCanceled)
                {
                    goto END;
                }
                if (!ok)
                {
                    error_msg = "无法执行adb命令";
                    error_code = -2;
                    success = false;
                }
                else
                {
                    Reboot(into);

                }
            }

        END:
            if (!success && !isCanceled)
            {

            }
        }

        public void StartPowerOff(object o)
        {
            bool success = true;
            string error_msg = null;
            int error_code = 0;
            if (!StartAdbProcess())
            {
                error_msg = "adb进程启动失败";
                error_code = -1;
                success = false;
            }
            if (isCanceled)
            {
                goto END;
            }

            if (isCanceled)
            {
                goto END;
            }
            if (success)
            {
                form.Invoke(handler, Messages.MSG_WRITE_STATE_CHANGE, 0, "正在检查设备...");

                bool ok = CheckAdbShell();
                if (isCanceled)
                {
                    goto END;
                }
                if (!ok)
                {
                    error_msg = "无法执行adb命令";
                    error_code = -2;
                    success = false;
                }
                else
                {
                    Poweroff();

                }
            }

        END:
            if (!success && !isCanceled)
            {

            }
        }


        protected bool CheckAdbShell()
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    return false;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("echo hello", receiver);
                }
                catch (Exception)
                {
                    success = false;
                    Thread.Sleep(1000);
                }

                if (success)
                {
                    break;
                }

            }

            if (receiver.ResultLines != null && receiver.ResultLines.Length > 0)
            {
                string result = receiver.ResultLines[receiver.ResultLines.Length - 1];

                return result.Equals("hello");

            }

            else
            {
                return false;
            }
        }

        protected void ExcuteAdbCmd(out string error, string cmd)
        {
            error = null;
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    return ;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand(cmd, receiver);
                }
                catch (Exception)
                {
                    success = false;
                    Thread.Sleep(1000);
                }

                if (success)
                {
                    break;
                }

            }
        }


        protected void Reboot(string into)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    return;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].Reboot(into);
                }
                catch (Exception)
                {
                    success = false;
                    Thread.Sleep(1000);
                }

                if (success)
                {
                    break;
                }

            }
        }


        protected virtual void Poweroff()
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    return;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("poweroff",receiver);
                }
                catch (Exception)
                {
                    success = false;
                    Thread.Sleep(1000);
                }

                if (success)
                {
                    break;
                }

            }
        }

        protected abstract string ExcuteIMEIReadCmd(int index, out string error);
        protected abstract bool ExcuteIMEIWriteCmd(out string error, string imei, int index);
        protected abstract string ExcuteWifiAddressReadCmd(out string error);
        protected abstract bool ExcuteWifiAddressWriteCmd(out string error, string mac);
        protected abstract string ExcuteBtAddressReadCmd(out string error);
        protected abstract bool ExcuteBtAddressWriteCmd(out string error, string mac);
        protected abstract string ExcuteSNReadCmd(out string error);
        protected abstract bool ExcuteSNWriteCmd(out string error, string sn);
        protected abstract string ExcuteSWVersionReadCmd(out string error);


    }
}
