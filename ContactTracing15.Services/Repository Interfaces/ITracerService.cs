using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;

namespace ContactTracing15.Services
{
    public interface ITracerService
    {
        IEnumerable<Tracer> GetAllTracers();
        Tracer GetTracer(int id);
        Tracer GetTracer(string username);
        Tracer Add(Tracer newTracer);
        Tracer Update(Tracer updatedTracer);
        Tracer Delete(int id);
        IEnumerable<Tracer> Search(string searchTerm);
        void Save();
        Tracer GetNextTracer();
        Tracer GetNextTracer(int id);
        IEnumerable<Case> GetAssignedCases(int id);

    }
}
