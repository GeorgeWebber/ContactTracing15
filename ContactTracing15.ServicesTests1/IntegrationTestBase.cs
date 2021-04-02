using ContactTracing15.Models;
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

        protected Case case1;
        protected Contact contact1;
        protected Tester tester1;
        protected TestingCentre testingCentre1;
        protected Tracer tracer1;
        protected TracingCentre tracingCentre1;


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

            MakeCase1();
            MakeContact1();
            MakeTester1();
            MakeTestingCentre1();
            MakeTracer1();
            MakeTracingCentre1();

            
        }       

        private void MakeCase1()
        {
            case1 = new Case
            {
                AddedDate = new DateTime(3, 3, 3, 3, 3, 3, 3),
                TestDate = new DateTime(4, 4, 4, 4, 4, 4, 4),
                Forename = "John",
                Surname = "Doe",
                Phone = "+0123456789",
                Postcode = "AB12",
                Traced = false,
                TracerID = 1
            };
        }

        private void MakeContact1()
        {
            contact1 = new Contact
            {
                Forename = "Jane",
                Surname = "Doe",
                CaseID = 1,
                Email = "jane.doe@email.com",
                AddedDate = new DateTime(2021, 3, 30),
                Phone = "0123456789",
                TracedDate = new DateTime(2021, 3, 31)
            };
        }

        private void MakeTester1()
        {
            tester1 = new Tester
            {
                Username = "Test_Username",
                //TODO: investigate robustness of hard-coding ID values
                TestingCentreID = 1
            };
        }

        private void MakeTestingCentre1()
        {
            testingCentre1 = new TestingCentre
            {
                Name = "Test_Testing_Centre",
                //testingCentre1.RegistrationCode = 123456;
                Postcode = "AB12 CDE"
                //TODO: investigate robustness of hard-coding ID values
                
            };
        }

        private void MakeTracer1()
        {
            tracer1 = new Tracer
            {
                Username = "Sally_Tracer",
                TracingCentreID = 1
            };
        }

        private void MakeTracingCentre1()
        {
            tracingCentre1 = new TracingCentre
            {
                Name = "Tracing Centre #1",
                Postcode = "FG34 HIJ"
            };
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
