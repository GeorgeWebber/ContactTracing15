using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactTracing15.Models
{
  public class Tester
  {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TesterID { get; set; }
        public string Name { get; set; }

        public ICollection<Case> Cases { get; set; }
  }
}
