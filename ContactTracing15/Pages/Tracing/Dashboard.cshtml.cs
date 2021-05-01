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
        private readonly IUserService userService;
        private readonly IContactService contactService;


        public DashboardModel(
            ICaseService caseService,
            ITracerService tracerService,
            IUserService userService,
            IContactService contactService)
            :base(tracerService, userService)
        {
            this.caseService = caseService;
            this.userService = userService;
            this.contactService = contactService;
        }

        public CaseDetail CurrentAssignedCase { get; set; }

        public bool CloseFailed { get; set; }

        public bool HasCurrentAssignedCase => CurrentAssignedCase != null;

        public IActionResult OnGet(int? caseId, int? dropCaseId, int? completeCaseId, int? deleteContactId, int? manualMarkContactId)
        {
            var claims = HttpContext.User.Claims;
            var currentUser = userService.GetUserByUserName(claims.Single(x => x.Type == "preferred_username").Value, int.Parse(claims.Single(x => x.Type == "usrtype").Value));
            if (dropCaseId != null && CaseListItems.AssignedCases.Any(x => x.CaseID == dropCaseId))
            {
                caseService.Drop(dropCaseId.Value, currentUser.UserId);
                return new RedirectToPageResult("Dashboard");
            }
            if (completeCaseId != null && CaseListItems.AssignedCases.Any(x => x.CaseID == completeCaseId))
            {
                var completeResult = caseService.Complete(completeCaseId.Value, currentUser.UserId);
                if (completeResult)
                {
                    return new RedirectToPageResult("Dashboard");
                }
                else
                {
                    CloseFailed = true;
                }
            }

            if (manualMarkContactId.HasValue)
            {
                var manualMarkContact = contactService.GetContact(manualMarkContactId.Value);
                if (manualMarkContact != null && CaseListItems.AssignedCases.Any(x => x.CaseID == manualMarkContact.CaseID))
                {
                    if (manualMarkContact.ContactedDate == null)
                    {
                        manualMarkContact.ContactedDate = DateTime.Now;
                    }
                    else
                    {
                        manualMarkContact.ContactedDate = null;
                    }
                    contactService.Update(manualMarkContact);
                }
            }

            if (caseId.HasValue && !CaseListItems.AssignedCases.Any(x => x.CaseID == caseId))
            {
                return Unauthorized();
            }


            if (caseId.HasValue)
            {
                if (deleteContactId.HasValue)
                {
                    var deleteContact = contactService.GetContact(deleteContactId.Value);
                    if (deleteContact != null && CaseListItems.AssignedCases.Any(x => x.CaseID == deleteContact.CaseID))
                    {
                        contactService.Delete(deleteContact.ContactID);
                    }
                }
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
            DateTraced = contact.AddedDate,
            ContactId = contact.ContactID,
            MarkUnmark = contact.ContactedDate == null ? "mark" : "unmark"
        };
    }
    public class ContactDetail
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateTraced { get; set; }
        public int ContactId { get; set; }
        public string MarkUnmark { get; set; }
        public string Info
        {
            get
            {
                if (EmailAddress != null && PhoneNumber != null)
                {
                    return $"{Name}: {EmailAddress}, {PhoneNumber}";
                }
                else if (EmailAddress == null)
                {
                    return $"{Name}: {PhoneNumber}";
                }
                else
                {
                    return $"{Name}: {EmailAddress}";
                }

            }
        }
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
