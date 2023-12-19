using DAL;
using Interface;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Utility;

namespace iTube.ViewModel
{
    public class UserVideoControlViewModel : INotifyPropertyChanged
    {
        private IVideoInterface VideoHelper = null;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Video> VideoList { get; set; }
        private DBHelper dbHelper;
        private Video selectedVideo = null;
        public Video SelectedVideo
        {
            get => selectedVideo;
            set
            {
                selectedVideo = value;
                MessageBus.Instance.Send<PlayVisibleMessage>(new PlayVisibleMessage(true, selectedVideo));
                MessageBus.Instance.Send<TabVisibleMessage>(new TabVisibleMessage(false));
                MessageBus.Instance.Send<BackButtonVisibleMessage>(new BackButtonVisibleMessage(true));
                MessageBus.Instance.Send<PlayVideoMessage>(new PlayVideoMessage(true, new Uri(selectedVideo.VideoLink)));
                selectedVideo = null;
                NotifyPropertyChanged(nameof(SelectedVideo));
            }
        }
        public UserVideoControlViewModel(IVideoInterface videoInterface)
        {
            VideoHelper = videoInterface;
            MessageBus.Instance.Register<UserVideoControlVisibleMessage>(this, OnUserVideoControlVisibleMessage);
            MessageBus.Instance.Register<RefreshVideoMessage>(this, OnRefreshVideoMessage);
            VideoList = new ObservableCollection<Video>();
            dbHelper = new DBHelper();
            GetVideo();

        }

        private void OnRefreshVideoMessage(RefreshVideoMessage message)
        {
            GetVideo();
        }

        private void OnUserVideoControlVisibleMessage(UserVideoControlVisibleMessage obj)
        {
            GetVideo();
        }

        private void GetVideo()
        {
            VideoList.Clear();
            foreach (Video video in VideoHelper.GetVideos(App.USER_IDX))
            {
                VideoList.Add(video);

            }

        }
    }
}

