using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class ResetVideoRateMessage :MessageBase
    {
        public int UID { get; private set; }
        public ResetVideoRateMessage(int UID)
        {
            this.UID = UID;     
        }
    }
}
