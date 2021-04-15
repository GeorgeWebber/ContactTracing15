using ContactTracing15.Helper.Extensions;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ContactTracing15.Pages.Tracing
{
    [Authorize(Policy = "TracersOnly")]
    public class DashboardModel : BaseDashboardModel // TODO create table of all cases assigned to a tracer with routing to details pages for each case (and form for adding contacts)
    {
        private readonly ICaseService caseService;

        
        public DashboardModel(
            ICaseService caseService,
            ITracerService tracerService,
            IUserService userService)
            :base(tracerService, userService)
        {
            this.caseService = caseService;
        }

        public CaseDetail CurrentAssignedCase { get; set; }

        public bool HasCurrentAssignedCase => CurrentAssignedCase != null;

        public IActionResult OnGet(int? caseId)
        {
            //if(User == null || User.Identity == null || !User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}

            if (caseId.HasValue && !CaseListItems.AssignedCases.Any(x => x.CaseID == caseId))
            {
                return Unauthorized();
            }

            if (caseId.HasValue)
            {
                CaseListItems.AssignedCases.FirstOrDefault(x => x.CaseID == caseId).IsActive = true;

                var currentCase = caseService.GetCase(caseId.Value);
                if (currentCase != null)
                {
                    CurrentAssignedCase = new CaseDetail
                    {
                        Name = currentCase.GetFullName(),
                        CaseID = currentCase.CaseID,
                        contacts = caseService.GetTracedContacts(currentCase.CaseID)
                    };
                }
            }

            return Page();
        }
    }

    public class CaseDetail
    {
        public string Name { get; set; }
        public int CaseID { get; set; }
        public IEnumerable<Contact> contacts { get; set; }
    }
}
