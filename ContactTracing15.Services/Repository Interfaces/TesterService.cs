using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Services;
using ContactTracing15.Models;
using System.Linq;
using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using System.IO;

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
        Tester ITesterService.GetTester(string username)
        {
            return _testerRepository.GetTester(username);
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

        DataTable ITesterService.ExportAsExcel()
        {
            DataTable dt = new DataTable();


            IEnumerable<Tester> testers = _testerRepository.GetAllTesters();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Testing Centre ID", typeof(int));



            var i = 0;

            foreach (Tester _tester in testers)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = _tester.TesterID;
                dt.Rows[i][1] = _tester.Username;
                dt.Rows[i][2] = _tester.TestingCentreID;
                i++;
            }
            return dt;
        }
    }
}
