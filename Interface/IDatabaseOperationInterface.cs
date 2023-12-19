using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface
{
    public interface IDatabaseOperationInterface
    {
        public void OpenConnection();
        void CloseConnection();

        DbDataReader ExecuteReaderQuery(string query);

        int ExecuteQuery(string query);
    }
}
