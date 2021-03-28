using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
     /// <summary>
     /// Object representing a NHS tester who processed covid tests and reports the positive ones in this system.
     /// </summary>
    public class Tester
    {
        ///<value></value>
        public int TesterID { get; set; }

        ///<value></value>
        public string Username { get; set; }

        ///<value></value>
        public int TestingCentreID { get; set; }

        ///<value></value>
        public TestingCentre TestingCentre { get; set; }

        ///<value></value>
        public ICollection<Case> Cases { get; set; }
    }
}
