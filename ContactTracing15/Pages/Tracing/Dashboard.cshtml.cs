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

namespace ContactTracing15.Pages.Tracing
{
    public class DashboardModel : BaseDashboardModel // TODO create table of all cases assigned to a tracer with routing to details pages for each case (and form for adding contacts)
    {
        private readonly ICaseRepository caseRepository;
        private readonly IContactRepository contactRepository;

        public DashboardModel(
            ICaseRepository caseRepository,
            ITracerRepository tracerRepository,
            IUserService userService,
            IContactRepository contactRepository)
            :base(tracerRepository, userService, caseRepository)

        {
            this.caseRepository = caseRepository;
            this.contactRepository = contactRepository;
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

                var currentCase = caseRepository.GetCase(caseId.Value);
                if (currentCase != null)
                {
                    CurrentAssignedCase = new CaseDetail
                    {
                        Name = currentCase.GetFullName(),
                        CaseID = currentCase.CaseID,
                        contacts = contactRepository.GetAllContacts().Where(x => x.CaseID == currentCase.CaseID)
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
