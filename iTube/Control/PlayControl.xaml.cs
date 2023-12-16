using iTube.Model;
using iTube.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace iTube.Control
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayControl : UserControl
    {
        private PlayViewModel ViewModel = null;

        public delegate void LoginVisibilityHandler(Visibility v);
        public event LoginVisibilityHandler loginVisibilityHandler;

        public PlayControl()
        {
            InitializeComponent();
            this.Loaded += PlayControl_Loaded;
        }

        private void PlayControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel = App.playViewModel;
            this.DataContext = this.ViewModel;
        }

        public void PlayVideo(Video video)
        {
            videoControl.PlayVideo(video.VideoLink);
            ViewModel.CurrentVideo = video;
        }

        public void BackPressed()
        {
            videoControl.StopVideo();
            ViewModel.CurrentVideo = null;
        }

        private void CommentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CommentListView.SelectedItem != null)
            {
                Comment comment = (Comment)CommentListView.SelectedItem;
                CommentListView.SelectedItem = null;

                if(comment.ChannelProfile.ChannelIndex == App.USER_IDX)
                {
                    MessageBoxResult rsltMessageBox = MessageBox.Show("Are you sure to delete this comment?", "Delete comment", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    switch (rsltMessageBox)
                    {
                        case MessageBoxResult.Yes:
                            ViewModel.DeleteComment(comment.Index);
                            break;

                        case MessageBoxResult.No:

                            break;
                    }
                }
            }
        }

        private void commentBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return && commentBox.Text.Trim() != string.Empty)
            {
                if (App.IS_LOGGED)
                    ViewModel.PostComment(App.USER_IDX, commentBox.Text);
                else
                    ShowLoginDialog();
                    
                commentBox.Text = string.Empty;

                Keyboard.ClearFocus();
            }
        }

        private void ShowLoginDialog()
        {
            MessageBoxResult rsltMessageBox = MessageBox.Show("Login to Leave comment?", "Login", MessageBoxButton.YesNo, MessageBoxImage.Question);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    loginVisibilityHandler(Visibility.Visible);
                    break;
            }
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            Rate type = (Rate)Enum.Parse(typeof(Rate), ((Button)sender).Name);

            ViewModel.RateVideo(type);
        }
    }
}
