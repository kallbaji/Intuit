using DAL;
using GalaSoft.MvvmLight.Command;
using iTube.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace iTube.ViewModel
{
    class LoginControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string iD=string.Empty;
        public string ID
        {
            get => iD;
            set
            {
                iD = value;
                NotifyPropertyChanged(nameof(ID));
                LoginCommand.RaiseCanExecuteChanged();
                CreateCommand.RaiseCanExecuteChanged();
            }
        }
        private string pWD=string.Empty;
        public string PWD
        {
            get => pWD;
            set
            {
                pWD = value;
                NotifyPropertyChanged(nameof(PWD));
                LoginCommand.RaiseCanExecuteChanged();
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        private bool createVisible = false;
        public bool CreateVisible
        {
            get => createVisible;
            set
            {
                createVisible = value;
                NotifyPropertyChanged(nameof(CreateVisible));
            }
        }

        private bool loginVisible = true;
        public bool LoginVisible
        {
            get => loginVisible;
            set
            {
                loginVisible = value;
                NotifyPropertyChanged(nameof(LoginVisible));
            }
        }

        private string name = string.Empty;

        private DBHelper dBHelper = null;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
                LoginCommand.RaiseCanExecuteChanged();
                CreateCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand LoginCommand { get; set; }

       

        public RelayCommand CreateCommand { get; set; }

        public RelayCommand LoginPageCommand { get; set; }



        public RelayCommand CreatePageCommand { get; set; }

        public LoginControlViewModel()
        {
            LoginCommand = new RelayCommand(OnLogin, CanExecuteLogin);
            CreateCommand = new RelayCommand(OnCreate, CanExecuteCreate);
            LoginPageCommand = new RelayCommand(OnLoginPage);
            CreatePageCommand = new RelayCommand(OnCreatePage);
            dBHelper = new  DBHelper();
        }

        private void OnCreatePage()
        {
            LoginVisible = false;
            CreateVisible = true;

          
        }

        private void OnLoginPage()
        {
            LoginVisible = true;
            CreateVisible = false;

        }

        private bool CanExecuteCreate()
        {
            return !string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(PWD) && !string.IsNullOrEmpty(Name);
        }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(PWD);
        }

        private void OnCreate()
        {
            try
            {


                dBHelper.OpenConnection();
                dBHelper.ExecuteQuery(String.Format("INSERT INTO user_table(nick,lid) VALUES(\"{0}\",\"{1}\");",
                 Name, ID));
                MySqlDataReader result = dBHelper.ExecuteReaderQuery("SELECT idx,nick, profile_pic FROM user_table WHERE lid = \"" + ID + "\"" + "AND nick = \"" + Name + "\";");
                while (result.Read())
                {
                    App.currrentProfile = new Profile()
                    {
                        ChannelIndex = Convert.ToInt32(result[0]),
                        ChannelName = result[1].ToString(),
                        ChannelArt = Utils.CreateProfileImage(result[2].ToString())
                    };
                 
                }
                result.Close();
                dBHelper.ExecuteQuery(String.Format("INSERT INTO Login(idx, uid, pwd) VALUES(\"{0}\", {1}, \"{2}\");",
                 ID, App.currrentProfile.ChannelIndex, PWD));
                App.playViewModel.ChannelProfile = App.currrentProfile;
                App.USER_IDX = App.currrentProfile.ChannelIndex;
                App.mainControlViewModel.IsLogin = true;
                App.IS_LOGGED = true;
                App.mainControlViewModel.IsLogin = true;
                dBHelper.CloseConnection();
            }
            catch (Exception ex)
            {


            }
        }

        private void OnLogin()
        {
            try
            {
                dBHelper.OpenConnection();
                MySqlDataReader result1 = dBHelper.ExecuteReaderQuery("SELECT uid FROM Login WHERE idx = \"" + ID + "\"" + "AND pwd = \"" + PWD + "\";");
                if (result1.Read())
                {
                    int uid = Convert.ToInt32(result1[0]);
                    result1.Close();

                    MySqlDataReader result = dBHelper.ExecuteReaderQuery("SELECT idx,nick, profile_pic FROM user_table WHERE idx =" + uid);
                    while (result.Read())
                    {
                        App.currrentProfile = new Profile()
                        {
                            ChannelIndex = Convert.ToInt32(result[0]),
                            ChannelName = result[1].ToString(),
                            ChannelArt = Utils.CreateProfileImage(result[2].ToString())
                        };

                    }

                    App.USER_IDX = App.currrentProfile.ChannelIndex;
                    App.playViewModel.ChannelProfile = App.currrentProfile;
                    App.USER_IDX = App.currrentProfile.ChannelIndex;
                    App.mainControlViewModel.IsLogin = true;
                    App.IS_LOGGED = true;
                    App.mainControlViewModel.IsLogin = true;
                }
                else
                {
                    MessageBoxResult rsltMessageBox = MessageBox.Show("Incorrect Username or Password", "Login Failed !", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                dBHelper.CloseConnection();
               
            }
            catch (Exception ex)
            {


            }

        }
    }
}
