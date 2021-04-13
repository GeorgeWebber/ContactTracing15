using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    /// <summary>
    /// Class to represent a contact tracing organisation.
    /// </summary>
    public class TracingCentre
    {

        ///<value></value>
        public int TracingCentreID { get; set; }

        ///<value></value>
        public string Name { get; set; }

        ///<value></value>
        private int RegistrationCode { get; set; }

        ///<value></value>
        public string Postcode { get; set; }

        ///<value></value>
        public virtual ICollection<Tracer> Tracers { get; set; }
    }
}
