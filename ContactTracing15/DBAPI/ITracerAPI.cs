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

        // TODO : edit return method to specify model type
        /// <summary>
        /// Assign a new case to the contact tracer with a given ID
        /// </summary>
        /// <returns>A Task object containing a model containing the new case's data </returns>
        public Task AssignNewCase(int testerID);

        // TODO : edit parameter to specify model type
        /// <summary>
        /// Add a new contact to the contact table of the database
        /// </summary>
        /// <returns>True if data is successfully added, False otherwise.</returns>
        public Task<Boolean> AddNewContact();

        // TODO : edit parameter to specify model type
        /// <summary>
        /// Edit a contact's data in the database
        /// </summary>
        /// <returns>Task object with True if data successfully modified, False otherwise. </returns>
        public Task<Boolean> EditContact();

    }
}
