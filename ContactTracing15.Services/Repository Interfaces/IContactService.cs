using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;

namespace ContactTracing15.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> GetAllContacts();
        Contact GetContact(int id);
        Contact Add(Contact newContact);
        Contact Update(Contact updatedContact);
        Contact Delete(int id);
        IEnumerable<Contact> Search(string searchTerm);
        void Save();
    }
}
