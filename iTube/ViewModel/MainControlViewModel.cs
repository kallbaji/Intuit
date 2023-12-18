using System;
using System.ComponentModel;
using Utility;

namespace iTube.ViewModel
{
    public class MainControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Action BackPressedCallback = null;

        private bool isLogin = false;
        public bool IsLogin
        {
            get => isLogin;
            set
            {
                isLogin = value;
                NotifyPropertyChanged(nameof(IsLogin));
                BackPressedCallback?.Invoke();
            }
        }

        private bool tabVisible = true;
        public bool TabVisible
        {
            get => tabVisible;
            set
            {
                tabVisible = value;
                NotifyPropertyChanged(nameof(TabVisible));

            }
        }

        private bool isChannelVisible = false;
        public bool IsChannelVisible
        {
            get => isChannelVisible;
            set
            {
                isChannelVisible = value;
                NotifyPropertyChanged(nameof(IsChannelVisible));

            }
        }
        private int selectedIndex = 0;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                NotifyPropertyChanged(nameof(SelectedIndex));

            }
        }
        
        private bool isVisible = false;
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));

            }
        }
        public MainControlViewModel()
        {
        
            MessageBus.Instance.Register<UploadTabVisibleMessage>(this, message => IsVisible = message.IsEnabled);
            MessageBus.Instance.Register<TabVisibleMessage>(this, OnTabVisibleMessageRecieved);
            MessageBus.Instance.Register<SelectedTabMessage>(this, OnSelectedTabMessageRecieved);
            MessageBus.Instance.Register<UserVideoControlVisibleMessage>(this, OnUserVideoControlMessage);

        }

        private void OnUserVideoControlMessage(UserVideoControlVisibleMessage obj)
        {
            IsChannelVisible = obj.IsVisible;
        }

        private void OnSelectedTabMessageRecieved(SelectedTabMessage obj)
        {
            SelectedIndex = obj.SelectedIndex;
        }

        private void OnTabVisibleMessageRecieved(TabVisibleMessage obj)
        {
            TabVisible = obj.IsEnabled;
           
        }
    }
}
