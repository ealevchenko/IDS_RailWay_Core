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
            ids_wir.CalcUsageFeeOfCar(861475);
        }
        /// <summary>
        /// Тест расчет платы по вагонам на пути
        /// </summary>
        public void CalcUsageFeeCarsOfWay()
        {
            ids_wir.CalcUsageFeeCarsOfWay(1042);
        }
    }
}
