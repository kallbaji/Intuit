﻿using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class BackButtonVisibleMessage : MessageBase
    {
        public bool IsEnabled { get; private set; }

        public BackButtonVisibleMessage(bool isEnabled)
        {
            this.IsEnabled = isEnabled;
        }
    }
}
