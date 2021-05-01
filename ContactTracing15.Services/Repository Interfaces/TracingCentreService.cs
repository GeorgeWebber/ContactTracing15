using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
                TracingCentreStats stats = new TracingCentreStats
                {
                    Name = centre.Name,
                    AverageTraceTimeLast28Days = new TimeSpan(2, 5, 2),
                    CasesAssignedLast28Days = CasesAssignedLast28Days_,
                    CasesReachedLast28Days = CasesReachedLast28Days_,
                    PercentageCasesReachedLast28Days = (double)CasesReachedLast28Days_ / CasesAssignedLast28Days_ * 100
                };
                AllStats.Add(stats);
            }

            return AllStats;
        }
    }
}
