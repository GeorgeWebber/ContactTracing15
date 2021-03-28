using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    /// <summary>
    /// Class to represent a contact tracer.
    /// </summary>
    public class Tracer
    {
        ///<value></value>
        public int TracerID { get; set; }

        ///<value></value>
        public string Username { get; set; }

        ///<value></value>
        public int TracingCentreID { get; set; }

        ///<value></value>
        public TracingCentre TracingCentre { get; set; }

        ///<value></value>
        public ICollection<Case> Cases { get; set; }
    }
}
