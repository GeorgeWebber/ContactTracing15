using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    public class TracingCentre
    {
        public int TracingCentreID { get; set; }
        public string Name { get; set; }
        private int RegistrationCode { get; set; }
        public string Postcode { get; set; }

        public ICollection<Tracer> Tracers { get; set; }
    }
}
