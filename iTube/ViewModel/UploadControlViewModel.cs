﻿using AWS_S3_Storage;
using DAL;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Utility;

namespace iTube.ViewModel
{
    class UploadControlViewModel : INotifyPropertyChanged
    {
        private DBHelper dbHelper;
        private AWSStorage aWSStorage;
        public UploadControlViewModel()
        {
            UploadCommand = new RelayCommand(OnUploadCommand, CanExecuteUploadCommand);
            dbHelper = new DBHelper(); ;
            aWSStorage = new AWSStorage();
        }

        private bool CanExecuteUploadCommand()
        {
            return FilePath?.Length > 0 && Tumbnail?.Length > 0 && title?.Length > 0;
        }

        private string CreateURLS3(string key)
        {

            return @"https://intuitbuket.s3.ap-south-1.amazonaws.com/" + key;

        }

        private async void OnUploadCommand()
        {
            MessageBus.Instance.Send<LogoutEnableMessage>(new LogoutEnableMessage(false));
            IsUploadDone = true;
           await  aWSStorage.UploadFile(Title, FilePath);
           await  aWSStorage.UploadFile(Title + "_Tumbnail", tumbnail);
            DateTime theDate = DateTime.Now;

            dbHelper.OpenConnection();
            dbHelper.ExecuteQuery(String.Format("INSERT INTO video(title, uploader, thumbnail,video,views,date_upload) VALUES(\"{0}\", " + App.USER_IDX + ", \"{1}\",\"{2}\",0,\'{3}\');", Title, CreateURLS3(Title + "_Tumbnail"), CreateURLS3(Title), theDate.ToString("yyyy-MM-dd H:mm:ss")));
            dbHelper.CloseConnection();
            IsUploadDone = false;
            MessageBus.Instance.Send<LogoutEnableMessage>(new LogoutEnableMessage(true));
            MessageBus.Instance.Send<RefreshVideoMessage>(new RefreshVideoMessage(App.USER_IDX));

        }

        public RelayCommand UploadCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string title = string.Empty;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                NotifyPropertyChanged(nameof(Title));
                UploadCommand.RaiseCanExecuteChanged();

            }
        }
        private string filePath = string.Empty;
        public string FilePath
        {
            get => filePath;
            set
            {
                filePath = value;
                NotifyPropertyChanged(nameof(FilePath));
                UploadCommand.RaiseCanExecuteChanged();

            }
        }
        private bool isUploadDone = false;
        public bool IsUploadDone
        {
            get => isUploadDone;
            set
            {
                isUploadDone = value;
                NotifyPropertyChanged(nameof(IsUploadDone));
                //UploadCommand.RaiseCanExecuteChanged();

            }
        }

        private string tumbnail = string.Empty;
        public string Tumbnail
        {
            get => tumbnail;
            set
            {
                tumbnail = value;
                NotifyPropertyChanged(nameof(Tumbnail));
                UploadCommand.RaiseCanExecuteChanged();

            }
        }

    }
}