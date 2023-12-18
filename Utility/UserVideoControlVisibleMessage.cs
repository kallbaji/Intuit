using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
   public  class UserVideoControlVisibleMessage : MessageBase
    {
        public bool IsVisible { get; private set; }
        public int Uploader { get; private set; }
        public UserVideoControlVisibleMessage(bool isVisible,int uploader)
        {
            IsVisible = isVisible;
            Uploader = uploader;
        }
    }
}
