using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace iTube.Model
{
    public class Profile : INotifyPropertyChanged
    {
        private int channelIndex;
        public int ChannelIndex
        {
            get => channelIndex;
            set
            {
                channelIndex = value;
                NotifyPropertyChanged(nameof(ChannelIndex));
            }
        }
        private string channelName;
        public string ChannelName
        {
            get => channelName;
            set
            {
                channelName = value;
                NotifyPropertyChanged(nameof(ChannelName));
            }
        }

        private BitmapImage channelArt;
        public BitmapImage ChannelArt
        {
            get => channelArt;
            set
            {
                channelArt = value;
                NotifyPropertyChanged(nameof(ChannelArt));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
