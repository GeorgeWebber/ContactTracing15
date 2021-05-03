using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;
using System.Data;

namespace ContactTracing15.Services
{
    public interface ITestingCentreService
    {
        IEnumerable<TestingCentre> GetAllTestingCentres();
        TestingCentre GetTestingCentre(int id);
        TestingCentre Add(TestingCentre newTestingCentre);
        TestingCentre Update(TestingCentre updatedTestingCentre);
        TestingCentre Delete(int id);

        DataTable ExportAsExcel();
        void Save();
    }
}
