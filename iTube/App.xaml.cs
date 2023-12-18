﻿using DAL;
using iTube.ViewModel;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace iTube
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string SHORT_SERVER_URI = "localhost";
        public static string SERVER_URI = "http://" + SHORT_SERVER_URI + "/itube";

        public static Boolean IS_LOGGED = false;
        public static int USER_IDX = 1;

        public static Profile currrentProfile = null;

        public static ListViewModel listViewModel = null;
        public static PlayViewModel playViewModel = null;
        public static MainControlViewModel mainControlViewModel = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            listViewModel = new ListViewModel();
            playViewModel = new PlayViewModel();
            base.OnStartup(e);

        }
    }
}
