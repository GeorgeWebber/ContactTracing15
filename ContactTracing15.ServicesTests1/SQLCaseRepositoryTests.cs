using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactTracing15.Services;
using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.ServicesTests1;
using ContactTracing15.Models;

namespace ContactTracing15.Services.Tests
{
    [TestClass()]
    public class SQLCaseRepositoryTests : IntegrationTestBase
    {

        [TestMethod()]
        public void SQLCaseRepositoryTest()
        {

            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest()
        {
            SQLTestingCentreRepository testingCentreRepository = new SQLTestingCentreRepository(dbContext);
            TestingCentre testCentre = new TestingCentre();
            testCentre.Name = "Centre #1";

            testingCentreRepository.Add(testCentre);



            SQLTesterRepository testerRepository = new SQLTesterRepository(dbContext);
            Tester tester = new Tester();
            tester.Username = "testing_user1";
            tester.TestingCentre = testCentre;
            testerRepository.Add(tester);

            SQLCaseRepository caseRepository = new SQLCaseRepository(dbContext); 

            Case case1 = new Case();
            case1.AddedDate = new DateTime(3, 3, 3, 3, 3, 3, 3);
            case1.TestDate = new DateTime(4, 4, 4, 4, 4, 4, 4);
            case1.Forename = "John";
            case1.Surname = "Doe";
            case1.Phone = "+0123456789";
            case1.Postcode = "AB12";
            case1.Traced = false;
            case1.TracerID = 1;

            caseRepository.Add(case1);

            Case caseFromDb = caseRepository.GetCase(1);

            Assert.AreEqual(caseFromDb.Forename, case1.Forename);
            Assert.AreEqual(caseFromDb.Surname, case1.Surname);
            Assert.AreEqual(caseFromDb.Traced , case1.Traced);

        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllCasesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCaseTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SearchTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SaveTest()
        {
            Assert.Fail();
        }
    }
}