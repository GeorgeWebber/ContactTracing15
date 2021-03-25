using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public interface ITestingCentreRepository
    {
        IEnumerable<TestingCentre> GetAllTestingCentres();
        TestingCentre GetTestingCentre(int id);
        TestingCentre Add(TestingCentre newTestingCentre);
        TestingCentre Update(TestingCentre updatedTestingCentre);
        TestingCentre Delete(int id);
        void Save();
    }
}
