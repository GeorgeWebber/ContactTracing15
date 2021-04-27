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
        readonly ITracerService _TracerService;
        readonly ITesterService _TesterService;
        public UserService(ITracerService tracerService, ITesterService testerService)
        {
            _TracerService = tracerService;
            _TesterService = testerService;
        }

        public User GetUserByUserName(string username, int usrType)
        {
            if (usrType == 0)
            {
                try
                {
                    var tracer = _TracerService.GetTracer(username);
                    return new User
                    {
                        Type = UserType.Tracer,
                        UserId = tracer.TracerID,
                        UserName = username
                    };
                }
                catch(InvalidOperationException e)
                {
                    return null;
                }
                

            }
            else if (usrType == 1)
            {
                try
                {
                    var tester = _TesterService.GetTester(username);
                    return new User
                    {
                        Type = UserType.Tester,
                        UserId = tester.TesterID,
                        UserName = username
                    };
                }
                catch (InvalidOperationException e)
                {
                    return null;
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
