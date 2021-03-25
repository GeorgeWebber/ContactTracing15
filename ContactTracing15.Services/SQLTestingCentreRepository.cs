using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class SQLTestingCentreRepository : ITestingCentreRepository
    {
        private readonly AppDbContext context;

        public SQLTestingCentreRepository(AppDbContext context)
        {
            this.context = context;
        }
        public TestingCentre Add(TestingCentre newTestingCentre)
        {
            context.TestingCentres.Add(newTestingCentre);
            context.SaveChanges();
            return newTestingCentre;
        }

        public TestingCentre Delete(int id)
        {
            TestingCentre deleting = context.TestingCentres.Find(id);
            if (deleting != null)
            {
                context.TestingCentres.Remove(deleting);
                context.SaveChanges();
            }
            return deleting;
        }

        public IEnumerable<TestingCentre> GetAllTestingCentres()
        {
            return context.TestingCentres;
        }

        public TestingCentre GetTestingCentre(int id)
        {
            return context.TestingCentres
              .FromSqlRaw<TestingCentre>("spGetTestingCentreById {0}", id)
              .ToList()
              .FirstOrDefault();
        }

        public TestingCentre Update(TestingCentre updatedTestingCentre)
        {
            var TestingCentre = context.TestingCentres.Attach(updatedTestingCentre);
            TestingCentre.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedTestingCentre;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
