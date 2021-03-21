using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.DBAPI
{
    public class TracerAPI : ITracerAPI
    {

        public Task<bool> AddNewContact()
        {
            throw new NotImplementedException();
        }

        public Task AssignNewCase(int testerID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditContact()
        {
            throw new NotImplementedException();
        }

        public Task GetActiveCases(int testerID)
        {
            throw new NotImplementedException();
        }
    }
}
