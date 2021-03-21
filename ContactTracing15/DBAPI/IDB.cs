using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.DBAPI
{
    /// <summary>
    /// Interface specifying a "database" type object. Just has one method to provide a SQLConnection object to the database
    /// </summary>
    interface IDB
    {
        /// <summary>
        /// A connection to the database object
        /// </summary>
        public SqlConnection Connection { get; }
    }
}
