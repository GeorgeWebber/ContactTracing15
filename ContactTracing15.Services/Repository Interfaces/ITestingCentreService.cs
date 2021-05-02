using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;

namespace ContactTracing15.Services
{
    public interface ITestingCentreService
    {
        IEnumerable<TestingCentre> GetAllTestingCentres();
        TestingCentre GetTestingCentre(int id);
        TestingCentre Add(TestingCentre newTestingCentre);
        TestingCentre Update(TestingCentre updatedTestingCentre);
        TestingCentre Delete(int id);

        void ExportAsExcel(string folderPath);
        void Save();
    }
}
