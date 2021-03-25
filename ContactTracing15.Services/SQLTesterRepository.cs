using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class SQLTesterRepository : ITesterRepository
    {
        private readonly AppDbContext context;

        public SQLTesterRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Tester Add(Tester newTester)
        {
            context.Testers.Add(newTester);
            context.SaveChanges();
            return newTester;
        }

        public Tester Delete(int id)
        {
            Tester deleting = context.Testers.Find(id);
            if (deleting != null)
            {
                context.Testers.Remove(deleting);
                context.SaveChanges();
            }
            return deleting;
        }

        public IEnumerable<Tester> GetAllTesters()
        {
            return context.Testers;
        }

        public Tester GetTester(int id)
        {
            return context.Testers
              .FromSqlRaw<Tester>("spGetTesterById {0}", id)
              .ToList()
              .FirstOrDefault();
        }

        public IEnumerable<Tester> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Testers;
            }

            return context.Testers.Where(e => e.Username.Contains(searchTerm)
                                            );
        }

        public Tester Update(Tester updatedTester)
        {
            var Tester = context.Testers.Attach(updatedTester);
            Tester.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedTester;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
