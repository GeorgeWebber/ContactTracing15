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
    public class SQLCaseRepositoryTests : IntegrationTestBase
    {

        [TestInitialize()]
        public void SetUpTests()
        {
            testingCentreRepository.Add(testingCentre1);

            int testingCentreID = testingCentreRepository.GetAllTestingCentres().First().TestingCentreID;
            tester1.TestingCentreID = testingCentreID;
            testerRepository.Add(tester1);

            int testerID = testerRepository.GetAllTesters().First().TesterID;
            case1.TesterID = testerID;

        }


        [TestMethod()]
        public void A10_AddTest()
        {

            caseRepository.Add(case1);
            IEnumerable<Case> allCases = caseRepository.GetAllCases();
            Boolean caseFound = false;

            foreach (Case caseFromDb in allCases)
            {
                if (caseFromDb.Forename == case1.Forename && caseFromDb.Surname == case1.Surname)
                {
                    caseFound = true;
                }
            }
            Assert.IsTrue(caseFound, "Case not found in the database after supposed addition");
        }

        [TestMethod()]
        public void A50_DeleteTest()
        {
            IEnumerable<Case> allCases = caseRepository.GetAllCases();

            List<int> idList = new List<int>();

            foreach (Case caseFromDb in allCases)
            {
                int id = caseFromDb.CaseID;
                idList.Add(id);
            }
            foreach (int id in idList)
            {
                caseRepository.Delete(id);
            }

            Assert.AreEqual(caseRepository.GetAllCases().Count(), 0);
        }

        [TestMethod()]
        public void A20_GetAllCasesTest()
        {
            IEnumerable<Case> allCases = caseRepository.GetAllCases();
            Assert.IsTrue(allCases.Count() > 0, "Non-zero number of cases in the table");
            //Assert.AreEqual(allTesters.First().Name, case1.Name);
        }

        [TestMethod()]
        public void A30_GetCaseTest()
        {
            IEnumerable<Case> allCases = caseRepository.GetAllCases();
            Case baseCase = allCases.First();

            Console.WriteLine(baseCase.CaseID);

            Case caseFromDb = caseRepository.GetCase(baseCase.CaseID);
            Assert.AreEqual(baseCase.Forename, caseFromDb.Forename);
            Assert.AreEqual(baseCase.Surname, caseFromDb.Surname);
            Assert.AreEqual(baseCase.AddedDate, caseFromDb.AddedDate);
        }

        [TestMethod()]
        public void A40_UpdateTest()
        {
            IEnumerable<Case> allCases = caseRepository.GetAllCases();
            Case baseCase = allCases.First();
            baseCase.Forename = "Updated Jane";
            caseRepository.Update(baseCase);

            Case caseFromDb2 = caseRepository.GetCase(baseCase.CaseID);

            Assert.AreEqual(baseCase.Forename, caseFromDb2.Forename);
        }
    }
}