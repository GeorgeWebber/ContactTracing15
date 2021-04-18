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
        public TracerService(ITracerRepository tracerRepository)
        {
            _tracerResitory = tracerRepository;
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
            //return _tracerResitory.GetTracerWithLeastCases();
            return _tracerResitory.GetTracer(1);
        }

        Tracer ITracerService.GetTracer(int id)
        {
            return _tracerResitory.GetTracer(id);
        }

        Tracer ITracerService.GetTracer(string username) 
        {
            return _tracerResitory.GetTracer(username);
        }

        void ITracerService.Save() 
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
            var tracer = _tracerResitory.GetTracer(id);
            return tracer.Cases.Where(x => x.Traced == false);
        }
    }
}
