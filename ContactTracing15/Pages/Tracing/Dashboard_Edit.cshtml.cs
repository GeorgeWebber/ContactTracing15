using ContactTracing15.Services;
using ContactTracing15.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using ContactTracing15.Helper.Extensions;

namespace ContactTracing15.Pages.Tracing
{
    public class DashboardEditModel : BaseDashboardModel // TODO create table of all cases assigned to a tracer with routing to details pages for each case (and form for adding contacts)
    { 
        public DashboardEditModel(
            ITracerRepository tracerRepository,
            IUserService userService,
            ICaseRepository caseRepository,
            IContactRepository contactRepository)
            :base(tracerRepository, userService, caseRepository)
        {
            this.caseRepository = caseRepository;
            this.contactRepository = contactRepository;
            AddContactForm = new AddContactForm();
        }

        private readonly ICaseRepository caseRepository;
        private readonly IContactRepository contactRepository;

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

                var currentCase = caseRepository.GetCase(caseId.Value);
                if (currentCase != null)
                {
                    CurrentAssignedCase = new CaseDetail
                    {
                        Name = currentCase.GetFullName(),
                        CaseID = currentCase.CaseID
                    };
                }
            }

            
            var parentCase = caseRepository.GetCase(caseId.Value);
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
                contactRepository.Add(AddContactForm.getContact(AddContactForm.CaseId));
                return new RedirectToPageResult($"Dashboard");  
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
            _contact.AddedDate = DateTime.Now;
            return _contact;
        }


    }
}
