using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public GovHomeModel(IConfiguration config, ICaseService caseService)
        {
            _config = config;
            _caseService = caseService;
        }

        public void OnGet()
        {
            RedirectToPage("HeatmapPage");
        }

    }
}
