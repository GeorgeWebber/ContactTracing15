using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTracing15.Services
{
    public interface IPrivacyService: IHostedService, IDisposable
    {
        public void CleanOldRecords();
    }
}
