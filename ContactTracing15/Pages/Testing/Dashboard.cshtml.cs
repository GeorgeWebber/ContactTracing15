using ContactTracing15.Helper.Extensions;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System;

namespace ContactTracing15.Pages.Testing
{
    [Authorize(Policy = "TestersOnly")]
    public class TesterDashboardModel : PageModel
    {
        public TesterDashboardModel(IUserService userService, ICaseService caseService)
        {
            _UserService = userService;
            _CaseService = caseService;
            Details = new DashboardDetails();
        }
        private readonly IUserService _UserService;
        private readonly ICaseService _CaseService;

        public void OnGet(int? lastCaseId)
        {
            var claims = HttpContext.User.Claims;
            var CurrentUser = _UserService.GetUserByUserName(claims.Single(x => x.Type == "preferred_username").Value, int.Parse(claims.Single(x => x.Type == "usrtype").Value));
            Details.Username = CurrentUser.UserName;

            if(lastCaseId != null)
            {
                var lastCase = _CaseService.GetCase(lastCaseId.Value);
                if(lastCase != null && lastCase.TesterID == CurrentUser.UserId && lastCase.Forename != null)
                {
                    Details.lastCaseName = lastCase.GetFullName();
                }
                    
            }
        }

        [BindProperty]
        public DashboardDetails Details { get; set; }
    }
    public class DashboardDetails
    {
        public string Username { get; set; }
        public string? lastCaseName { get; set; }

    }

}
