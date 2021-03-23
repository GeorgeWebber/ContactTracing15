using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ContactTracing15.Models;

namespace ContactTracing15.Data
{
    public class MainDBContext : DbContext
    {
        public MainDBContext (DbContextOptions<MainDBContext> options)
            : base(options)
        {
        }

        public DbSet<ContactTracing15.Models.Case> Cases { get; set; }
        public DbSet<ContactTracing15.Models.Contact> Contacts { get; set; }
        public DbSet<ContactTracing15.Models.Tester> Testers { get; set; }
        public DbSet<ContactTracing15.Models.Tracer> Tracers { get; set; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Case>().ToTable("Case");
            modelBuilder.Entity<Contact>().ToTable("Contact");
            modelBuilder.Entity<Tester>().ToTable("Tester");
            modelBuilder.Entity<Tracer>().ToTable("Tracer");
        }
       
    }


}
