using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;

namespace ContactTracing15.Services
{
    public class TestingCentreService : ITestingCentreService
    {
        private readonly ITestingCentreRepository _testingCentreRepository;

        public TestingCentreService(ITestingCentreRepository testingCentreRepository)
        {
            _testingCentreRepository = testingCentreRepository;
        }
        TestingCentre ITestingCentreService.Add(TestingCentre newTestingCentre)
        {
            return _testingCentreRepository.Add(newTestingCentre);
        }

        TestingCentre ITestingCentreService.Delete(int id)
        {
            return _testingCentreRepository.Delete(id);
        }

        IEnumerable<TestingCentre> ITestingCentreService.GetAllTestingCentres()
        {
            return _testingCentreRepository.GetAllTestingCentres();
        }

        TestingCentre ITestingCentreService.GetTestingCentre(int id)
        {
            return _testingCentreRepository.GetTestingCentre(id);
        }

        void ITestingCentreService.Save()
        {
            _testingCentreRepository.Save();
        }

        TestingCentre ITestingCentreService.Update(TestingCentre updatedTestingCentre)
        {
            return _testingCentreRepository.Update(updatedTestingCentre);
        }
    }
}
