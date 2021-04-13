using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15
{
    public class UserTypeRequirement : IAuthorizationRequirement
    {
        public UserTypeRequirement(int _userType)
        {
            UserType = _userType;
        }

        public int UserType { get; set; }
    }
}
