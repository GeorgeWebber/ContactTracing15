using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.DBAPI
{
    public class DB : IDB
    {
        public Task<bool> ExecuteCommand(string commandString)
        {
            throw new NotImplementedException();
        }
    }
}
