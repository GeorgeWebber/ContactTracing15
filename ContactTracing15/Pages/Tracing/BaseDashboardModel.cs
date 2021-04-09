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
            ITracerRepository tracerRepository,
            IUserService userService)
        {
            this.tracerRepository = tracerRepository;
            this.userService = userService;
        }

        private CaseListItems _caseListItems;
        private readonly ITracerRepository tracerRepository;
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
                        var currentUser = userService.GetUserByUserName(User.Identity.Name);
                        var cases = tracerRepository?.GetTracer(currentUser.UserId)?.Cases
                            ?? Enumerable.Empty<Case>();
                        assignedCases = cases.Select(MapToCaseListItem);
                    }

                    assignedCases = new[] {
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
                    };

                    _caseListItems = new CaseListItems
                    {
                        AssignedCases = assignedCases
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
