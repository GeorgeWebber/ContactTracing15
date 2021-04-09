using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public class UserService : IUserService
    {
        public User GetUserByUserName(string username)
        {
            // TODO : Do this
            return new User
            {
                Type = UserType.Tracer,
                UserId = 1,
                UserName = "tracerUser"
            };
        }
    }
}
