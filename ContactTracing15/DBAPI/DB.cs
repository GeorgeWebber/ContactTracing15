using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.DBAPI
{
    public class DB : IDB
    {
        public DB(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }
        public SqlConnection Connection { get; }
    }
}
