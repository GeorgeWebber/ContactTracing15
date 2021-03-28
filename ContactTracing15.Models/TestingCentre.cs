using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    /// <summary>
    /// Class to represent a testing centre.
    /// </summary>
    public class TestingCentre
    {
        ///<value></value>
        public int TestingCentreID { get; set; }

        ///<value></value>
        public string Name { get; set; }

        ///<value></value>
        private int RegistrationCode { get; set; }

        ///<value></value>
        public string Postcode { get; set; }

        ///<value></value>
        public ICollection<Tester> Testers { get; set; }

    }
}
