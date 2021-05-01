using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactTracing15.Services
{
    public class PrivacyService : IPrivacyService
    { 
        //private readonly ICaseService _caseService;
        //private readonly IContactService _contactService;
        private System.Timers.Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private IServiceProvider _service;
        public PrivacyService(IServiceProvider services)//ICaseService caseService, IContactService contactService)
        {
            Console.WriteLine("PS startup-----------------------------");
            _service = services;
            //_caseService = caseService;
            //_contactService = contactService;
            _expression = CronExpression.Parse(@"* * * * *"); // run every minutes
            //_expression = CronExpression.Parse(@"*/5 * * * *"); // run every 5 minutes
            //_expression = CronExpression.Parse(@"50 12 * * *"); // run every day at 12:50, you guys get the gist of it
            _timeZoneInfo = TimeZoneInfo.Local;
        }

        public virtual async Task StartAsync(CancellationToken cancellationToken)
        {
            await ScheduleJob(cancellationToken);
        }


        protected virtual async Task ScheduleJob(CancellationToken cancellationToken)
        {
            var next = _expression.GetNextOccurrence(DateTimeOffset.Now, _timeZoneInfo);
            if (next.HasValue)
            {
                var delay = next.Value - DateTimeOffset.Now;
                if (delay.TotalMilliseconds <= 0)   // prevent non-positive values from being passed into Timer
                {
                    await ScheduleJob(cancellationToken);
                }
                _timer = new System.Timers.Timer(delay.TotalMilliseconds);
                _timer.Elapsed += async (sender, args) =>
                {
                    _timer.Dispose();  // reset and dispose timer
                    _timer = null;

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await DoWork(cancellationToken);
                    }

                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await ScheduleJob(cancellationToken);    // reschedule next
                    }
                };
                _timer.Start();
            }
            await Task.CompletedTask;
        }

        public virtual async Task DoWork(CancellationToken cancellationToken)
        {
            Console.WriteLine("Private Service routine works");
            CleanOldRecords();
        }

        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Stop();
            await Task.CompletedTask;
        }

        public virtual void Dispose()
        {
            _timer?.Dispose();
        }

        public void CleanOldRecords()
        {
            using (var scope = _service.CreateScope())
            {
                var _caseService = scope.ServiceProvider
                    .GetRequiredService<ICaseService>();
                var _contactService = scope.ServiceProvider
                    .GetRequiredService<IContactService>();
                var oldThreshold = DateTime.Now.AddDays(-14);
                foreach (Case _case in _caseService.GetOldCases(oldThreshold))
                {
                    _caseService.RemovePersonalData(_case.CaseID);
                }
                foreach (Contact _contact in _contactService.GetOldContacts(oldThreshold))
                {
                    _contactService.RemovePersonalData(_contact.ContactID);
                }
            }
        }

    }
}
