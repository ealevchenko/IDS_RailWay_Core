using EF_IDS.Entities;
using IDS_;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebAPI.Repositories;
using WebAPI.Repositories.GIVC;
using Microsoft.Extensions.Logging;

namespace WebAPI.Controllers.GIVC
{
    [Route("[controller]")]
    [ApiController]
    public class GIVCController : ControllerBase
    {
        private IRepository<GivcRequest> repo;
        private readonly ILogger<GIVCController> _logger;
        private readonly IConfiguration _configuration;

        // конструктор вводит зарегистрированный репозиторий
        public GIVCController(IRepository<GivcRequest> repo, ILogger<GIVCController> logger, IConfiguration configuration)
        {
            this.repo = repo;
            _logger = logger;
            _configuration = configuration;
            _logger.LogDebug(1, "NLog injected into GIVCController");

        }

        // GET: Givc/Request
        [HttpGet("Request")]
        public async Task<IEnumerable<GivcRequest>> GetGivcRequest()
        {
            return await repo.RetrieveAllAsync();
        }
        // GET: Givc/Request/type_requests/req1892/count/10
        [HttpGet("Request/type_requests/{type_requests}/count/{count:int}")]
        public async Task<IActionResult> GetListGivcRequestOfType(string type_requests, int? count)
        {
            IEnumerable<GivcRequest> list = await repo.RetrieveAllAsync();
            string mes_not_data = "По указанному типу запроса нет данных!";
            string mes_not_type_requests = "Не указан тип справки!";
            if (String.IsNullOrWhiteSpace(type_requests)) return BadRequest(new { message = mes_not_type_requests });
            if (list == null) return BadRequest(new { message = mes_not_data });
            if (count > 0)
            {
                list = list.Where(c => c.TypeRequests == type_requests).OrderByDescending(c => c.Id).Take((int)count);
            }
            else
            {
                list = list.Where(c => c.TypeRequests == type_requests).OrderByDescending(c => c.Id);
            }

            var res = list.Select(u => new { u.Id, u.DtRequests, u.TypeRequests, u.ParametersReguest, u.CountLine, u.Create, u.CreateUser });
            return res != null ? new ObjectResult(res) : BadRequest(new { message = mes_not_data });
        }

        // GET: Givc/Request/[id]
        [HttpGet("Request/{id}", Name = "GetGivcRequest")]
        public async Task<IActionResult> GetGivcRequest(int id)
        {
            GivcRequest c = await repo.RetrieveAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(c); // 200 OK
        }
        // GET: Givc/Request/last/[type_requests]
        [HttpGet("Request/last/{type_requests}")]
        public async Task<IActionResult> GetGivcRequest(string type_requests)
        {
            IEnumerable<GivcRequest> list = await repo.RetrieveAllAsync();
            GivcRequest? c = null;
            if (list != null)
            {
                c = list.Where(c => c.TypeRequests == type_requests).OrderByDescending(c => c.DtRequests).FirstOrDefault();
            }
            return c != null ? new ObjectResult(c) : NotFound();
        }
        // GET: Givc/Request/last/result/[type_requests]
        [HttpGet("Request/last/result/{type_requests}")]
        public async Task<IActionResult> GetResultGivcRequest(string type_requests)
        {
            IEnumerable<GivcRequest> list = await repo.RetrieveAllAsync();
            GivcRequest? c = null;
            if (list != null)
            {
                c = list.Where(c => c.TypeRequests == type_requests).OrderByDescending(c => c.DtRequests).FirstOrDefault();
            }
            return c != null && c.ResultRequests != null ? new ObjectResult(c.ResultRequests) : NotFound();
        }


        // POST: Givc/Request
        // BODY: GivcRequest (JSON, XML)
        [HttpPost("Request")]
        public async Task<IActionResult> Create([FromBody] GivcRequest c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            GivcRequest added = await repo.CreateAsync(c);
            return CreatedAtRoute("GetGivcRequest", new { id = added.Id }, c); // 201 Created
        }
        ///TODO! Блаканул бабло
        // POST: Givc/Request
        // BODY: GivcRequest (JSON, XML)
        //[HttpPost]
        //public async Task<IActionResult> Request([FromBody] parameters_reguest parameters)
        //{
        //    IDS_GIVC ids_givc = new IDS_GIVC(_logger, _configuration);
        //    // Текущая дата
        //    DateTime dt_curr = DateTime.Now;
        //    DateTime cur_date = dt_curr.Date;
        //    int cur_day = dt_curr.Date.Day;
        //    int cur_hour = dt_curr.Hour;
        //    // Получим последний запрос
        //    GivcRequest? last_givc_req = ids_givc.GetLastGivcRequest(parameters.type_requests);
        //    DateTime? dt_last = last_givc_req != null ? last_givc_req.DtRequests : null;
        //    string s_param = System.Text.Json.JsonSerializer.Serialize(parameters); // Сериализуем parameters
        //    if (dt_last != null)
        //    {
        //        int last_day = ((DateTime)dt_last).Date.Day;
        //        int last_hour = ((DateTime)dt_last).Hour;
        //        // Проверка на исключение повторного запроса
        //        if (s_param == last_givc_req.ParametersReguest && cur_day == last_day && cur_hour == last_hour)
        //        { return BadRequest(new { message = "The request has already been executed!" }); } // 400 Bad request

        //    }
        //    GivcRequest? req = ids_givc.RequestToGIVC(parameters, null); ;
        //    if (req == null)
        //    {
        //        return BadRequest(); // 400 Bad request
        //    }
        //    GivcRequest added = await repo.CreateAsync(req);
        //    return CreatedAtRoute("GetGivcRequest", new { id = added.Id }, req); // 201 Created
        //}


        //// PUT: api/GivcRequest/[id]
        //// BODY: GivcRequest (JSON, XML)
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] GivcRequest c)
        //{
        //    if (c == null || c.Id != id)
        //    {
        //        return BadRequest(); // 400 Bad request
        //    }
        //    var existing = await repo.RetrieveAsync(id);
        //    if (existing == null)
        //    {
        //        return NotFound(); // 404 Resource not found
        //    }
        //    await repo.UpdateAsync(id, c);
        //    return new NoContentResult(); // 204 No content
        //}

        //// DELETE: api/GivcRequest/[id]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var existing = await repo.RetrieveAsync(id);
        //    if (existing == null)
        //    {
        //        return NotFound(); // 404 Resource not found
        //    }
        //    bool deleted = await repo.DeleteAsync(id);
        //    if (deleted)
        //    {
        //        return new NoContentResult(); // 204 No content
        //    }
        //    else
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
