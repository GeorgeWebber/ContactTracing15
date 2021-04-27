using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;

namespace ContactTracing15.Services
{
    public interface ITracingCentreService
    {
        IEnumerable<TracingCentreStats> GetAllTracingCentreStats();
    }
}