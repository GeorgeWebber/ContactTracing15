using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.DBAPI
{
    /// <summary>
    /// A first implementation of the IDB interface
    /// </summary>
    public class DB : IDB
    {
        public DB(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }
        public SqlConnection Connection { get; }
    }
}
