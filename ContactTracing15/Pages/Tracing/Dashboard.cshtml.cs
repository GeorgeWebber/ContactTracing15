using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactTracing15.Pages.Tracing
{
    public class DashboardModel : PageModel // TODO create table of all cases assigned to a tracer with routing to details pages for each case (and form for adding contacts)
    {
        public void OnGet()
        {
        }
    }
}
