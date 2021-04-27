using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace ContactTracing15.Pages.GovAgent
{

    [Authorize(Policy = "GovAgentOnly")]
    public class GovHomeModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ICaseService _caseService;
        private readonly IContactService _contactService;
        private readonly ITracingCentreService _tracingCentreService;

        public string AverageTraceTimeLast28DaysString { get; set; }
        public double PercentageCasesReachedLast28Days { get; set; }
        public int TotalCasesReached { get; set; }
        public int TotalCasesEver { get; set; }
        public int TotalContactsReached { get; set; }
        public double AverageContactsPerCaseLast28Days { get; set; }

        public IEnumerable<TracingCentreStats> AllTracingCentreStats;



        public GovHomeModel(IConfiguration config, ICaseService caseService, IContactService contactService, ITracingCentreService tracingCentreService)
        {
            _config = config;
            _caseService = caseService;
            _contactService = contactService;
            _tracingCentreService = tracingCentreService;
            SetStats();

        }

        public void SetStats()
        {
            TimeSpan time = _caseService.AverageTraceTimeLast28Days();
            AverageTraceTimeLast28DaysString = GetTimeString(time);

            PercentageCasesReachedLast28Days = _caseService.PercentageCasesReachedLast28Days();
            TotalCasesReached = _caseService.TotalCasesReached();
            TotalCasesEver = _caseService.TotalCasesEver();
            TotalContactsReached = _contactService.TotalContactsReached();
            AverageContactsPerCaseLast28Days = _contactService.AverageContactsPerCaseLast28Days();
            AllTracingCentreStats = _tracingCentreService.GetAllTracingCentreStats();
        }

        public string GetTimeString(TimeSpan time)
        {
            string timeString = "";
            if (time.Days > 0)
            {
                timeString += time.Days + " days, " + time.Hours + " hours";
            }
            else
            {
                timeString += time.Hours + " hours, " + time.Minutes + " minutes";
            }
            
            return timeString;
        }

        public void OnGet()
        {
            
        }

        public void DownloadDatabase()
        {
            Console.WriteLine("downloading database");
        }

    }
}
