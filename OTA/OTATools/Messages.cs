using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OTATools
{
    static class Messages
    {
        public const int MSG_LOGIN_START = 10000;
        public const int MSG_LOGIN_SUCCESS = 10001;
        public const int MSG_LOGIN_FAIL = 10002;
        public const int MSG_GET_DEVICES_START = 10003;
        public const int MSG_GET_DEVICES_SUCCESS = 10004;
        public const int MSG_GET_DEVICES_FAIL = 10005;
        public const int MSG_SAVE_DEVICES_START = 10006;
        public const int MSG_SAVE_DEVICES_SUCCESS = 10007;
        public const int MSG_SAVE_DEVICES_FAIL = 10008;


        public const int MSG_UPLOAD_START = 10009;
        public const int MSG_UPLOAD_END = 10010;

        public const int MSG_UPLOAD_SUSPEND = 10027;
        public const int MSG_UPLOAD_PROGRESS_CHANGE = 10011;

        public const int MSG_GET_FILE_INFO_START = 10012;
        public const int MSG_GET_FILE_INFO_SUCCESS = 10013;
        public const int MSG_GET_FILE_INFO_FAIL = 10014;

        public const int MSG_GET_UPDATE_FILE_LIST_START = 10015;
        public const int MSG_GET_UPDATE_FILE_LIST_FAIL = 10016;
        public const int MSG_GET_UPDATE_FILE_LIST_SUCCESS = 10017;

        public const int MSG_SAVE_UPDATE_FILE_LIST_START = 10018;
        public const int MSG_SAVE_UPDATE_FILE_LIST_FAIL = 10019;
        public const int MSG_SAVE_UPDATE_FILE_LIST_SUCCESS = 10020;

        public const int MSG_DELETE_UPDATE_FILE_START = 10021;
        public const int MSG_DELETE_UPDATE_FILE_FAIL = 10022;
        public const int MSG_DELETE_UPDATE_FILE_SUCCESS = 10023;


        public const int MSG_DELETE_DEVICE_DIRECTORY_START = 10024;
        public const int MSG_DELETE_DEVICE_DIRECTORY_FAIL = 10025;
        public const int MSG_DELETE_DEVICE_DIRECTORY_SUCCESS = 10026;


        public static void SendMessage(Form form, MessageHandler handler, int what, object obj)
        {
            if (handler != null && form != null&&form.IsHandleCreated)
            {
                try
                {
                    form.Invoke(handler, what, obj);
                }
                catch (Exception)
                {
                }
            }
        }

    }
}
