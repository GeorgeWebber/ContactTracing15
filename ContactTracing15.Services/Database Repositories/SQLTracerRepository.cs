using ContactTracing15.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ContactTracing15.Services
{
    public class SQLTracerRepository : ITracerRepository
    {
        private readonly AppDbContext context;

        public SQLTracerRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Tracer Add(Tracer newTracer)
        {
            context.Tracers.Add(newTracer);
            context.SaveChanges();
            return newTracer;
        }

        public Tracer Delete(int id)
        {
            Tracer deleting = context.Tracers.Find(id);
            if (deleting != null)
            {
                context.Tracers.Remove(deleting);
                context.SaveChanges();
            }
            return deleting;
        }

        public IEnumerable<Tracer> GetAllTracers()
        {
            return context.Tracers;
        }

        public Tracer GetTracer(int id)
        {
            return context.Tracers
              .Include(x => x.Cases)
              .Where(x => x.TracerID == id)
              .ToList()
              .FirstOrDefault();
        }

        public Tracer GetTracer(string name)
        {
            return context.Tracers
              .Where(x => x.Username == name)
              .ToList()
              .FirstOrDefault();
        }

        public IEnumerable<Tracer> GetTracerWithLeastCases()
        {
            return context.Tracers
              .FromSqlRaw<Tracer>(@"select * from tracers where  TracerID in (
                    select top (2) t.tracerid
                    from tracers t 
                    left join (select * from cases where (Traced = 0)) c
                    on c.tracerid = t.tracerID
                    group by t.tracerid
                    order by count(c.tracerid) asc)")

              .ToList();
        }

        public IEnumerable<Tracer> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.Tracers;
            }

            return context.Tracers.Where(e => e.Username.Contains(searchTerm)
                                            );
        }

        public Tracer Update(Tracer updatedTracer)
        {
            var Tracer = context.Tracers.Attach(updatedTracer);
            Tracer.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return updatedTracer;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
