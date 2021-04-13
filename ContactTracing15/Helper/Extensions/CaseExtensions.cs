using ContactTracing15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactTracing15.Helper.Extensions
{
    public static class CaseExtensions
    {
        public static string GetFullName(this Case c)
        {
            return $"{c.Forename} {c.Surname}";
        }
    }
}
