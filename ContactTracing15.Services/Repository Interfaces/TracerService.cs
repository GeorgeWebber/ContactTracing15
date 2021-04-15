using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;
using System.Linq;

namespace ContactTracing15.Services
{
   
    public class TracerService : ITracerService
    {
        private readonly ITracerRepository _tracerResitory;
        private readonly ICaseService _caseService;
        public TracerService(ITracerRepository tracerRepository, ICaseService caseService)
        {
            _tracerResitory = tracerRepository;
            _caseService = caseService;
        }
        Tracer ITracerService.Add(Tracer newTracer)
        {
            return _tracerResitory.Add(newTracer);
        }

        Tracer ITracerService.Delete(int id)
        {
            return _tracerResitory.Delete(id);
        }

        IEnumerable<Tracer> ITracerService.GetAllTracers()
        {
            return _tracerResitory.GetAllTracers();
        }

        Tracer ITracerService.GetNextTracer()  //TODO, do this with SQL commands in the repository
        {
            return _tracerResitory.GetAllTracers().OrderBy(x => x.Cases.Count()).First();
        }

        Tracer ITracerService.GetTracer(int id)
        {
            return _tracerResitory.GetTracer(id);
        }

        Tracer ITracerService.GetTracer(string username) //TODO, do this with SQL
        {
            return _tracerResitory.GetAllTracers().Single(x => x.Username == username);
        }

        void ITracerService.Save() //I don't actually know what this does but ok
        {
            _tracerResitory.Save();
        }

        IEnumerable<Tracer> ITracerService.Search(string searchTerm)
        {
            return _tracerResitory.Search(searchTerm);
        }

        Tracer ITracerService.Update(Tracer updatedTracer)
        {
            return _tracerResitory.Update(updatedTracer);
        }

        IEnumerable<Case> ITracerService.GetAssignedCases(int id)  //TODO reimplement this
        {
            return _caseService.GetAllCases().Where(x => x.TracerID == id);
        }
    }
}
