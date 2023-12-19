using DAL;
using iTube.ViewModel;
using Model;
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
using Utility;

namespace iTube.Control
{
    /// <summary>
    /// Interaction logic for PlayerControl.xaml
    /// </summary>
    public partial class PlayControl : UserControl
    {
        private PlayViewModel ViewModel = null;

        public PlayControl()
        {
            InitializeComponent();
            this.Loaded += PlayControl_Loaded;
        }

        private void PlayControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.ViewModel = new PlayViewModel(VideoInfoDBOperation.Instance);
            this.DataContext = this.ViewModel;
        }

        private void CommentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CommentListView.SelectedItem != null)
            {
                Comment comment = (Comment)CommentListView.SelectedItem;
                CommentListView.SelectedItem = null;

                if (comment.ChannelProfile.ChannelIndex == App.USER_IDX)
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
            if (e.Key == Key.Return && commentBox.Text.Trim() != string.Empty)
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
                    MessageBus.Instance.Send<LoginScreenVisible>(new LoginScreenVisible(true));
                    break;
            }
        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
           RateEnum type = (RateEnum)Enum.Parse(typeof(RateEnum), ((Button)sender).Name);

            ViewModel.RateVideo(type);
        }
    }
}
