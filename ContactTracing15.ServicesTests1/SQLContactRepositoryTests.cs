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
    public class SQLContactRepositoryTests : IntegrationTestBase
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
            caseRepository.Add(case1);

            int caseID = caseRepository.GetAllCases().First().CaseID;
            contact1.CaseID = caseID;



        }

        [TestMethod()]
        public void A10_AddTest()
        {
            contactRepository.Add(contact1);
            IEnumerable<Contact> allContacts = contactRepository.GetAllContacts();
            Boolean contactFound = false;

            foreach (Contact contactFromDb in allContacts)
            {
                if (contactFromDb.Forename == contact1.Forename && contactFromDb.Surname == contact1.Surname)
                {
                    contactFound = true;
                }
            }
            Assert.IsTrue(contactFound, "Contact not found in the database after supposed addition");

        }

        [TestMethod()]
        public void A50_DeleteTest()
        {
            IEnumerable<Contact> allContacts = contactRepository.GetAllContacts();

            List<int> idList = new List<int>();

            foreach (Contact contactFromDb in allContacts)
            {
                int id = contactFromDb.ContactID;
                idList.Add(id);
            }
            foreach (int id in idList)
            {
                contactRepository.Delete(id);
            }

            Assert.AreEqual(contactRepository.GetAllContacts().Count(), 0);
        }

        [TestMethod()]
        public void A20_GetAllContactsTest()
        {
            IEnumerable<Contact> allContacts = contactRepository.GetAllContacts();
            Assert.IsTrue(allContacts.Count() > 0, "Non-zero number of contacts in the table");
            //Assert.AreEqual(allTesters.First().Name, contact1.Name);
        }

        [TestMethod()]
        public void A30_GetContactTest()
        {
            IEnumerable<Contact> allContacts = contactRepository.GetAllContacts();
            Contact baseContact = allContacts.First();

            Console.WriteLine(baseContact.ContactID);

            Contact contactFromDb = contactRepository.GetContact(baseContact.ContactID);
            Assert.AreEqual(baseContact.Forename, contactFromDb.Forename);
            Assert.AreEqual(baseContact.Surname, contactFromDb.Surname);
            Assert.AreEqual(baseContact.AddedDate, contactFromDb.AddedDate);
        }

        [TestMethod()]
        public void A40_UpdateTest()
        {
            IEnumerable<Contact> allContacts = contactRepository.GetAllContacts();
            Contact baseContact = allContacts.First();
            baseContact.Forename = "Updated John";
            contactRepository.Update(baseContact);

            Contact contactFromDb2 = contactRepository.GetContact(baseContact.ContactID);

            Assert.AreEqual(baseContact.Forename, contactFromDb2.Forename);
        }
    }
}