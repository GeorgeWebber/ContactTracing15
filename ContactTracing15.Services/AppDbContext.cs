using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    /// <summary>
    /// "DbContext instance represents a combination of the Unit Of Work and Repository patterns such that it can be used to query from a database and group together changes that will then be written back to the store as a unit" 
    /// AppDbContext is a subclass of DbContext that facilitates database access from within other classes.
    /// </summary>
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
