using AWS_S3_Storage;
using DAL;
using GalaSoft.MvvmLight.Command;
using Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Utility;

namespace iTube.ViewModel
{
    class UploadControlViewModel : INotifyPropertyChanged
    {
        private IUploadInterface uploadHelper;
        public UploadControlViewModel(IUploadInterface uploadInterface)
        {
            UploadCommand = new RelayCommand(OnUploadCommand, CanExecuteUploadCommand);
            uploadHelper = uploadInterface;
        }

        private bool CanExecuteUploadCommand()
        {
            return FilePath?.Length > 0 && Tumbnail?.Length > 0 && title?.Length > 0;
        }

        private async void OnUploadCommand()
        {
            MessageBus.Instance.Send<LogoutEnableMessage>(new LogoutEnableMessage(false));
            IsUploadDone = true;
            var result = await uploadHelper.UploadVideo(Title,Tumbnail,FilePath,App.USER_IDX);
            IsUploadDone = false;
            MessageBus.Instance.Send<LogoutEnableMessage>(new LogoutEnableMessage(true));

            if (result == true)
            {
                MessageBus.Instance.Send<RefreshVideoMessage>(new RefreshVideoMessage(App.USER_IDX));
            }
            else
            {
                MessageBoxResult rsltMessageBox = MessageBox.Show("Not able to upload the video!", "upload Failed !", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
