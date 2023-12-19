using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class RefreshVideoMessage : MessageBase
    {
        public int UserIndex { get;  private set; }
        public RefreshVideoMessage(int userIndex)
        {
            UserIndex = userIndex;    
        }

    }
}
