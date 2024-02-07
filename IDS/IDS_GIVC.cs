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
        public int RequestToGIVC(string type_requests, string user)
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
                    TypeRequests = type_requests,
                    Create = DateTime.Now,
                    CreateUser = user,
                };
                client_givc = new WebClientGIVC(_logger, _configuration);
                if (type_requests == "req1892")
                {
                    //req1892 res = client_givc.GetReq1892(467004, 467004, 7932, 7932);
                    req1892 res = client_givc.GetReq1892(467004, 467201, 7932, 7932, "01.01.2024", "31.12.2024");
                    if (client_givc.ErrorWeb != null && client_givc.ErrorWeb == false && client_givc.ErrorToking == false)
                    {
                        result_givc_req.CountLine = res != null && res.disl_vag != null ? res.disl_vag.Count() : 0;
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
                _logger.LogError(_eventId, e, "RequestToGIVC(type_requests={0}, user={1})", type_requests, user);
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
