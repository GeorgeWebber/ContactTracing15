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
        private readonly IContactRepository _contactRepository;
        private readonly ITracerService _tracerService;
        private readonly IEmailService _emailService;
        public CaseService(ICaseRepository caseRepository, IContactRepository contactRepository, ITracerService tracerService, IEmailService emailService)
        {
            _caseRepository = caseRepository;
            _contactRepository = contactRepository;
            _tracerService = tracerService;
            _emailService = emailService;
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

        public IEnumerable<Contact> GetTracedContacts(int id)
        {
            return _caseRepository.GetCase(id).Contacts;
        }

        public IEnumerable<Case> GetOldCases(DateTime threshold) //TODO redo this with SQL query
        {
            return _caseRepository.GetAllCases().Where(x => x.RemovedDate == null && x.AddedDate <= threshold).ToList();
        }
        
        public Case RemovePersonalData(int id) //TODO, perhaps do this with SQL if it's faster, otherwise this is fine as is
        {
            var _case = _caseRepository.GetCase(id);
            _case.Forename = null;
            _case.Surname = null;
            _case.Email = null;
            _case.Phone = null;
            _case.Phone2 = null;
            _case.TracerID = null;
            _case.RemovedDate = DateTime.Now;
            return _caseRepository.Update(_case);
        }

        IEnumerable<string> ICaseService.GetPostcodesByRecentDays(DateTime from_, DateTime to_)
        {
            return _caseRepository.GetAllCases().Where(u => u.AddedDate > from_ && u.AddedDate < to_).Select(u => u.Postcode).ToList();
            //return _caseRepository.GetpostcodesByDate(from_, to_);
        }

        Case ICaseService.AssignAndAdd(Case newCase)
        {
            newCase.TracerID = _tracerService.GetNextTracer().TracerID;
            return _caseRepository.Add(newCase);
        }

        Case ICaseService.Drop(int caseId, int tracerId)
        {
            var dropCase = _caseRepository.GetCase(caseId);
            if (DateTime.Now.AddDays(-7) > dropCase.TestDate || dropCase.DroppedNum >= 3)
            {
                dropCase.TracerID = null;
                dropCase.Dropped = true;
            }
            else
            {
                dropCase.TracerID = _tracerService.GetNextTracer(tracerId).TracerID;
                dropCase.DroppedNum++;
            }
            return _caseRepository.Update(dropCase);
        }

        Case ICaseService.Complete(int caseId, int tracerId)
        {
            var completeCase = _caseRepository.GetCase(caseId);
            completeCase.Traced = true;
            foreach (Contact contact in GetTracedContacts(caseId))
            {
                var _contact = _contactRepository.GetContact(contact.ContactID);
                _contact.TracedDate = DateTime.Now;
                _contactRepository.Update(_contact);
                if (contact.Email != null) { 
                    _emailService.ContactByEmail(contact); }
            }
            return _caseRepository.Update(completeCase);
        }

        //TODO:  Returns the average time taken to contact trace a case in the last 28 days
        TimeSpan ICaseService.AverageTraceTimeLast28Days()
        {
            return DateTime.Now - DateTime.Now.AddDays(-1);
        }

        double ICaseService.PercentageCasesReachedLast28Days()
        {
            int cases = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now).ToList().Count();
            if (cases == 0) { return 0; }
            int traced = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now).Where(x => x.Traced).ToList().Count();
            return  (double) traced / cases * 100;
        }


        int ICaseService.TotalCasesReached()
        {
            return _caseRepository.GetAllCases().Where(x => x.Traced).ToList().Count();
        }

        int ICaseService.TotalCasesEver()
        {
            return  _caseRepository.GetAllCases().ToList().Count();
        }

    }
}
