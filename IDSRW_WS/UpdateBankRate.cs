using EF_IDS.Concrete;
using EF_IDS.Concrete.Directory;
using EF_IDS.Entities;
using IDS_;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDS_.ClientBank;

namespace WS_IDS
{
    public class UpdateBankRate : IHostedService, IDisposable
    {

        protected static object locker_test = new object();
        protected bool run = false;
        private readonly ILogger<UpdateBankRate> _logger;
        private readonly IConfiguration _configuration;
        EventId _eventId = new EventId(0);
        private int interval = 3600;                                // Интервал выполнения таймера
        private List<int> list_r030;

        private String? connectionString;
        private DbContextOptions<EFDbContext> options;

        private Timer? _timer = null;
        private ClientBank cl_bank = null;

        Stopwatch stopWatch = new Stopwatch();

        public UpdateBankRate(ILogger<UpdateBankRate> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _eventId = int.Parse(_configuration["EventID:UpdateBankRate"]);

            interval = int.Parse(_configuration["Interval:UpdateBankRate"]);
            list_r030 = _configuration.GetSection("Control:list_r030").Value.Split(',').Select(s => int.Parse(s)).ToList();
            cl_bank = new ClientBank(logger, configuration);
            connectionString = configuration.GetConnectionString("IDS");
            var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
            this.options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning(_eventId, "UpdateBankRate -start, interval{0}", interval);
            //_timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(interval));
            _timer = new Timer(DoWork, null, 0, interval * 1000);
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            if (run)
            {
                _logger.LogWarning(_eventId, "UpdateBankRate - is run, skip work! - {0}", run);
                return;
            }
            lock (locker_test)
            {
                run = true;
            }
            //var count = Interlocked.Increment(ref executionCount);
            _logger.LogWarning(_eventId, "UpdateBankRate - run");

            EFDbContext context = new EFDbContext(this.options);
            EFDirectoryBankRate ef_dir_br = new EFDirectoryBankRate(context);
            List<DirectoryBankRate> list_dir_br = ef_dir_br.Context.Where(b => b.Date == DateTime.Now.Date).ToList();
            if (list_dir_br.Count() < list_r030.Count())
            {
                stopWatch.Start();
                // Удалим старое
                if (list_dir_br.Count() > 0)
                {
                    ef_dir_br.Delete(list_dir_br.Select(d => d.Id).ToList());
                }
                List<BankRate> list = this.cl_bank.GetBankRates();
                foreach (BankRate bank in list)
                {
                    if (list_r030.IndexOf((int)bank.r030) >= 0)
                    {
                        DirectoryBankRate dir_br = new DirectoryBankRate()
                        {
                            Id = 0,
                            Date = DateTime.Now.Date,
                            Code = (int)bank.r030,
                            Cc = bank.cc,
                            Name = bank.txt,
                            Rate = (float)bank.rate
                        };
                        ef_dir_br.Add(dir_br);
                    }
                }
                int result = context.SaveChanges();
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                _logger.LogWarning(_eventId, "UpdateBankRate - runing, result={0}, runtime = {1}", result, elapsedTime);
            }
            else
            {
                _logger.LogWarning(_eventId, "UpdateBankRate - skiping");
            }


            lock (locker_test)
            {
                run = false;
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning(_eventId, "UpdateBankRate - stop");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
