using Google.Protobuf.WellKnownTypes;
using Interface;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace DAL
{
    public class LoginDBOperation : ILoginInterface
    {

        public IDatabaseOperationInterface dBHelper { get; set; }
        public static readonly LoginDBOperation Instance = new LoginDBOperation();

        private LoginDBOperation()
        {

        }
        public Tuple<bool, Profile> OnCreate(string username, string password, string name)
        {
            Profile currrentProfile = Utils.GetProfileByIdx(1);
            try
            {


                dBHelper.OpenConnection();
                dBHelper.ExecuteQuery(String.Format("INSERT INTO user_table(nick,lid) VALUES(\"{0}\",\"{1}\");",
                 name, username));
                DbDataReader result = dBHelper.ExecuteReaderQuery("SELECT idx,nick, profile_pic FROM user_table WHERE lid = \"" + username + "\"" + "AND nick = \"" + name + "\";");
                if (result.Read())
                {
                    currrentProfile = new Profile()
                    {
                        ChannelIndex = Convert.ToInt32(result[0]),
                        ChannelName = result[1].ToString(),
                        ChannelArt = Utils.CreateProfileImage(result[2].ToString())
                    };
                    result.Close();
                    dBHelper.ExecuteQuery(String.Format("INSERT INTO Login(idx, uid, pwd) VALUES(\"{0}\", {1}, \"{2}\");",
                                     username, currrentProfile.ChannelIndex, password));
                }


                return Tuple.Create(true, currrentProfile);
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, currrentProfile);

            }
            finally { dBHelper.CloseConnection(); }
        }

        public Tuple<bool, Profile> OnLogin(string username, string password)
        {
            Profile currrentProfile = Utils.GetProfileByIdx(1);
            bool result = false;
            try
            {
                dBHelper.OpenConnection();
                DbDataReader result1 = dBHelper.ExecuteReaderQuery("SELECT uid FROM Login WHERE idx = \"" + username + "\"" + "AND pwd = \"" + password + "\";");
                if (result1.Read())
                {
                    result = true;
                    currrentProfile = Utils.GetProfileByIdx(Convert.ToInt32(result1[0]));
                }
                else
                {
                    result = false;
                }
                result1.Close();
                return Tuple.Create(result, currrentProfile); ;
            }
            catch (Exception ex)
            {
                return Tuple.Create(false, currrentProfile); ;

            }
            finally { dBHelper.CloseConnection(); } 
        }
    }
}
