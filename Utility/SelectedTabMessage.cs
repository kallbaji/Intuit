using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
   public  class SelectedTabMessage : MessageBase
    {
        public int SelectedIndex { get; private set; }

        public SelectedTabMessage(int selectedIndex)
        {
            SelectedIndex = selectedIndex;
        }

    }
}
