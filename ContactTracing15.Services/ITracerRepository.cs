using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public interface ITracerRepository
    {
        IEnumerable<Tracer> GetAllTracers();
        Tracer GetTracer(int id);
        Tracer Add(Tracer newTracer);
        Tracer Update(Tracer updatedTracer);
        Tracer Delete(int id);
        IEnumerable<Tracer> Search(string searchTerm);
        void Save();
    }
}
