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
            IContactService contactService,
            ICaseService caseService)
            :base(tracerService, userService)
        {
            _contactService = contactService;
            _caseService = caseService;
            AddContactForm = new AddContactForm();
        }

        private readonly ICaseService _caseService;
        private readonly IContactService _contactService;

        [BindProperty]
        public AddContactForm AddContactForm { get; set; }

        public CaseDetail CurrentAssignedCase { get; set; }

        public bool HasCurrentAssignedCase => CurrentAssignedCase != null;

        public IActionResult OnGet(int? caseId, int? editContactId)
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
            if (editContactId.HasValue)
            {
                var editContact = _contactService.GetContact(editContactId.Value);
                if (editContact == null)
                {
                    return NotFound();
                }
                AddContactForm.ContactId = editContactId.Value;
                AddContactForm.Forename = editContact.Forename;
                AddContactForm.Surname = editContact.Surname;
                AddContactForm.Phone = editContact.Phone;
                AddContactForm.Email = editContact.Email;
            }
            return Page();
            
        }

        public IActionResult OnPost()
        {
            var currentCase = _caseService.GetCase(AddContactForm.CaseId);
            if (currentCase != null)
            {
                CurrentAssignedCase = new CaseDetail
                {
                    Name = currentCase.GetFullName(),
                    CaseID = currentCase.CaseID
                };
            }
            if (ModelState.IsValid)
            {
                var extraValid = true;
                if (AddContactForm.Email == null && AddContactForm.Phone == null)
                {
                    ModelState.AddModelError("AddContactForm.Email", "You must supply either an email address or phone number");
                    ModelState.AddModelError("AddContactForm.Phone", "You must supply either an email address or phone number");
                    extraValid = false;
                }
                if (extraValid)
                {
                    if (AddContactForm.ContactId.HasValue)
                    {
                        var editContact = _contactService.GetContact(AddContactForm.ContactId.Value);
                        if (editContact == null || editContact.CaseID != currentCase.CaseID)
                        {
                            return new RedirectToPageResult("Dashboard", new { caseId = AddContactForm.CaseId });
                        }
                        _contactService.Update(AddContactForm.getContact(editContact));
                        return new RedirectToPageResult("Dashboard", new { caseId = AddContactForm.CaseId });
                    }
                    else
                    {
                        _contactService.Add(AddContactForm.getContact());
                        return new RedirectToPageResult("Dashboard", new { caseId = AddContactForm.CaseId });
                    }
                }
            }
            return Page();
        }
    }
    public class AddContactForm
    {
        [HiddenInput]
        public int? ContactId { get; set; }
        [HiddenInput]
        public int CaseId { get; set; }
        [Required(ErrorMessage = "Please enter contact's forename"), Display(Name = "Forename")]
        public string Forename { get; set; }
        [Required(ErrorMessage = "Please enter contact's Surname"), Display(Name = "Surname")]
        public string Surname { get; set; }

        [EmailAddress, Display(Name = "Email Address")]
        public string Email { get; set; }

#nullable enable
        [Phone, Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        public Contact getContact()
        {
            Contact _contact = new Contact();
            _contact.Forename = this.Forename;
            _contact.Surname = this.Surname;
            _contact.Phone = this.Phone;
            _contact.Email = this.Email;

            _contact.AddedDate = DateTime.Now;
            _contact.CaseID = this.CaseId;
            return _contact;
        }
        public Contact getContact(Contact _contact)
        {
            _contact.Forename = this.Forename;
            _contact.Surname = this.Surname;
            _contact.Phone = this.Phone;
            _contact.Email = this.Email;

            return _contact;
        }


    }
}
