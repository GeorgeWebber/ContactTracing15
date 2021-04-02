using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactTracing15.Services;
using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.ServicesTests1;
using System.Linq;
using ContactTracing15.Models;

namespace ContactTracing15.Services.Tests
{
    [TestClass()]
    public class SQLTesterRepositoryTests : IntegrationTestBase
    {
        [TestInitialize()]
        public void SetUpTests()
        {
            testingCentreRepository.Add(testingCentre1);

            int testingCentreID = testingCentreRepository.GetAllTestingCentres().First().TestingCentreID;
            tester1.TestingCentreID = testingCentreID;

        }

        [TestMethod()]
        public void A10_AddTest()
        {
            testerRepository.Add(tester1);
            IEnumerable<Tester> allTesters = testerRepository.GetAllTesters();
            Boolean testerFound = false;

            foreach (Tester testerFromDb in allTesters)
            {
                if (testerFromDb.Username == tester1.Username)
                {
                    testerFound = true;
                }
            }
            Assert.IsTrue(testerFound, "Tester not found in the database after supposed addition");
        }

        [TestMethod()]
        public void A50_DeleteTest()
        {
            IEnumerable<Tester> allTesters = testerRepository.GetAllTesters();

            List<int> idList = new List<int>();

            foreach (Tester testerFromDb in allTesters)
            {
                int id = testerFromDb.TesterID;
                idList.Add(id);
            }
            foreach (int id in idList)
            {
                testerRepository.Delete(id);
            }

            Assert.AreEqual(testerRepository.GetAllTesters().Count(), 0);
        }

        [TestMethod()]
        public void A20_GetAllTestersTest()
        {
            IEnumerable<Tester> allTesters = testerRepository.GetAllTesters();
            Assert.IsTrue(allTesters.Count() > 0, "Non-zero number of testers in the table");
            //Assert.AreEqual(allTesters.First().Name, tester1.Name);
        }

        [TestMethod()]
        public void A30_GetTesterTest()
        {
            IEnumerable<Tester> allTesters = testerRepository.GetAllTesters();
            Tester baseTester = allTesters.First();

            Console.WriteLine(baseTester.TestingCentreID);

            Tester testerFromDb = testerRepository.GetTester(baseTester.TesterID);
            Assert.AreEqual(baseTester.Username, testerFromDb.Username);
        }

        [TestMethod()]
        public void A40_UpdateTest()
        {
            IEnumerable<Tester> allTesters = testerRepository.GetAllTesters();
            Tester baseTester = allTesters.First();
            baseTester.Username = "Updated Username";
            testerRepository.Update(baseTester);

            Tester testerFromDb2 = testerRepository.GetTester(baseTester.TesterID);

            Assert.AreEqual(baseTester.Username, testerFromDb2.Username);
        }

    }
}