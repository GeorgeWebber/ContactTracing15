using System;
using System.Collections.Generic;

namespace ContactTracing15.Models
{
    /// <summary>
    /// An object representing a single positive case.
    /// </summary>
    public class SafeCase
    {
        /// <value>Unique case id</value>
        public int CaseID { get; set; }

        /// <value>Date of test</value>
        public DateTime TestDate { get; set; }

        /// <value>Date case was inserted into the database</value>
        public DateTime AddedDate { get; set; }

        /// <value>Truncated postcode of the positive case</value>
        public string Postcode { get; set; }

        /// <value>Boolean value representing whether a case has given to a contact tracer or not</value>
        public bool Traced { get; set; }
        public int DroppedNum { get; set; }
        public bool Dropped { get; set; }

#nullable enable

        public DateTime? TracedDate { get; set; }

        /// <value>Date of first symptoms (Nullable)</value>
        public DateTime? SymptomDate { get; set; }

        /// <value>Date personal data removed from this case to comply with privacy concerns (Nullable)</value>
        public DateTime? RemovedDate { get; set; }

    }
}
