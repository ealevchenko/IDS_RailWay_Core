using EF_IDS.Concrete;
using IDS_;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WS_IDS
{
    public class UpdateRent : IHostedService, IDisposable
    {

        protected static object locker_test = new object();
        protected bool run = false;
        //private int executionCount = 0;
        private readonly ILogger<UpdateRent> _logger;
        private readonly IConfiguration _configuration;
        EventId _eventId = new EventId(0);
        private int interval = 3600;                               // Интервал выполнения таймера
        private int control_period_arr = 10;                       // Период контроля принятых составов (дней)
        private int control_period_out = 10;                       // Период контроля отправленных составов (дней)
        Stopwatch stopWatch = new Stopwatch();

        private Timer? _timer = null;
        private IDS_WIR ids_wir = null;


        private string GetElapsedTime(TimeSpan ts)
        {
            return String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        public UpdateRent(ILogger<UpdateRent> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _eventId = int.Parse(_configuration["EventID:UpdateRent"]);
            interval = int.Parse(configuration["Interval:UpdateRent"]);
            control_period_arr = int.Parse(configuration["Control:UpdateRentArrival"]);
            control_period_out = int.Parse(configuration["Control:UpdateRentOutgoing"]);
            this.ids_wir = new IDS_WIR(logger, configuration, _eventId);

        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning(_eventId, "UpdateRent - start, interval{0}", interval);
            //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
            _timer = new Timer(DoWork, null, 0, interval * 1000);
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            if (run)
            {
                _logger.LogWarning(_eventId, "UpdateRent - is run, skip work!", run);
                return;
            }
            lock (locker_test)
            {
                run = true;
            }
            //var count = Interlocked.Increment(ref executionCount);
            //_logger.LogInformation("Таймер. Count: {Count}", count);
            _logger.LogWarning(_eventId, "UpdateRent - run");
            stopWatch.Start();
            ResultUpdateWagon res_cl = ids_wir.ClearDoubling_Directory_WagonsRent(null);
            stopWatch.Stop();
            _logger.LogWarning(_eventId, "UpdateRent:ClearDoubling - runing, result = {0}, runtime = {1}", res_cl.result, GetElapsedTime(stopWatch.Elapsed));
            stopWatch.Start();
            OperationResultID res_arr = this.ids_wir.UpdateOperationArrivalSostav(DateTime.Now.AddDays(-control_period_arr), null);
            stopWatch.Stop();
            _logger.LogWarning(_eventId, "UpdateRent:OperationArrival - runing, result = {0}, runtime = {1}", res_arr.result, GetElapsedTime(stopWatch.Elapsed));
            stopWatch.Start();
            OperationResultID res_out = this.ids_wir.UpdateOperationOutgoingSostav(DateTime.Now.AddDays(-control_period_out), null);
            stopWatch.Stop();
            _logger.LogWarning(_eventId, "UpdateRent:OperationOutgoing - runing, result = {0}, runtime = {1}", res_out.result, GetElapsedTime(stopWatch.Elapsed));
            lock (locker_test)
            {
                run = false;
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning(_eventId, "UpdateRent - stop");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
