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
              .FromSqlRaw<Tracer>("spGetTracerById {0}", id)
              .ToList()
              .FirstOrDefault();
        }

        public Tracer GetTracer(string name)
        {
            return context.Tracers
              .FromSqlRaw<Tracer>(@"@TracerName int
                                    as
                                    Begin
                                        Select * from Tracers
                                        where Username = @Tracername
                                    End", name)
              .ToList()
              .FirstOrDefault();
        }
        
        public Tracer GetTracerWithLeastCases()
        {
            return context.Tracers
              .FromSqlRaw<Tracer>(@"select top (1) t.TracerID
                                    from Tracers t left join
                                        Cases c
                                        on c.TracerID = t.TracerID
                                    group by t.TracerID
                                    order by count(c.TracerID) asc;")
              .ToList()
              .FirstOrDefault();
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
