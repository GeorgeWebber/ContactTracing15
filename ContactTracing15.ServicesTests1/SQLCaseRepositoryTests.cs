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
        public void AddTest()
        {

            testingCentreRepository.Add(testingCentre1);

            testerRepository.Add(tester1);

            

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