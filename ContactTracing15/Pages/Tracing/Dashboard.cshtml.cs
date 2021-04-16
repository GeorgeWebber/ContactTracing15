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
using System;

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

        public IActionResult OnGet(int? caseId, int? dropCaseId, int? completeCaseId)
        {
            if (dropCaseId != null && CaseListItems.AssignedCases.Any(x => x.CaseID == dropCaseId))
            {
                var caseToDrop = caseService.GetCase(dropCaseId.Value);
                caseToDrop.TracerID = null;
                caseService.Save();
                return new RedirectToPageResult("Dashboard");
            }
            if (completeCaseId != null && CaseListItems.AssignedCases.Any(x => x.CaseID == completeCaseId))
            {
                var caseToComplete = caseService.GetCase(completeCaseId.Value);
                caseToComplete.Traced = true;
                caseService.Save();
                return new RedirectToPageResult("Dashboard");
            }

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
                        contacts = caseService.GetTracedContacts(currentCase.CaseID).Select(MapToContactDetail).OrderBy(x => x.DateTraced),
                        EmailAddress = currentCase.Email,
                        PhoneNumber = currentCase.Phone,
                        PhoneNumber2 = currentCase.Phone2
                    };
                }
            }

            return Page();
        }
        private static ContactDetail MapToContactDetail(Contact contact) => new ContactDetail
        {
            Name = contact.GetFullName(),
            EmailAddress = contact.Email,
            PhoneNumber = contact.Phone,
            DateTraced = contact.AddedDate
        };
    }
    public class ContactDetail
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateTraced { get; set; }

    }
    public class CaseDetail
    {
        public string Name { get; set; }
        public int CaseID { get; set; }
        public IEnumerable<ContactDetail> contacts { get; set; }
        public string? EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
    }
}
