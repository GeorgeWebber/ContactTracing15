using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15
{
    public class UserTypeRequirement : IAuthorizationRequirement
    {
        public UserTypeRequirement(string _userType)
        {
            UserType = _userType;
        }

        public string UserType { get; set; }
    }
}
