using iTube.Model;
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

        public MainControl()
        {
            InitializeComponent();
            this.Loaded += MainControl_Loaded;
        }

        private void MainControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        

        private void ListControl_PlayVideo(Video video)
        {
            playControl.PlayVideo(video);
            CollapseEntireControl();
            playControl.Visibility = Visibility.Visible;

            backVisibility(Visibility.Visible);
        }

        public void BackPressed()
        {
            playControl.BackPressed();
            CollapseEntireControl();
            tabControl.Visibility = Visibility.Visible;

            backVisibility(Visibility.Collapsed);
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
