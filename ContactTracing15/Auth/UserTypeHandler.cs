using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ContactTracing15.Services;
using Microsoft.AspNetCore.Http;

namespace ContactTracing15
{
    public class UserTypeHandler : AuthorizationHandler<UserTypeRequirement>
    {
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