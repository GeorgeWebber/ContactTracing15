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
    public class SQLTracerRepositoryTests : IntegrationTestBase
    {
        [TestInitialize()]
        public void SetUpTests()
        {
            tracingCentreRepository.Add(tracingCentre1);

            int tracingCentreID = tracingCentreRepository.GetAllTracingCentres().First().TracingCentreID;
            tracer1.TracingCentreID = tracingCentreID;

        }

        [TestMethod()]
        public void A10_AddTest()
        {
            tracerRepository.Add(tracer1);
            IEnumerable<Tracer> allTracers = tracerRepository.GetAllTracers();
            Boolean tracerFound = false;

            foreach (Tracer tracerFromDb in allTracers)
            {
                if (tracerFromDb.Username == tracer1.Username)
                {
                    tracerFound = true;
                }
            }
            Assert.IsTrue(tracerFound, "Tracer not found in the database after supposed addition");
        }

        [TestMethod()]
        public void A60_DeleteTest()
        {
            IEnumerable<Tracer> allTracers = tracerRepository.GetAllTracers();

            List<int> idList = new List<int>();

            foreach (Tracer tracerFromDb in allTracers)
            {
                int id = tracerFromDb.TracingCentreID;
                idList.Add(id);
            }
            foreach (int id in idList)
            {
                tracerRepository.Delete(id);
            }

            Assert.AreEqual(tracerRepository.GetAllTracers().Count(), 0);
        }

        [TestMethod()]
        public void A20_GetAllTracersTest()
        {
            IEnumerable<Tracer> allTracers = tracerRepository.GetAllTracers();
            Assert.IsTrue(allTracers.Count() > 0, "Non-zero number of centres in the table");
            //Assert.AreEqual(allCentres.First().Name, tracer1.Name);
        }

        [TestMethod()]
        public void A30_GetTracerTest()
        {
            IEnumerable<Tracer> allTracers = tracerRepository.GetAllTracers();
            Tracer baseTracer = allTracers.First();

            Console.WriteLine(baseTracer.TracingCentreID);

            Tracer tracerFromDb = tracerRepository.GetTracer(baseTracer.TracerID);
            Assert.AreEqual(baseTracer.Username, tracerFromDb.Username);
        }

        [TestMethod()]
        public void A50_SearchTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void A40_UpdateTest()
        {
            IEnumerable<Tracer> allTracers = tracerRepository.GetAllTracers();
            Tracer baseTracer = allTracers.First();
            baseTracer.Username = "Updated Username";
            tracerRepository.Update(baseTracer);

            Tracer tracerFromDb2 = tracerRepository.GetTracer(baseTracer.TracerID);

            Assert.AreEqual(baseTracer.Username, tracerFromDb2.Username);
        }

    }
}