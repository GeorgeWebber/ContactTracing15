using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class SQLCaseRepository : ICaseRepository
    {
        private readonly AppDbContext context;

        public SQLCaseRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Case Add(Case newCase)
        {
            context.Cases.Add(newCase);
            context.SaveChanges();
            return newCase;
        }

        public Case Delete(int id)
        {
            Case deleting = context.Cases.Find(id);
            if (deleting != null)
            {
                context.Cases.Remove(deleting);
                context.SaveChanges();
            }
            return deleting;
        }

        public IEnumerable<Case> GetAllCases()
        {
            return context.Cases;
        }

        public IEnumerable<Case> GetCasesByDate(DateTime from_, DateTime to_)
        {
            return context.Cases.FromSqlRaw<Case>(@"SELECT * FROM Cases WHERE AddedDate between {0} AND {1}", from_, to_).ToList();
        }

        public IEnumerable<String> GetpostcodesByDate(DateTime from_, DateTime to_)
        {
            return (IEnumerable<string>)context.Cases.FromSqlRaw(@"SELECT Postcode FROM Cases WHERE AddedDate between {0} and {1}", from_, to_).ToList();
        }

        public Case GetCase(int id)
        {
            return context.Cases
              .FromSqlRaw<Case>("spGetCaseById {0}", id)
              .Include(p => p.Contacts)
              .ToList()
              .FirstOrDefault();
        }

        public IEnumerable<Case> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Cases;
            }

            return context.Cases.Where(e => e.Forename.Contains(searchTerm) ||
                                            e.Surname.Contains(searchTerm)||
                                            e.Phone.Contains(searchTerm)||
                                            e.Phone2.Contains(searchTerm)||
                                            e.Email.Contains(searchTerm)||
                                            e.Postcode.Contains(searchTerm)
                                            );
        }

        public Case Update(Case updatedCase)
        {
            var Case = context.Cases.Attach(updatedCase);
            Case.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedCase;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
