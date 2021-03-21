using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ContactTracing15.DBAPI
{
    /// <summary>
    /// An interface to specify what commands should be available to the testing facility users
    /// </summary>
    interface ITesterAPI
    {
        //TODO: add type parameter to the return type, specifying the model returned
        /// <summary>
        /// Add data on a positive case to the database
        /// </summary>
        /// <returns>Task object specifying the result of the operation</returns>
        public Task AddCase();
    }

     
}
