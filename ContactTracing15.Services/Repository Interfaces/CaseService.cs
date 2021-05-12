using ContactTracing15.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Reflection;
using ClosedXML.Excel;

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
            newCase.TracerID = _tracerService.GetNextTracer(1).TracerID;
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
            completeCase.TracedDate = DateTime.Now;
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
            if (num_cases == 0) { return TimeSpan.FromTicks(0); }
            return TimeSpan.FromTicks(total_ticks / num_cases);
        }

        TimeSpan ICaseService.AverageTraceTimeOfCentreLast28Days(TracingCentre centre)
        {
            var cases = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now)
                .Where(x => x.Traced == true)
                .Where(x => x.Tracer.TracingCentreID == centre.TracingCentreID);
            var total_ticks = cases.Select(x => x.TracedDate.Value.Ticks - x.AddedDate.Ticks).Sum();
            var num_cases = cases.ToList().Count();
            if (num_cases == 0) { return TimeSpan.FromTicks(0); }
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
                .Where(x => x.Tracer != null)
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

        DataTable ICaseService.ExportAsExcel()
        {
            DataTable dt = new DataTable();


            IEnumerable<Case> cases = _caseRepository.GetAllCases();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("Test Date", typeof(DateTime));
            dt.Columns.Add("Added Date", typeof(DateTime));
            dt.Columns.Add("Postcode", typeof(string));
            dt.Columns.Add("Traced?", typeof(bool));
            dt.Columns.Add("Dropped times", typeof(int));
            dt.Columns.Add("Dropped?", typeof(bool));
            dt.Columns.Add("Traced Date", typeof(String));
            dt.Columns.Add("Symptom date", typeof(String));
            dt.Columns.Add("Removed date", typeof(String));

            

            var i = 0;

            foreach (Case _case in cases)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = _case.CaseID;
                dt.Rows[i][1] = _case.TestDate;
                dt.Rows[i][2] = _case.AddedDate;
                dt.Rows[i][3] = _case.Postcode;
                dt.Rows[i][4] = _case.Traced;
                dt.Rows[i][5] = _case.DroppedNum;
                dt.Rows[i][6] = _case.Dropped;
                if (_case.TracedDate != null)
                {
                    dt.Rows[i][7] = _case.TracedDate.ToString();
                }
                if (_case.SymptomDate != null)
                {
                    dt.Rows[i][8] = _case.SymptomDate.ToString();
                }
                if (_case.RemovedDate != null)
                {
                    dt.Rows[i][9] = _case.RemovedDate.ToString();
                }
                i++;
            }

            return dt;
        }


    }
}
