using DAL;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Utility;

namespace iTube.ViewModel
{
   public class UserVideoControlViewModel : INotifyPropertyChanged
    {
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
        public UserVideoControlViewModel()
        {
            MessageBus.Instance.Register<UserVideoControlVisibleMessage>(this, OnUserVideoControlVisibleMessage);
            VideoList = new ObservableCollection<Video>();
            dbHelper = new DBHelper();
            GetVideo();

        }

        private void OnUserVideoControlVisibleMessage(UserVideoControlVisibleMessage obj)
        {
            GetVideo();
        }

        public void GetVideo()
        {
            VideoList.Clear();
            dbHelper.OpenConnection();

            MySqlDataReader result = dbHelper.ExecuteReaderQuery(
                "SELECT " +
                "idx, title, uploader, thumbnail, video, views, date_upload FROM video WHERE uploader= " + App.USER_IDX + ";"
                );
            while (result.Read())
            {
                Video video = new Video()
                {
                    ChannelProfile = Utils.GetProfileByIdx(Convert.ToInt32(result[2])),

                    Index = Convert.ToInt32(result[0].ToString()),
                    Title = result[1].ToString(),
                    Thumbnail = new BitmapImage(new Uri(result[3].ToString())),
                    VideoLink = result[4].ToString(),
                    Views = Convert.ToInt32(result[5]),
                    Date = (DateTime)result[6]
                };



                VideoList.Add(video);

            }
            result.Close();

            dbHelper.CloseConnection();
        }
    }
}

