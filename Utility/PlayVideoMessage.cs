using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
   public class PlayVideoMessage : MessageBase
    {
        public bool PlayVideo { get; private set; }
        public Uri FileURL { get; private set; }
        public PlayVideoMessage(bool playVideo,Uri uri)
        {
            this.PlayVideo = playVideo;
            this.FileURL = uri;
        }
    }
}
