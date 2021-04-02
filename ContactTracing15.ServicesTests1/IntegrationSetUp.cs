using ContactTracing15.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactTracing15.ServicesTests1
{
    public class IntegrationSetUp
    {
        [ClassInitialize]
        public async Task SetUp()
        {
            var dbContextFactory = new AppDbContextFactory();
            

            using (var dbContext = dbContextFactory.CreateDbContext(new string[] { }))
            {
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
            }
        }
    }
}
