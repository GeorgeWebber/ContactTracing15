using ContactTracing15.Models;
using System;
using System.Collections.Generic;

namespace ContactTracing15.Services
{
    /// <summary>
    /// Interface specifying how the Case database table is to be interacted with.
    /// </summary>
    public interface ICaseRepository
    {
        IEnumerable<Case> GetAllCases();
        Case GetCase(int id);
        Case Add(Case newCase);
        Case Update(Case updatedCase);
        Case Delete(int id);
        IEnumerable<Case> Search(string searchTerm);
        void Save();
    }
}
