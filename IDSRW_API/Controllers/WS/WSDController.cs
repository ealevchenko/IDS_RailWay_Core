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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Controllers.Directory
{
    #region ОПЕРАЦИЯ ПРИНЯТЬ (АРМ)
    public class OperationArrivalWagons
    {
        public int id_outer_way { get; set; }
        public List<ListOperationWagon> wagons { get; set; }
        public int id_way_on { get; set; }
        public bool head { get; set; }
        public DateTime lead_time { get; set; }
        public string locomotive1 { get; set; }
        public string? locomotive2 { get; set; }

    }
    #endregion

    #region ОПЕРАЦИЯ ОТПРАВИТЬ (АРМ)
    public class OperationOutgoingWagons
    {
        public int id_way_from { get; set; }
        public List<ListOperationWagon> wagons { get; set; }
        public int id_outer_way { get; set; }
        public DateTime lead_time { get; set; }
        public string locomotive1 { get; set; }
        public string? locomotive2 { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ ВЕРНУТЬ (Обновленный АРМ)
    public class OperationReturnWagons
    {
        public int id_outer_way { get; set; }
        public List<ListOperationWagon> wagons { get; set; }
        public int id_way { get; set; }
        public bool head { get; set; }
        public DateTime? lead_time { get; set; }
        public string? locomotive1 { get; set; }
        public string? locomotive2 { get; set; }
        public bool type_return { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ РОСПУСК (Обновленный АРМ)
    public class OperationDissolutionWagons
    {
        public int id_way_from { get; set; }
        public List<ListDissolutionWagon> list_dissolution { get; set; }
        public DateTime date_start { get; set; }
        public DateTime date_stop { get; set; }
        public string locomotive1 { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ ДИСЛОКАЦИИ (Обновленный АРМ)
    public class OperationDislocationWagons
    {
        public int id_way_from { get; set; }
        public List<ListOperationWagon> wagons { get; set; }
        public int id_way_on { get; set; }
        public bool head { get; set; }
        public DateTime lead_time { get; set; }
        public string locomotive1 { get; set; }
        public string? locomotive2 { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ АДМ
    public class AdmDivisionOutgoingWagons
    {
        public int num_doc { get; set; }
        public List<int> nums { get; set; }
        public int id_division { get; set; }
    }
    public class AdmChangeVesgOutgoingWagons
    {
        public int num_doc { get; set; }
        public int num_vag { get; set; }
        public int vesg { get; set; }
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
            _logger = logger;
            _configuration = configuration;
            _eventId_ids_wir = int.Parse(_configuration["EventID:IDS_WIR"]);
            _logger.LogDebug(1, "NLog injected into WSDController");
        }

        #region ОТПРАВЛЕННЫЕ СОСТАВЫ (АРМ)

        /// <summary>
        /// Получить открытые отправленные составы
        /// </summary>
        /// <param name="id_way"></param>
        /// <returns></returns>
        // GET: WSD/view/open/outgoing/sostav/way/217
        [HttpGet("view/open/outgoing/sostav/way/{id_way}")]
        public async Task<ActionResult<IEnumerable<ViewOutgoingSostav>>> GetViewOutgoingSostavOfIdWay(int id_way)
        {
            try
            {
                List<ViewOutgoingSostav> result = await db.getViewOutgoingSostav()
                    .AsNoTracking()
                    .Where(w => w.IdWayFrom == id_way && w.DateDepartureAmkr == null && w.Status > 0 && w.Status < 3)
                    .ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

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

        // GET: WSD/view/wagons/outer_way/station_from/8
        [HttpGet("view/wagons/outer_way/station_from/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewWagonsOfOuterWay>>> getViewOpenWagonsOfOuterWaysStationFrom(int id_station)
        {
            try
            {
                List<ViewWagonsOfOuterWay> result = await db.getViewOpenWagonsOfOuterWaysStationFrom(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region РАСЧЕТ ПЛАТЫ ЗА ПОЛЬЗОВАНИЕ (АРМ)

        // GET: WSD/view/calc_wagon/way/821933
        [HttpGet("view/calc_wagon/way/{id_way}")]
        public async Task<ActionResult<IEnumerable<CalcWagonUsageFee>>> getCalcUsageFeeCarsOfWay(int id_way)
        {
            try
            {
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);

                List<CalcWagonUsageFee> result = ids_wir.CalcUsageFeeCarsOfWay(id_way);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Расчет платы за пользование по отправленному составу
        /// </summary>
        /// <param name="id_sostav"></param>
        /// <returns></returns>
        // GET: WSD/view/calc_wagon/outgoing/sostav/282860
        [HttpGet("view/calc_wagon/outgoing/sostav/{id_sostav}")]
        public async Task<ActionResult<IEnumerable<ResultUpdateIDWagon>>> getCalcUsageFeeOfOutgoingSostav(int id_sostav)
        {
            try
            {
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                ResultUpdateIDWagon result = ids_wir.CalcUsageFeeOfOutgoingSostav(id_sostav);
                //List<ResultIDWagon> list_res = result.listResult.ToList();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region ОПЕРАЦИИ НАД ВАГОНАМИ (АРМ)
        // POST: WSD/operation/arrival
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/arrival")]
        public async Task<ActionResult<ResultTransfer>> PostArrivalWagonsOfStationAMKR([FromBody] OperationArrivalWagons value)
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
                ResultTransfer result = ids_wir.ArrivalWagonsOfStationAMKR(value.id_outer_way, value.wagons, value.id_way_on, value.head, value.lead_time, value.locomotive1, value.locomotive2, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/outgoing
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/outgoing")]
        public async Task<ActionResult<ResultTransfer>> PostOutgoingWagonsOfStationAMKR([FromBody] OperationOutgoingWagons value)
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
                ResultTransfer result = ids_wir.OutgoingWagonsOfStationAMKR(value.id_way_from, value.wagons, value.id_outer_way, value.lead_time, value.locomotive1, value.locomotive2, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/outgoing
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/return")]
        public async Task<ActionResult<ResultTransfer>> PostReturnWagonsOfStationAMKR([FromBody] OperationReturnWagons value)
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
                ResultTransfer result = ids_wir.ReturnWagonsOfStationAMKR(value.id_outer_way, value.wagons, value.id_way, value.head, value.lead_time, value.locomotive1, value.locomotive2, value.type_return, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/dissolution
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/dissolution")]
        public async Task<ActionResult<ListResultTransfer>> PostDissolutionWagonsOfStationAMKR([FromBody] OperationDissolutionWagons value)
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
                ListResultTransfer result = ids_wir.DissolutionWagonsOfStationAMKR(value.id_way_from, value.list_dissolution, value.date_start, value.date_stop, value.locomotive1, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/dislocation
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/dislocation")]
        public async Task<ActionResult<ResultTransfer>> PostDislocationWagonsOfStationAMKR([FromBody] OperationDislocationWagons value)
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
                ResultTransfer result = ids_wir.DislocationWagonsOfStationAMKR(value.id_way_from, value.wagons, value.id_way_on, value.head, value.lead_time, value.locomotive1, value.locomotive2, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region АДМИНИСТРИРОВАНИЕ
        // POST: WSD/admin/change/division/outgoing
        // BODY: WSD (JSON, XML)
        [HttpPost("admin/change/division/outgoing")]
        public async Task<ActionResult<ResultTransfer>> PostAdmChangeDivisionOutgoingWagons([FromBody] AdmDivisionOutgoingWagons value)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

                if (value == null || !IsAuthenticated)
                {
                    return BadRequest();
                }
                if (user == "EUROPE\\ealevchenko" || user == "EUROPE\\lvgubarenko")
                {
                    IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                    int result = ids_wir.ChangeDivisionOutgoingWagons(value.num_doc, value.nums, value.id_division);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST: WSD/admin/change/vesg/outgoing
        // BODY: WSD (JSON, XML)
        [HttpPost("admin/change/vesg/outgoing")]
        public async Task<ActionResult<ResultTransfer>> PostAdmChangeVesgOutgoingWagons([FromBody] AdmChangeVesgOutgoingWagons value)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

                if (value == null || !IsAuthenticated)
                {
                    return BadRequest();
                }
                if (user == "EUROPE\\ealevchenko" || user == "EUROPE\\lvgubarenko")
                {
                    IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                    int result = ids_wir.ChangeVesgOutgoingWagons(value.num_doc, value.num_vag, value.vesg);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion
    }
}
