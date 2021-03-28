using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    /// <summary>
    /// Interface specifying how the Tracer database table is to be interacted with.
    /// </summary>
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
