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

        /// <summary>
        /// Add data on a positive case to the database
        /// *Should take a parameter - this parameter will be a model type I believe - George*
        /// </summary>
        /// <returns>Task object specifying the result of the operation</returns>
        public Task AddCase();
    }

     
}
