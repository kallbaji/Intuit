using DAL;
using iTube.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace iTube.ViewModel
{
    public class ListViewModel
    {
        public ObservableCollection<Video> VideoList { get; set; }
        private DBHelper dbHelper;

        public ListViewModel()
        {
            VideoList = new ObservableCollection<Video>();
            dbHelper = new DBHelper("itube", "itube",App.SHORT_SERVER_URI, "itube");
            GetVideo();
        }

        public void GetVideo()
        {
            
            dbHelper.OpenConnection();

            string result = dbHelper.ExecuteReaderQuery(
                "SELECT " +
                "idx, title, uploader, thumbnail, video, views, date FROM video;" 
                );
            if (result != null)
            {
                while (result!=null)
                {
                    Video video = new Video()
                    {
                        //ChannelProfile = Utils.GetProfileByIdx(Convert.ToInt32(result[2].ToString())),

                        //Index = Convert.ToInt32(result[0].ToString()),
                        //Title = result[1].ToString(),
                        //Thumbnail = new BitmapImage(new Uri(result[3].ToString())),
                        //VideoLink = result[4].ToString(),
                        //Views = Convert.ToInt32(result[5]),
                        //Date = (DateTime)result[6]
                    };



                    VideoList.Add(video);
                }

              //  result.Close();
            }
            dbHelper.CloseConnection();
        }
    }
}
