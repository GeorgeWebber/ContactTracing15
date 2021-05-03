using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;
using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using System.IO;

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

        DataTable ITestingCentreService.ExportAsExcel()
        {
            DataTable dt = new DataTable();


            IEnumerable<TestingCentre> testingCentres = _testingCentreRepository.GetAllTestingCentres();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Postcode", typeof(string));



            var i = 0;

            foreach (TestingCentre _testingtentre in testingCentres)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = _testingtentre.TestingCentreID;
                dt.Rows[i][1] = _testingtentre.Name;
                dt.Rows[i][2] = _testingtentre.Postcode;
                i++;
            }
            return dt;
        }
    }
}
