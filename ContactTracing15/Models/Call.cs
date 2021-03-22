using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactTracing15.Models
{
  public class Call
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CallID { get; set; }
    public int TracerID { get; set; }
    public int ContactID { get; set; }
    
    public Tracer Tracer { get; set; }
    public Contact Contact { get; set; }
  }
}
