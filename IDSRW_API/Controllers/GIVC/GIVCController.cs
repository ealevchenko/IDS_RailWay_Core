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
            return c != null && c.ResultRequests!=null  ? new ObjectResult(c.ResultRequests) : NotFound();
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
        // POST: Givc/Request
        // BODY: GivcRequest (JSON, XML)
        [HttpPost]
        public async Task<IActionResult> Request([FromBody] parameters_reguest parameters)
        {
            IDS_GIVC ids_givc = new IDS_GIVC(_logger, _configuration);
            //int res_cl = ids_givc.RequestToGIVC(new parameters_reguest() { type_requests = conf_reg.type_requests, kod_stan_beg = conf_reg.kod_stan_beg, kod_stan_end = conf_reg.kod_stan_end, kod_grp_beg = conf_reg.kod_grp_beg, kod_grp_end = conf_reg.kod_grp_end }, null);
            GivcRequest? c = null;
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            GivcRequest added = await repo.CreateAsync(c);
            return CreatedAtRoute("GetGivcRequest", new { id = added.Id }, c); // 201 Created
        }


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
