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
        protected SQLCaseRepository caseRepository;
        protected SQLContactRepository contactRepository;
        protected SQLTesterRepository testerRepository;
        protected SQLTestingCentreRepository testingCentreRepository;
        protected SQLTracerRepository tracerRepository;
        protected SQLTracingCentreRepository tracingCentreRepository;


        [TestInitialize]
        public void SetUp()
        {
            var dbContextFactory = new AppDbContextFactory();
            dbContext = dbContextFactory.CreateDbContext(new string[] { });
            caseRepository = new SQLCaseRepository(dbContext);
            contactRepository = new SQLContactRepository(dbContext);
            testerRepository = new SQLTesterRepository(dbContext);
            testingCentreRepository = new SQLTestingCentreRepository(dbContext);
            tracerRepository = new SQLTracerRepository(dbContext);
            tracingCentreRepository = new SQLTracingCentreRepository(dbContext);
            

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
