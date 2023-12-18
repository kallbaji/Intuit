using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace iTube.ViewModel
{
    public class VideoControllerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Uri filenameUri = null;
        public Uri FilenameUri
        {
            get => filenameUri;
            set
            {
                filenameUri = value;
                NotifyPropertyChanged(nameof(FilenameUri));
            }
        }


        Action playVideoCallBackHandler = null;
        Action stopVideoCallBackHandler = null;
        public VideoControllerViewModel(Action playvideoCallBackHandler, Action stopVideoCallBackHandler)
        {
            this.playVideoCallBackHandler = playvideoCallBackHandler;
            this.stopVideoCallBackHandler = stopVideoCallBackHandler;
            MessageBus.Instance.Register<PlayVideoMessage>(this, OnPlayVideoMessageRecieved);
            MessageBus.Instance.Register<PlayVisibleMessage>(this, OnPlayVisibleMessageRecieved);

        }

        private void OnPlayVisibleMessageRecieved(PlayVisibleMessage obj)
        {
            if (obj.Video == null)
                stopVideoCallBackHandler?.Invoke();
        }

        private void OnPlayVideoMessageRecieved(PlayVideoMessage obj)
        {
            FilenameUri = obj.FileURL;
            playVideoCallBackHandler?.Invoke();
        }
    }
}
