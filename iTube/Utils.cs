using DAL;
using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace iTube
{
    static class Utils
    {
        public static Profile GetProfileByIdx(int uid)
        {
            DBHelper dbHelper = new DBHelper();
            dbHelper.OpenConnection();
            MySqlDataReader result = dbHelper.ExecuteReaderQuery("SELECT nick, profile_pic FROM user_table WHERE idx = " + uid + ";");
            while (result.Read())
            {
                Profile profile = new Profile()
                {
                    ChannelIndex = uid,
                    ChannelName = result[0].ToString(),
                    ChannelArt = CreateProfileImage(result[1].ToString())
                };
                result.Close();
                return profile;
            }
            dbHelper.CloseConnection();
            return null;
        }

        public static BitmapImage CreateProfileImage(string filename)
        {
            BitmapImage profileImage = new BitmapImage();
            profileImage.BeginInit();
            if (filename.Trim().Length == 0)
            {
                profileImage.UriSource = new Uri("pack://application:,,,/iTube;component/Resource/ic_person.png", UriKind.RelativeOrAbsolute);
            }

            else
            {
                profileImage.UriSource = new Uri(App.SERVER_URI + "/profile/" + filename);
            }
            profileImage.EndInit();
            return profileImage;
        }
    }
}
