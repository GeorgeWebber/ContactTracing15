using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClosedXML.Excel;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;


/*  Home page for government actors who would like to view contact tracing data for analysis
 *  Displays summary statistics on the tracing process as well as linking to other analysis pages
 */


namespace ContactTracing15.Pages.GovAgent
{

    [Authorize(Policy = "GovAgentOnly")]
    public class GovHomeModel : PageModel
    {
        private readonly IConfiguration _config;
        private readonly ICaseService _caseService;
        private readonly IContactService _contactService;
        private readonly ITracerService _tracerService;
        private readonly ITesterService _testerService;
        private readonly ITracingCentreService _tracingCentreService;
        private readonly ITestingCentreService _testingCentreService;

        //Variables to store the values of key statistics for display on the gov dashboard
        public string AverageTraceTimeLast28DaysString { get; set; }
        public string PercentageCasesReachedLast28Days { get; set; }
        public int TotalCasesReached { get; set; }
        public int TotalCasesEver { get; set; }
        public int TotalContactsReached { get; set; }
        public string AverageContactsPerCaseLast28Days { get; set; }


        //Resolve dependencies and set initial statistic values
        public GovHomeModel(IConfiguration config, ICaseService caseService, IContactService contactService, ITracerService tracerService, 
            ITesterService testerService, ITracingCentreService tracingCentreService, ITestingCentreService testingCentreService)
        {
            _config = config;
            _caseService = caseService;
            _contactService = contactService;
            _tracerService = tracerService;
            _testerService = testerService;
            _tracingCentreService = tracingCentreService;
            _testingCentreService = testingCentreService;
            SetStats();

        }

        // Set the statistic variables to the correct values using the database access services
        public void SetStats()
        {
            TimeSpan time = _caseService.AverageTraceTimeLast28Days();
            AverageTraceTimeLast28DaysString = GetTimeString(time);
            PercentageCasesReachedLast28Days = _caseService.PercentageCasesReachedLast28Days().ToString("0.0");
            TotalCasesReached = _caseService.TotalCasesReached();
            TotalCasesEver = _caseService.TotalCasesEver();
            TotalContactsReached = _contactService.TotalContactsReached();
            AverageContactsPerCaseLast28Days = _contactService.AverageContactsPerCaseLast28Days().ToString("0.0");

        }

        // Convert a TimeSpan object into a reasonable string representation for viewing on the page
        public string GetTimeString(TimeSpan time)
        {
            if (time <= TimeSpan.FromTicks(0))
            {
                return "N/A";
            }
            string timeString = "";
            if (time.Days > 0)
            {
                timeString += time.Days + " d " + time.Hours + " h";
            }
            else
            {
                timeString += time.Hours + " h " + time.Minutes + " m";
            }
            return timeString;
        }

        // Trigger a download of the most recent version of the Excel analytics spreadsheet
        public IActionResult OnGetDownload()
        {
            FileContentResult download = StartExcelDownload();
            if (download != null)
            {
                return download;
            }
            else
            {
                return Page();
            }
        }

        // Start a file download and return the resultant file
        public FileContentResult StartExcelDownload ()
        {

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "ContactTracingReport.xlsx";
            try
            {
                using (var workbook = new XLWorkbook(@"ContactTracingReport.xlsx"))
                {
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}
