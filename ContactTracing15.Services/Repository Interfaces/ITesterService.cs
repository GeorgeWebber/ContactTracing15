using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;

namespace ContactTracing15.Services
{
    public interface ITesterService
    {
        IEnumerable<Tester> GetAllTesters();
        Tester GetTester(int id);
        Tester GetTester(string username);
        Tester Add(Tester newTester);
        Tester Update(Tester updatedTester);
        Tester Delete(int id);
        IEnumerable<Tester> Search(string searchTerm);
        void Save();
    }
}
