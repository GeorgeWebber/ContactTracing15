using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int CaseID { get; set; }
        public string Email { get; set; }
        public DateTime AddedDate { get; set; }


#nullable enable
        public string? Phone { get; set; }
        public DateTime? TracedDate { get; set; }
        public DateTime? RemovedDate { get; set; }

#nullable disable
        public Case Case { get; set; }
    }
}
