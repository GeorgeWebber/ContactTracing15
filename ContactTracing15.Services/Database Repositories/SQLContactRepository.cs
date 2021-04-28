using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class SQLContactRepository : IContactRepository
    {
        private readonly AppDbContext context;

        public SQLContactRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Contact Add(Contact newContact)
        {
            context.Contacts.Add(newContact);
            context.SaveChanges();
            return newContact;
        }

        public Contact Delete(int id)
        {
            Contact deleting = context.Contacts.Find(id);
            if (deleting != null)
            {
                context.Contacts.Remove(deleting);
                context.SaveChanges();
            }
            return deleting;
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return context.Contacts;
        }

        public Contact GetContact(int id)
        {
            return context.Contacts
              .Where(x => x.ContactID == id)
              .ToList()
              .FirstOrDefault();
        }

        public IEnumerable<Contact> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Contacts;
            }

            return context.Contacts.Where(e => e.Forename.Contains(searchTerm) ||
                                            e.Surname.Contains(searchTerm) ||
                                            e.Phone.Contains(searchTerm) ||
                                            e.Email.Contains(searchTerm) 
                                            );
        }

        public Contact Update(Contact updatedContact)
        {
            var Contact = context.Contacts.Attach(updatedContact);
            Contact.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedContact;
        }
        public IEnumerable<Contact> GetContactsByDate(DateTime from_, DateTime to_)
        {
            return context.Contacts.FromSqlRaw<Contact>(@"SELECT * FROM Contacts WHERE AddedDate between {0} AND {1}", from_, to_).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
