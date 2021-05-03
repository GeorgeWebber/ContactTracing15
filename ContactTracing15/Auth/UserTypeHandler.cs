using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Http;


/* A class to implement the authorization policy tests required to keep non-authorized site traffic away from protected pages. * 
 */

namespace ContactTracing15
{
    public class UserTypeHandler : AuthorizationHandler<UserTypeRequirement>
    {

        // Checks if the user's claimed user type matches the user type permitted to access the page in question
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserTypeRequirement requirement)
        {

            if (!context.User.HasClaim(c => c.Type == "usrtype"))
            {
                return Task.CompletedTask;
            }

            string userType = (context.User.FindFirst(c => c.Type == "usrtype").Value);

            if (userType == requirement.UserType)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}