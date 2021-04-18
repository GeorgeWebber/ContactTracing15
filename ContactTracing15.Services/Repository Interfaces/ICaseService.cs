using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;

namespace ContactTracing15.Services
{
    public interface ICaseService
    {

        IEnumerable<Case> GetAllCases();
        Case GetCase(int id);
        Case Add(Case newCase);
        Case Update(Case updatedCase);
        Case Delete(int id);
        IEnumerable<Case> Search(string searchTerm);
        void Save();
        IEnumerable<Contact> GetTracedContacts(int id);

        // Returns an enumerable of postcodes of cases registered in the past certain # of days
        IEnumerable<string> GetRecentPostcodes(int days);

        Case AssignAndAdd(Case newCase);
    }
}
