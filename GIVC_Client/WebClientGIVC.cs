using Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace GIVC
{
    public class WebClientGIVC
    {
        private readonly ILogger<Object> _logger;
        private readonly IConfiguration _configuration;
        private string url;
        private string url_token;
        private string userName;
        private string password;
        private string Edrpou = "24432974";
        public string? JsonResponse { get { return this.web_api != null ? this.web_api.JsonResponse : null; } }

        private WebApiToken web_api = null;
        public bool?  ErrorWeb { get { return web_api!=null ? web_api.error : null; } }
        public bool?  ErrorToking { get { return web_api!=null ? web_api.error_token : null; } }
        public WebClientGIVC(ILogger<Object> logger, IConfiguration configuration)
        {
            try
            {
                _logger = logger;
                _configuration = configuration;
                userName = _configuration["GIVC:userName"];
                password = _configuration["GIVC:password"];
                url = _configuration["GIVC:url"];
                url_token = _configuration["GIVC:url_token"];
                Edrpou = _configuration["GIVC:Edrpou"];
                web_api = new WebApiToken(logger, userName, password, url, url_token);
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("WebClientGIVC(), Exception={0}", e));
            }
        }
        /// <summary>
        /// Получить натурную ведомость
        /// </summary>
        /// <param name="esr_form"></param>
        /// <param name="nom_sost"></param>
        /// <param name="esr_nazn"></param>
        /// <returns></returns>
        public req0002 GetReq0002(int esr_form, int nom_sost, int esr_nazn)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            req0002 resp = web_api.GetDeserializeJSON_ApiValues<req0002>("GetData/req0002" + String.Format("?esr_form={0}&nom_sost={1}&esr_nazn={2}", esr_form, nom_sost, esr_nazn)); // 
            return resp;
        }
        /// <summary>
        /// 1892 «Універсальна форма по дислокації вагонів призначенням на задану станцію з урахуванням одержувача, вантажу, 
        /// відправника, індексу поїзда, дислокації тощо»
        /// </summary>
        /// <param name="kod_stan_beg"></param>
        /// <param name="kod_stan_end"></param>
        /// <param name="kod_grp_beg"></param>
        /// <param name="kod_grp_end"></param>
        /// <returns></returns>
        public req1892 GetReq1892(int kod_stan_beg, int kod_stan_end, int kod_grp_beg, int kod_grp_end)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            req1892 resp = web_api.GetDeserializeJSON_ApiValues<req1892>("GetData/req1892" + String.Format("?kod_stan_beg={0}&kod_stan_end={1}&kod_grp_beg={2}&kod_grp_end={3}&edrpou={4}", kod_stan_beg, kod_stan_end, kod_grp_beg, kod_grp_end, Edrpou)); // 467004, 467201
            return resp;
        }
        public req1892 GetReq1892(int kod_stan_beg, int kod_stan_end, int kod_grp_beg, int kod_grp_end, string date_beg, string date_end)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            req1892 resp = web_api.GetDeserializeJSON_ApiValues<req1892>("GetData/req1892" + String.Format("?kod_stan_beg={0}&kod_stan_end={1}&kod_grp_beg={2}&kod_grp_end={3}&date_beg={4}&date_end={5}&edrpou={6}", kod_stan_beg, kod_stan_end, kod_grp_beg, kod_grp_end, date_beg, date_end, Edrpou)); // 467004, 467201
            return resp;
        }
        //public req1892 GetReq1892(int kod_stan_beg, int kod_stan_end, int kod_grp_beg, int kod_grp_end, string kod_gruz_beg, string kod_gruz_end)
        //{
        //    if (String.IsNullOrWhiteSpace(url)) return null;
        //    req1892 resp = web_api.GetDeserializeJSON_ApiValues<req1892>("GetData/req1892" + String.Format("?kod_stan_beg={0}&kod_stan_end={1}&kod_grp_beg={2}&kod_grp_end={3}&kod_gruz_beg={4}&kod_gruz_end={5}&edrpou={6}", kod_stan_beg, kod_stan_end, kod_grp_beg, kod_grp_end, kod_gruz_beg, kod_gruz_end, Edrpou)); // 467004, 467201
        //    return resp;
        //}
        /// <summary>
        /// 1892 «Універсальна форма по дислокації вагонів призначенням на задану станцію з урахуванням одержувача, вантажу, 
        /// відправника, індексу поїзда, дислокації тощо»
        /// </summary>
        /// <param name="kod_stan_beg"></param>
        /// <param name="kod_stan_end"></param>
        /// <returns></returns>
        public req1892 GetReq1892(int kod_stan_beg, int kod_stan_end)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            req1892 resp = web_api.GetDeserializeJSON_ApiValues<req1892>("GetData/req1892" + String.Format("?kod_stan_beg={0}&kod_stan_end={1}", kod_stan_beg, kod_stan_end)); // 467004, 467201
            return resp;
        }
        /// <summary>
        /// 1091 «Підхід порожніх вагонів до станції»
        /// </summary>
        /// <param name="kod_dor"></param>
        /// <param name="kod_grp_beg"></param>
        /// <param name="kod_grp_end"></param>
        /// <param name="esr_nazn_vag"></param>
        /// <returns></returns>
        public req1091 GetReq1091(int kod_dor, int kod_grp_beg, int kod_grp_end, int esr_nazn_vag)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            req1091 resp = web_api.GetDeserializeJSON_ApiValues<req1091>("GetData/req1091" + String.Format("?kod_dor={0}&kod_grp_beg={1}&kod_grp_end={2}&esr_nazn_vag={3}", kod_dor, kod_grp_beg, kod_grp_end, esr_nazn_vag));
            return resp;
        }
        /// <summary>
        /// 4373 «Підхід поїздів до станції»
        /// </summary>
        /// <param name="esr_op"></param>
        /// <param name="sost_pogr_pp"></param>
        /// <returns></returns>
        public req4373 GetReq4373(int esr_op, int sost_pogr_pp)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            req4373 resp = web_api.GetDeserializeJSON_ApiValues<req4373>("GetData/req4373" + String.Format("?esr_op={0}&sost_pogr_pp={1}", esr_op, sost_pogr_pp));
            return resp;
        }
        /// <summary>
        /// 7002 «Підхід вагонів під вивантаження»
        /// </summary>
        /// <param name="Edrpou"></param>
        /// <param name="kod_stan_beg"></param>
        /// <param name="kod_stan_end"></param>
        /// <param name="kod_grp_beg"></param>
        /// <param name="kod_grp_end"></param>
        /// <returns></returns>
        public req7002 GetReq7002(string Edrpou, int kod_stan_beg, int kod_stan_end, int kod_grp_beg, int kod_grp_end)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            req7002 resp = web_api.GetDeserializeJSON_ApiValues<req7002>("GetData/req7002" + String.Format("?Edrpou={0}&kod_stan_beg={1}&kod_stan_end={2}&kod_grp_beg={3}&kod_grp_end={4}", Edrpou, kod_stan_beg, kod_stan_end, kod_grp_beg, kod_grp_end));
            return resp;
        }
        /// <summary>
        /// Довідка «Дислокація вагонів з вантажем, які прямують зі станції відправлення (вантажовідправник) на станцію призначення (вантажоодержувач)»
        /// </summary>
        /// <param name="kod_stan_form"></param>
        /// <param name="kod_gro"></param>
        /// <param name="kod_stan_nazn"></param>
        /// <param name="kod_grp"></param>
        /// <param name="kod_gruz"></param>
        /// <returns></returns>
        public reqDisvag GetReqDisvag(int kod_stan_form, int kod_gro, int kod_stan_nazn, int kod_grp, int kod_gruz)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            reqDisvag resp = web_api.GetDeserializeJSON_ApiValues<reqDisvag>("GetData/reqDisvag" + String.Format("?kod_stan_form={0}&kod_gro={1}&kod_stan_nazn={2}&kod_grp={3}&kod_gruz={4}", kod_stan_form, kod_gro, kod_stan_nazn, kod_grp, kod_gruz));
            return resp;
        }
        /// <summary>
        /// Довідка «Дислокація вагонів з вантажем, які прямують зі станції відправлення (вантажовідправник) на станцію призначення (вантажоодержувач)»
        /// </summary>
        /// <param name="Edrpou"></param>
        /// <param name="kod_grp"></param>
        /// <returns></returns>
        public reqDisvag GetReqDisvag(string Edrpou, int kod_grp)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            reqDisvag resp = web_api.GetDeserializeJSON_ApiValues<reqDisvag>("GetData/reqDisvag" + String.Format("?Edrpou={0}&kod_grp={1}", Edrpou, kod_grp));
            return resp;
        }
        public reqNDI GetReqNDI(int NomNDI)
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            reqNDI resp = web_api.GetDeserializeJSON_ApiValues<reqNDI>("GetData/reqNDI" + String.Format("?NomNDI={0}", NomNDI));
            return resp;
        }
        public string Getreq1091()
        {
            if (String.IsNullOrWhiteSpace(url)) return null;
            string resp = web_api.GetApiValues("GetData/req1091" + String.Format("?kod_dor={0}&kod_grp_beg={1}&kod_grp_end={2}&esr_nazn_vag={3}", 45, 7932, 7932, 467004));
            return resp;
        }
    }
}
