using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;
using System.Linq;
using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using System.IO;

namespace ContactTracing15.Services
{
   
    public class TracerService : ITracerService
    {
        private readonly ITracerRepository _tracerResitory;
        private readonly ICaseRepository _caseRepository;
        public TracerService(ITracerRepository tracerRepository, ICaseRepository caseRepository)
        {
            _tracerResitory = tracerRepository;
            _caseRepository = caseRepository;
        }
        Tracer ITracerService.Add(Tracer newTracer)
        {
            return _tracerResitory.Add(newTracer);
        }

        Tracer ITracerService.Delete(int id)
        {
            return _tracerResitory.Delete(id);
        }

        IEnumerable<Tracer> ITracerService.GetAllTracers()
        {
            return _tracerResitory.GetAllTracers();
        }

        Tracer ITracerService.GetNextTracer()  
        {
            return _tracerResitory.GetTracerWithLeastCases().First();
        }
        Tracer ITracerService.GetNextTracer(int id)
        {
            var tracer = _tracerResitory.GetTracerWithLeastCases().First(x => x.TracerID != id);
            return tracer ?? _tracerResitory.GetTracer(id);
        }

        Tracer ITracerService.GetTracer(int id)
        {
            return _tracerResitory.GetTracer(id);
        }

        Tracer ITracerService.GetTracer(string username) 
        {
            return _tracerResitory.GetTracer(username);
        }

        void ITracerService.Save() 
        {
            _tracerResitory.Save();
        }

        IEnumerable<Tracer> ITracerService.Search(string searchTerm)
        {
            return _tracerResitory.Search(searchTerm);
        }

        Tracer ITracerService.Update(Tracer updatedTracer)
        {
            return _tracerResitory.Update(updatedTracer);
        }

        IEnumerable<Case> ITracerService.GetAssignedCases(int id)
        {
            return _tracerResitory.GetTracer(id).Cases.Where(x => !x.Traced);

        }

        void ITracerService.ExportAsExcel(string folderPath)
        {
            DataTable dt = new DataTable();


            IEnumerable<Tracer> tracers = _tracerResitory.GetAllTracers();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Username", typeof(string));
            dt.Columns.Add("Tracing Centre ID", typeof(int));



            var i = 0;

            foreach (Tracer _tracer in tracers)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = _tracer.TracerID;
                dt.Rows[i][1] = _tracer.Username;
                dt.Rows[i][2] = _tracer.TracingCentreID;
                i++;
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Tracers");
                wb.SaveAs(folderPath + "ExcelExportTracers.xlsx");
            }
        }
    }
}
