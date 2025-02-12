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
using Newtonsoft.Json.Linq;
using static IDS_.IDS_WIR;

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

    #region ОПЕРАЦИЯ ПРЕДЪЯВЛЕНИЯ СОСТАВА (Обновленный АРМ)
    public class OperationProvideWagons
    {
        public int id_way_from { get; set; }
        public long? id_sostav { get; set; }
        public List<ListOperationWagon> wagons { get; set; }
        public DateTime lead_time { get; set; }
    }
    public class OperationDTProvideWagons
    {
        public long? id_sostav { get; set; }
        public DateTime lead_time { get; set; }
    }
    public class OperationMoveWagonsProvideWay
    {
        public int id_way_on { get; set; }
        public List<int> nums { get; set; }
        public DateTime lead_time { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ ОТПРАВКИ СОСТАВА НА УЗ (Обновленный АРМ)
    public class OperationSendingSostavOnUZ
    {
        public long id_outgoing_sostav { get; set; }
        public DateTime lead_time { get; set; }
        public String composition_index { get; set; }
        public bool update_epd { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ ФОРМИРОВАНИЯ ПОДАЧ (ВЫГРУЗКА, ПОГРУЗКА...)
    public class OperationAddFilingUnloading
    {
        public int id_filing { get; set; }
        public string? num_filing { get; set; }
        public int type_filing { get; set; }
        public int? vesg { get; set; }
        public int id_way { get; set; }
        public int id_division { get; set; }
        public List<UnloadingWagons> wagons { get; set; }
    }
    public class OperationAddFilingLoading
    {
        public int id_filing { get; set; }
        public string? num_filing { get; set; }
        public int type_filing { get; set; }
        public int? vesg { get; set; }
        public int id_way { get; set; }
        public int id_division { get; set; }
        public List<LoadingWagons> wagons { get; set; }
    }
    public class OperationAddFilingCleaning
    {
        public int id_filing { get; set; }
        public int type_filing { get; set; }
        public int id_way { get; set; }
        public List<CleaningWagons> wagons { get; set; }
    }
    public class OperationAddFilingProcessing
    {
        public int id_filing { get; set; }
        public int type_filing { get; set; }
        public int id_way { get; set; }
        public List<ProcessingWagons> wagons { get; set; }
    }
    public class OperationADWagonFiling
    {
        public int id_filing { get; set; }
        public List<long> wagons { get; set; }
    }
    public class OperationUpdateFilingOperationUnloading
    {
        public int id_filing { get; set; }
        public int mode { get; set; }
        public List<UnloadingWagons> wagons { get; set; }
    }
    public class OperationUpdateFilingOperationLoading
    {
        public int id_filing { get; set; }
        public string? num_filing { get; set; }
        public int? vesg { get; set; }
        public DateTime? doc_received { get; set; }
        public int mode { get; set; }
        public List<LoadingWagons> wagons { get; set; }
    }
    public class OperationUpdateFilingOperationCleaning
    {
        public int id_filing { get; set; }
        public int mode { get; set; }
        public List<CleaningWagons> wagons { get; set; }
    }
    public class OperationUpdateFilingOperationProcessing
    {
        public int id_filing { get; set; }
        public int mode { get; set; }
        public List<ProcessingWagons> wagons { get; set; }
    }
    public class OperationUpdateFiling
    {
        public int id_filing { get; set; }
        public int mode { get; set; }
        public int id_division { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ С ВАГОНАМИ НА ПУТИ (Обновленный АРМ)
    public class OperationAutoPosition
    {
        public int id_way { get; set; }
        public int position { get; set; }
        public bool reverse { get; set; }
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
        // GET: WSD/view/open/outgoing/sostav/way/216
        [HttpGet("view/open/outgoing/sostav/way/{id_way}")]
        public async Task<ActionResult<IEnumerable<ViewOutgoingSostav>>> GetViewOpenOutgoingSostavOfIdWay(int id_way)
        {
            try
            {
                List<ViewOutgoingSostav> result = await db.getViewOutgoingSostav()
                    .AsNoTracking()
                    .Where(w => w.IdWayFrom == id_way && w.DateDepartureAmkr == null && w.Status < 3 && w.DateReadinessAmkr > new DateTime(2024, 1, 1, 0, 0, 0) && w.CountAll != w.CountReturn)
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
        /// <summary>
        /// Получить вагоны по отправленному составу по id составу
        /// </summary>
        /// <param name="id_station"></param>
        /// <returns></returns>
        // GET: WSD/view/wagon/outgoing/sostav/id/324280
        [HttpGet("view/wagon/outgoing/sostav/id/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewCarWay>>> GetViewWagonsOutgoingSostavOfIdSostav(int id_station)
        {
            try
            {
                List<ViewCarWay> result = await db.getViewWagonsOutgoingSostavOfIdSostav(id_station).ToListAsync();
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

        #region ПРИБЫВШЫЕ СОСТАВЫ (АРМ)
        // GET: WSD/view/wagon/incoming/sostav/id/346778
        [HttpGet("view/wagon/incoming/sostav/id/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewIncomingCars>>> GetViewIncomingCarsOfIdSostav(int id_station)
        {
            try
            {
                List<ViewIncomingCars> result = await db.getViewIncomingCarsOfIdSostav(id_station).ToListAsync();
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

        // GET: WSD/view/dislocation/amkr/wagon/num/56247042
        [HttpGet("view/dislocation/amkr/wagon/num/{num}")]
        public async Task<ActionResult<StatusWagonDislocation>> GetViewDislocationAMKRWagonOfNum(int num)
        {
            try
            {
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                StatusWagonDislocation? result = ids_wir.InfoViewDislocationAMKRWagonOfNum(num);
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
        public async Task<ActionResult<IEnumerable<ViewWagonsOfOuterWay>>> GetViewOpenWagonsOfOuterWaysStationOn(int id_station)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                List<ViewWagonsOfOuterWay> result = await db.getViewOpenWagonsOfOuterWaysStationOn(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                db.Database.SetCommandTimeout(0);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: WSD/view/wagons/outer_way/station_from/8
        [HttpGet("view/wagons/outer_way/station_from/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewWagonsOfOuterWay>>> GetViewOpenWagonsOfOuterWaysStationFrom(int id_station)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                List<ViewWagonsOfOuterWay> result = await db.getViewOpenWagonsOfOuterWaysStationFrom(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                db.Database.SetCommandTimeout(0);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: WSD/view/wagons/filing/period/start/2024-10-15T00:00:00/stop/2024-12-30T00:00:00/station/id/7
        [HttpGet("view/wagons/filing/period/start/{start:DateTime}/stop/{stop:DateTime}/station/id/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewWagonsFiling>>> GetViewWagonsFilingOfPeriodIdStation(DateTime start, DateTime stop, int id_station)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                List<ViewWagonsFiling> result = await db.getViewWagonsFilingOfPeriodIdStation(start, stop, id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                db.Database.SetCommandTimeout(0);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #region РАСЧЕТ ПЛАТЫ ЗА ПОЛЬЗОВАНИЕ (АРМ)

        // GET: WSD/view/calc_wagon/way/215
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

        // POST: WSD/operation/provide
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/provide")]
        public async Task<ActionResult<ResultTransfer>> PostProvideWagonsOfStationAMKR([FromBody] OperationProvideWagons value)
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
                ResultTransfer result = ids_wir.ProvideWagonsOfStationAMKR(value.id_way_from, value.id_sostav, value.wagons, value.lead_time, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/provide/datetime
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/provide/datetime")]
        public async Task<ActionResult<int>> PostDateTimeProvideWagonsOfStationAMKR([FromBody] OperationDTProvideWagons value)
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
                int result = ids_wir.DateTimeProvideWagonsOfStationAMKR(value.id_sostav, value.lead_time, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/provide/move/wagons
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/provide/move/wagons")]
        public async Task<ActionResult<ResultTransfer>> PostMoveWagonsProvideWayOfStationAMKR([FromBody] OperationMoveWagonsProvideWay value)
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
                ResultTransfer result = ids_wir.MoveWagonsProvideWayOfStationAMKR(value.id_way_on, value.nums, value.lead_time, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/sending_uz/sostav
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/sending_uz/sostav")]
        public async Task<ActionResult<ResultTransfer>> PostOperationSendingSostavOnUZ([FromBody] OperationSendingSostavOnUZ value)
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
                ResultTransfer result = ids_wir.SendingSostavOnUZ(value.id_outgoing_sostav, value.lead_time, value.composition_index, value.update_epd, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/way/auto_position
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/way/auto_position")]
        public async Task<ActionResult<int>> postAutoPosition([FromBody] OperationAutoPosition value)
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
                int result = ids_wir.AutoPosition(value.id_way, value.position, value.reverse, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region ОПЕРАЦИЯ ФОРМИРОВАНИЯ ПОДАЧ (ВЫГРУЗКА, ПОГРУЗКА...)

        // POST: WSD/add/filing/operation/unloading
        // BODY: WSD (JSON, XML)
        [HttpPost("add/filing/operation/unloading")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostAddFilingUnloading([FromBody] OperationAddFilingUnloading value)
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
                ResultUpdateIDWagon result = ids_wir.AddFiling(value.id_filing, value.num_filing, value.type_filing, value.id_way, value.id_division, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/add/filing/operation/loading
        // BODY: WSD (JSON, XML)
        [HttpPost("add/filing/operation/loading")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostAddFilingLoading([FromBody] OperationAddFilingLoading value)
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
                ResultUpdateIDWagon result = ids_wir.AddFiling(value.id_filing, value.num_filing, value.type_filing, value.id_way, value.id_division, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/add/filing/operation/cleaning
        // BODY: WSD (JSON, XML)
        [HttpPost("add/filing/operation/cleaning")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostAddFilingCleaning([FromBody] OperationAddFilingCleaning value)
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
                ResultUpdateIDWagon result = ids_wir.AddFiling(value.id_filing, null, value.type_filing, value.id_way, null, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/add/filing/operation/processing
        // BODY: WSD (JSON, XML)
        [HttpPost("add/filing/operation/processing")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostAddFilingProcessing([FromBody] OperationAddFilingProcessing value)
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
                ResultUpdateIDWagon result = ids_wir.AddFiling(value.id_filing, null, value.type_filing, value.id_way, null, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/add/wagon/filing
        // BODY: WSD (JSON, XML)
        [HttpPost("add/wagon/filing")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostAddWagonFiling([FromBody] OperationADWagonFiling value)
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
                ResultUpdateIDWagon result = ids_wir.AddWagonOfFiling(value.id_filing, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/delete/wagon/filing
        // BODY: WSD (JSON, XML)
        [HttpPost("delete/wagon/filing")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostDeleteWagonFiling([FromBody] OperationADWagonFiling value)
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
                ResultUpdateIDWagon result = ids_wir.DeleteWagonOfFiling(value.id_filing, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/update/filing/operation/unloading
        // BODY: WSD (JSON, XML)
        [HttpPost("update/filing/operation/unloading")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostUpdateFilingOperationUnloading([FromBody] OperationUpdateFilingOperationUnloading value)
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
                ResultUpdateIDWagon result = ids_wir.UpdateOperationFiling(value.id_filing, null, null, null, value.mode, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/update/filing/operation/loading
        // BODY: WSD (JSON, XML)
        [HttpPost("update/filing/operation/loading")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostUpdateFilingOperationLoading([FromBody] OperationUpdateFilingOperationLoading value)
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
                ResultUpdateIDWagon result = ids_wir.UpdateOperationFiling(value.id_filing, value.num_filing, value.vesg, value.doc_received, value.mode, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/update/filing/operation/cleaning
        // BODY: WSD (JSON, XML)
        [HttpPost("update/filing/operation/cleaning")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostUpdateFilingOperationCleaning([FromBody] OperationUpdateFilingOperationCleaning value)
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
                ResultUpdateIDWagon result = ids_wir.UpdateOperationFiling(value.id_filing, null, null, null, value.mode, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/update/filing/operation/processing
        // BODY: WSD (JSON, XML)
        [HttpPost("update/filing/operation/processing")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostUpdateFilingOperationProcessing([FromBody] OperationUpdateFilingOperationProcessing value)
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
                ResultUpdateIDWagon result = ids_wir.UpdateOperationFiling(value.id_filing, null, null, null, value.mode, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }        
        
        // POST: WSD/update/filing
        // BODY: WSD (JSON, XML)
        [HttpPost("update/filing")]
        public async Task<ActionResult<ResultUpdateIDWagon>> PostUpdateFiling([FromBody] OperationUpdateFiling value)
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
                ResultUpdateIDWagon result = ids_wir.UpdateFiling(value.id_filing, value.mode, value.id_division, user);
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

    //[Route("[controller]")]
    //[ApiController]
    //public class FileController : ControllerBase
    //{
    //    [HttpGet]
    //    [Produces("text/html")]
    //    public string Get()
    //    {
    //        string responseString = @"<html><head><title>Натурная ведомость поезда № 230</title><link rel=""stylesheet"" type=""text/css"" href=""../../Content/view/shared/print.css""></head><body class=""a4""><h2>Натурная ведомость поезда № 230</h2><table class=""table-title""><tbody><tr><th>Индекс поезда</th><td>4670-096-0010</td><th>Прибытие</th><td>2025-02-12 04:25:00</td><th>Прием</th><td>2025-02-12 05:35:00</td></tr><tr><th>Поезд прибыл на станцию</th><td>Восточная-Приемоотправочная</td><th>Путь</th><td>4 - Вытяжной </td><th>Нумерация</th><td>с головы</td></tr></tbody></table><table class=""table-info""><tbody><tr><th scope=""col"">№</th><th scope=""col"">Станция отправления</th><th scope=""col"">Груз</th><th scope=""col"">Серт. данные</th><th scope=""col"">Оператор</th><th scope=""col"">Ограничение</th><th scope=""col"">Собственник</th><th scope=""col"">Код</th><th scope=""col"">№ Вагона</th><th scope=""col"">№ ж.д. накладной</th><th scope=""col"">Вес. тн</th><th scope=""col"">Цех получатель</th><th scope=""col"">Разметка</th><th scope=""col"">Примечание</th></tr><tr><th>1</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>КТЛ</td><td></td><td>До выяснения</td><td>22</td><td><b>55224851</b></td><td>42600486</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>2</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>66033283</b></td><td>42600593</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>3</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>52176914</b></td><td>42600403</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>4</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>53187027</b></td><td>42600437</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>5</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>56098684</b></td><td>42605857</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>6</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>64527104</b></td><td>42605956</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>7</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>55552509</b></td><td>42605840</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>8</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>59673459</b></td><td>42605907</td><td><b>0.00</b></td><td>До выяснения</td><td>ст.</td><td></td></tr><tr><th>9</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>55174320</b></td><td>42606798</td><td><b>0.00</b></td><td>До выяснения</td><td>ст.</td><td></td></tr><tr><th>10</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>61318044</b></td><td>42606855</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>11</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>КТЛ</td><td></td><td>До выяснения</td><td>22</td><td><b>56332588</b></td><td>42605873</td><td><b>0.00</b></td><td>До выяснения</td><td>ст.</td><td></td></tr><tr><th>12</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>54785209</b></td><td>42605816</td><td><b>0.00</b></td><td>До выяснения</td><td>ст.</td><td></td></tr><tr><th>13</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>56138381</b></td><td>42605865</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>14</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>61632980</b></td><td>42605923</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>15</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ДЕВКАЛИОН</td><td></td><td>До выяснения</td><td>22</td><td><b>64167703</b></td><td>42605949</td><td><b>0.00</b></td><td>До выяснения</td><td>ст.</td><td></td></tr><tr><th>16</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56056013</b></td><td>42613513</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>17</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>54022611</b></td><td>42613505</td><td><b>0.00</b></td><td>До выяснения</td><td>ст.</td><td></td></tr><tr><th>18</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56178718</b></td><td>42613547</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>19</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>59014217</b></td><td>42613570</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>20</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56109341</b></td><td>42613539</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>21</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>64048606</b></td><td>42613596</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>22</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56104359</b></td><td>42613521</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>23</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>57859282</b></td><td>42613562</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>24</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>52311271</b></td><td>42613489</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>25</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56279102</b></td><td>42613554</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>26</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>53629101</b></td><td>42613497</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>27</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>62479555</b></td><td>42613588</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>28</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68018225</b></td><td>42609891</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>29</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68828474</b></td><td>42609925</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>30</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>60518453</b></td><td>42609867</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>31</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68018464</b></td><td>42609909</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>32</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>60518552</b></td><td>42609875</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>33</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56745177</b></td><td>42609859</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>34</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68021575</b></td><td>42609917</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>35</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56055338</b></td><td>42609818</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>36</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56335706</b></td><td>42609842</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>37</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68015411</b></td><td>42609883</td><td><b>0.00</b></td><td>До выяснения</td><td>сс УЗ</td><td></td></tr><tr><th>38</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56055833</b></td><td>42609826</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>39</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68829886</b></td><td>42609933</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>40</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56056500</b></td><td>42609834</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>41</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>55089676</b></td><td>42609800</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>42</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>59014282</b></td><td>42583252</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>43</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>60517752</b></td><td>42583260</td><td><b>0.00</b></td><td>До выяснения</td><td>сс УЗ</td><td></td></tr><tr><th>44</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56103948</b></td><td>42583237</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>45</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>55089585</b></td><td>42583211</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>46</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68023076</b></td><td>42583302</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>47</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68022235</b></td><td>42583294</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>48</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56279409</b></td><td>42583245</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>49</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>55089908</b></td><td>42583229</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>50</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>52311461</b></td><td>42583187</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>51</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68018159</b></td><td>42583278</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>52</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>53630919</b></td><td>42583195</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>53</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68020841</b></td><td>42583286</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>54</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>62479456</b></td><td>42583815</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>55</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>56178973</b></td><td>42583872</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>56</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>55968390</b></td><td>42583948</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th>57</th><td>Береговая</td><td>Вагоны порожние </td><td></td><td>ПГОК</td><td></td><td>До выяснения</td><td>22</td><td><b>68003367</b></td><td>42583807</td><td><b>0.00</b></td><td>До выяснения</td><td>сс.</td><td></td></tr><tr><th colspan=""6"" class=""total"">Всего вагонов</th><td class=""total"">57</td><th colspan=""3"" class=""total"">Общий вес</th><td class=""total"">0.00</td><th colspan=""3""></th></tr><tr><th colspan=""14"">Информация по операторам</th></tr><tr><th colspan=""6"" class=""total"">ТОВ «ТК«КТЛ»</th><td class=""total"">2</td><th colspan=""3""></th><td class=""total"">0.00</td><th colspan=""3""></th></tr><tr><th colspan=""6"" class=""total"">ТОВ ""ДЕВКАЛІОН ЛТД""</th><td class=""total"">13</td><th colspan=""3""></th><td class=""total"">0.00</td><th colspan=""3""></th></tr><tr><th colspan=""6"" class=""total"">Полтавский ГОК</th><td class=""total"">42</td><th colspan=""3""></th><td class=""total"">0.00</td><th colspan=""3""></th></tr></tbody></table><br><br><div>Подпись приемосдатчика ______________________</div></body></html>
    //        ";
    //        return responseString;
    //    }
    //}
}
