using ContactTracing15.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.ServicesTests1
{
    public abstract class IntegrationTestBase
    {
            protected AppDbContext dbContext = null!;

            [TestInitialize]
            public void SetUp()
            {
                var dbContextFactory = new AppDbContextFactory();
                dbContext = dbContextFactory.CreateDbContext(new string[] { });
            }

            [TestCleanup]
            public void TearDown()
            {
                if (dbContext != null)
                {
                    dbContext.Dispose();
                }
            }
        }
}
