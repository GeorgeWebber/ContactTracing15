using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactTracing15.Models;

namespace ContactTracing15.Helper.Extensions
{
    public static class ContactExtensions
    {
        public static string GetFullName(this Contact c)
        {
            return $"{c.Forename} {c.Surname}";
        }
    }
}
