using ContactTracing15.Helper.Extensions;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.Pages.Tracing
{
    public class BaseDashboardModel :  PageModel   
    {
        public BaseDashboardModel(
            ITracerService tracerService,
            IUserService userService)
        {
            this.tracerService = tracerService;
            this.userService = userService;
        }

        private CaseListItems _caseListItems;
        private readonly ITracerService tracerService;
        private readonly IUserService userService;

        public CaseListItems CaseListItems
        {
            get
            {
                if (_caseListItems == null)
                {
                    var assignedCases = Enumerable.Empty<CaseListItem>();
                    if (User != null)
                    {
                        var claims = HttpContext.User.Claims;
                        var currentUser = userService.GetUserByUserName(claims.Single(x => x.Type == "preferred_username").Value,int.Parse(claims.Single(x => x.Type=="usrtype").Value));
                        //var cases = tracerRepository?.GetTracer(currentUser.UserId)?.Cases
                        //    ?? Enumerable.Empty<Case>();
                        var cases = currentUser!=null ? tracerService.GetAssignedCases(currentUser.UserId) : Enumerable.Empty<Case>();
                        
                        assignedCases = cases.Select(MapToCaseListItem);
                    }

                    /*assignedCases = new[] {
                        new CaseListItem
                        {
                            CaseRaised = DateTime.Now.AddDays(-7),
                            CaseID = 1,
                            Name = "Test name"
                        },
                        new CaseListItem
                        {
                            CaseRaised = DateTime.Now.AddDays(-9),
                            CaseID = 2,
                            Name = "Test name 2"
                        }
                    };*/

                    _caseListItems = new CaseListItems
                    {
                        AssignedCases = assignedCases.ToList()
                    };
                }

                return _caseListItems;
            }
        }

        private static CaseListItem MapToCaseListItem(Case assignedCase) => new CaseListItem
        {
            CaseID = assignedCase.CaseID,
            CaseRaised = assignedCase.AddedDate,
            Name = assignedCase.GetFullName(),
        };
    }

    public class CaseListItems
    {
        public IEnumerable<CaseListItem> AssignedCases { get; set; }

        public bool HasAssignedCases => AssignedCases.Any();
    }

    public class CaseListItem
    {
        public string Name { get; set; }
        public int CaseID { get; set; }
        public DateTime CaseRaised { get; set; }
        public bool IsActive { get; set; }
    }
}
