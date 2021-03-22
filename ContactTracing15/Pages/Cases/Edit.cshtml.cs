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

namespace ContactTracing15.Pages.Cases
{
    public class EditModel : PageModel
    {
        private readonly ContactTracing15.Data.ApplicationDbContext _context;

        public EditModel(ContactTracing15.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Case Case { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Case = await _context.Cases
                .Include(_ => _.Tester).FirstOrDefaultAsync(m => m.CaseID == id);

            if (Case == null)
            {
                return NotFound();
            }
           ViewData["TesterID"] = new SelectList(_context.Set<Tester>(), "TesterID", "TesterID");
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

            _context.Attach(Case).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaseExists(Case.CaseID))
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

        private bool CaseExists(int id)
        {
            return _context.Cases.Any(e => e.CaseID == id);
        }
    }
}
