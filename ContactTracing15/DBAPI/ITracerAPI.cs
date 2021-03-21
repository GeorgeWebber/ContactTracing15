using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ContactTracing15.DBAPI
{
    /// <summary>
    /// An interface to specify what commands should be available to contact tracer users
    /// </summary>
    interface ITracerAPI
    {
        // TODO : edit return method to specify a list of model type (model not created yet)
        /// <summary>
        /// Get a list of active cases corresponding to a tester ID
        /// </summary>
        /// <returns>A Task object containing a list of ** type </returns>
        public Task GetActiveCases(int testerID);
    }
}
