using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactTracing15.Models
{
  public class Case
  {
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int CaseID { get; set; }
    public string Forename { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Postcode { get; set; }
    public string Phone { get; set; }
    public string? Phone2 { get; set; }
    public int TesterID { get; set; }
    public DateTime TestDate { get; set; }
    public DateTime AddedDate { get; set; }
    public DateTime? RemovedDate { get; set; }

    public Tester Tester { get; set; }

  }
}
