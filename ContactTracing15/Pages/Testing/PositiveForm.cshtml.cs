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
using Microsoft.AspNetCore.Authorization;
using ContactTracing15.Helper;
using Microsoft.Extensions.Configuration;

namespace ContactTracing15.Pages.Testing
{
    [Authorize(Policy="TestersOnly")]
    public class PositiveFormModel : PageModel
    {
        private readonly ICaseService _CaseService;
        private readonly ITesterService _TesterService;
        private readonly IUserService _UserService;
        private readonly ITestingCentreService _TestingCentreService;
        private readonly IConfiguration _config;



        public PositiveFormModel(AppDbContext context, IConfiguration config, ICaseService caseService, ITesterService testerService, IUserService userService, ITestingCentreService testingCentreService)
        {
            _CaseService = caseService;
            _TesterService = testerService;
            _UserService = userService;
            _TestingCentreService = testingCentreService;
            _config = config;
            CaseForm = new CaseForm();
        }

        public void OnGetAsync()  
        {
            var claims = HttpContext.User.Claims;
            var CurrentUser = _UserService.GetUserByUserName(claims.Single(x => x.Type == "preferred_username").Value, int.Parse(claims.Single(x => x.Type == "usrtype").Value));
            var Tester = _TesterService.GetTester(CurrentUser.UserId);
            CaseForm.TesterId = Tester.TesterID;
            CaseForm.TestingCentrePostcode = _TestingCentreService.GetTestingCentre(Tester.TestingCentreID).Postcode;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            bool extraValid = true;
            if (ModelState.IsValid)
            {
                if (!PostcodeValidator.IsValid(CaseForm.Postcode, _config["googleApiKey"]))
                {
                    ModelState.AddModelError("CaseForm.Postcode", PostcodeValidator.FormatErrorMessage());
                    extraValid = false;
                }

                if (CaseForm.Email == null && CaseForm.Phone == null) {
                    ModelState.AddModelError("CaseForm.Email", "You must supply either an email address or primary phone number");
                    ModelState.AddModelError("CaseForm.Phone", "You must supply either an email address or primary phone number");
                    extraValid = false;
                }
                else if (CaseForm.Phone2 != null && CaseForm.Phone == null)
                {
                    ModelState.AddModelError("CaseForm.Phone", "You should supply a primary phone number before entering a secondary phone number");
                    extraValid = false;
                }

                if (CaseForm.TestDate > DateTime.Now.AddDays(1))
                {
                    ModelState.AddModelError("CaseForm.TestDate", "Test date is too far in the future");
                    extraValid = false;
                }

                if (CaseForm.SymptomDate > DateTime.Now.AddDays(1))
                {
                    ModelState.AddModelError("CaseForm.Phone", "Symptom date is too far in the future");
                    extraValid = false;
                }

                if (extraValid)
                {
                    var lastCase = _CaseService.AssignAndAdd(CaseForm.getCase());
                    return RedirectToPage("Dashboard", new { lastCaseId = lastCase.CaseID });
                }


            }
            return Page();
        }


        [BindProperty]
        public CaseForm CaseForm { get; set; }

    }

    public class CaseForm
    { 

        [HiddenInput]
        public int TesterId { get; set; }
        [HiddenInput]
        public string TestingCentrePostcode { get; set; }

        [Required(ErrorMessage = "Please enter patient's forename"), Display(Name = "Forename")]
        public string Forename { get; set; }
        [Required(ErrorMessage = "Please enter patient's Surname"), Display(Name = "Surname")]
        public string Surname { get; set; }
        [Phone, Display(Name = "Phone Number")]
        public string Phone { get; set; }

#nullable enable
        [Phone, Display(Name = "Secondary Phone Number")]
        public string? Phone2 { get; set; }
        [Required(ErrorMessage = "Please enter date when test was taken"), Display(Name = "Date of Test")]
        public DateTime? TestDate { get; set; }

        [Display(Name = "Start of Postcode")]
        public string? Postcode { get; set; }

        [EmailAddress, Display(Name = "Email Address")]
        public string? Email { get; set; }
        [Display(Name = "Date Symptoms Started (if known)")]
        public DateTime? SymptomDate { get; set; }

        public Case getCase()
        {
            Case _case = new Case();
            _case.Forename = this.Forename;
            _case.Surname = this.Surname;
            _case.Phone = this.Phone;
            _case.Phone2 = this.Phone2;
            _case.TestDate = this.TestDate == null ? DateTime.Now :(DateTime) this.TestDate;
            _case.Postcode = this.Postcode != null ? this.Postcode.ToUpper() : TestingCentrePostcode;
            _case.Email = this.Email;
            _case.SymptomDate = this.SymptomDate;

            _case.TesterID = this.TesterId;
            _case.AddedDate = DateTime.Now;
            _case.Traced = false;
            return _case;
        }
    }
}
