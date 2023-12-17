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

namespace iTube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (Exception)
            {

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            mainControl.BackPressed();
        }

        private void MainControl_backVisibility(Visibility visibility)
        {
            Back.Visibility = visibility;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            App.USER_IDX = 1;
            App.currrentProfile = Utils.GetProfileByIdx(1);
            App.playViewModel.ChannelProfile = App.currrentProfile;
            App.IS_LOGGED = false;
            Logout.Visibility = Visibility.Collapsed;
            (mainControl.DataContext as MainControlViewModel).IsVisible = false;
            Login.Visibility = Visibility.Visible;
        }

        private void mainControl_logoutVisibility(Visibility visibility)
        {
            Logout.Visibility = visibility;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login.Visibility = Visibility.Collapsed;
            mainControl.loginControl.Visibility = Visibility.Visible;
        }
    }
}
