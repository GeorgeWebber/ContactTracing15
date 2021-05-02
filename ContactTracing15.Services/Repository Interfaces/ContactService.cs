using ContactTracing15.Models;
using ContactTracing15.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Reflection;
using ClosedXML.Excel;
using System.IO;

namespace ContactTracing15.Services
{ 
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        private readonly ICaseRepository _caseRepository;
        private readonly ITracerService _tracerService;

        public  ContactService(IContactRepository contactRepository, ICaseRepository caseRepository)
        {
            _contactRepository = contactRepository;
            _caseRepository = caseRepository;
        }
        Contact IContactService.Add(Contact newContact)
        {
            return _contactRepository.Add(newContact);
        }

        Contact IContactService.Delete(int id)
        {
            return _contactRepository.Delete(id);
        }

        IEnumerable<Contact> IContactService.GetAllContacts()
        {
            return _contactRepository.GetAllContacts();
        }

        Contact IContactService.GetContact(int id)
        {
            return _contactRepository.GetContact(id);
        }

        void IContactService.Save()
        {
            _contactRepository.Save();
        }

        IEnumerable<Contact> IContactService.Search(string searchTerm)
        {
            return _contactRepository.Search(searchTerm);
        }
        public IEnumerable<Contact> GetOldContacts(DateTime threshold)   
        {
            var low_bar = new DateTime(1000, 1, 1);
            return _contactRepository.GetContactsByDate(low_bar, threshold).Where(x => x.RemovedDate == null).ToList();
        }

        public Contact RemovePersonalData(int id)  //TODO, perhaps do this with SQL if it's faster, otherwise this is fine as is
        {
            var _contact = _contactRepository.GetContact(id);
            _contact.Forename = null;
            _contact.Surname = null;
            _contact.Email = null;
            _contact.Phone = null;
            _contact.RemovedDate = DateTime.Now;
            return _contactRepository.Update(_contact);
        }

        Contact IContactService.Update(Contact updatedContact)
        {
            return _contactRepository.Update(updatedContact);
        }

        int IContactService.TotalContactsReached()
        {
            return _contactRepository.GetAllContacts().Where(x => x.ContactedDate != null).ToList().Count();
        }

        double IContactService.AverageContactsPerCaseLast28Days()
        {
            int contacts = _contactRepository.GetContactsByDate(DateTime.Now.AddDays(-28), DateTime.Now).ToList().Count();
            int cases = _caseRepository.GetCasesByDate(DateTime.Now.AddDays(-28), DateTime.Now).Where(x => x.Traced).ToList().Count();
            if (cases == 0) { return 0; }
            return (double) contacts / cases;
        }

        void IContactService.ExportAsExcel(string folderPath)
        {
            DataTable dt = new DataTable();


            IEnumerable<Contact> contacts = _contactRepository.GetAllContacts();

            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("CaseId", typeof(int));
            dt.Columns.Add("Added Date", typeof(DateTime));
            dt.Columns.Add("Traced Date", typeof(String));
            dt.Columns.Add("Contacted date", typeof(String));
            dt.Columns.Add("Removed date", typeof(String));



            var i = 0;

            foreach (Contact _contact in contacts)
            {
                dt.Rows.Add();
                dt.Rows[i][0] = _contact.ContactID;
                dt.Rows[i][1] = _contact.CaseID;
                dt.Rows[i][2] = _contact.AddedDate;

                if (_contact.TracedDate != null)
                {
                    dt.Rows[i][7] = _contact.TracedDate.ToString();
                }
                if (_contact.ContactedDate != null)
                {
                    dt.Rows[i][8] = _contact.ContactedDate.ToString();
                }
                if (_contact.RemovedDate != null)
                {
                    dt.Rows[i][9] = _contact.RemovedDate.ToString();
                }
                i++;
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Contacts");
                wb.SaveAs(folderPath + "ExcelExportContacts.xlsx");
            }

        }
    }
}
