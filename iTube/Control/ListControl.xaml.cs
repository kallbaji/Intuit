using iTube.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for ListControl.xaml
    /// </summary>
    public partial class ListControl : UserControl
    {
        public delegate void VideoController(Video video);
        public event VideoController PlayVideo;

        public ListControl()
        {
            InitializeComponent();
            this.Loaded += ListControl_Loaded;
        }

        private void ListControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = App.listViewModel;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListView.SelectedItem != null)
            {
                Video videoData = (Video)ListView.SelectedItem;
                ListView.SelectedItem = null;

                PlayVideo(videoData);
            }
        }
    }
}
