using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Helper
{
    public class WebApiToken
    {
        private readonly ILogger<Object> _logger;
        private string url;
        private string url_token;
        private string user;
        private string psw;
        private static string token = null;
        private string jsonString = null;
        public string JsonResponse { get { return this.jsonString; } }
        public bool error;
        public bool error_token;
        public WebApiToken(ILogger<Object> logger, string user, string psw, string url, string url_token)
        {

            _logger = logger;
            try
            {
                this.error = false;
                this.user = user;
                this.psw = psw;
                this.url = url;
                this.url_token = url_token;
                //this.error_token = AuthorizationToken();
                //Dictionary<string, string> tokenDictionary = GetTokenDictionary(this.user, this.psw, token);
                //bool keyExistance = tokenDictionary.ContainsKey("access_token");
                //if (keyExistance) token = tokenDictionary["access_token"];
            }
            catch (Exception e)
            {
                this.error = true;
                _logger.LogError(String.Format("WebApiToken(url_token={0}, userName={1}, password={2}), Exception={3}", url_token, this.user, this.psw, e));

            }
        }
        public bool AuthorizationToken()
        {
            try
            {
                Dictionary<string, string> tokenDictionary = GetTokenDictionary(this.user, this.psw, token);
                bool keyExistance = tokenDictionary.ContainsKey("access_token");
                if (keyExistance) token = tokenDictionary["access_token"];
                return !keyExistance;
            }
            catch (Exception e)
            {
                return false;
                _logger.LogError(String.Format("WebApiToken(url_token={0}, userName={1}, password={2}), Exception={3}", url_token, this.user, this.psw, e));

            }
        }
        /// <summary>
        /// Получение токена
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetTokenDictionary(string user, string psw, string ref_token)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(url)) return null;
                //var pairs = new List<KeyValuePair<string, string>>
                //{
                //    new KeyValuePair<string, string>( "username", userName ),
                //    new KeyValuePair<string, string> ( "Password", password ),
                //    new KeyValuePair<string, string> ( "refresh_token", null ),
                //};
                //var content = new  FormUrlEncodedContent(pairs);
                //var content = new JsonContent(pairs);

                using StringContent content = new(System.Text.Json.JsonSerializer.Serialize(new
                {
                    username = user,
                    password = psw,
                    refresh_token = ref_token
                }),
                                                    Encoding.UTF8,
                                                    "application/json");

                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                //       | SecurityProtocolType.Tls11
                //       | SecurityProtocolType.Tls12;// SecurityProtocolType.Ssl3;

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(1);
                    var response =
                        client.PostAsync(url + "/" + url_token, content).Result;
                    _logger.LogDebug(String.Format("Web API Connect [AbsoluteUri :{0}, user : {1} status:{2}", response.RequestMessage.RequestUri.AbsoluteUri, user, response.StatusCode));
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string err = "Ошибка выполнения client.PostAsync :" + response.ToString();
                        _logger.LogError(err);

                    }
                    var result = response.Content.ReadAsStringAsync().Result;
                    // Десериализация полученного JSON-объекта
                    Dictionary<string, string> tokenDictionary =
                        JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
                    return tokenDictionary;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("GetTokenDictionary(userName={0}, password={1}), Exception={2}", user, psw, e));
                return null;
            }
        }
        public HttpClient CreateClient(string accessToken = "")
        {
            try
            {
                var client = new HttpClient();
                client.Timeout = TimeSpan.FromMinutes(10); // Добавил таймаут 10 мин
                if (!string.IsNullOrWhiteSpace(accessToken))
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
                }
                return client;
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("CreateClient(accessToken={0}), Exception={1}", accessToken, e));
                return null;
            }
        }
        public string GetUserInfo(string token)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(url)) return null;
                using (var client = CreateClient(token))
                {
                    if (client == null) return null;
                    var response = client.GetAsync(url + "/api/Account/UserInfo").Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("GetUserInfo(token={0}), Exception={1}", token, e));
                return null;
            }
        }
        public string GetApiValues(string api_url)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(url)) return null;
                this.error_token = AuthorizationToken();
                if (this.error_token) return "error_toking";
                using (var client = CreateClient(token))
                {
                    if (client == null) return null;
                    var response = client.GetAsync(url + "/" + api_url).Result;
                    _logger.LogDebug(String.Format("Web API GetAsync [requestUri :{0}, status:{1}", url + "/" + api_url, response.StatusCode));
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        string err = response.ToString();
                        _logger.LogError(err);
                    }
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("GetApiValues(api_comand={0}), Exception={1})", api_url, e));
                return null;
            }
        }
        public T? GetDeserializeJSON_ApiValues<T>(string api_comand)
        {
            try
            {
                jsonString = GetApiValues(api_comand);
                T? result = System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(String.Format("GetDeserializeJSON_ApiValues(api_comand={0}), Exception={1})", api_comand, e));
                return default(T);
            }
        }

    }
}