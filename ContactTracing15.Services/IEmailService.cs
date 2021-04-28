using System;
using System.Collections.Generic;
using System.Text;
using ContactTracing15.Models;
using ContactTracing15.Services;

namespace ContactTracing15.Services
{
    public interface IEmailService
    {
        void ContactByEmail(Contact contact);
    }
}
