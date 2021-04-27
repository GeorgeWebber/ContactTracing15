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
        private readonly ICaseRepository _caseRepository;
        public TracerService(ITracerRepository tracerRepository, ICaseRepository caseRepository)
        {
            _tracerResitory = tracerRepository;
            _caseRepository = caseRepository;
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

        Tracer ITracerService.GetNextTracer()  
        {
            return _tracerResitory.GetTracerWithLeastCases().First();
        }
        Tracer ITracerService.GetNextTracer(int id)  
        {
            var tracer = _tracerResitory.GetTracerWithLeastCases().First(x => x.TracerID != id);
            return tracer ?? _tracerResitory.GetTracer(id);
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
            return _caseRepository.GetAllCases().Where(x => (x.Traced == false && x.TracerID == id));
        }
    }
}
