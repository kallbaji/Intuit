using AWS_S3_Storage;
using DAL;
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
        public static bool IS_LOGGED = false;
        public static int USER_IDX = 1;
        public static Profile currrentProfile = null;



        protected override void OnStartup(StartupEventArgs e)
        {
            ;
            var awsHelper = AWSStorage.Instance;
            ViedoDBOperation.Instance.dbHelper = new DBHelper();
            UploadDBOperation.Instance.dbHelper = new DBHelper();
            UploadDBOperation.Instance.uploadHelper = awsHelper;
            LoginDBOperation.Instance.dBHelper = new DBHelper();
            VideoInfoDBOperation.Instance.dbHelper = new DBHelper();
            VideoInfoDBOperation.Instance.downloadHelper = awsHelper;
            base.OnStartup(e);

        }
    }
}
