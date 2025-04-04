﻿using GIVC;
using IDS_;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IDS_.IDS_WIR;

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
            int result = ids_wir.ChangeDivisionOutgoingWagons(866, nums, 31);
        }
        /// <summary>
        /// Исправить вес груза по отправленному вагону (по номеру ведомости и номеру вагона)
        /// </summary>
        public void ChangeVesgOutgoingWagons()
        {
            int result = ids_wir.ChangeVesgOutgoingWagons(1155, 64226459, 66750);
        }

        public void UpdateFiling()
        {
            List<UnloadingWagons> wagons = new List<UnloadingWagons>();
            wagons.Add(new UnloadingWagons { id_wim = 11808821, start = null, stop = null, id_wagon_operations = null, id_status_load = null });
            wagons.Add(new UnloadingWagons { id_wim = 11808828, start = new DateTime(2024, 10, 16, 1, 0, 0), stop = new DateTime(2024, 10, 16, 2, 0, 0), id_wagon_operations = 13, id_status_load = 3 });

            //ResultUpdateIDWagon result = ids_wir.AddFiling(0, 164, 78, new DateTime(2024, 10, 16, 0, 0, 0), wagons, "TЭM18-183", null, null);
        }
        public void CorrectArrivalDocument()
        {
            int num_doc = 491;
            int num_nakl = 40036675;
            List<int> nums = new List<int> { 52227980 };
            ArrivalCorrectDocument correct_document = new ArrivalCorrectDocument() { };
            //ArrivalCorrectDocument correct_document = new ArrivalCorrectDocument()
            //{
            //    CodeShipper = 3700,
            //    CodePayerSender = "8245745",
            //    DistanceWay = 997
            //};
            List<ArrivalCorrectVagonDocument> correct_vagons = new List<ArrivalCorrectVagonDocument>();
            //correct_vagons.Add(new ArrivalCorrectVagonDocument()
            //{
            //    Num = 63208300,
            //    Vesg = 69850,
            //    IdCargo = 68,
            //    PaySumma = 2938500
            //});
            //correct_vagons.Add(new ArrivalCorrectVagonDocument()
            //{
            //    Num = 63208300,
            //    Vesg = 69850,
            //    IdCargo = 68,
            //    PaySumma = 2938500
            //});
            //correct_vagons.Add(new ArrivalCorrectVagonDocument()
            //{
            //    Num = 63010276,
            //    Vesg = 69900,
            //    IdCargo = 68,
            //    PaySumma = 2938500
            //});
            //correct_vagons.Add(new ArrivalCorrectVagonDocument()
            //{
            //    Num = 63006472,
            //    Vesg = 69800,
            //    IdCargo = 68,
            //    PaySumma = 2938500
            //});
            //correct_vagons.Add(new ArrivalCorrectVagonDocument()
            //{
            //    Num = 63190615,
            //    Vesg = 69900,
            //    IdCargo = 68,
            //    PaySumma = 2938500
            //});
            //correct_vagons.Add(new ArrivalCorrectVagonDocument()
            //{
            //    Num = 63190615,
            //    Vesg = 69400,
            //    IdCargo = 68,
            //    PaySumma = 2938500
            //});
            ResultCorrect result = ids_wir.CorrectArrivalDocument(num_doc, num_nakl, nums, correct_document, correct_vagons);
        }

    }
}
