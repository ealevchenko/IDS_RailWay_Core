// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EF_IDS.Concrete;
using EF_IDS.Entities;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text;
using System.Text.Json;
using IDS_;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using Helper;
using GIVC;
using System.Reflection;
//using NLog.Fluent;
using TestCore.TestModele;
using Test.TestModele;

namespace HelloApp
{
    public class Runner
    {
        private readonly ILogger<Runner> _logger;

        public Runner(ILogger<Runner> logger)
        {
            _logger = logger;
        }

        public void DoAction(string name)
        {
            _logger.LogDebug(20, "Doing hard work! {Action}", name);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger<Program> logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();
            try
            {
                //IConfiguration Configuration = new ConfigurationBuilder()
                //  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //  .AddEnvironmentVariables()
                //  .AddCommandLine(args)
                //  .Build();

                //var loggerFactory = LoggerFactory.Create(builder =>
                //{
                //    builder.AddConsole();
                //});
                //ILogger<Program> logger = loggerFactory.CreateLogger<Program>();

                //logger.LogInformation("Start {Description}.", "fun");

                var config = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                //WebClientGIVC client_givc = new WebClientGIVC(logger, config);
                //IDS_GIVC ids_givc = new IDS_GIVC(logger, config);
                //int res_cl = ids_givc.RequestToGIVC(new parameters_reguest() { type_requests = "req8858", nom_vag="63208540",  date_beg = "13.02.2024", date_end = "14.02.2024" }, null);
                //int res_cl = ids_givc.RequestToGIVC(new parameters_reguest() { type_requests = "req0002", esr_form = 4000, nom_sost = 677, esr_nazn = 4670 }, null);
                //int res_cl = ids_givc.RequestToGIVC(new parameters_reguest() { type_requests = "req0002", esr_form = 4000, nom_sost = 664, esr_nazn = 4670 }, null);
                //int res_cl = ids_givc.RequestToGIVC(new parameters_reguest() { type_requests = "reqDisvag", kod_stan_form = 481904, kod_gro = 4742, kod_stan_nazn = 467004, kod_grp = 7932, kod_gruz = 161043 }, null);

                #region TestGIVC УЗ ГИВЦ
                //TestGIVC tGIVC = new TestGIVC(logger, config);
                // ====== ЗАПРОСЫ !!! БАБЛО !!! =========
                //tGIVC.Req0002();
                //tGIVC.Req1892();
                //tGIVC.Req1091();
                //tGIVC.Req4373();
                //tGIVC.Req7002();
                //tGIVC.reqDisvag();
                //tGIVC.reqNDI();
                //tGIVC.req2610();
                //tGIVC.req8858();
                // ====== Справки =========
                //tGIVC.reference1892();
                #endregion

                #region Test_IDS_WIR Тесты wir
                Test_IDS_WIR twir = new Test_IDS_WIR(logger, config);
                //twir.CalcUsageFeeOfCar(); // Тест расчет платы по вагону
                //twir.CalcUsageFeeCarsOfWay(); // Тест расчет платы по вагонам на пути
                //twir.CalcUsageFeeOfOutgoingSostav(); // Тест расчет платы по отправленому составу
                //twir.IDS_WIR_CalcUsageFeeOfOutgoingSostavOfPeriod(); // Расчет платы за пользование по сданным составам за выбранный период
                //twir.ChangeDivisionOutgoingWagons(); // 
                //twir.ChangeVesgOutgoingWagons(); // 
                //twir.UpdateFiling();
                //twir.CorrectArrivalDocument();
                //twir.DeleteWagonOfAMKR();
                //twir.CorrectArrivalNotEPD_Document();
                //twir.CorrectOutgoingDocument();
                //twir.DeleteFilingOfID();
                //twir.ClearWagonLoadingFilingOfID();
                twir.DeleteWagonClearFilingOfID();
                //twir.IDS_WIR_UpdateInstructionalLetter();
                //twir.IDS_WIR_UpdateOpenInstructionalLetter();
                #endregion
                //+++++++++++++++++++++++++++++++ req1892 ++++++++++++++++++++++++++++++
                //WebClientGIVC client_givc = new WebClientGIVC(logger, config);
                //req1892 res = client_givc.GetReq1892(467004, 467201, 7932, 7932);
                //req1892 res = client_givc.GetReq1892(467004, 467201);

                //string path = "req1892.txt";
                //using (StreamWriter writer = new StreamWriter(path, false))
                //{
                //    //string str_field = "";
                //    string str = "";
                //    object myClass = new disl_vag_detali();
                //    List<string> fieds = new List<string>();

                //    //foreach (var prop in typeof(myClass).GetProperties())
                //    //{
                //    //    fieds.Add(field.Name);
                //    //    str_field += field.Name + ";";
                //    //}
                //    //foreach (var field in myClass.GetType().GetProperties())
                //    //{
                //    //    fieds.Add(field.Name);
                //    //    str_field += field.Name + ";";
                //    //}


                //    string str_field =
                //                "gruz_detali[etsng];" +
                //                "gruz_detali[nvs];" +
                //                "km;" +
                //                "ves_gruz;" +
                //                "mnkua_opv;" +
                //               "kol_vag;" +
                //               "st_otpr_detali[esr_otpr];" +
                //               "st_otpr_detali[n_rpus];" +
                //               "kod_grotp;" +
                //               "mname_rv;" +
                //               "esr_form;" +
                //               "st_nazn_detali[n_rpus];" +
                //               "st_nazn_detali[esr_nazn_vag];" +
                //               "n_dorus;" +
                //               "date_pogr;" +
                //               "kod_grp;" +
                //               "nom_sost;" +
                //               "date_op;" +
                //               "stan_detali[n_rpus];" +
                //               "stan_detali[esr_op];" +
                //               "prog_cha_prib;" +
                //               "kod_dor;" +
                //               "rod_vag;" +
                //               "esr_nazn;" +
                //               "pr_nrp;";
                //    writer.WriteLine(str_field);


                //    foreach (disl_vag_detali disl in res.disl_vag.ToList())
                //    {
                //        var flds = disl.GetType().GetProperties();
                //        var fl = flds.Where(c => c.Name == "n_rpus");
                //        //var val = fl.va
                //        //FieldInfo fld = typeof(flds).GetField("n_rpus");
                //        //var prop = typeof(disl).GetProperties()
                //        str =
                //            (disl.gruz != null && disl.gruz.etsng != null ? disl.gruz.etsng : "") + ";" +
                //            (disl.gruz != null && disl.gruz.nvs != null ? disl.gruz.nvs : "") + ";"  +
                //            (disl.km != null ? disl.km : "") + ";" +
                //            (disl.ves_gruz != null ? disl.ves_gruz : "") + ";" +
                //            (disl.mnkua_opv != null ? disl.mnkua_opv : "") + ";" +
                //            (disl.kol_vag != null ? disl.kol_vag : "") + ";" +
                //            (disl.st_otpr != null && disl.st_otpr.esr_otpr != null ? disl.st_otpr.esr_otpr : "") + ";" +
                //            (disl.st_otpr != null && disl.st_otpr.n_rpus != null ? disl.st_otpr.n_rpus : "") + ";" +
                //            (disl.kod_grotp != null ? disl.kod_grotp : "") + ";" +
                //            (disl.mname_rv != null ? disl.mname_rv : "") + ";" +
                //            (disl.esr_form != null ? disl.esr_form : "") + ";" +
                //            (disl.st_nazn != null && disl.st_nazn.n_rpus != null ? disl.st_nazn.n_rpus : "") + ";" +
                //            (disl.st_nazn != null && disl.st_nazn.esr_nazn_vag != null ? disl.st_nazn.esr_nazn_vag : "") + ";" +
                //            (disl.n_dorus != null ? disl.n_dorus : "") + ";" +
                //            (disl.date_pogr != null ? disl.date_pogr : "") + ";" +
                //            (disl.kod_grp != null ? disl.kod_grp : "") + ";" +
                //            (disl.nom_sost != null ? disl.nom_sost : "") + ";" +
                //            (disl.date_op != null ? disl.date_op : "") + ";" +
                //            (disl.stan != null && disl.stan.n_rpus != null ? disl.stan.n_rpus : "") + ";" +
                //            (disl.stan != null && disl.stan.esr_op != null ? disl.stan.esr_op : "") + ";" +
                //            (disl.prog_cha_prib != null ? disl.prog_cha_prib : "") + ";" +
                //            (disl.kod_dor != null ? disl.kod_dor : "") + ";" +
                //            (disl.rod_vag != null ? disl.rod_vag : "") + ";" +
                //            (disl.esr_nazn != null ? disl.esr_nazn : "") + ";" +
                //            (disl.pr_nrp != null ? disl.pr_nrp : "") + ";";
                //        writer.WriteLine(str);
                //    }


                //}

                //string res = client_givc.Getreq1091();

                //req1892? weatherForecast =
                //      JsonSerializer.Deserialize<req1892>(jsonString);

                //using var servicesProvider = new ServiceCollection()
                //    .AddTransient<Runner>() // Runner is the custom class
                //    .AddLogging(loggingBuilder =>
                //    {
                //        // configure Logging with NLog
                //        loggingBuilder.ClearProviders();
                //        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //        loggingBuilder.AddNLog(config);
                //    }).BuildServiceProvider();

                //var runner = servicesProvider.GetRequiredService<Runner>();
                //runner.DoAction("Action1");

                ////IDS_WIR ids_wir = new IDS_WIR(logger, config);
                //////ids_wir.ClearDoubling_Directory_WagonsRent(null);
                ////ids_wir.UpdateOperationArrivalSostav(284389, null);

                //Console.WriteLine("Hello, World!");



                //Person tom = new Person("Tom", 37);
                //string json = JsonSerializer.Serialize(tom);
                //Console.WriteLine(json);
                //Person? restoredPerson = JsonSerializer.Deserialize<Person>(json);
                //Console.WriteLine(restoredPerson?.Name); // Tom

                //string reqUrl = $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                //       | SecurityProtocolType.Tls11
                //       | SecurityProtocolType.Tls12;
                ////| SecurityProtocolType.Ssl3;
                //HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(reqUrl);
                //request.Method = "GET";
                //request.Accept = "application/json";
                //request.ContentType = "application/json; charset=utf-8";//"application/x-www-form-urlencoded";
                //try
                //{
                //    using (System.Net.WebResponse response = request.GetResponse())
                //    {
                //        try
                //        {
                //            using (System.IO.StreamReader rd = new System.IO.StreamReader(response.GetResponseStream()))
                //            {
                //                string json = rd.ReadToEnd();
                //                List<ExchangeRate>? restoredPerson = JsonSerializer.Deserialize<List<ExchangeRate>>(json);
                //                //Console.WriteLine(restoredPerson?.Name); // Tom
                //            }
                //        }
                //        catch (Exception e)
                //        {

                //        }
                //    }
                //}
                //catch (Exception e)
                //{


                //}

                //using (EFDbContext db = new EFDbContext())
                //{
                //    // создаем два объекта User
                //    //User user1 = new User { Name = "Tom", Age = 33 };
                //    //User user2 = new User { Name = "Alice", Age = 26 };

                //    //// добавляем их в бд
                //    //db.Users.Add(user1);
                //    //db.Users.Add(user2);
                //    //db.SaveChanges();
                //    //Console.WriteLine("Объекты успешно сохранены");

                //    // получаем объекты из бд и выводим на консоль
                //    //var Cars = db.ArrivalCars.ToList();
                //    //Console.WriteLine("Список объектов:");
                //    //foreach (var u in Cars)
                //    //{
                //    //    Console.WriteLine($"{u.Id}.{u.IdArrival} - {u.Num}");
                //    //}
                //}

            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                //logger.Error(ex, "Stopped program because of exception");
                logger.LogError(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
            Console.WriteLine("Press ANY key to exit");
            Console.ReadKey();
        }
    }
}
