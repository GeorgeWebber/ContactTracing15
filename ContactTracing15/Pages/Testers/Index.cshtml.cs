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
    public class IndexModel : PageModel
    {
        private readonly ContactTracing15.Data.MainDBContext _context;

        public IndexModel(ContactTracing15.Data.MainDBContext context)
        {
            _context = context;
        }

        public IList<Tester> Tester { get;set; }

        public async Task OnGetAsync()
        {
            Tester = await _context.Testers.ToListAsync();
        }
    }
}
