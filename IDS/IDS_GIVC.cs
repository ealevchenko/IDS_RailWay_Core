using EF_IDS.Concrete;
using EF_IDS.Concrete.Directory;
using EF_IDS.Entities;
using GIVC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDS_
{
    public class parameters_reguest
    {
        public string type_requests { get; set; }        
        public int? kod_stan_beg { get; set; }
        public int? kod_stan_end { get; set; }
        public int? kod_grp_beg { get; set; }
        public int? kod_grp_end { get; set; }
        public string? nom_vag { get; set; }
        public string? date_beg { get; set; }
        public string? date_end { get; set; }
        public int? esr_form { get; set; }
        public int? nom_sost { get; set; }
        public int? esr_nazn { get; set; }
        public int? kod_stan_form { get; set; }
        public int? kod_gro { get; set; }
        public int? kod_stan_nazn { get; set; }
        public int? kod_grp { get; set; }
        public int? kod_gruz { get; set; }
    }
    public class IDS_GIVC : IDS_Base
    {
        private DbContextOptions<EFDbContext> options;
        private readonly ILogger<Object> _logger;
        private readonly IConfiguration _configuration;
        EventId _eventId = new EventId(0);
        private String? connectionString;
        private WebClientGIVC client_givc = null;
        private DataGIVC data_givc = null;
        public void SetupDB(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("IDS");
            var optionsBuilder = new DbContextOptionsBuilder<EFDbContext>();
            this.options = optionsBuilder.UseSqlServer(connectionString).Options;
        }

        public IDS_GIVC(ILogger<Object> logger, IConfiguration configuration) : base()
        {
            _logger = logger;
            _configuration = configuration;
            _eventId = int.Parse(_configuration["EventID:IDS_GIVC"]);
            data_givc = new DataGIVC(logger, configuration);
            SetupDB(configuration);
        }
        public IDS_GIVC(ILogger<Object> logger, IConfiguration configuration, EventId rootId) : base()
        {
            _logger = logger;
            _configuration = configuration;
            _eventId = rootId.Id + int.Parse(_configuration["EventID:IDS_GIVC"]);
            data_givc = new DataGIVC(logger, configuration);
            SetupDB(configuration);
        }

        #region ГИВС УЗ
        /// <summary>
        /// Выполнить запрос в БД ГИВЦ
        /// </summary>
        /// <param name="type_requests"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public int RequestToGIVC(parameters_reguest parameters, string user)
        {
            try
            {
                EFDbContext context = new EFDbContext(this.options);

                // Проверим и скорректируем пользователя
                if (String.IsNullOrWhiteSpace(user))
                {
                    user = System.Environment.UserDomainName + @"\" + System.Environment.UserName;
                }
                EFGivcRequest ef_givc = new EFGivcRequest(context);
                GivcRequest result_givc_req = new GivcRequest()
                {
                    //Id = 0,
                    DtRequests = DateTime.Now,
                    TypeRequests = parameters.type_requests,
                    ParametersReguest = System.Text.Json.JsonSerializer.Serialize(parameters),
                    Create = DateTime.Now,
                    CreateUser = user,
                };
                client_givc = new WebClientGIVC(_logger, _configuration);
                if (parameters.type_requests == "req1892")
                {
                    //req1892 res = client_givc.GetReq1892(467004, 467201, 7932, 7932, "01.01.2024", "31.12.2024");
                    req1892 res = client_givc.GetReq1892((int)parameters.kod_stan_beg, (int)parameters.kod_stan_end, (int)parameters.kod_grp_beg, (int)parameters.kod_grp_end, DateTime.Now.Date.AddMonths(-2).ToString("dd.MM.yyyy"), DateTime.Now.Date.AddDays(1).ToString("dd.MM.yyyy"));
                    if (client_givc.ErrorWeb != null && client_givc.ErrorWeb == false && client_givc.ErrorToking == false)
                    {
                        result_givc_req.CountLine = res != null && res.disl_vag != null ? res.disl_vag.Count() : 0;
                    }
                    else
                    {
                        result_givc_req.CountLine = -1;
                    }
                }
                if (parameters.type_requests == "req8858")
                {
                    req8858 res = client_givc.GetReq8858((string)parameters.nom_vag, (string)parameters.date_beg, (string)parameters.date_end);
                    if (client_givc.ErrorWeb != null && client_givc.ErrorWeb == false && client_givc.ErrorToking == false)
                    {
                        result_givc_req.CountLine = res != null && res.disl_vag != null ? res.disl_vag.Count() : 0;
                    }
                    else
                    {
                        result_givc_req.CountLine = -1;
                    }
                }
                if (parameters.type_requests == "req0002")
                {
                    req0002 res = client_givc.GetReq0002((int)parameters.esr_form, (int)parameters.nom_sost, (int)parameters.esr_nazn);
                    if (client_givc.ErrorWeb != null && client_givc.ErrorWeb == false && client_givc.ErrorToking == false)
                    {
                        result_givc_req.CountLine = res != null && res.info_fraza != null ? res.info_fraza.Count() : 0;
                    }
                    else
                    {
                        result_givc_req.CountLine = -1;
                    }
                }
                if (parameters.type_requests == "reqDisvag")
                {
                    reqDisvag res = client_givc.GetReqDisvag((int)parameters.kod_stan_form, (int)parameters.kod_gro, (int)parameters.kod_stan_nazn, (int)parameters.kod_grp, (int)parameters.kod_gruz);
                    if (client_givc.ErrorWeb != null && client_givc.ErrorWeb == false && client_givc.ErrorToking == false)
                    {
                        result_givc_req.CountLine = res != null && res.data != null ? res.data.Count() : 0;
                    }
                    else
                    {
                        result_givc_req.CountLine = -1;
                    }
                }
                result_givc_req.ResultRequests = client_givc.JsonResponse;
                ef_givc.Add(result_givc_req);
                int result = context.SaveChanges();
                _logger.LogInformation(_eventId, "Запрос на ГИВЦ выполнен {0} получено строк {1}, Код выполнения операции сохранения в БД {2}", result_givc_req.DtRequests, result_givc_req.CountLine, result);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "RequestToGIVC(parameters={0}, user={1})", parameters, user);
                return (int)errors_base.global;
            }
        }
        /// <summary>
        /// Вернуть последнюю строку запроса
        /// </summary>
        /// <param name="type_request"></param>
        /// <returns></returns>
        public GivcRequest? GetLastGivcRequest(string type_request)
        {
            try
            {
                EFDbContext context = new EFDbContext(this.options);
                EFGivcRequest ef_givc = new EFGivcRequest(context);
                GivcRequest? givc = ef_givc.Context.Where(g => g.TypeRequests == type_request).OrderByDescending(g => g.DtRequests).FirstOrDefault();
                return givc;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "GetLastGivcRequest(type_request={0})", type_request);
                return null;
            }
        }
        /// <summary>
        /// Вернуть последнюю дату запроса по типу запроса
        /// </summary>
        /// <param name="type_request"></param>
        /// <returns></returns>
        public DateTime? GetLastDateTimeRequest(string type_request)
        {
            try
            {
                EFDbContext context = new EFDbContext(this.options);
                EFGivcRequest ef_givc = new EFGivcRequest(context);
                GivcRequest? givc = ef_givc.Context.Where(g => g.TypeRequests == type_request).OrderByDescending(g => g.DtRequests).FirstOrDefault();
                return givc != null ? givc.DtRequests : null;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "GetLastDateTimeRequest(type_request={0})", type_request);
                return null;
            }
        }
        /// <summary>
        /// Вернуть распарсеный запрос из строки
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="givc_req"></param>
        /// <returns></returns>
        public T? GetGivcRequest<T>(GivcRequest givc_req)
        {
            try
            {
                T? result = data_givc.GetDeserializeJSON_ApiValuesResult<T>(givc_req.ResultRequests);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "GetGivcRequest(givc_req={0})", givc_req);
                return default(T?);
            }
        }
        /// <summary>
        /// Вернуть распарсеный запрос req1892 из строки
        /// </summary>
        /// <param name="givc_req"></param>
        /// <returns></returns>
        public req1892? GetGivcRequest1892(GivcRequest givc_req)
        {
            try
            {
                if (givc_req == null || givc_req.TypeRequests != "req1892") return null;
                req1892? res = GetGivcRequest<req1892>(givc_req);
                return res;
            }
            catch (Exception e)
            {
                _logger.LogError(_eventId, e, "GetGivcRequest1892(givc_req={0})", givc_req);
                return null;
            }

        }

        #endregion
    }
}
