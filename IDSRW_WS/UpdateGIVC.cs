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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WS_IDS
{
    public class config_reguest
    {
        public string type_requests { get; set; }
        public List<int> period { get; set; }
        public int kod_stan_beg { get; set; }
        public int kod_stan_end { get; set; }
        public int kod_grp_beg { get; set; }
        public int kod_grp_end { get; set; }
    }
    public class run_reguest
    {
        public bool error { get; set; }
        public int run_day { get; set; }
        public int run_hour { get; set; }
        public string type_requests { get; set; }
        public List<int> period { get; set; }
        public int kod_stan_beg { get; set; }
        public int kod_stan_end { get; set; }
        public int kod_grp_beg { get; set; }
        public int kod_grp_end { get; set; }
    }

    //public List<config_reguest> ListReguests = 

    public class UpdateGIVC : IHostedService, IDisposable
    {

        protected static object locker_run = new object();
        protected bool run = false;
        //private int executionCount = 0;
        private readonly ILogger<UpdateGIVC> _logger;
        private readonly IConfiguration _configuration;
        private EventId _eventId = new EventId(0);
        private int interval = 60;                            // Интервал выполнения таймера
        private Stopwatch stopWatch = new Stopwatch();
        //private int day = 0;
        //private bool run_exec = false;
        //private int exec_attempts = 10;

        private List<config_reguest> list_reguests = new List<config_reguest>();
        private List<run_reguest> list_run_reguest = new List<run_reguest>();

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
            // считаем настройки и сформируем запросы
            list_reguests = _configuration.GetSection("GIVC:ListReguests").Get<List<config_reguest>>();
            if (list_reguests != null && list_reguests.Count > 0)
            {
                foreach (config_reguest conf_reg in list_reguests)
                {
                    run_reguest reg_run = new run_reguest()
                    {
                        error = false,
                        run_day = 0,
                        run_hour = 0,
                        period = conf_reg.period,
                        type_requests = conf_reg.type_requests,
                        kod_stan_beg = conf_reg.kod_stan_beg,
                        kod_stan_end = conf_reg.kod_stan_end,
                        kod_grp_beg = conf_reg.kod_grp_beg,
                        kod_grp_end = conf_reg.kod_grp_end
                    };
                    list_run_reguest.Add(reg_run);
                }
            }
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
            lock (locker_run)
            {
                run = true;
            }
            DateTime dt_curr = DateTime.Now;
            DateTime cur_date = dt_curr.Date;
            int cur_day = dt_curr.Date.Day;
            int cur_hour = dt_curr.Hour;


            foreach (run_reguest conf_reg in list_run_reguest)
            {
                // В начале суток сбросим установки
                if (conf_reg.run_day != cur_day && cur_hour == 0)
                {
                    conf_reg.run_day = 0;
                    conf_reg.run_hour = 0;
                    conf_reg.error = false;
                }

                int res_h = conf_reg.period.IndexOf(cur_hour);

                if (conf_reg.run_day != cur_day && res_h >= 0 && conf_reg.run_hour!=cur_hour)
                {
                    DateTime? dt_last = null;
                    // Выполнить запрос
                    GivcRequest? last_givc_req = ids_givc.GetLastGivcRequest(conf_reg.type_requests);
                    dt_last = last_givc_req != null ? last_givc_req.DtRequests : null;
                    if (dt_last == null  || (dt_last != null && (((DateTime)dt_last).Date != cur_date)) || (dt_last != null && (((DateTime)dt_last).Date == cur_date && ((DateTime)dt_last).Hour != cur_hour)))
                    {
                        conf_reg.run_day = cur_day;     // Отметка о выполнении день
                        conf_reg.run_hour = cur_hour;   // Отметка о выполнении час
                        _logger.LogWarning(_eventId, "UpdateGIVC - run");
                        stopWatch.Start();
                        int res_cl = ids_givc.RequestToGIVC(conf_reg.type_requests, new parameters_reguest() { kod_stan_beg = conf_reg.kod_stan_beg, kod_stan_end = conf_reg.kod_stan_end, kod_grp_beg = conf_reg.kod_grp_beg, kod_grp_end = conf_reg.kod_grp_end}, null);
                        stopWatch.Stop();
                        _logger.LogWarning(_eventId, "UpdateGIVC:RequestToGIVC - runing, result = {0}, runtime = {1}", res_cl, GetElapsedTime(stopWatch.Elapsed));
                    }
                    else
                    {
                        _logger.LogWarning(_eventId, "UpdateGIVC:RequestToGIVC - skip, Record exists, request date = {0}", dt_last);
                    }
                }
            }



            //var count = Interlocked.Increment(ref executionCount);
            //_logger.LogInformation("Таймер. Count: {Count}", count);

            // В начале суток сбросим все тригеры выполнения
            //if (cur_day != day && cur_hour == 0)
            //{
            //    run_exec = false;
            //    exec_attempts = 10;
            //}
            // Выполним в 10 часов 
            //if (cur_day != day && cur_hour == 10 && run_exec == false && exec_attempts > 0)
            //{
            //    DateTime? dt_last = null;// ids_givc.GetLastDateTimeRequest("req1892");
            //    GivcRequest? last_givc_req = ids_givc.GetLastGivcRequest("req1892");
            //    if (last_givc_req != null &&
            //        ((DateTime)last_givc_req.DtRequests).Date == cur_date &&
            //        last_givc_req.CountLine <= 0)
            //    {
            //        exec_attempts--; // Уменьшаем количество попыток
            //        dt_last = null;
            //    }
            //    else
            //    {
            //        dt_last = last_givc_req != null ? last_givc_req.DtRequests : null;
            //    }

            //    if (dt_last == null || (dt_last != null && (((DateTime)dt_last).Date != cur_date)) || (dt_last != null && (((DateTime)dt_last).Date == cur_date && ((DateTime)dt_last).Hour != cur_hour)))
            //    {
            //        day = cur_day;
            //        run_exec = true;
            //        _logger.LogWarning(_eventId, "UpdateGIVC - run");
            //        stopWatch.Start();
            //        int res_cl = ids_givc.RequestToGIVC("req1892", null);
            //        stopWatch.Stop();
            //        _logger.LogWarning(_eventId, "UpdateGIVC:RequestToGIVC - runing, result = {0}, runtime = {1}", res_cl, GetElapsedTime(stopWatch.Elapsed));
            //    }
            //    else
            //    {
            //        _logger.LogWarning(_eventId, "UpdateGIVC:RequestToGIVC - skip, Record exists, request date = {0}", dt_last);
            //    }

            //}
            lock (locker_run)
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
