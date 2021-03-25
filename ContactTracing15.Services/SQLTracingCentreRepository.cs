using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class SQLTracingCentreRepository : ITracingCentreRepository
    {
        private readonly AppDbContext context;

        public SQLTracingCentreRepository(AppDbContext context)
        {
            this.context = context;
        }
        public TracingCentre Add(TracingCentre newTracingCentre)
        {
            context.TracingCentres.Add(newTracingCentre);
            context.SaveChanges();
            return newTracingCentre;
        }

        public TracingCentre Delete(int id)
        {
            TracingCentre deleting = context.TracingCentres.Find(id);
            if (deleting != null)
            {
                context.TracingCentres.Remove(deleting);
                context.SaveChanges();
            }
            return deleting;
        }

        public IEnumerable<TracingCentre> GetAllTracingCentres()
        {
            return context.TracingCentres;
        }

        public TracingCentre GetTracingCentre(int id)
        {
            return context.TracingCentres
              .FromSqlRaw<TracingCentre>("spGetTracingCentreById {0}", id)
              .ToList()
              .FirstOrDefault();
        }

        public TracingCentre Update(TracingCentre updatedTracingCentre)
        {
            var TracingCentre = context.TracingCentres.Attach(updatedTracingCentre);
            TracingCentre.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedTracingCentre;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
