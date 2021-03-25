using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public interface ITesterRepository
    {
        IEnumerable<Tester> GetAllTesters();
        Tester GetTester(int id);
        Tester Add(Tester newTester);
        Tester Update(Tester updatedTester);
        Tester Delete(int id);
        IEnumerable<Tester> Search(string searchTerm);
        void Save();
    }
}
