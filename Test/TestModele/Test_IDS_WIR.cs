using GIVC;
using IDS_;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.TestModele
{
    public class Test_IDS_WIR
    {
        private IDS_WIR ids_wir = null;
        private readonly ILogger<Object> _logger;
        private readonly IConfiguration _configuration;
        public Test_IDS_WIR(ILogger<Object> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
            ids_wir = new IDS_WIR(_logger, _configuration);
        }

        /// <summary>
        /// Тест расчет платы по вагону
        /// </summary>
        public void CalcUsageFeeOfCar()
        {
            ids_wir.CalcUsageFeeOfWIR(862636);
        }
        /// <summary>
        /// Тест расчет платы по вагонам на пути
        /// </summary>
        public void CalcUsageFeeCarsOfWay()
        {
            ids_wir.CalcUsageFeeCarsOfWay(1042);
        }
        /// <summary>
        /// Тест расчет платы по отправленому составу
        /// </summary>
        public void CalcUsageFeeOfOutgoingSostav()
        {
            ids_wir.CalcUsageFeeOfOutgoingSostav(293643);
        }
        /// <summary>
        /// Расчет платы за пользование по сданным составам за выбранный период
        /// </summary>
        public void IDS_WIR_CalcUsageFeeOfOutgoingSostavOfPeriod()
        {
            DateTime start = new DateTime(2024, 3, 6, 0, 0, 0);
            DateTime stop = new DateTime(2024, 6, 19, 23, 59, 59);
            List<ResultUpdateIDWagon> res = ids_wir.CalcUsageFeeOfOutgoingSostav(start, stop);
        }
    }
}
