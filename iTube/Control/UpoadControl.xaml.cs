using DAL;
using iTube.ViewModel;
using Microsoft.Win32;
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
    /// Interaction logic for UpoadControl.xaml
    /// </summary>
    public partial class UpoadControl : UserControl
    {
        public UpoadControl()
        {
            InitializeComponent();
            DataContext = new UploadControlViewModel(UploadDBOperation.Instance);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)

            {
                (DataContext as UploadControlViewModel).Tumbnail = openFileDialog.FileName;
                tumbnail.Text = openFileDialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath.Text = openFileDialog.FileName;
                (DataContext as UploadControlViewModel).FilePath = openFileDialog.FileName;
            }
        }
    }
}
