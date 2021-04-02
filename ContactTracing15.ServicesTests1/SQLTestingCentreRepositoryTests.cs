using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactTracing15.Services;
using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.ServicesTests1;
using ContactTracing15.Models;
using System.Linq;

namespace ContactTracing15.Services.Tests
{
    [TestClass()]
    public class SQLTestingCentreRepositoryTests : IntegrationTestBase
    {
        [TestMethod()]
        public void SQLTestingCentreRepositoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AddTest()
        {

            TestingCentre testCentre = new TestingCentre();
            testCentre.Name = "Centre #1";
            testingCentreRepository.Add(testCentre);


            IEnumerable<TestingCentre> allCentres = testingCentreRepository.GetAllTestingCentres();

            TestingCentre centreFromDb = allCentres.First();
            Assert.AreEqual(testCentre.Name, centreFromDb.Name);

        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetAllTestingCentresTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetTestingCentreTest()
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