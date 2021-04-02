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
        public void A10_AddTest()
        {
            testingCentreRepository.Add(testingCentre1);
            IEnumerable<TestingCentre> allCentres = testingCentreRepository.GetAllTestingCentres();
            Boolean centreFound = false;

            foreach (TestingCentre centreFromDb in allCentres)
            {
                if (centreFromDb.Name == testingCentre1.Name)
                {
                    centreFound = true;
                }
            }
            Assert.IsTrue(centreFound, "Testing centre not found in the database after supposed addition");

        }
        [TestMethod()]
        public void A20_GetAllTestingCentresTest()
        {

            IEnumerable<TestingCentre> allCentres = testingCentreRepository.GetAllTestingCentres();
            Assert.IsTrue(allCentres.Count() > 0, "Non-zero number of centres in the table");
            //Assert.AreEqual(allCentres.First().Name, testingCentre1.Name);

        }

        [TestMethod()]
        public void A30_GetTestingCentreTest()
        {
            IEnumerable<TestingCentre> allCentres = testingCentreRepository.GetAllTestingCentres();
            TestingCentre baseCentre = allCentres.First();

            TestingCentre centreFromDb = testingCentreRepository.GetTestingCentre(baseCentre.TestingCentreID);
            Assert.AreEqual(baseCentre.Name, centreFromDb.Name);

        }        

        [TestMethod()]
        public void A40_UpdateTest()
        {
            IEnumerable<TestingCentre> allCentres = testingCentreRepository.GetAllTestingCentres();
            TestingCentre baseCentre = allCentres.First();
            baseCentre.Name = "Updated Name";
            testingCentreRepository.Update(baseCentre);

            TestingCentre centreFromDb2 = testingCentreRepository.GetTestingCentre(baseCentre.TestingCentreID);

            Assert.AreEqual(baseCentre.Name, centreFromDb2.Name);
        }

        [TestMethod()]
        public void A50_DeleteTest()
        {
            IEnumerable<TestingCentre> allCentres = testingCentreRepository.GetAllTestingCentres();

            List<int> idList = new List<int>();

            foreach (TestingCentre centreFromDb in allCentres)
            {
                int id = centreFromDb.TestingCentreID;
                idList.Add(id);
            }
            foreach (int id in idList)
            {
                testingCentreRepository.Delete(id);
            }
       
            Assert.AreEqual(testingCentreRepository.GetAllTestingCentres().Count(), 0);

        }
    }
}