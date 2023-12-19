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
using Interface;
using System.Windows;

namespace iTube.ViewModel
{
    public class PlayViewModel : INotifyPropertyChanged
    {
        IViedoInfoInterface viedoInfoHelper = null;
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

        private RateEnum videoRate;
        public RateEnum VideoRate
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

        public PlayViewModel(IViedoInfoInterface viedoInfoInterface)
        {
            viedoInfoHelper = viedoInfoInterface;
            MessageBus.Instance.Register<PlayVisibleMessage>(this, OnPlayVisibleMessageRecieved);
            MessageBus.Instance.Register<ResetVideoRateMessage>(this, OnResetVideoRateMessageRecieved);
            
            CommentList = new ObservableCollection<Comment>();
            DownloadCommand = new RelayCommand(OnDownloadCommand);
        }

        private void OnResetVideoRateMessageRecieved(ResetVideoRateMessage message)
        {
            ChannelProfile = Utils.GetProfileByIdx(App.USER_IDX);
            if (App.USER_IDX != 1)
            {
                
                
                videoRate = viedoInfoHelper.GetRate(Index, App.USER_IDX); 
            
            
            }
            else { 
                VideoRate = RateEnum.NONE;
            
            
            }
            
        }

        private void OnPlayVisibleMessageRecieved(PlayVisibleMessage obj)
        {
            PlayVisible = obj.IsEnabled;
            CurrentVideo = obj.Video;

        }

        private async void OnDownloadCommand()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                if (await viedoInfoHelper.DownloadFile(Title, dialog.FileName))
                {
                    MessageBoxResult rsltMessageBox = MessageBox.Show("Download Sucessfull", "Download", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBoxResult rsltMessageBox = MessageBox.Show("Download Failed!", "Download", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
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

                viedoInfoHelper.dbHelper.OpenConnection();
                GetComment();
                GetRate();
                AddViewCount();
                viedoInfoHelper.dbHelper.CloseConnection();
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
            viedoInfoHelper.AddViewCount(Views, Index);
            this.Views++;
            CurrentVideo.Views++;
        }

        private void GetComment()
        {
            CommentList.Clear();
            CommentCount = 0;

            foreach (var comment in viedoInfoHelper.GetComments(Index))
            {
                commentList.Add(comment);
                CommentCount++;
            }
        }

        public void PostComment(int uid, string content)
        {
            viedoInfoHelper.dbHelper.OpenConnection();
            viedoInfoHelper.PostComment(uid, content, Index);

            GetComment();

            viedoInfoHelper.dbHelper.CloseConnection();
        }

        public void DeleteComment(int cid)
        {
            viedoInfoHelper.dbHelper.OpenConnection();
            viedoInfoHelper.DeleteComment(cid);

            GetComment();

            viedoInfoHelper.dbHelper.CloseConnection();
        }

        private void GetRate()
        {
            LikeCount = 0;
            DislikeCount = 0;
            VideoRate = RateEnum.NONE;
            List<Rate> rates = viedoInfoHelper.GetRate(Index);
            foreach (Rate item in rates)
            {
                if (item.UID == App.USER_IDX && item.UID != 1)
                {
                    VideoRate = item.RateEnum;
                }

                switch (item.RateEnum)
                {
                    case RateEnum.LIKE:
                        LikeCount++;
                        break;
                    case RateEnum.DISLIKE:
                        DislikeCount++;
                        break;
                }
            }

        }

        public void RateVideo(RateEnum rate)
        {
            viedoInfoHelper.dbHelper.OpenConnection();
            if (VideoRate == rate)
            {
                ControlCount(rate, true);
                viedoInfoHelper.DeleteRate(App.USER_IDX, Index);
                VideoRate = RateEnum.NONE;
            }
            else
            {
                ControlCount(rate, false);
                viedoInfoHelper.DeleteRate(App.USER_IDX, Index);
                viedoInfoHelper.InsertRate(App.USER_IDX, Index, (int)rate);
                VideoRate = rate;
            }
            viedoInfoHelper.dbHelper.CloseConnection();
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

        private void ControlCount(RateEnum rate, Boolean isRemove)
        {
            if (isRemove)
            {
                if (VideoRate == RateEnum.LIKE)
                    LikeCount--;
                else if (VideoRate == RateEnum.DISLIKE)
                    DislikeCount--;
            }
            else
            {
                if (VideoRate == RateEnum.LIKE)
                {
                    LikeCount--;
                    DislikeCount++;
                }
                else if (VideoRate == RateEnum.DISLIKE)
                {
                    DislikeCount--;
                    LikeCount++;
                }
                else
                {
                    if (rate == RateEnum.LIKE)
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
}
