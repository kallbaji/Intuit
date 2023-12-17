using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTube.ViewModel
{
   public  class MainControlViewModel :  INotifyPropertyChanged
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
        public MainControlViewModel(Action callback)
        {
            BackPressedCallback = callback;
        }
    }
}
