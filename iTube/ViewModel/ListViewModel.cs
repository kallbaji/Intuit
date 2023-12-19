using DAL;
using Interface;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Utility;

namespace iTube.ViewModel
{
    public class ListViewModel : INotifyPropertyChanged
    {
        private IVideoInterface VideoHelper = null;
         public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<Video> VideoList { get; set; }
        private Video selectedVideo = null;
        public Video SelectedVideo
        {
            get => selectedVideo;
            set
            {
                selectedVideo = value;
                MessageBus.Instance.Send<PlayVisibleMessage>(new PlayVisibleMessage(true,selectedVideo));
                MessageBus.Instance.Send<TabVisibleMessage>(new TabVisibleMessage(false));
                MessageBus.Instance.Send<BackButtonVisibleMessage>(new BackButtonVisibleMessage(true));
                MessageBus.Instance.Send<PlayVideoMessage>(new PlayVideoMessage(true, new Uri(selectedVideo.VideoLink)));
                selectedVideo = null;
                NotifyPropertyChanged(nameof(SelectedVideo));
            }
        }
        public ListViewModel(IVideoInterface videoInterface)
        {

            MessageBus.Instance.Register<RefreshVideoMessage>(this, OnRefreshVideoMessage);
            VideoList = new ObservableCollection<Video>();
            VideoHelper = videoInterface;
            GetVideo();
        }
        private void OnRefreshVideoMessage(RefreshVideoMessage message)
        {
            GetVideo();
        }
        private void GetVideo()
        {
            VideoList.Clear();
            foreach(Video video in VideoHelper.GetVideos())
            {
                VideoList.Add(video);   

            }
          
            
        }
    }
}
