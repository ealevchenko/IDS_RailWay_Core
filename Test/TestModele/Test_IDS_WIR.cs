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
            ids_wir.CalcUsageFeeOfOutgoingSostav(282860);
        }
        /// <summary>
        /// Расчет платы за пользование по сданным составам за выбранный период
        /// </summary>
        public void IDS_WIR_CalcUsageFeeOfOutgoingSostavOfPeriod()
        {
            DateTime start = new DateTime(2024, 3, 6, 0, 0, 0);
            DateTime stop = new DateTime(2024, 3, 31, 23, 59, 59);
            List<ResultUpdateIDWagon> res = ids_wir.CalcUsageFeeOfOutgoingSostav(start, stop);
        }
        /// <summary>
        /// Исправить «Цех погрузки» отправленных вагонов (по номеру ведомости и номерам вагонов)
        /// </summary>
        public void ChangeDivisionOutgoingWagons()
        {
            List<int> nums = new List<int> { 63531321, 63530620, 63532063, 63664775, 63530547, 63531305, 63664361, 63664684, 63532337, 63531685, 63664551, 63532543, 63531842, 63532022, 63662951, 63664346, 63530919, 63530380, 63532410, 63530570, 63532642, 63531925, 63530315, 63531602, 63663108, 63662464, 63531453, 63664627, 63662308, 63376453, 63530943, 63530406, 63664882, 63531404, 63665038, 63531982, 63530232, 63663157, 63531784, 63664635, 63530174, 63664338, 63532352, 63376487, 63663728, 63664510, 63532634, 63532105, 63662738, 63532386, 63532212, 63662720, 63531677, 63376305, 63531487 };
            int result = ids_wir.ChangeDivisionOutgoingWagons(866,nums, 31);
        }

    }
}
