using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.DBAPI
{
    /// <summary>
    /// An interface specifying the method available to government agents using the system
    /// </summary>
    interface IGovAgentAPI
    {
        /// <summary>
        /// A method to access the allowed data in the form of an SqlDataReader object
        /// </summary>
        /// <returns>SqlDataReader object containing the data government agents are allowed to access</returns>
        public SqlDataReader RetrieveDataset();


    }
}
