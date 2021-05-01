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
        public IEnumerable<Contact> GetOldContacts(DateTime threshold)   
        {
            var low_bar = new DateTime(1000, 1, 1);
            return _contactRepository.GetContactsByDate(low_bar, threshold).Where(x => x.RemovedDate == null).ToList();
        }

        public Contact RemovePersonalData(int id)  //TODO, perhaps do this with SQL if it's faster, otherwise this is fine as is
        {
            var _contact = _contactRepository.GetContact(id);
            _contact.Forename = null;
            _contact.Surname = null;
            _contact.Email = null;
            _contact.Phone = null;
            _contact.RemovedDate = DateTime.Now;
            return _contactRepository.Update(_contact);
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
            int contacts = _contactRepository.GetContactsByDate(DateTime.Now.AddDays(-28), DateTime.Now).ToList().Count();
            int cases = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now).Where(x => x.Traced).ToList().Count();
            if (cases == 0) { return 0; }
            return (double) contacts / cases;
        }
    }
}
