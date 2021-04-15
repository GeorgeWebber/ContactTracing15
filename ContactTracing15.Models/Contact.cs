using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    /// <summary>
    /// Object representing the contact of a positive case
    /// </summary>
    public class Contact
    {

        /// <value>Unique id for the contact</value>
        public int ContactID { get; set; }

        /// <value></value>
        public string Forename { get; set; }

        /// <value></value>
        public string Surname { get; set; }

        /// <value></value>
        public int CaseID { get; set; }

        /// <value></value>
        public string Email { get; set; }

        /// <value></value>
        public DateTime AddedDate { get; set; }


#nullable enable
        /// <value>(Nullable)</value>
        public string? Phone { get; set; }

        /// <value>Date that the contact was traced(Nullable)</value>
        public DateTime? TracedDate { get; set; }

        /// <value>Date that the personal data on this contact was removed from the database (Nullable)</value>
        public DateTime? RemovedDate { get; set; }

#nullable disable

        /// <value>Parent case of this contact</value>
        public Case Case { get; set; }
    }
}
