using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using System.IO;

namespace ContactTracing15.Services
{
    public class TracingCentreService : ITracingCentreService
    {
        private readonly ICaseService _caseService;
        private readonly ITracingCentreRepository _tracingCentreRepository;

        public TracingCentreService(ICaseService caseService, ITracingCentreRepository tracingCentreRepository)
        {
            _caseService = caseService;
            _tracingCentreRepository = tracingCentreRepository;
        }
        public IEnumerable<TracingCentreStats> GetAllTracingCentreStats()
        {
            var TracingCentres = _tracingCentreRepository.GetAllTracingCentres().ToList();

            List<TracingCentreStats> AllStats = new List<TracingCentreStats>();

            foreach (TracingCentre centre in TracingCentres)
            {
                var CasesAssignedLast28Days_ = _caseService.CasesAssignedToTracingCentreLast28Days(centre);
                var CasesReachedLast28Days_ = _caseService.CasesTracedByTracingCentreLast28Days(centre);
                var AverageTraceTimeLast28Days_ = _caseService.AverageTraceTimeOfCentreLast28Days(centre);


                TracingCentreStats stats = new TracingCentreStats
                {
                    Name = centre.Name,
                    AverageTraceTimeLast28Days = AverageTraceTimeLast28Days_,
                    CasesAssignedLast28Days = CasesAssignedLast28Days_,
                    CasesReachedLast28Days = CasesReachedLast28Days_,
                    PercentageCasesReachedLast28Days = (double)CasesReachedLast28Days_ / CasesAssignedLast28Days_ * 100
                };
                AllStats.Add(stats);
            }

            return AllStats;
        }

        DataTable ITracingCentreService.ExportAsExcel()
        {
            DataTable dt = new DataTable();


            IEnumerable<TracingCentre> tracingCentres = _tracingCentreRepository.GetAllTracingCentres();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Postcode", typeof(string));

            var i = 0;

            foreach (TracingCentre _tracingtentre in tracingCentres)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = _tracingtentre.TracingCentreID;
                dt.Rows[i][1] = _tracingtentre.Name;
                dt.Rows[i][2] = _tracingtentre.Postcode;
                i++;
            }
            return dt;
        }
    }
}
