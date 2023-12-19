using DAL;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace iTube.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainWindowViewModel()
        {
            MessageBus.Instance.Register<LogoutEnableMessage>(this, OnLogoutMessageRecieved);
            MessageBus.Instance.Register<BackButtonVisibleMessage>(this, OnBackMessageRecieved);
            LoginCommand = new RelayCommand(OnLoginCommand);
            LogoutCommand = new RelayCommand(OnLogoutCommand);
            BackCommand = new RelayCommand(OnBackCommand);
        }


        private void OnBackCommand()
        {
            BackVisible = false;
            MessageBus.Instance.Send(new PlayVisibleMessage(false, null));
            MessageBus.Instance.Send(new TabVisibleMessage(true));

        }

        private void OnBackMessageRecieved(BackButtonVisibleMessage obj)
        {
            BackVisible = obj.IsEnabled;
        }

        private void OnLogoutCommand()
        {
            App.USER_IDX = 1;
            App.currrentProfile = Utils.GetProfileByIdx(1);
            App.IS_LOGGED = false;
            LogoutVisible = false;
            MessageBus.Instance.Send(new LoginScreenVisible(false));
            MessageBus.Instance.Send(new UploadTabVisibleMessage(false));
            MessageBus.Instance.Send(new UserVideoControlVisibleMessage(false, App.USER_IDX));
            MessageBus.Instance.Send(new ResetVideoRateMessage(App.USER_IDX));
            MessageBus.Instance.Send(new SelectedTabMessage(0));
            LoginVisible = true;
        }

        private void OnLoginCommand()
        {
            LoginVisible = false;
            MessageBus.Instance.Send<LoginScreenVisible>(new LoginScreenVisible(true));
           

        }

        public RelayCommand LoginCommand { get; set; }

        public RelayCommand LogoutCommand { get; set; }


        private void OnLogoutMessageRecieved(LogoutEnableMessage obj)
        {
            LogoutVisible = obj.IsEnabled;
        }

        private bool logoutVisible = false;
        public bool LogoutVisible
        {
            get => logoutVisible;
            set
            {
                logoutVisible = value;
                NotifyPropertyChanged(nameof(LogoutVisible));
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
        private bool backVisible = false;
        public bool BackVisible
        {
            get => backVisible;
            set
            {
                backVisible = value;
                NotifyPropertyChanged(nameof(BackVisible));
            }
        }

        public RelayCommand BackCommand { get; }
    }
}
