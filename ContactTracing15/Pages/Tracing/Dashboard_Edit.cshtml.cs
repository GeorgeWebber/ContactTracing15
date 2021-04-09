using ContactTracing15.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactTracing15.Pages.Tracing
{
    public class DashboardEditModel : BaseDashboardModel // TODO create table of all cases assigned to a tracer with routing to details pages for each case (and form for adding contacts)
    { 
        public DashboardEditModel(
            ITracerRepository tracerRepository,
            IUserService userService)
            :base(tracerRepository, userService)
        {
        }

        public IActionResult OnGet(int? caseId)
        {

            return Page();
        }
    }
}
