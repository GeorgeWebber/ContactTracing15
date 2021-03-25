using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Http;

namespace ContactTracing15.Pages.Tracing
{
    public class ContactFormModel : PageModel
    {
        private readonly AppDbContext _context;

        public ContactFormModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGetAsync(int? pcID)  //Need to Dejank this and switch to routing parameters instead of session variables
        {
            SQLContactRepository = new SQLContactRepository(_context);
            SQLCaseRepository = new SQLCaseRepository(_context);
            try
            {
                ParentCaseId = (int)HttpContext.Session.GetInt32("ID");
                ParentCase = SQLCaseRepository.GetCase(ParentCaseId);
            }
            catch
            {
                (new SQLTracingCentreRepository(_context)).Add(new TracingCentre { Name = "temp" });
                (new SQLTracerRepository(_context)).Add(new Tracer { Username = "Also Tyler", TracingCentreID = (new SQLTracingCentreRepository(_context)).GetAllTracingCentres().First(x => x.Name == "temp").TracingCentreID });
                ParentCase = SQLCaseRepository.GetAllCases().First();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SQLContactRepository = new SQLContactRepository(_context);
            SQLCaseRepository = new SQLCaseRepository(_context);
            try
            {
                ParentCaseId = (int)HttpContext.Session.GetInt32("ID");
                ParentCase = SQLCaseRepository.GetCase(ParentCaseId);
            }
            catch
            {
                ParentCase = SQLCaseRepository.GetAllCases().First();
            }
            //Product = await db.Products.FindAsync(Id);  some sort of await command here, so this runs when a command succeeds
            if (ModelState.IsValid)
            {
                SQLContactRepository.Add(ContactForm.getContact(ParentCase));
                return RedirectToPage("../Index");
            }
            return Page();
        }
        SQLContactRepository SQLContactRepository;
        SQLCaseRepository SQLCaseRepository;

        public int ParentCaseId { get; set; }
        public Case ParentCase { get; set; }

        [BindProperty]
        public ContactForm ContactForm { get; set; }

    }

    public class ContactForm
    {
        [Required(ErrorMessage = "Please enter contact's forename"), Display(Name = "Forename")]
        public string Forename { get; set; }
        [Required(ErrorMessage = "Please enter contact's Surname"), Display(Name = "Surname")]
        public string Surname { get; set; }

        [EmailAddress, Required(ErrorMessage = "Please enter email address for contact"), Display(Name = "Email Address")]
        public string Email { get; set; }

#nullable enable
        [Phone,  Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        public Contact getContact(Case parent)
        {
            Contact _contact= new Contact();
            _contact.Forename = this.Forename;
            _contact.Surname = this.Surname;
            _contact.Phone = this.Phone;
            _contact.Email = this.Email;

            _contact.AddedDate = DateTime.Now;
            _contact.TracedDate = DateTime.Now;
            _contact.CaseID = parent.CaseID;
            _contact.AddedDate = DateTime.Now;
            return _contact;
        }


    }
}