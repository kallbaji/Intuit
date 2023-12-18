using System;
using System.ComponentModel;


namespace Model
{
    public class Comment : INotifyPropertyChanged
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

        private DateTime date;
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                NotifyPropertyChanged(nameof(DateTime));
            }
        }

        private string content;
        public string Content
        {
            get => content;
            set
            {
                content = value;
                NotifyPropertyChanged(nameof(Content));
            }
        }
    }
}
