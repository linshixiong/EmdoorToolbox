using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Managed.Adb;
using System.Threading;

namespace Common
{
    class MarvellCmdExecutor:CmdExecutor
    {
        public MarvellCmdExecutor(MessageHandler handler, Form form): base(handler, form)
        {
            
        }

        protected override string ExcuteIMEIReadCmd(int index, out string error)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return null;
                }
                success = true;
                string cmd = index == 0 ? "tcmd-subcase.sh read-mrd-imei" : "tcmd-subcase.sh read-mrd-imei-2";
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

            if (receiver.ResultLines != null && receiver.ResultLines.Length > 0)
            {
                string result = receiver.ResultLines[receiver.ResultLines.Length - 1];
                string imei = null;
                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        imei = receiver.ResultLines[0];
                        if (imei != null)
                        {
                            if (imei.Length < 14)
                            {
                                imei = null;
                            }
                            imei = imei.Substring(0, 14);
                            imei = Util.CalculateIMEI(imei);
                        }
                    }
                }

                error = null;
                return imei;

            }

            else
            {
                error = null;
                return null;
            }

        }

        protected override bool ExcuteIMEIWriteCmd(out string error, string imei, int index)
        {
            //Thread.Sleep(2000);
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            error = null;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return false;
                }
                success = true;
                string cmd = index == 0 ? "tcmd-subcase.sh update-mrd-imei" : "tcmd-subcase.sh update-mrd-imei-2";
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand(cmd + " " + imei, receiver);
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

                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        return true;
                    }
                }


                return false;

            }

            else
            {
                return false;
            }

        }

        protected override string ExcuteWifiAddressReadCmd(out string error)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return null;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("tcmd-subcase.sh  wifi-get-mac", receiver);
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
                string wifi = null;
                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        wifi = receiver.ResultLines[0].Replace(":", "").ToUpper(); ;
                    }
                }

                error = null;
                return wifi;
            }

            else
            {
                error = null;
                return null;
            }

        }

        protected override bool ExcuteWifiAddressWriteCmd(out string error, string mac)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            error = null;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return false;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("tcmd-subcase.sh wifi-set-mac " + mac, receiver);
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

                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        return true;
                    }
                }


                return false;

            }

            else
            {
                return false;
            }

        }

        protected override string ExcuteBtAddressReadCmd(out string error)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return null;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("tcmd-subcase.sh  bt-get-mac", receiver);
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
                string bt = null;
                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        bt = receiver.ResultLines[0].Replace(":", "").ToUpper(); ;
                    }
                }

                error = null;
                return bt;

            }

            else
            {
                error = null;
                return null;
            }

        }

        protected override bool ExcuteBtAddressWriteCmd(out string error, string mac)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            error = null;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return false;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("tcmd-subcase.sh bt-set-mac " + mac, receiver);
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

                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        return true;
                    }
                }


                return false;

            }

            else
            {
                return false;
            }

        }

        protected override string ExcuteSNReadCmd(out string error)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return null;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("tcmd-subcase.sh   read-mrd-sn", receiver);
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

            if (receiver.ResultLines != null && receiver.ResultLines.Length > 1)
            {

                string result = receiver.ResultLines[receiver.ResultLines.Length - 1];
                string sn = null;
                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        sn = receiver.ResultLines[1].Substring(receiver.ResultLines[1].LastIndexOf(',') + 1);
                    }
                }
                error = null;
                return sn;

            }

            else
            {
                error = null;
                return null;
            }

        }

        protected override bool ExcuteSNWriteCmd(out string error, string sn)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            error = null;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return false;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("tcmd-subcase.sh update-mrd-sn " + sn, receiver);
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

                if (result.Contains("ret"))
                {
                    if (result.Contains("0"))
                    {
                        return true;
                    }
                }


                return false;

            }

            else
            {
                return false;
            }

        }

        protected override string ExcuteSWVersionReadCmd(out string error)
        {
            CommandResultReceiver receiver = new CommandResultReceiver();
            receiver.TrimLines = true;
            bool success;
            for (int i = 0; i <= 3; i++)
            {
                if (isCanceled)
                {
                    error = null;
                    return null;
                }
                success = true;
                try
                {
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("getprop ro.build.display.id", receiver);
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
                string result = receiver.ResultLines[0];


                String vesion = result;

                error = null;
                return vesion;

            }

            else
            {
                error = null;
                return null;
            }

        }

        protected override void Poweroff()
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
                    AdbHelper.Instance.GetDevices(AndroidDebugBridge.SocketAddress)[0].ExecuteShellCommand("tcmd-subcase.sh poweroff" , receiver);
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
 
    }
}
