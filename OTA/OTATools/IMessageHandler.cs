using System;
using System.Collections.Generic;
using System.Text;

namespace OTATools
{
  
    public delegate void MessageHandler(int msgId, object obj);

    public interface IMessageHandler
    {

        void HandleMessge(int msgId, object obj);
    }
}
