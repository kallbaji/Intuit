using System;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace Model
{
    public class Video : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int index;
        public int Index
        {
            get => index;
            set
            {
                index = value;
            }
        }

        private BitmapImage thumbnail;
        public BitmapImage Thumbnail
        {
            get => thumbnail;
            set
            {
                thumbnail = value;
                NotifyPropertyChanged(nameof(Thumbnail));
            }
        }

        private Profile channelProfile;
        public Profile ChannelProfile
        {
            get => channelProfile;
            set
            {
                channelProfile = value;
                NotifyPropertyChanged(nameof(ChannelProfile));
            }
        }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(Title));
            }
        }
        private string videoLink;
        public string VideoLink
        {
            get => videoLink;
            set
            {
                videoLink = value;
            }
        }
        private int views;
        public int Views
        {
            get => views;
            set
            {
                views = value;
                NotifyPropertyChanged(nameof(Views));
            }
        }
        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                NotifyPropertyChanged(nameof(Date));
            }
        }
    }
}
