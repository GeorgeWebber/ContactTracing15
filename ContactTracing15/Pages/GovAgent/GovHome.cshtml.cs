using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ContactTracing15.Pages.GovAgent
{

    [Authorize(Policy = "TracersOnly")]
    public class GovHomeModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
