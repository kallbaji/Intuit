using Google.Protobuf.WellKnownTypes;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class VideoInfoDBOperation : IViedoInfoInterface
    {

        public IDatabaseOperationInterface dbHelper { get; set; }
        public IVideoOperationInterface downloadHelper { get; set; }
        private VideoInfoDBOperation() { }

        public static readonly VideoInfoDBOperation Instance = new VideoInfoDBOperation();

        public async Task<bool> DownloadFile(string title, string fileName)
        {

            return await downloadHelper.DownloadFile(title, fileName);

        }

        public List<Comment> GetComments(int videoIndex)
        {

            List<Comment> comments = new List<Comment>();
            try
            {


                DbDataReader result = dbHelper.ExecuteReaderQuery("SELECT idx, uid, content, date FROM comment WHERE vid = " + videoIndex + ";");

                while (result.Read())
                {
                    Comment comment = new Comment()
                    {
                        Index = Convert.ToInt32(result[0].ToString()),
                        Content = result[2].ToString(),
                        ChannelProfile = Utils.GetProfileByIdx(Convert.ToInt32(result[1].ToString())),
                        Date = (DateTime)result[3]
                    };

                    comments.Add(comment);

                }
                result.Close();
            }
            catch (Exception ex)
            {


            }
            return comments;
        }

        public void AddViewCount(int views, int videoIndex)
        {
            try
            {
                dbHelper.ExecuteQuery("UPDATE video SET views = " + (views + 1) + " WHERE idx = " + videoIndex + ";");

            }
            catch (Exception ex)
            {


            }
        }
        public void DeleteComment(int cid)
        {

            dbHelper.ExecuteQuery("DELETE FROM comment WHERE idx = " + cid + ";");


        }
        public void PostComment(int uid, string content, int videoIndex)
        {

            dbHelper.ExecuteQuery(String.Format("INSERT INTO comment(vid, uid, content,date) VALUES({0}, {1}, \"{2}\",\"{3}\");",
                videoIndex, uid, content, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss")));


        }

        public List<Rate> GetRate(int vid)
        {
            List<Rate> ret = new List<Rate>();
            DbDataReader result = dbHelper.ExecuteReaderQuery("SELECT uid, score FROM rate WHERE vid = " + vid + ";");
            while (result.Read())
            {
                RateEnum rate = (RateEnum)Convert.ToInt32(result[1].ToString());


                int uid = Convert.ToInt32(result[0].ToString());

                ret.Add(new Rate() { UID = uid, RateEnum = rate });

            }
            result.Close();
            return ret;
        }

        public RateEnum GetRate(int vid, int uid)
        {
            RateEnum rate = RateEnum.NONE;
            dbHelper.OpenConnection();
            DbDataReader result = dbHelper.ExecuteReaderQuery("SELECT score FROM rate WHERE vid = " + vid + " AND  uid = " + uid + ";");
            if (result.Read())
            {
                 rate = (RateEnum)Convert.ToInt32(result[0].ToString());

            }
            result.Close();
            dbHelper.CloseConnection();
            return rate;
        }

        public void DeleteRate(int uid, int vid)
        {
            dbHelper.ExecuteQuery(String.Format("DELETE FROM rate WHERE uid = '{0}' AND vid = '{1}';", uid, vid));
        }
        public void InsertRate(int uid, int vid, int rate)
        {

        }
    }
}
