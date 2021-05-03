using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ContactTracing15.Models;
using ContactTracing15.Services;
using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactTracing15.Services
{
    public class ExcelService : IExcelService
    {
        //private readonly ICaseService _caseService;
        //private readonly IContactService _contactService;
        private System.Timers.Timer _timer;
        private readonly CronExpression _expression;
        private readonly TimeZoneInfo _timeZoneInfo;
        private IServiceProvider _service;
        public ExcelService(IServiceProvider services)//ICaseService caseService, IContactService contactService)
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
            Console.WriteLine("Excel Service routine works");
            GenerateSpreadsheet();
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

        public void GenerateSpreadsheet()
        {

            //TODO: change code here to generate spreadsheet
            using (var scope = _service.CreateScope())
            {
                var _testingCentreService = scope.ServiceProvider
                    .GetRequiredService<ITestingCentreService>();
                var _tracingCentreService = scope.ServiceProvider
                    .GetRequiredService<ITracingCentreService>();

                var _testerService = scope.ServiceProvider
                    .GetRequiredService<ITesterService>();
                var _tracerService = scope.ServiceProvider
                    .GetRequiredService<ITracerService>();
                
                var _caseService = scope.ServiceProvider
                    .GetRequiredService<ICaseService>();
                var _contactService = scope.ServiceProvider
                    .GetRequiredService<IContactService>();

                using (XLWorkbook wb = new XLWorkbook())
                {

                    DataTable dt_tec = _testingCentreService.ExportAsExcel();
                    DataTable dt_trc = _tracingCentreService.ExportAsExcel();
                    DataTable dt_te = _testerService.ExportAsExcel();
                    DataTable dt_tr = _tracerService.ExportAsExcel();
                    DataTable dt_ca = _caseService.ExportAsExcel();
                    DataTable dt_co = _contactService.ExportAsExcel();


                    wb.Worksheets.Add(dt_tec, "Testing Centres");
                    wb.Worksheets.Add(dt_trc, "Tracing Centres");
                    wb.Worksheets.Add(dt_te, "Testers");
                    wb.Worksheets.Add(dt_tr, "Tracers");
                    wb.Worksheets.Add(dt_ca, "Cases");
                    wb.Worksheets.Add(dt_co, "Contacts");
                    
                    wb.SaveAs("ContactTracingReport.xlsx");
                }
            }
        }

    }
}

