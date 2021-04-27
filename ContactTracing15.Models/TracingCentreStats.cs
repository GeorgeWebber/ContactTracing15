using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.Models
{
    public class TracingCentreStats
    {
        public string Name { get; set; }
        public int CasesAssignedLast28Days { get; set; }
        public int CasesReachedLast28Days { get; set; }
        public double PercentageCasesReachedLast28Days { get; set; }
        public TimeSpan AverageTraceTimeLast28Days { get; set; }
    }
}
