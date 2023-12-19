using Google.Protobuf.WellKnownTypes;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DAL
{
    public class ViedoDBOperation : IVideoInterface
    {
        private ViedoDBOperation() { }

        public IDatabaseOperationInterface dbHelper { get; set; }
        public static readonly ViedoDBOperation Instance = new ViedoDBOperation();
        public List<Video> GetVideos()
        {
            List<Video> VideoList = new List<Video>();

            try
            {


                dbHelper.OpenConnection();

                DbDataReader result = dbHelper.ExecuteReaderQuery(
                    "SELECT " +
                    "idx, title, uploader, thumbnail, video, views, date_upload FROM video;"
                    );
                while (result.Read())
                {
                    Video video = new Video()
                    {
                        ChannelProfile = Utils.GetProfileByIdx(Convert.ToInt32(result[2])),

                        Index = Convert.ToInt32(result[0].ToString()),
                        Title = result[1].ToString(),
                        Thumbnail = new BitmapImage(new Uri(result[3].ToString())),
                        VideoLink = result[4].ToString(),
                        Views = Convert.ToInt32(result[5]),
                        Date = (DateTime)result[6]
                    };



                    VideoList.Add(video);

                }
                result.Close();
            }
            catch (Exception)
            {


            }
            finally { dbHelper.CloseConnection(); }
            return VideoList;
        }

        public List<Video> GetVideos(int userIndex)
        {
            List<Video> VideoList = new List<Video>();
            try
            {


                dbHelper.OpenConnection();

                DbDataReader result = dbHelper.ExecuteReaderQuery(
                "SELECT " +
                    "idx, title, uploader, thumbnail, video, views, date_upload FROM video WHERE uploader= " + userIndex + ";"
                    );
                while (result.Read())
                {
                    Video video = new Video()
                    {
                        ChannelProfile = Utils.GetProfileByIdx(Convert.ToInt32(result[2])),

                        Index = Convert.ToInt32(result[0].ToString()),
                        Title = result[1].ToString(),
                        Thumbnail = new BitmapImage(new Uri(result[3].ToString())),
                        VideoLink = result[4].ToString(),
                        Views = Convert.ToInt32(result[5]),
                        Date = (DateTime)result[6]
                    };



                    VideoList.Add(video);

                }
                result.Close();


            }
            catch (Exception)
            {


            }
            finally { dbHelper.CloseConnection(); }

            return VideoList;
        }
    }
}

