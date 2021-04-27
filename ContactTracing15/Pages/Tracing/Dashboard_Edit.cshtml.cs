using ContactTracing15.Services;
using ContactTracing15.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using ContactTracing15.Helper.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ContactTracing15.Pages.Tracing
{
    [Authorize(Policy = "TracersOnly")]
    public class DashboardEditModel : BaseDashboardModel 
    { 
        public DashboardEditModel(
            ITracerService tracerService,
            IUserService userService,
            IContactRepository contactService,
            ICaseService caseService)
            :base(tracerService, userService)
        {
            _contactService = contactService;
            _caseService = caseService;
            AddContactForm = new AddContactForm();
        }

        private readonly ICaseService _caseService;
        private readonly IContactRepository _contactService;

        [BindProperty]
        public AddContactForm AddContactForm { get; set; }

        public CaseDetail CurrentAssignedCase { get; set; }

        public bool HasCurrentAssignedCase => CurrentAssignedCase != null;

        public IActionResult OnGet(int? caseId)
        {
            if (!caseId.HasValue)
            {
                return NotFound();
            }
            if (caseId.HasValue && !CaseListItems.AssignedCases.Any(x => x.CaseID == caseId))
            {
                return Unauthorized();
            }

            if (caseId.HasValue)
            {
                CaseListItems.AssignedCases.FirstOrDefault(x => x.CaseID == caseId).IsActive = true;

                var currentCase = _caseService.GetCase(caseId.Value);
                if (currentCase != null)
                {
                    CurrentAssignedCase = new CaseDetail
                    {
                        Name = currentCase.GetFullName(),
                        CaseID = currentCase.CaseID
                    };
                }
            }

            
            var parentCase = _caseService.GetCase(caseId.Value);
            if (parentCase == null)
            {
                return NotFound();
            }
            AddContactForm.CaseId = caseId.Value;
            return Page();
            
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _contactService.Add(AddContactForm.getContact(AddContactForm.CaseId));
                return new RedirectToPageResult("Dashboard", new { caseId = AddContactForm.CaseId });  
            }
            return Page();
        }
    }
    public class AddContactForm
    {
        [HiddenInput]
        public int CaseId { get; set; }
        [Required(ErrorMessage = "Please enter contact's forename"), Display(Name = "Forename")]
        public string Forename { get; set; }
        [Required(ErrorMessage = "Please enter contact's Surname"), Display(Name = "Surname")]
        public string Surname { get; set; }

        [EmailAddress, Required(ErrorMessage = "Please enter email address for contact"), Display(Name = "Email Address")]
        public string Email { get; set; }

#nullable enable
        [Phone, Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        public Contact getContact(int parentCaseId)
        {
            Contact _contact = new Contact();
            _contact.Forename = this.Forename;
            _contact.Surname = this.Surname;
            _contact.Phone = this.Phone;
            _contact.Email = this.Email;

            _contact.AddedDate = DateTime.Now;
            _contact.TracedDate = DateTime.Now;
            _contact.CaseID = parentCaseId;
            return _contact;
        }


    }
}
