using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
            public AppDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
                optionsBuilder.UseSqlServer(
                  "Server=(localdb)\\mssqllocaldb;Database=TracingAppDB;Trusted_Connection=True;");

                return new AppDbContext(optionsBuilder.Options);
            }
    }
}
