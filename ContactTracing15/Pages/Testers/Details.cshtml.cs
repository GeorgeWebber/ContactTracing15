using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContactTracing15.Data;
using ContactTracing15.Models;

namespace ContactTracing15.Pages.Testers
{
    public class DetailsModel : PageModel
    {
        private readonly ContactTracing15.Data.MainDBContext _context;

        public DetailsModel(ContactTracing15.Data.MainDBContext context)
        {
            _context = context;
        }

        public Tester Tester { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tester = await _context.Testers.FirstOrDefaultAsync(m => m.TesterID == id);

            if (Tester == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
