using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public interface IContactRepository
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
