using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;
using System.Linq;

namespace ContactTracing15.Services
{
    public class TesterService : ITesterService
    {
        private readonly ITesterRepository _testerRepository;
        public TesterService(ITesterRepository testerRepository)
        {
            _testerRepository = testerRepository;
        }
        Tester ITesterService.Add(Tester newTester)
        {
            return _testerRepository.Add(newTester);
        }

        Tester ITesterService.Delete(int id)
        {
            return _testerRepository.Delete(id);
        }

        IEnumerable<Tester> ITesterService.GetAllTesters()
        {
            return _testerRepository.GetAllTesters();
        }

        Tester ITesterService.GetTester(int id)
        {
            return _testerRepository.GetTester(id);
        }
        Tester ITesterService.GetTester(string username) //TODO replace with SQL
        {
            return _testerRepository.GetAllTesters().Single(x => x.Username == username);
        }

        void ITesterService.Save()
        {
            _testerRepository.Save();
        }

        IEnumerable<Tester> ITesterService.Search(string searchTerm)
        {
            return _testerRepository.Search(searchTerm);
        }

        Tester ITesterService.Update(Tester updatedTester)
        {
            return _testerRepository.Update(updatedTester);
        }
    }
}
