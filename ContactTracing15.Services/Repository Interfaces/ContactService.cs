using ContactTracing15.Models;
using ContactTracing15.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{ 
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICaseRepository _caseRepository;
        private readonly ITracerService _tracerService;

        public  ContactService(IContactRepository contactRepository, ICaseRepository caseRepository)
        {
            _contactRepository = contactRepository;
            _caseRepository = caseRepository;
        }
        Contact IContactService.Add(Contact newContact)
        {
            return _contactRepository.Add(newContact);
        }

        Contact IContactService.Delete(int id)
        {
            return _contactRepository.Delete(id);
        }

        IEnumerable<Contact> IContactService.GetAllContacts()
        {
            return _contactRepository.GetAllContacts();
        }

        Contact IContactService.GetContact(int id)
        {
            return _contactRepository.GetContact(id);
        }

        void IContactService.Save()
        {
            _contactRepository.Save();
        }

        IEnumerable<Contact> IContactService.Search(string searchTerm)
        {
            return _contactRepository.Search(searchTerm);
        }

        Contact IContactService.Update(Contact updatedContact)
        {
            return _contactRepository.Update(updatedContact);
        }

        int IContactService.TotalContactsReached()
        {
            return _contactRepository.GetAllContacts().Where(x => x.ContactedDate != null).ToList().Count();
        }

        double IContactService.AverageContactsPerCaseLast28Days()
        {
            int contacts = _contactRepository.GetContactsByDate(DateTime.Now, DateTime.Now.AddDays(-28)).ToList().Count();
            int cases = _caseRepository.GetCasesByDate(DateTime.Now, DateTime.Now.AddDays(-28)).Where(x => x.Traced).ToList().Count();
            if (cases == 0) { return 0; }
            return (double) contacts / cases;
        }
    }
}
