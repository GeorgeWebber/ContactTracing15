using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactTracing15.Pages.GovAgent
{
    [Authorize(Policy = "GovAgentOnly")]
    public class TracingCentreSummaryModel : PageModel
    {
        public IEnumerable<TracingCentreStats> AllTracingCentreStats;

        private readonly ITracingCentreService _tracingCentreService;

        public void OnGet()
        {
        }

        public TracingCentreSummaryModel(ITracingCentreService tracingCentreService)
        {
            _tracingCentreService = tracingCentreService;
            AllTracingCentreStats = _tracingCentreService.GetAllTracingCentreStats();
        }
    }
}
