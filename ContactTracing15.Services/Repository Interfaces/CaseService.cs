using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IContactService _contactService;
        private readonly ITracerService _tracerService;
        public CaseService(ICaseRepository caseRepository, IContactService contactService, ITracerService tracerService)
        {
            _caseRepository = caseRepository;
            _contactService = contactService;
            _tracerService = tracerService;
        }

        Case ICaseService.Add(Case newCase)
        {
            return _caseRepository.Add(newCase);
        }

        Case ICaseService.Delete(int id)
        {
            return _caseRepository.Delete(id);
        }

        IEnumerable<Case> ICaseService.GetAllCases()
        {
            return _caseRepository.GetAllCases();
        }

        Case ICaseService.GetCase(int id)
        {
            return _caseRepository.GetCase(id);
        }

        void ICaseService.Save()
        {
            _caseRepository.Save();
        }

        IEnumerable<Case> ICaseService.Search(string searchTerm)
        {
            return _caseRepository.Search(searchTerm);
        }

        Case ICaseService.Update(Case updatedCase)
        {
            return _caseRepository.Update(updatedCase);
        }
        IEnumerable<Contact> ICaseService.GetTracedContacts(int id)  //TODO reimplement this
        {
            return _contactService.GetAllContacts().Where(x => x.CaseID == id);
        }

        IEnumerable<string> ICaseService.GetRecentPostcodes(int days) //TODO reimplement this
        {

            DateTime from_ = DateTime.Now.AddDays(-days);
            DateTime to_ = DateTime.Now;

            return _caseRepository.GetAllCases().Where(u => u.AddedDate > from_ && u.AddedDate < to_).Select(u => u.Postcode).ToList();

            //return _caseRepository.GetpostcodesByDate(DateTime.Now.AddDays(-days), DateTime.Now);
        }

        Case ICaseService.AssignAndAdd(Case newCase)
        {
            newCase.TracerID = _tracerService.GetNextTracer().TracerID;
            return _caseRepository.Add(newCase);
        }

        Case ICaseService.Drop(int caseId)
        {
            var dropCase = _caseRepository.GetCase(caseId);
            dropCase.TracerID = _tracerService.GetNextTracer().TracerID;
            return _caseRepository.Update(dropCase);
        }

        Case ICaseService.Complete(int caseId)
        {
            var completeCase = _caseRepository.GetCase(caseId);
            completeCase.Traced = true;
            return _caseRepository.Update(completeCase);
        }

        //TODO:  Returns the average time taken to contact trace a case in the last 28 days
        TimeSpan ICaseService.AverageTraceTimeLast28Days()
        {
            return DateTime.Now - DateTime.Now.AddDays(-1);
        }

        //TODO:  Returns the percentage of cases successfully traced in the last 28 days (perhaps factoring in dropped cases?)
        double ICaseService.PercentageCasesReachedLast28Days()
        {
            return 50;
        }



        //TODO: return the total number of cases that have been successfully traced
        int ICaseService.TotalCasesReached()
        {
            return 0;
        }

        //TODO: return the total number of positive cases that have been reached ever
        int ICaseService.TotalCasesEver()
        {
            return 0;
        }

    }
}
