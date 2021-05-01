using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;

namespace ContactTracing15.Services
{
    public class PrivacyService : IPrivacyService
    {
        private readonly ICaseService _caseService;
        private readonly IContactService _contactService;

        public PrivacyService(ICaseService caseService, IContactService contactService)
        {
            _caseService = caseService;
            _contactService = contactService;
        }

        public void CleanOldRecords()
        {
            var oldThreshold = DateTime.Now.AddDays(-14);
            foreach (Case _case in _caseService.GetOldCases(oldThreshold))
            {
                _caseService.RemovePersonalData(_case.CaseID);
            }
            foreach(Contact _contact in _contactService.GetOldContacts(oldThreshold))
            {
                _contactService.RemovePersonalData(_contact.ContactID);
            }
        }
    }
}
