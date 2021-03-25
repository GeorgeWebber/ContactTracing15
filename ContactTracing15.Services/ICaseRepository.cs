using ContactTracing15.Models;
using System;
using System.Collections.Generic;

namespace ContactTracing15.Services
{
    public interface ICaseRepository
    {
        IEnumerable<Case> GetAllCases();
    }
}
