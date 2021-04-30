using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class TracingCentreService : ITracingCentreService
    {
        private readonly ITracerService _tracerService;
        private readonly ICaseService _caseService;
        private readonly IContactService _contactService;
        private readonly ITracingCentreRepository _tracingCentreRepository;

        public TracingCentreService(ICaseService caseService, IContactService contactService, ITracerService tracerService, ITracingCentreRepository tracingCentreRepository)
        {
            _caseService = caseService;
            _contactService = contactService;
            _tracerService = tracerService;
            _tracingCentreRepository = tracingCentreRepository;
        }

        //TODO: Returns the tracing centre statistic objects for each tracing centre as an enumerable
        // This statistics object is defined in ContactTracing15.Models, and includes:
        // Name: Tracing Centre Name
        // CasesAssignedLast28Days: Total number of cases assigned last 28 days
        // CasesReachedLast28Days: Total number of cases reached in the last 28 days
        // PercentageCasesReachedLast28Days: Percentage of cases reached in the last 28 days
        // AverageTraceTimeLast28Days: average time between assigning and closing case in the last 28 days
        public IEnumerable<TracingCentreStats> GetAllTracingCentreStats()
        {
            var TracingCentres = _tracingCentreRepository.GetAllTracingCentres().ToList();

            List<TracingCentreStats> Stats = new List<TracingCentreStats>();

            foreach (TracingCentre centre in TracingCentres)
            {
                var traceTime = 
                TracingCentreStats stats = new TracingCentreStats
                {
                    Name = centre.Name,
                    AverageTraceTimeLast28Days = new TimeSpan(2, 5, 2),
                    CasesAssignedLast28Days = 5,
                    CasesReachedLast28Days = 4,
                    PercentageCasesReachedLast28Days = 80
                };
            }

            TracingCentreStats tracingCentreStats = new TracingCentreStats
            {
                Name = "A centre",
                AverageTraceTimeLast28Days = new TimeSpan(2, 5, 2),
                CasesAssignedLast28Days = 5,
                CasesReachedLast28Days = 4,
                PercentageCasesReachedLast28Days = 80
            };

            List<TracingCentreStats> exampleEnumerable = new List<TracingCentreStats>();
            exampleEnumerable.Add(tracingCentreStats);
            return exampleEnumerable;
        }
    }
}
