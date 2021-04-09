using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public interface IUserService
    {
        User GetUserByUserName(string username);
    }
}
