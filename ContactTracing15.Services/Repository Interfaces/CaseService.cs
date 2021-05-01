using ContactTracing15.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;

namespace ContactTracing15.Services
{
    public class CaseService : ICaseService
    {
        private readonly ICaseRepository _caseRepository;
        private readonly IContactRepository _contactRepository;
        private readonly ITracerService _tracerService;
        private readonly IEmailService _emailService;
        public CaseService(ICaseRepository caseRepository, IContactRepository contactRepository, ITracerService tracerService, IEmailService emailService)
        {
            _caseRepository = caseRepository;
            _contactRepository = contactRepository;
            _tracerService = tracerService;
            _emailService = emailService;
        }

        Case ICaseService.Add(Case newCase)
        {
            return _caseRepository.Add(newCase);
        }

        Case ICaseService.Delete(int id)
        {
            return _caseRepository.Delete(id);
        }

        IEnumerable<Case> ICaseService.GetAllCases()
        {
            return _caseRepository.GetAllCases();
        }

        Case ICaseService.GetCase(int id)
        {
            return _caseRepository.GetCase(id);
        }

        void ICaseService.Save()
        {
            _caseRepository.Save();
        }

        IEnumerable<Case> ICaseService.Search(string searchTerm)
        {
            return _caseRepository.Search(searchTerm);
        }

        Case ICaseService.Update(Case updatedCase)
        {
            return _caseRepository.Update(updatedCase);
        }

        public IEnumerable<Contact> GetTracedContacts(int id)
        {
            return _caseRepository.GetCase(id).Contacts;
        }

        public IEnumerable<Case> GetOldCases(DateTime threshold)
        {
            var low_bar = new DateTime(1000, 1, 1);
            return _caseRepository.GetCasesByDate(low_bar, threshold).Where(x => x.RemovedDate == null).ToList();
        }
        
        public Case RemovePersonalData(int id) //TODO, perhaps do this with SQL if it's faster, otherwise this is fine as is
        {
            var _case = _caseRepository.GetCase(id);
            _case.Forename = null;
            _case.Surname = null;
            _case.Email = null;
            _case.Phone = null;
            _case.Phone2 = null;
            _case.TracerID = null;
            _case.RemovedDate = DateTime.Now;
            return _caseRepository.Update(_case);
        }

        IEnumerable<string> ICaseService.GetPostcodesByRecentDays(DateTime from_, DateTime to_)
        {
            return _caseRepository.GetCasesByDate(from_, to_).Select(u => u.Postcode);
        }

        Case ICaseService.AssignAndAdd(Case newCase)
        {
            newCase.TracerID = _tracerService.GetNextTracer().TracerID;
            return _caseRepository.Add(newCase);
        }

        Case ICaseService.Drop(int caseId, int tracerId)
        {
            var dropCase = _caseRepository.GetCase(caseId);
            if (DateTime.Now.AddDays(-7) > dropCase.TestDate || dropCase.DroppedNum >= 3)
            {
                dropCase.TracerID = null;
                dropCase.Dropped = true;
            }
            else
            {
                dropCase.TracerID = _tracerService.GetNextTracer(tracerId).TracerID;
                dropCase.DroppedNum++;
            }
            return _caseRepository.Update(dropCase);
        }

        bool ICaseService.Complete(int caseId, int tracerId)
        {
            var completeCase = _caseRepository.GetCase(caseId);
            completeCase.Traced = true;
            var contacts = GetTracedContacts(caseId);
            if (contacts.Any(x => x.Email == null && x.ContactedDate == null))
            {
                return false;
            }
            foreach (Contact contact in contacts)
            {
                var _contact = _contactRepository.GetContact(contact.ContactID);
                _contact.TracedDate = DateTime.Now;
                if (contact.Email != null) { 
                    _emailService.ContactByEmail(contact);
                    _contact.ContactedDate = DateTime.Now;
                }
                _contactRepository.Update(_contact);
            }
            _caseRepository.Update(completeCase);
            return true;
        }

        TimeSpan ICaseService.AverageTraceTimeLast28Days()
        {
            var cases = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now).Where(x => x.Traced == true);
            var total_ticks = cases.Select(x => x.TracedDate.Value.Ticks - x.AddedDate.Ticks).Sum();
            var num_cases = cases.ToList().Count();
            return TimeSpan.FromTicks(total_ticks / num_cases);
        }

        double ICaseService.PercentageCasesReachedLast28Days()
        {
            int cases = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now).ToList().Count();
            if (cases == 0) { return 0; }
            int traced = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now).Where(x => x.Traced).ToList().Count();
            return  (double) traced / cases * 100;
        }

        int ICaseService.CasesAssignedToTracingCentreLast28Days(TracingCentre centre)
        {
            return _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now)
                .Where(x => x.Tracer.TracingCentre.TracingCentreID == centre.TracingCentreID)
                .ToList()
                .Count();
        }

        int ICaseService.CasesTracedByTracingCentreLast28Days(TracingCentre centre)
        {
            return _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now)
                .Where(x => x.Traced)
                .Where(x => x.Tracer.TracingCentre.TracingCentreID == centre.TracingCentreID)
                .ToList()
                .Count();
        }


        int ICaseService.TotalCasesReached()
        {
            return _caseRepository.GetAllCases().Where(x => x.Traced).ToList().Count();
        }

        int ICaseService.TotalCasesEver()
        {
            return  _caseRepository.GetAllCases().ToList().Count();
        }

        void ICaseService.ExportAsExcel()
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" +
            "ExcelReport.xlsx";

            Excel.Application xlsApp;
            Excel.Workbook xlsWorkbook;
            Excel.Worksheet xlsWorksheet;
            object misValue = System.Reflection.Missing.Value;

            try
            {
                FileInfo oldFile = new FileInfo(fileName);
                if (oldFile.Exists)
                {
                    File.SetAttributes(oldFile.FullName, FileAttributes.Normal);
                    oldFile.Delete();
                }
            }
            catch (Exception ex)
            {
                return;
            }

            xlsApp = new Excel.Application();
            xlsWorkbook = xlsApp.Workbooks.Add(misValue);
            xlsWorksheet = (Excel.Worksheet)xlsWorkbook.Sheets[1];

            // Create the header for Excel file
            xlsWorksheet.Cells[1, 1] = "Covid Cases Report";
            Excel.Range range = xlsWorksheet.get_Range("A1", "E1");
            range.Merge(1);
            range.Borders.Color = Color.Black.ToArgb();
            range.Interior.Color = Color.Yellow.ToArgb();

        }


    }
}
