using System;
using System.Collections.Generic;

namespace ContactTracing15.Models
{
    /// <summary>
    /// An object representing a single positive case.
    /// </summary>
    public class Case
    {
        /// <value>Unique case id</value>
        public int CaseID { get; set; }

        /// <value></value>
        public string Forename { get; set; }

        /// <value></value>
        public string Surname { get; set; }

        /// <value>Unique id of the NHS tester recording the positve case</value>
        public int TesterID { get; set; }

        /// <value>Positive case's primary phone number</value>
        public string Phone { get; set; }

        /// <value>Date of test</value>
        public DateTime TestDate { get; set; }

        /// <value>Date case was inserted into the database</value>
        public DateTime AddedDate { get; set; }

        /// <value>Truncated postcode of the positive case</value>
        public string Postcode { get; set; }

        /// <value>Boolean value representing whether a case has given to a contact tracer or not</value>
        public bool Traced { get; set; }

#nullable enable

        /// <value>(Nullable)</value>
        public string? Email { get; set; }

        /// <value>Positive case's secondary phone number (Nullable)</value>
        public string? Phone2 { get; set; }

        /// <value>Unique id of the contact tracer assigned to this case (Nullable)</value>
        public int? TracerID { get; set; }

        /// <value>Date of first symptoms (Nullable)</value>
        public DateTime? SymptomDate { get; set; }

        /// <value>Date personal data removed from this case to comply with privacy concerns (Nullable)</value>
        public DateTime? RemovedDate { get; set; }

        /// <value>Tracer object representing the contact tracer assigned to this case (Nullable)</value>
        public Tracer? Tracer { get; set; }
#nullable disable

        /// <value>Tester object representing the NHS tester assigned to this case</value>
        public Tester Tester { get; set; }

        /// <value>A collection of contacts of this case</value>
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
