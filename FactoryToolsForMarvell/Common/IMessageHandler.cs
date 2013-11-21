using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
  
    public delegate void MessageHandler(int msgId,int requestCode, object obj);


    public interface IMessageHandler
    {

        void HandleMessge(int msgId, int requestCode, object obj);
    }
}
