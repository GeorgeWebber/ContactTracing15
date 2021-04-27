using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public class TracingCentreService : ITracingCentreService
    {
        //TODO: Returns the tracing centre statistic objects for each tracing centre as an enumerable
        // This statistics object is defined in ContactTracing15.Models, and includes:
        // Name: Tracing Centre Name
        // CasesAssignedLast28Days: Total number of cases assigned last 28 days
        // CasesReachedLast28Days: Total number of cases reached in the last 28 days
        // PercentageCasesReachedLast28Days: Percentage of cases reached in the last 28 days
        // AverageTraceTimeLast28Days: average time between assigning and closing case in the last 28 days
        public IEnumerable<TracingCentreStats> GetAllTracingCentreStats()
        {
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
