using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    /// <summary>
    /// Interface specifying how the TracingCentre database table is to be interacted with.
    /// </summary>
    public interface ITracingCentreRepository
    {
        IEnumerable<TracingCentre> GetAllTracingCentres();
        TracingCentre GetTracingCentre(int id);
        TracingCentre Add(TracingCentre newTracingCentre);
        TracingCentre Update(TracingCentre updatedTracingCentre);
        TracingCentre Delete(int id);
        void Save();
    }
}
