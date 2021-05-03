using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/* A class specifying an authorization requirement used to prevent users from accessing pages they aren't allowed to.
 * This requirement is passed to UserTypeHandler to be checked when a user attempts to access a protected page.
 */

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
