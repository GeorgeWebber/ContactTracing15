using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContactTracing15.Data;
using ContactTracing15.Models;

namespace ContactTracing15.Pages.Contacts
{
    public class EditModel : PageModel
    {
        private readonly ContactTracing15.Data.MainDBContext _context;

        public EditModel(ContactTracing15.Data.MainDBContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["CaseID"] = new SelectList(_context.Cases, "CaseID", "CaseID");
           ViewData["TracerID"] = new SelectList(_context.Tracers, "TracerID", "TracerID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(Contact.ContactID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactID == id);
        }
    }
}
