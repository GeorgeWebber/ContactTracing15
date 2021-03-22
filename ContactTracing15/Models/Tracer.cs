using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.Models
{
  public class Tracer
  {
    public int TracerID { get; set; }

    public ICollection<Contact> Contacts { get; set; }
  }
}
