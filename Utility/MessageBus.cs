using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
   public class MessageBus 
    {
        public static readonly IMessenger Instance = new Messenger();
        
    }
}
