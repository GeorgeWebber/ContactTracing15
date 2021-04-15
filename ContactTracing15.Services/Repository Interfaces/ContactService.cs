using ContactTracing15.Models;
using ContactTracing15.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{ 
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public  ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
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
    }
}
