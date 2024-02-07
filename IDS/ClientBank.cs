using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IDS_
{
    public class ClientBank
    {
        public class BankRate
        {
            public int? r030 { get; set; }
            public string txt { get; set; }
            public decimal? rate { get; set; }
            public string cc { get; set; }
            public string exchangedate { get; set; }
        }

        private readonly ILogger<Object> _logger;
        private readonly IConfiguration _configuration;
        private string reqUrl;
        private string Method;
        private string Accept;
        private string ContentType;

        public ClientBank(ILogger<Object> logger, IConfiguration configuration) : base()
        {
            _logger = logger;
            _configuration = configuration;
            reqUrl = _configuration["BankRate:reqUrl"];
            Method = _configuration["BankRate:Method"];
            Accept = _configuration["BankRate:Accept"];
            ContentType = _configuration["BankRate:ContentType"];
        }

        public List<BankRate> GetBankRates()
        {
            try
            {
                //string reqUrl = $"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                       | SecurityProtocolType.Tls11
                       | SecurityProtocolType.Tls12;
                //| SecurityProtocolType.Ssl3;
                HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(reqUrl);
                request.Method = Method;//"GET";
                request.Accept = Accept;//"application/json";
                request.ContentType = ContentType;//"application/json; charset=utf-8";
                try
                {
                    using (System.Net.WebResponse response = request.GetResponse())
                    {
                        try
                        {
                            using (System.IO.StreamReader rd = new System.IO.StreamReader(response.GetResponseStream()))
                            {
                                string json = rd.ReadToEnd();
                                List<BankRate>? result = JsonSerializer.Deserialize<List<BankRate>>(json);
                                return result;
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.LogError("[GetBankRates] - {0}", e);
                            return null;
                        }
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("[GetBankRates] - {0}", e);
                    return null;

                }

            }
            catch (Exception e)
            {
                _logger.LogError("[GetBankRates] - {0}", e);
                return null;
            }

        }
    }
}
