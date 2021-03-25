using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactTracing15.Models
{
  public class Tracer
  {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TracerID { get; set; }
        public string Name { get; set; }

        public ICollection<Contact> Contacts { get; set; }
    }
}
