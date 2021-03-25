using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    public class Tester
    {
        public int TesterID { get; set; }
        public string Username { get; set; }
        public int TestingCentreID { get; set; }
        
        public TestingCentre TestingCentre { get; set; }
        public ICollection<Case> Cases { get; set; }
    }
}
