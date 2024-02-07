using EF_IDS.Concrete;
using EF_IDS.Entities;
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
    public class UpdateGIVC : IHostedService, IDisposable
    {

        protected static object locker_test = new object();
        protected bool run = false;
        //private int executionCount = 0;
        private readonly ILogger<UpdateGIVC> _logger;
        private readonly IConfiguration _configuration;
        private EventId _eventId = new EventId(0);
        private int interval = 60;                            // Интервал выполнения таймера
        private Stopwatch stopWatch = new Stopwatch();
        private int day = 0;
        private bool run_exec = false;
        private int exec_attempts = 10;

        private Timer? _timer = null;
        private IDS_GIVC ids_givc = null;

        private string GetElapsedTime(TimeSpan ts)
        {
            return String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        public UpdateGIVC(ILogger<UpdateGIVC> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _eventId = int.Parse(_configuration["EventID:UpdateGIVC"]);
            this.ids_givc = new IDS_GIVC(logger, configuration, _eventId);
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning(_eventId, "UpdateGIVC - start, interval{0}", interval);
            //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
            _timer = new Timer(DoWork, null, 0, interval * 1000);
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            if (run)
            {
                _logger.LogWarning(_eventId, "UpdateGIVC - is run, skip work!", run);
                return;
            }
            lock (locker_test)
            {
                run = true;
            }
            //var count = Interlocked.Increment(ref executionCount);
            //_logger.LogInformation("Таймер. Count: {Count}", count);
            DateTime dt = DateTime.Now;
            DateTime cur_date = dt.Date;
            int cur_day = dt.Date.Day;
            int cur_hour = dt.Hour;
            // В начале суток сбросим все тригеры выполнения
            if (cur_day != day && cur_hour == 0)
            {
                run_exec = false;
                exec_attempts = 10;
            }
            // Выполним в 10 часов 
            if (cur_day != day && cur_hour == 10 && run_exec == false && exec_attempts > 0)
            {
                DateTime? dt_last = null;// ids_givc.GetLastDateTimeRequest("req1892");
                GivcRequest? last_givc_req = ids_givc.GetLastGivcRequest("req1892");
                if (last_givc_req != null &&
                    ((DateTime)last_givc_req.DtRequests).Date == cur_date &&
                    last_givc_req.CountLine <=0)
                {
                    exec_attempts--; // Уменьшаем количество попыток
                    dt_last = null;
                }
                else
                {
                    dt_last = last_givc_req != null ? last_givc_req.DtRequests : null;
                }

                if (dt_last == null || (dt_last != null && (((DateTime)dt_last).Date != cur_date)) || (dt_last != null && (((DateTime)dt_last).Date == cur_date && ((DateTime)dt_last).Hour != cur_hour)))
                {
                    day = cur_day;
                    run_exec = true;
                    _logger.LogWarning(_eventId, "UpdateGIVC - run");
                    stopWatch.Start();
                    int res_cl = ids_givc.RequestToGIVC("req1892", null);
                    stopWatch.Stop();
                    _logger.LogWarning(_eventId, "UpdateGIVC:RequestToGIVC - runing, result = {0}, runtime = {1}", res_cl, GetElapsedTime(stopWatch.Elapsed));
                }
                else
                {
                    _logger.LogWarning(_eventId, "UpdateGIVC:RequestToGIVC - skip, Record exists, request date = {0}", dt_last);
                }

            }
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
