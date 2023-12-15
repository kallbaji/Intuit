using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
   public class DBHelper
    {
        
        

        public DBHelper(string id, string pw, string host, string name)
        {
            //Connection = new MySqlConnection();
            //DB_ID = id;
            //DB_PW = pw;
            //DB_HOST = host;
            //DB_NAME = name;

            //Connection.ConnectionString = String.Format("server={0};database={1};uid={2};password={3};", DB_HOST, DB_NAME, DB_ID, DB_PW);
        }

        public void OpenConnection() {
           // Connection.Open();
        }

        public void CloseConnection()
        {
           // Connection.Close();
        }
        
        public string ExecuteReaderQuery(string query)
        {
            //Command = new MySqlCommand(query, Connection);
           // MySqlDataReader Result = Command.ExecuteReader();
            //return Result;
            return null;
        }

        public int ExecuteQuery(string query)
        {
            //Command = new MySqlCommand(query, Connection);
            //int Result = Command.ExecuteNonQuery();
            //return Result;
            return 0;
        }
    }
}
