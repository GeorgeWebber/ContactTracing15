using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ContactTracing15
{
    public class UserTypeHandler : AuthorizationHandler<UserTypeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserTypeRequirement requirement)
        {
 
            var userType = 0;

            if (userType == requirement.UserType)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}