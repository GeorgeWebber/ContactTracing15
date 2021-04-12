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
        private readonly AppDbContext _context;
        private readonly ICaseRepository _caseRepository;
        private readonly ITesterRepository _testerRepository;

        public PositiveFormModel(AppDbContext context, ICaseRepository caseRepository, ITesterRepository testerRepository)
        {
            _context = context;
            _caseRepository = caseRepository;
            _testerRepository = testerRepository;
        }

        public void OnGetAsync()  //here sessions variables are probably appropriate, however everything else happening is not
        {
            try
            {
                testerId = (int)HttpContext.Session.GetInt32("ID");
                tester = _testerRepository.GetTester(testerId);
                testingCentre = tester.TestingCentre;
            }
            catch
            {
                (new SQLTestingCentreRepository(_context)).Add(new TestingCentre { Name = "temp", Postcode = "OX28" });
                testingCentre = (new SQLTestingCentreRepository(_context)).GetAllTestingCentres().First(x => x.Name == "temp");
                Console.WriteLine(testingCentre.Name);
                _testerRepository.Add(new Tester { Username = "Tyler" , TestingCentreID = testingCentre.TestingCentreID});
                tester = _testerRepository.GetAllTesters().First(x => x.Username == "Tyler");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //_caseRepository = new SQLCaseRepository(_context);
            //_testerRepository = new SQLTesterRepository(_context);
            try
            {
                testerId = (int)HttpContext.Session.GetInt32("ID");
                Console.WriteLine("Session ID is: " + testerId);
                tester = _testerRepository.GetTester(testerId);
                testingCentre = tester.TestingCentre;
            }
            catch
            {
                (new SQLTestingCentreRepository(_context)).Add(new TestingCentre { Name = "temp", Postcode = "OX28" });
                testingCentre = (new SQLTestingCentreRepository(_context)).GetAllTestingCentres().First(x => x.Name == "temp");
                Console.WriteLine(testingCentre.Name);
                _testerRepository.Add(new Tester { Username = "Tyler", TestingCentreID = testingCentre.TestingCentreID });
                tester = _testerRepository.GetAllTesters().First(x => x.Username == "Tyler");
            }
            //Product = await db.Products.FindAsync(Id);  some sort of await command here, so this runs when a command succeeds
            if (ModelState.IsValid)
            {
                _caseRepository.Add(CaseForm.getCase(testingCentre,tester));
                return RedirectToPage("../Index");
            }
            return Page();
        }
        

        public int testerId { get; set; }
        public Tester tester { get; set; }
        public TestingCentre testingCentre { get; set; }

        [BindProperty]
        public CaseForm CaseForm { get; set; }

    }

    public class CaseForm
    {
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

        public Case getCase(TestingCentre testingCentre, Tester tester)
        {
            Case _case = new Case();
            _case.Forename = this.Forename;
            _case.Surname = this.Surname;
            _case.Phone = this.Phone;
            _case.Phone2 = this.Phone2;
            _case.TestDate = this.TestDate == null ? DateTime.Now :(DateTime) this.TestDate;
            _case.Postcode = this.Postcode != null ? this.Postcode : testingCentre.Postcode;
            _case.SymptomDate = this.SymptomDate;

            _case.TesterID = tester.TesterID;
            _case.AddedDate = DateTime.Now;
            _case.Traced = false;
            return _case;
        }
        

    }
}
