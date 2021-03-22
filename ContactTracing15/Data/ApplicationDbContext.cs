using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;

namespace ContactTracing15.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tracer> Tracers { get; set; }
        public DbSet<Tester> Testers { get; set; }
        public DbSet<Call> Calls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          modelBuilder.Entity<Case>().ToTable("Case");
          modelBuilder.Entity<Contact>().ToTable("Contact");
          modelBuilder.Entity<Tracer>().ToTable("Tracer");
          modelBuilder.Entity<Tester>().ToTable("Tester");
          modelBuilder.Entity<Call>().ToTable("Call");
        }
  }


}
