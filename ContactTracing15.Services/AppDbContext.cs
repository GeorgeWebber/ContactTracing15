using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<Case> Cases { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tester> Testers { get; set; }
        public DbSet<Tracer> Tracers { get; set; }
        public DbSet<TestingCentre> TestingCentres { get; set; }
        public DbSet<TracingCentre> TracingCentres { get; set; }

    }
}
