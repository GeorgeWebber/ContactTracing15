using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactTracing15.Models;

namespace ContactTracing15.Data
{
    public class CovidTestingContext : DbContext
    {
        public CovidTestingContext (DbContextOptions<CovidTestingContext> options)
            : base(options)
        {
        }

        public DbSet<ContactTracing15.Models.Case> Case { get; set; }
    }
}
