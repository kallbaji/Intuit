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
using System.Windows.Threading;

namespace iTube.Control
{
    /// <summary>
    /// Interaction logic for VideoControl.xaml
    /// </summary>
    public partial class VideoControl : UserControl
    {
        private Boolean IsPlaying = true;
        private DispatcherTimer VideoTimer;

        public VideoControl()
        {
            InitializeComponent();
            this.Loaded += VideoControl_Loaded;
        }

        private void VideoControl_Loaded(object sender, RoutedEventArgs e)
        {
            VideoTimer = new DispatcherTimer();
            VideoTimer.Tick += VideoTimer_Tick;
        }

        public void PlayVideo(string filename)
        {
            mediaPlayer.Source = new Uri(App.SERVER_URI+"/video/"+filename);
            mediaPlayer.Play();
        }

        public void StopVideo()
        {
            IsPlaying = false;
            ControlButton();
            mediaPlayer.Source = null;
            mediaPlayer.Stop();
            mediaPlayer.Close();
        }

        private void VideoTimer_Tick(object sender, EventArgs e)
        {
            Slider.Value = mediaPlayer.Position.TotalSeconds;
        }

        private void mediaPlayer_MouseEnter(object sender, MouseEventArgs e)
        {
            Control.Visibility = Visibility.Visible;
            Slider.Visibility = Visibility.Visible;
        }

        private void mediaPlayer_MouseLeave(object sender, MouseEventArgs e)
        {
            Control.Visibility = Visibility.Collapsed;
            Slider.Visibility = Visibility.Collapsed;
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (IsPlaying)
            {
                mediaPlayer.Pause();
            }
            else
            {
                mediaPlayer.Play();  
            }
            IsPlaying = !IsPlaying;
            ControlButton();
        }

        private void mediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            IsPlaying = true;
            ControlButton();

            TimeSpan timeSpan = mediaPlayer.NaturalDuration.TimeSpan;
            Slider.Maximum = timeSpan.TotalSeconds;
            Slider.SmallChange = 1;
            Slider.LargeChange = Math.Min(10, timeSpan.Seconds / 10);

            VideoTimer.Interval = TimeSpan.FromMilliseconds(1000);
            VideoTimer.Start();
        }

        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, 0, (int)Slider.Value, 0);
            mediaPlayer.Position = timeSpan;
        }
        private void ControlButton()
        {
            Play.Visibility = IsPlaying ? Visibility.Collapsed : Visibility.Visible;
            Pause.Visibility = IsPlaying ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
