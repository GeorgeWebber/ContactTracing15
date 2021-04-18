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

namespace ContactTracing15.Pages.Testing
{
    public class PositiveFormModel : PageModel
    {
        private readonly ICaseService _CaseService;
        private readonly ITesterService _TesterService;
        private readonly IUserService _UserService;
        private readonly ITestingCentreService _TestingCentreService;

        public PositiveFormModel(AppDbContext context, ICaseService caseService, ITesterService testerService, IUserService userService, ITestingCentreService testingCentreService)
        {
            _CaseService = caseService;
            _TesterService = testerService;
            _UserService = userService;
            _TestingCentreService = testingCentreService;
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
            
            if (ModelState.IsValid)
            {
                var lastCase = _CaseService.AssignAndAdd(CaseForm.getCase());
                return RedirectToPage("Dashboard", new { lastCaseId = lastCase.CaseID });
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
        [Phone, Required(ErrorMessage = "Please enter patient's preferred phone number"), Display(Name = "Phone Number")]
        public string Phone { get; set; }

#nullable enable
        [Phone, Display(Name = "Secondary Phone Number")]
        public string? Phone2 { get; set; }
        [Required(ErrorMessage = "Please enter date when test was taken"), Display(Name = "Date of Test")]
        public DateTime? TestDate { get; set; }
        [RegularExpression(@"([a-z]|[A-Z]){1,2}[0-9]{1,2}", ErrorMessage = "Please enter first half of Patient's Postcode, e.g AA01"), Display(Name = "Start of Postcode")]
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
            _case.Postcode = this.Postcode != null ? this.Postcode : TestingCentrePostcode;
            _case.SymptomDate = this.SymptomDate;

            _case.TesterID = this.TesterId;
            _case.AddedDate = DateTime.Now;
            _case.Traced = false;
            return _case;
        }
        

    }
}
