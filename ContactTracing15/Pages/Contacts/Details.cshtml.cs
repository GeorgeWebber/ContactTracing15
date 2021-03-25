using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactTracing15.Data;
using ContactTracing15.Models;

namespace ContactTracing15.Pages.Contacts
{
    public class DetailsModel : PageModel
    {
        private readonly ContactTracing15.Data.MainDBContext _context;

        public DetailsModel(ContactTracing15.Data.MainDBContext context)
        {
            _context = context;
        }

        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contact = await _context.Contacts
                .Include(c => c.Case)
                .Include(c => c.Tracer).FirstOrDefaultAsync(m => m.ContactID == id);

            if (Contact == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
