using ContactTracing15.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using ContactTracing15.Services;
using System.Text;

namespace ContactTracing15.Services
{
    public class UserService : IUserService
    {
        readonly ITracerRepository _SQLTracerRepository;
        readonly ITesterRepository _SQLTesterRepository;
        public UserService(ITracerRepository SQLTracerRepository, ITesterRepository SQLTesterRepository)
        {
            _SQLTracerRepository = SQLTracerRepository;
            _SQLTesterRepository = SQLTesterRepository;
        }

        public User GetUserByUserName(string username, int usrType)
        {
            if (usrType == 0)
            {
                try
                {
                    var tracer = _SQLTracerRepository.GetAllTracers().Single(x => x.Username == username);
                    return new User
                    {
                        Type = UserType.Tracer,
                        UserId = tracer.TracerID,
                        UserName = username
                    };
                }
                catch(InvalidOperationException e)
                {
                    //return null;
                    return new User
                    {
                        Type = UserType.Tracer,
                        UserId = 561,
                        UserName = "failed"
                    };
                }
                

            }
            return new User
            {
                Type = UserType.Tracer,
                UserId = 1,
                UserName = "tracerUser"
            };
        }
    }
}
