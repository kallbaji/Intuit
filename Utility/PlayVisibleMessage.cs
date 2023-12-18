using GalaSoft.MvvmLight.Messaging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class PlayVisibleMessage :MessageBase
    {
        public bool IsEnabled { get; private set; }

     public Video Video { get; private set; }

        public PlayVisibleMessage(bool isEnabled,Video video)
        {
            this.IsEnabled = isEnabled;
            this.Video = video;
        }
    }
}
