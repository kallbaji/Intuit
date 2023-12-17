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
    /// Interaction logic for MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
        public delegate void BackVisibility(Visibility visibility);
        public event BackVisibility backVisibility;

        public delegate void LogoutVisibility(Visibility visibility);
        public event LogoutVisibility logoutVisibility;

        public delegate void UploadVisibility(Visibility visibility);
        public event UploadVisibility uploadVisibility;

        public MainControl()
        {
            DataContext = new MainControlViewModel(LoginPressed);
            App.mainControlViewModel = this.DataContext as MainControlViewModel;
            InitializeComponent();
          
        }

        private void ListControl_PlayVideo(Video video)
        {
            
            CollapseEntireControl();
            playControl.Visibility = Visibility.Visible;

            backVisibility(Visibility.Visible);
            playControl.PlayVideo(video);
        }

        public void BackPressed()
        {
            App.listViewModel.GetVideo();
            playControl.BackPressed();
            CollapseEntireControl();
            tabControl.Visibility = Visibility.Visible;

            backVisibility(Visibility.Collapsed);
        }

        public void LoginPressed()
        {
            
            CollapseEntireControl();
            backVisibility(Visibility.Visible);
            playControl.Visibility = Visibility.Visible;
            logoutVisibility(Visibility.Visible);
            (this.DataContext as MainControlViewModel).IsVisible = true;
        }

        private void playControl_loginVisibilityHandler(Visibility v)
        {
            CollapseEntireControl();
            loginControl.Visibility = v;
        }

        private void CollapseEntireControl()
        {
            tabControl.Visibility = Visibility.Collapsed;
            playControl.Visibility = Visibility.Collapsed;
            loginControl.Visibility = Visibility.Collapsed;
        }
    }
}
