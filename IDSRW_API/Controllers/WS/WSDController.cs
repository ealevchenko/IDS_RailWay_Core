using EF_IDS.Concrete;
using EF_IDS.Entities;
using EF_IDS.Functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Repositories;
using WebAPI.Repositories.Directory;
using EFIDS.Functions;
using IDS_;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPI.Controllers.GIVC;

namespace WebAPI.Controllers.Directory
{

    #region ОПЕРАЦИЯ ПРИНЯТЬ (Обновленный АРМ)
    public class OperationArrivalWagons
    {
        public int id_outer_way { get; set; }
        public List<ListOperationWagon> wagons { get; set; }
        public int id_way_on { get; set; }
        public bool head { get; set; }
        public DateTime lead_time { get; set; }
        public string locomotive1 { get; set; }
        public string locomotive2 { get; set; }
        //public string user { get; set; }
    }
    #endregion

    [Route("[controller]")]
    [ApiController]
    public class WSDController : ControllerBase
    {
        private EFDbContext db;
        private readonly ILogger<WSDController> _logger;
        private readonly IConfiguration _configuration;
        EventId _eventId = new EventId(0);
        EventId _eventId_ids_wir = new EventId(0);

        public WSDController(EFDbContext db, ILogger<WSDController> logger, IConfiguration configuration)
        {
            this.db = db;
            _eventId_ids_wir = int.Parse(_configuration["EventID:IDS_WIR"]);
            _logger = logger;
            _configuration = configuration;
            _logger.LogDebug(1, "NLog injected into WSDController");
        }

        // GET: WSD/view/wagon/way/115
        [HttpGet("view/wagon/way/{id_way}")]
        public async Task<ActionResult<IEnumerable<ViewCarWay>>> GetViewWagonsOfIdWay(int id_way)
        {
            try
            {
                List<ViewCarWay> result = await db.getViewWagonsOfIdWay(id_way).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: WSD/view/total_balance
        [HttpGet("view/total_balance")]
        public async Task<ActionResult<IEnumerable<ViewTotalBalance>>> GetViewTotalBalance()
        {
            try
            {
                List<ViewTotalBalance> result = await db.getViewTotalBalance().ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: WSD/view/operators/station/8
        [HttpGet("view/operators/station/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewOperatorsStation>>> GetViewOperatorsOfStation(int id_station)
        {
            try
            {
                List<ViewOperatorsStation> result = await db.getViewOperatorsOfStation(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: WSD/view/operators/send/station/8
        [HttpGet("view/operators/send/station/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewOperatorsOuterWay>>> GetViewOperatorsSendOfIdStation(int id_station)
        {
            try
            {
                List<ViewOperatorsOuterWay> result = await db.getViewOperatorsSendOfIdStation(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: WSD/view/operators/arrival/station/8
        [HttpGet("view/operators/arrival/station/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewOperatorsOuterWay>>> GetViewOperatorsArrivalOfIdStation(int id_station)
        {
            try
            {
                List<ViewOperatorsOuterWay> result = await db.getViewOperatorsArrivalOfIdStation(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: WSD/view/wagons/outer_way/station_on/8
        [HttpGet("view/wagons/outer_way/station_on/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewWagonsOfOuterWay>>> getViewOpenWagonsOfOuterWaysStationOn(int id_station)
        {
            try
            {
                List<ViewWagonsOfOuterWay> result = await db.getViewOpenWagonsOfOuterWaysStationOn(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/arrival
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/arrival")]
        public async Task<ActionResult> PostArrivalWagonsOfStationAMKR([FromBody] OperationArrivalWagons value)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

                if (value == null || !IsAuthenticated)
                {
                    return BadRequest();
                }
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                ResultTransfer result = ids_wir.ArrivalWagonsOfStation(value.id_outer_way, value.wagons, value.id_way_on, value.head, value.lead_time, value.locomotive1, value.locomotive2, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
