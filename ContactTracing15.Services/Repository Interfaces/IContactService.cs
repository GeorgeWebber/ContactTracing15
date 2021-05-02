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
        void ExportAsExcel(string folderPath);
        IEnumerable<Contact> Search(string searchTerm);
        IEnumerable<Contact> GetOldContacts(DateTime threshold);
        Contact RemovePersonalData(int id);
        void Save();
        int TotalContactsReached();
        double AverageContactsPerCaseLast28Days();

    }
}
