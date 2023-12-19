using AWS_S3_Storage;
using DAL;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using MySql.Data.MySqlClient;
using Utility;
using Model;
using System.Data.Common;

namespace iTube.ViewModel
{
    public class PlayViewModel : INotifyPropertyChanged
    {
        private DBHelper dbHelper;
        private AWSStorage aWSStorage;


        public RelayCommand DownloadCommand { get; set; }
        private ObservableCollection<Comment> commentList;
        public ObservableCollection<Comment> CommentList
        {
            get => commentList;
            set
            {
                commentList = value;
                NotifyPropertyChanged(nameof(CommentList));
            }
        }

        private Video currentVideo;
        public Video CurrentVideo
        {
            get => currentVideo;
            set
            {
                currentVideo = value;
                SetVideoInfo();
            }
        }
        #region Variables
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

        private Profile channelProfile;
        public Profile ChannelProfile
        {
            get => channelProfile;
            set
            {
                channelProfile = value;
                NotifyPropertyChanged(nameof(channelProfile));
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

        private Rate videoRate;
        public Rate VideoRate
        {
            get => videoRate;
            set
            {
                videoRate = value;
                NotifyPropertyChanged(nameof(VideoRate));
            }
        }

        private int likeCount;
        public int LikeCount
        {
            get => likeCount;
            set
            {
                likeCount = value;
                NotifyPropertyChanged(nameof(LikeCount));
            }
        }

        private int dislikeCount;
        public int DislikeCount
        {
            get => dislikeCount;
            set
            {
                dislikeCount = value;
                NotifyPropertyChanged(nameof(DislikeCount));
            }
        }

        private int commentCount;
        public int CommentCount
        {
            get => commentCount;
            set
            {
                commentCount = value;
                NotifyPropertyChanged(nameof(CommentCount));
            }
        }


        private int ChannelIndex
        {
            get;
            set;
        }
        private int Index
        {
            get;
            set;
        }
        #endregion

        public PlayViewModel()
        {
            MessageBus.Instance.Register<PlayVisibleMessage>(this, OnPlayVisibleMessageRecieved);
            dbHelper = new DBHelper();
            CommentList = new ObservableCollection<Comment>();
            DownloadCommand = new RelayCommand(OnDownloadCommand);
            aWSStorage = new AWSStorage();
        }

        private void OnPlayVisibleMessageRecieved(PlayVisibleMessage obj)
        {
            PlayVisible = obj.IsEnabled;
            CurrentVideo = obj.Video;

        }

        private  void OnDownloadCommand()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
                 aWSStorage.DownloadFile(Title, dialog.FileName);
        }

        private void SetVideoInfo()
        {
            if (CurrentVideo != null)
            {
                Index = CurrentVideo.Index;
                Title = CurrentVideo.Title;
                ChannelProfile = Utils.GetProfileByIdx(App.USER_IDX);
                Date = CurrentVideo.Date;
                Views = CurrentVideo.Views;
                ChannelIndex = CurrentVideo.ChannelProfile.ChannelIndex;

                dbHelper.OpenConnection();
                GetComment();
                GetRate();
                AddViewCount();
                dbHelper.CloseConnection();
            }
            else
            {
                Index = -1;
                Title = null;
                ChannelProfile = null;
                Date = DateTime.Now;
                Views = -1;
                ChannelIndex = -1;
            }
        }

        private void AddViewCount()
        {
            dbHelper.ExecuteQuery("UPDATE video SET views = " + (Views + 1) + " WHERE idx = " + Index + ";");

            this.Views++;
            CurrentVideo.Views++;
        }

        private void GetComment()
        {
            CommentList.Clear();
            CommentCount = 0;

            DbDataReader result = dbHelper.ExecuteReaderQuery("SELECT idx, uid, content, date FROM comment WHERE vid = " + Index + ";");

            while (result.Read())
            {
                Comment comment = new Comment()
                {
                    Index = Convert.ToInt32(result[0].ToString()),
                    Content = result[2].ToString(),
                    ChannelProfile = Utils.GetProfileByIdx(Convert.ToInt32(result[1].ToString())),
                    Date = (DateTime)result[3]
                };

                CommentList.Add(comment);
                CommentCount++;
            }
            result.Close();
        }

        public void PostComment(int uid, string content)
        {
            dbHelper.OpenConnection();
            dbHelper.ExecuteQuery(String.Format("INSERT INTO comment(vid, uid, content,date) VALUES({0}, {1}, \"{2}\",\"{3}\");",
                Index, uid, content, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")));

            GetComment();

            dbHelper.CloseConnection();
        }

        public void DeleteComment(int cid)
        {
            dbHelper.OpenConnection();
            dbHelper.ExecuteQuery("DELETE FROM comment WHERE idx = " + cid + ";");

            GetComment();

            dbHelper.CloseConnection();
        }

        private void GetRate()
        {
            LikeCount = 0;
            DislikeCount = 0;
            VideoRate = Rate.NONE;
            DbDataReader result = dbHelper.ExecuteReaderQuery("SELECT uid, score FROM rate WHERE vid = " + Index + ";");
            while (result.Read())
            {
                Rate rate = (Rate)Convert.ToInt32(result[1].ToString());
                int uid = Convert.ToInt32(result[0].ToString());
                if (uid == App.USER_IDX && uid != 0)
                {
                    VideoRate = rate;
                }

                switch (rate)
                {
                    case Rate.LIKE:
                        LikeCount++;
                        break;
                    case Rate.DISLIKE:
                        DislikeCount++;
                        break;
                }
            }
            result.Close();
        }

        public void RateVideo(Rate rate)
        {
            dbHelper.OpenConnection();
            if (VideoRate == rate)
            {
                ControlCount(rate, true);
                dbHelper.ExecuteQuery(String.Format("DELETE FROM rate WHERE uid = '{0}' AND vid = '{1}';", App.USER_IDX, Index));

                VideoRate = Rate.NONE;
            }
            else
            {
                ControlCount(rate, false);
                dbHelper.ExecuteQuery(String.Format("DELETE FROM rate WHERE uid = '{0}' AND vid = '{1}';", App.USER_IDX, Index));
                dbHelper.ExecuteQuery(String.Format("INSERT INTO rate(uid, vid, score) VALUES({0},{1},{2});", App.USER_IDX, Index, (int)rate));

                VideoRate = rate;
            }
            dbHelper.CloseConnection();
        }

        private bool playVisible = false;
        public bool PlayVisible
        {
            get => playVisible;
            set
            {
                playVisible = value;
                NotifyPropertyChanged(nameof(PlayVisible));
            }
        }

        private void ControlCount(Rate rate, Boolean isRemove)
        {
            if (isRemove)
            {
                if (VideoRate == Rate.LIKE)
                    LikeCount--;
                else if (VideoRate == Rate.DISLIKE)
                    DislikeCount--;
            }
            else
            {
                if (VideoRate == Rate.LIKE)
                {
                    LikeCount--;
                    DislikeCount++;
                }
                else if (VideoRate == Rate.DISLIKE)
                {
                    DislikeCount--;
                    LikeCount++;
                }
                else
                {
                    if (rate == Rate.LIKE)
                        LikeCount++;
                    else
                        DislikeCount++;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public enum Rate
    {
        LIKE = 1,
        NONE = 0,
        DISLIKE = -1
    }
}
