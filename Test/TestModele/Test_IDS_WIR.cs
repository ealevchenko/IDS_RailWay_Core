using GIVC;
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
            int num_doc = 489;
            int num_nakl = 47598222;
            bool union = false;
            bool create_new = true;
            List<int> nums = new List<int> { 53187753, 58940578, 60972551, 61013249, 62110614 };

            ArrivalCorrectDocument? correct_document = null;
            //ArrivalCorrectDocument correct_document = new ArrivalCorrectDocument()
            //{
            //    CodeShipper = 3700,
            //    CodePayerSender = "8245745",
            //    DistanceWay = 997
            //};
            List<ArrivalCorrectVagonDocument>? correct_vagons = null;
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
            ResultCorrect result = ids_wir.CorrectArrivalDocument(num_doc, num_nakl, union, create_new, nums, correct_document, correct_vagons);
            Console.WriteLine($" result : {result.result} \n message :{result.message}");
        }
        public void DeleteWagonOfAMKR()
        {
            int num_doc = 520;
            List<int> nums = new List<int> { 60726544, 60724341, 60455508, 54779467, 63470777, 63813711 };
            ResultCorrect result = ids_wir.DeleteWagonOfAMKR(num_doc, nums);
            Console.WriteLine($" result : {result.result} \n message :{result.message}");
        }
        public void CorrectArrivalNotEPD_Document()
        {
            int num_doc = 569;
            int? num_nakl = null;
            List<int> nums = new List<int> { 64054109, 57601114, 57599714, 62389135, 52291978, 52373057, 56299241, 52365681, 56355571, 60971470, 61640785, 52179603, 57656886, 61593000, 63856769, 56963150, 53575080, 64954654, 64954142, 56193782, 52366002, 57640252, 55428106, 65001216, 63403489, 64615354, 55082077, 63016166, 63868137, 60985280, 61100574, 63536072, 64051568, 64052079, 64954126, 63374912, 64939051, 52876323, 63938229, 63659551, 53133807, 56067044, 64050669, 56365455, 61583167, 52737756, 56655335, 56682321, 56223258, 55168363, 64908239, 64050156 };
            // новый документ
            ArrivalCorrectDocument correct_document = new ArrivalCorrectDocument()
            {
                CodeStnFrom = 467108,
                CodeStnTo = 467004,
                CodeBorderCheckpoint = null,
                CrossTime = null,
                CodeShipper = 9200,
                CodeConsignee = 9200,
                Klient = true,
                CodePayerSender = null,
                CodePayerArrival = null,
                DistanceWay = 10,
            };
            // корекция по всем вагонам
            ArrivalCorrectVagonDocument correct_all_vagons = new ArrivalCorrectVagonDocument()
            {
                Gruzp = null,
                UTara = null,
                VesTaryArc = null,
                IdCargo = 1,
                Vesg = null,
                PaySumma = null,
                IdStationOnAmkr = null,
                IdDivisionOnAmkr = 99,
            };
            // корекция по вагоно
            List<ArrivalCorrectVagonDocument>? correct_vagons = new List<ArrivalCorrectVagonDocument>();
            correct_vagons.Add(new ArrivalCorrectVagonDocument()
            {
                Num = null,
                Gruzp = null,
                UTara = null,
                VesTaryArc = null,
                IdCargo = 1,
                Vesg = null,
                PaySumma = null,
                IdStationOnAmkr = null,
                IdDivisionOnAmkr = 99,
            });

            ResultCorrect result = ids_wir.CorrectArrivalNotEPD_Document(num_doc, num_nakl, nums, correct_document, correct_all_vagons, correct_vagons);
            Console.WriteLine($" result : {result.result} \n message :{result.message}");
        }
        public void CorrectOutgoingDocument()
        {

            int id_sostav = 366014;
            List<int> nums = new List<int> { 65000929 };
            OutgoingCorrectDocument doc = new OutgoingCorrectDocument()
            {
                NumDoc = 46700497,
                CodeShipper = 7932,
                CodeConsignee = null,
                CodeStnTo = 13538

            };
            List<OutgoingCorrectVagonDocument> vagons = new List<OutgoingCorrectVagonDocument>();
            
            vagons.Add(new OutgoingCorrectVagonDocument() { Num = 65000929, IdCargo = 114, Vesg = 68000 });



            ResultCorrect result = ids_wir.CorrectOutgoingDocument(id_sostav, nums, doc, vagons);
            Console.WriteLine($" result : {result.result} \n message :{result.message}");















        }
        /// <summary>
        /// Тест обновить инструктивные письма
        /// </summary>
        public void IDS_WIR_UpdateInstructionalLetter()
        {
            DateTime date = new DateTime(2025, 8, 1, 0, 0, 0);
            OperationResultID res = ids_wir.UpdateInstructionalLetter(date, null);
            Console.WriteLine($" result : {res.result} \n CountlistResult :{res.listResult.Count()}");
        }
        /// <summary>
        /// Тест обновить открытые инструктивные письма
        /// </summary>
        public void IDS_WIR_UpdateOpenInstructionalLetter()
        {
            OperationResultID res = ids_wir.UpdateOpenInstructionalLetter(null);
            Console.WriteLine($" result : {res.result} \n CountlistResult :{res.listResult.Count()}");
        }
        /// <summary>
        /// Административная функция. Удалить подачу (или вагоны в подаче)
        /// </summary>
        public void DeleteFilingOfID()
        {
            int id_filing = 186342;
            List<int> nums = new List<int> { 52984861 };
            ResultCorrect result = ids_wir.DeleteFilingOfID(id_filing, nums);
            Console.WriteLine($" result : {result.result} \n message :{result.message}");
        }
        /// <summary>
        /// Административная функция. Очистить вагон в подаче
        /// </summary>
        public void ClearWagonLoadingFilingOfID()
        {
            int id_filing = 194273;
            List<int> nums = new List<int> { 28333 };
            ResultCorrect result = ids_wir.ClearWagonLoadingFilingOfID(id_filing, nums);
            Console.WriteLine($" result : {result.result} \n message :{result.message}");
        }
    }
}
