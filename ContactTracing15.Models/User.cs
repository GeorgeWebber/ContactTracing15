using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Models
{
    public enum UserType
    {
        Tracer = 0,
        Tester = 1,
        GovOfficial = 2
    }

    public class User
    {
        public UserType Type { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
