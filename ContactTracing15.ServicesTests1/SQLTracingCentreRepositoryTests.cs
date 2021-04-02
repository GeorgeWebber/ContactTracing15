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
    public class SQLTracingCentreRepositoryTests :  IntegrationTestBase
    {

        [TestMethod()]
        public void A10_AddTest()
        {
            tracingCentreRepository.Add(tracingCentre1);
            IEnumerable<TracingCentre> allCentres = tracingCentreRepository.GetAllTracingCentres();
            Boolean centreFound = false;

            foreach (TracingCentre centreFromDb in allCentres)
            {
                if (centreFromDb.Name == tracingCentre1.Name)
                {
                    centreFound = true;
                }
            }
            Assert.IsTrue(centreFound, "Testing centre not found in the database after supposed addition");
        }

        [TestMethod()]
        public void A50_DeleteTest()
        {
            IEnumerable<TracingCentre> allCentres = tracingCentreRepository.GetAllTracingCentres();

            List<int> idList = new List<int>();

            foreach (TracingCentre centreFromDb in allCentres)
            {
                int id = centreFromDb.TracingCentreID;
                idList.Add(id);
            }
            foreach (int id in idList)
            {
                tracingCentreRepository.Delete(id);
            }

            Assert.AreEqual(tracingCentreRepository.GetAllTracingCentres().Count(), 0);
        }

        [TestMethod()]
        public void A20_GetAllTracingCentresTest()
        {
            IEnumerable<TracingCentre> allCentres = tracingCentreRepository.GetAllTracingCentres();
            Assert.IsTrue(allCentres.Count() > 0, "Non-zero number of centres in the table");
            //Assert.AreEqual(allCentres.First().Name, tracingCentre1.Name);
        }

        [TestMethod()]
        public void A30_GetTracingCentreTest()
        {
            IEnumerable<TracingCentre> allCentres = tracingCentreRepository.GetAllTracingCentres();
            TracingCentre baseCentre = allCentres.First();

            Console.WriteLine(baseCentre.TracingCentreID);

            TracingCentre centreFromDb = tracingCentreRepository.GetTracingCentre(baseCentre.TracingCentreID);
            Assert.AreEqual(baseCentre.Name, centreFromDb.Name);
        }

        [TestMethod()]
        public void A40_UpdateTest()
        {
            IEnumerable<TracingCentre> allCentres = tracingCentreRepository.GetAllTracingCentres();
            TracingCentre baseCentre = allCentres.First();
            baseCentre.Name = "Updated Name";
            tracingCentreRepository.Update(baseCentre);

            TracingCentre centreFromDb2 = tracingCentreRepository.GetTracingCentre(baseCentre.TracingCentreID);

            Assert.AreEqual(baseCentre.Name, centreFromDb2.Name);
        }
    }
}