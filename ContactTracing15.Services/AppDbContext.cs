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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestingCentre>().HasData(
                new TestingCentre
                {
                    TestingCentreID = 1,
                    Name = "centre1",
                    Postcode = "OX1"
                },
                new TestingCentre
                {
                    TestingCentreID = 2,
                    Name = "centre2",
                    Postcode = "BH17"
                }
            );
            modelBuilder.Entity<TracingCentre>().HasData(
                new TracingCentre
                {
                    TracingCentreID = 1,
                    Name = "centre1",
                    Postcode = "OX1"
                },
                new TracingCentre
                {
                    TracingCentreID = 2,
                    Name = "centre2",
                    Postcode = "BH17"
                }
            );
            modelBuilder.Entity<Tester>().HasData(
                new Tester
                {
                    TesterID = 1,
                    Username = "tester2@123.com",
                    TestingCentreID = 1
                },
                new Tester
                {
                    TesterID = 2,
                    Username = "tester3@123.com",
                    TestingCentreID = 2
                }
            );
            modelBuilder.Entity<Tracer>().HasData(
                new Tracer
                {
                    TracerID = 1,
                    Username = "tracer2@123.com",
                    TracingCentreID = 1
                },
                new Tracer
                {
                    TracerID = 2,
                    Username = "tracer3@123.com",
                    TracingCentreID = 2
                }
            );
        }

    }
}
