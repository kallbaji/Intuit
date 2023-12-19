using DAL;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DAL
{
   public static class Utils
    {
        public static Profile GetProfileByIdx(int uid)
        {
            DBHelper dbHelper = new DBHelper();
            Profile profile = null;
            try
            {

                
            dbHelper.OpenConnection();
            DbDataReader result = dbHelper.ExecuteReaderQuery("SELECT nick, profile_pic FROM user_table WHERE idx = " + uid + ";");
                if (result.Read())
                {
                    profile = new Profile()
                    {
                        ChannelIndex = uid,
                        ChannelName = result[0].ToString(),
                        ChannelArt = CreateProfileImage(result[1].ToString())
                    };
                   
                }
                result.Close();
                dbHelper.CloseConnection();
                return profile;
                }
            catch (Exception)
            {

                  return profile; ;
            }
           
        }

        public static BitmapImage CreateProfileImage(string filename)
        {
            BitmapImage profileImage = new BitmapImage();
            profileImage.BeginInit();
            if (filename.Trim().Length == 0)
            {
                profileImage.UriSource = new Uri("pack://application:,,,/Utility;component/Resource/ic_person.png", UriKind.RelativeOrAbsolute);
            }

            else
            {
                profileImage.UriSource = new Uri(filename);
            }
            profileImage.EndInit();
            return profileImage;
        }
    }
}
