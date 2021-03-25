using System;
using System.Collections.Generic;

namespace ContactTracing15.Models
{
    public class Case
    {
        public int CaseID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int TesterID { get; set; }
        public string Phone { get; set; }
        public DateTime TestDate { get; set; }
        public DateTime AddedDate { get; set; }
        public string Postcode { get; set; }
        public bool Traced { get; set; }

#nullable enable
        public string? Email { get; set; }
        public string? Phone2 { get; set; }
        public int? TracerID { get; set; }
        public DateTime? SymptomDate { get; set; }
        public DateTime? RemovedDate { get; set; }

        public Tracer? Tracer { get; set; }
#nullable disable
        public Tester Tester { get; set; }
        public ICollection<Contact> Contacts { get; set; }
    }
}
