﻿using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;
using System.Data;

namespace ContactTracing15.Services
{
    public interface ICaseService
    {

        IEnumerable<Case> GetAllCases();
        Case GetCase(int id);
        Case Add(Case newCase);
        Case Update(Case updatedCase);
        Case Delete(int id);
        IEnumerable<Case> Search(string searchTerm);
        void Save();
        IEnumerable<Contact> GetTracedContacts(int id);
        IEnumerable<Case> GetOldCases(DateTime threshold);
        Case RemovePersonalData(int id);

        DataTable ExportAsExcel();

        // Returns an enumerable of postcodes of cases registered in the day range "today-daysFrom" to "today - daysTo"
        IEnumerable<string> GetPostcodesByRecentDays(DateTime from_, DateTime to_);

        int CasesAssignedToTracingCentreLast28Days(TracingCentre centre);

        int CasesTracedByTracingCentreLast28Days(TracingCentre centre);

        Case AssignAndAdd(Case newCase);
        Case Drop(int caseId, int tracerId);
        bool Complete(int caseId, int tracerId);

        TimeSpan AverageTraceTimeLast28Days();
        TimeSpan AverageTraceTimeOfCentreLast28Days(TracingCentre centre);

        double PercentageCasesReachedLast28Days();

        int TotalCasesReached();

        int TotalCasesEver();

    }
}
