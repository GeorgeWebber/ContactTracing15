using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.Models
{
  public class Tester
  {
    public int TesterID { get; set; }

    public ICollection<Case> Cases { get; set; }
  }
}
