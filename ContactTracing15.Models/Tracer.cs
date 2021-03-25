using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    public class Tracer
    {
        public int TracerID { get; set; }
        public string Username { get; set; }
        public int TracingCentreID { get; set; }

        public TracingCentre TracingCentre { get; set; }
        public ICollection<Case> Cases { get; set; }
    }
}
