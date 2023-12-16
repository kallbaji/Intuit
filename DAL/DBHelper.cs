using MySql.Data.MySqlClient;
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
        private MySqlConnection Connection;
        private MySqlCommand Command;

        private string DB_ID="admin";
        private string DB_PW="gaurishankarpandey";
        private string DB_HOST= "intuit.clu1umb27qya.ap-southeast-2.rds.amazonaws.com";
        private string DB_NAME="iTube";


        public DBHelper()
        {
            Connection = new MySqlConnection();

            var connectionStringBuilder = new MySqlConnectionStringBuilder
            {
                Server =DB_HOST,
                UserID = DB_ID,
                Password = DB_PW,
                Database = DB_NAME,
                Port=3306
                
            };
            Connection.ConnectionString = connectionStringBuilder.ToString();
        }

        public void OpenConnection()
        {
            Connection.Open();
        }

        public void CloseConnection()
        {
            Connection.Close();
        }

        public MySqlDataReader ExecuteReaderQuery(string query)
        {
            Command = new MySqlCommand(query, Connection);
            MySqlDataReader Result = Command.ExecuteReader();
            return Result;
        }

        public int ExecuteQuery(string query)
        {
            Command = new MySqlCommand(query, Connection);
            int Result = Command.ExecuteNonQuery();
            return Result;
        }
    }
}
