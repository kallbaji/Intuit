using DAL;
using GalaSoft.MvvmLight.Command;
using Interface;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Utility;

namespace iTube.ViewModel
{
    class LoginControlViewModel : INotifyPropertyChanged
    {

        private ILoginInterface LoginHelper = null;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string iD = string.Empty;
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
        private string pWD = string.Empty;
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

        private bool loginPageVisible = true;
        public bool LoginPageVisible
        {
            get => loginPageVisible;
            set
            {
                loginPageVisible = value;
                NotifyPropertyChanged(nameof(LoginPageVisible));
            }
        }

        private bool createPageVisible = false;
        public bool CreatePageVisible
        {
            get => createPageVisible;
            set
            {
                createPageVisible = value;
                NotifyPropertyChanged(nameof(CreatePageVisible));
            }
        }

        private bool loginVisible = false;
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

        public LoginControlViewModel(ILoginInterface loginInterface)
        {
            LoginHelper = loginInterface;
            MessageBus.Instance.Register<LoginScreenVisible>(this, message => LoginVisible = message.IsEnabled);
            LoginCommand = new RelayCommand(OnLogin, CanExecuteLogin);
            CreateCommand = new RelayCommand(OnCreate, CanExecuteCreate);
            LoginPageCommand = new RelayCommand(OnLoginPage);
            CreatePageCommand = new RelayCommand(OnCreatePage);
            
        }

        private void OnCreatePage()
        {
            ID = string.Empty;
            Name = string.Empty;
            PWD = string.Empty;
            LoginPageVisible = false;
            CreatePageVisible = true;


        }

        private void OnLoginPage()
        {
            ID = string.Empty;
            Name = string.Empty;
            PWD = string.Empty;
            LoginPageVisible = true;
            CreatePageVisible = false;

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

            var result = LoginHelper.OnCreate(ID, PWD, Name);
            if (result.Item1)
            {

                App.currrentProfile = result.Item2;
                App.playViewModel.ChannelProfile = App.currrentProfile;
                App.USER_IDX = App.currrentProfile.ChannelIndex;
                App.mainControlViewModel.IsLogin = true;
                App.IS_LOGGED = true;
                MessageBus.Instance.Send(new LogoutEnableMessage(true));
                MessageBus.Instance.Send(new UploadTabVisibleMessage(true));
                LoginVisible = false;
                MessageBus.Instance.Send(new UserVideoControlVisibleMessage(true, App.USER_IDX));
                OnLoginPage();

            }
            else
            {
                App.currrentProfile = result.Item2;
                App.USER_IDX = App.currrentProfile.ChannelIndex;
                App.IS_LOGGED = false;
                App.mainControlViewModel.IsLogin = false;
                MessageBoxResult rsltMessageBox = MessageBox.Show("Create Account failed", "Create Failed !", MessageBoxButton.OK, MessageBoxImage.Information);


            }
        }

        private void OnLogin()
        {
            var result = LoginHelper.OnLogin(ID, PWD);
            if (result.Item1)
            {
                App.currrentProfile=result.Item2;
                App.USER_IDX = App.currrentProfile.ChannelIndex;
                App.playViewModel.ChannelProfile = App.currrentProfile;
                App.USER_IDX = App.currrentProfile.ChannelIndex;
                App.mainControlViewModel.IsLogin = true;
                App.IS_LOGGED = true;
                LoginVisible = false;
                MessageBus.Instance.Send(new LogoutEnableMessage(true));
                LoginVisible = false;
                MessageBus.Instance.Send(new UploadTabVisibleMessage(true));
                MessageBus.Instance.Send(new UserVideoControlVisibleMessage(true, App.USER_IDX));
                OnLoginPage();
            }
            else
            {
                MessageBoxResult rsltMessageBox = MessageBox.Show("Incorrect Username or Password", "Login Failed !", MessageBoxButton.OK, MessageBoxImage.Information);
            }



        }
    }
}
