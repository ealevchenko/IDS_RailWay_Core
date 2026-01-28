using EF_IDS.Concrete;
using EF_IDS.Entities;
using EF_IDS.Functions;
using EFIDS.Functions;
using IDS_;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Controllers.GIVC;
using WebAPI.Repositories;
using WebAPI.Repositories.Directory;
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
    //public class OperationAddFilingProcessing
    //{
    //    public int id_filing { get; set; }
    //    public int type_filing { get; set; }
    //    public int id_way { get; set; }
    //    public List<ProcessingWagons> wagons { get; set; }
    //}
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
    //public class OperationUpdateFilingOperationProcessing
    //{
    //    public int id_filing { get; set; }
    //    public int mode { get; set; }
    //    public List<ProcessingWagons> wagons { get; set; }
    //}
    public class OperationUpdateFiling
    {
        public int id_filing { get; set; }
        public int mode { get; set; }
        public int id_division { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ С ГРУППОЙ ВАГОНОВ
    public class OperationUpdateGroupWagonNote2
    {
        public string note_2 { get; set; }
        public List<GroupWagons> wagons { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ С ВАГОНАМИ НА ПУТИ (Обновленный АРМ)
    public class OperationAutoPosition
    {
        public int id_way { get; set; }
        public int position { get; set; }
        public bool reverse { get; set; }
    }
    public class OperationManualPosition
    {
        public int id_way { get; set; }
        public List<PositionWagons> positions { get; set; }
    }
    #endregion

    #region ОПЕРАЦИЯ ИНСТРУКТИВНЫЕ ПИСЬМА
    public class StatusInstructionalLettersWagons
    {
        public List<int> nums { get; set; }
        public DateTime date_lett { get; set; }
    }
    public class OperationUpdateInstructionalLetters
    {
        public int id { get; set; }
        public string num { get; set; } = null!;
        public DateTime dt { get; set; }
        public string owner { get; set; } = null!;
        public int destination_station { get; set; }
        public string? note { get; set; }
        public List<IdNumStatusWagon> wagons { get; set; }
    }
    #endregion
    #region ОПЕРАЦИЯ ПЛАТА ПОЛЬЗОВАНИЯ
    public class OperationUpdateUsageFeePeriodDetali
    {
        public int id { get; set; }
        public int id_usage_fee_period { get; set; }
        public int? code_stn_from { get; set; }
        public int? id_cargo_group_arrival { get; set; }
        public int? id_cargo_arrival { get; set; }
        public bool? arrival_end_unload { get; set; }
        public int? code_stn_to { get; set; }
        public int? id_cargo_group_outgoing { get; set; }
        public int? id_cargo_outgoing { get; set; }
        public bool? outgoing_start_load { get; set; }
        public int? grace_time { get; set; }
        public int? id_currency { get; set; }
        public decimal? rate { get; set; }
    }
    public class OperationUpdateUsageFeePeriod
    {
        public int id { get; set; }
        public DateTime start { get; set; }
        public DateTime stop { get; set; }
        public bool hour_after_30 { get; set; }
        public int id_currency { get; set; }
        public decimal rate { get; set; }
        public int? id_currency_derailment { get; set; }
        public decimal? rate_derailment { get; set; }
        public float? coefficient_route { get; set; }
        public float? coefficient_not_route { get; set; }
        public int? grace_time_1 { get; set; }
        public int? grace_time_2 { get; set; }
        public string? note { get; set; }
        public List<ListUsageFeeEdit> list_period_edit { get; set; }

    }
    public class OperationUpdateUsageFee
    {
        public int id { get; set; }
        public int? manual_time { get; set; }
        public decimal? manual_fee_amount { get; set; }
        public string? note { get; set; }
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
    public class AdmCorrectArrivalDocument
    {
        public int num_doc { get; set; }
        public int num_nakl { get; set; }
        public List<int> nums { get; set; }
        public bool union { get; set; } = false;
        public bool create_new { get; set; } = false;
        public ArrivalCorrectDocument? correct_document { get; set; } = null;
        public List<ArrivalCorrectVagonDocument>? correct_vagons { get; set; } = null;
    }
    public class AdmDeleteWagonOfAMKR
    {
        public int num_doc { get; set; }
        public List<int> nums { get; set; }
    }

    #endregion

    //[Authorize]
    [Route("[controller]")]
    [ApiController]
    public class WSDController : ControllerBase
    {
        private EFDbContext db;
        private readonly ILogger<WSDController> _logger;
        private readonly IConfiguration _configuration;
        EventId _eventId = new EventId(0);
        EventId _eventId_ids_wir = new EventId(0);
        //string _Role_ACCEPT_RW = "";
        //string _Role_SEND_RW = "";
        //string _Role_TRF_ACCEPT_RW = "";
        //string _Role_TRF_SEND_RW = "";
        //string _Role_DOK_ACCEPT_RW = "";
        //string _Role_DOK_SEND_RW = "";
        //string _Role_DOK_RO = "";
        //string _Role_LET_WORK_RO = "";
        //public string _Role_ADMIN = "";
        //string _Role_PAY_RW = "";
        //string _Role_LETTERS = "";
        //string _Role_DIRECTORY_RW = "";
        //string _Role_ADDRESS_RW = "";
        //string _Role_COM_STAT_RW = "";
        //string _Role_COND_ARR_RW = "";
        //string _Role_COND_SEND_RW = "";

        public WSDController(EFDbContext db, ILogger<WSDController> logger, IConfiguration configuration)
        {
            this.db = db;
            _logger = logger;
            _configuration = configuration;
            _eventId_ids_wir = int.Parse(_configuration["EventID:IDS_WIR"]);
            //_Role_ACCEPT_RW = _configuration["Roles:ACCEPT_RW"];
            //_Role_SEND_RW = _configuration["Roles:SEND_RW"];
            //_Role_TRF_ACCEPT_RW = _configuration["Roles:TRF_ACCEPT_RW"];
            //_Role_TRF_SEND_RW = _configuration["Roles:TRF_SEND_RW"];
            //_Role_DOK_ACCEPT_RW = _configuration["Roles:DOK_ACCEPT_RW"];
            //_Role_DOK_SEND_RW = _configuration["Roles:DOK_SEND_RW"];
            //_Role_DOK_RO = _configuration["Roles:DOK_RO"];
            //_Role_LET_WORK_RO = _configuration["Roles:LET_WORK_RO"];
            //_Role_ADMIN = _configuration["Roles:ADMIN"];
            //_Role_PAY_RW = _configuration["Roles:PAY_RW"];
            //_Role_LETTERS = _configuration["Roles:LETTERS"];
            //_Role_DIRECTORY_RW = _configuration["Roles:DIRECTORY_RW"];
            //_Role_ADDRESS_RW = _configuration["Roles:ADDRESS_RW"];
            //_Role_COM_STAT_RW = _configuration["Roles:COM_STAT_RW"];
            //_Role_COND_ARR_RW = _configuration["Roles:COND_ARR_RW"];
            //_Role_COND_SEND_RW = _configuration["Roles:COND_SEND_RW"];
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

        // GET: WSD/view/vagons/balance
        [HttpGet("view/vagons/balance")]
        public async Task<ActionResult> GetViewWagonsOfBalance()
        {
            try
            {
                var result = await db.WagonInternalMovements
                         //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                         .AsNoTracking()
                         .Where(x => x.IdStation != 10 && (x.IdOuterWay == null && x.WayEnd == null) || (x.IdOuterWay != null && x.OuterWayStart != null && x.OuterWayEnd == null))
                         .Select(d => new
                         {
                             Id = d.Id,
                             IdWir = d.IdWagonInternalRoutes,
                             Num = d.IdWagonInternalRoutesNavigation.Num,
                             CurrentIdStation = d.IdStation,
                             CurrentStationNameRu = d.IdStationNavigation.StationNameRu,
                             CurrentStationNameEn = d.IdStationNavigation.StationNameEn,
                             CurrentTypeWay = d.IdOuterWay != null ? "Перегон" : "Путь станции",
                             CurrentWayNameRu = d.IdOuterWay != null ? d.IdOuterWayNavigation.NameOuterWayRu : d.IdWayNavigation.WayNumRu + "-" + d.IdWayNavigation.WayAbbrRu,
                             CurrentWayNameEn = d.IdOuterWay != null ? d.IdOuterWayNavigation.NameOuterWayEn : d.IdWayNavigation.WayNumEn + "-" + d.IdWayNavigation.WayAbbrEn,
                             Wagon = db.DirectoryWagonsRents
                                .AsNoTracking()
                                .Where(r => r.Num == d.IdWagonInternalRoutesNavigation.Num && r.RentEnd == null)
                                .Select(o => new
                                {

                                    IdRent = o.Id,

                                    IdOperator = o.IdOperator,
                                    OperatorAbbrRu = o.IdOperatorNavigation != null ? o.IdOperatorNavigation.AbbrRu : null,
                                    OperatorAbbrEn = o.IdOperatorNavigation != null ? o.IdOperatorNavigation.AbbrEn : null,
                                    Group = db.DirectoryOperatorsWagonsGroups.AsNoTracking().Where(g => g.IdOperator == o.IdOperator).FirstOrDefault() != null ? db.DirectoryOperatorsWagonsGroups.AsNoTracking().Where(g => g.IdOperator == o.IdOperator).FirstOrDefault().Group : null,

                                    IdLimiting = o.IdLimiting,
                                    LimitingAbbrRu = o.IdLimitingNavigation != null ? o.IdLimitingNavigation.LimitingAbbrRu : null,
                                    LimitingAbbrEn = o.IdLimitingNavigation != null ? o.IdLimitingNavigation.LimitingAbbrEn : null,
                                    Paid = (bool?)(o.IdOperatorNavigation != null ? o.IdOperatorNavigation.Paid : null),

                                    IdGenus = (int?)(o.NumNavigation != null ? o.NumNavigation.IdGenus : null),
                                    GenusAbbrRu = (o.NumNavigation != null && o.NumNavigation.IdGenusNavigation != null ? o.NumNavigation.IdGenusNavigation.AbbrRu : null),
                                    GenusAbbrEn = (o.NumNavigation != null && o.NumNavigation.IdGenusNavigation != null ? o.NumNavigation.IdGenusNavigation.AbbrEn : null),

                                    IdTypeOwnership = (int?)(o.NumNavigation != null ? o.NumNavigation.IdTypeOwnership : null),
                                    TypeOwnershipRu = (o.NumNavigation != null && o.NumNavigation.IdTypeOwnershipNavigation != null ? o.NumNavigation.IdTypeOwnershipNavigation.TypeOwnershipRu : null),
                                    TypeOwnershipEn = (o.NumNavigation != null && o.NumNavigation.IdTypeOwnershipNavigation != null ? o.NumNavigation.IdTypeOwnershipNavigation.TypeOwnershipEn : null),

                                    IdCountrys = (int?)(o.NumNavigation != null ? o.NumNavigation.IdCountrys : null),
                                    CountrysCodeSng = o.NumNavigation != null && o.NumNavigation.IdCountrysNavigation != null ? o.NumNavigation.IdCountrysNavigation.CodeSng : null,
                                    CountryAbbrRu = o.NumNavigation != null && o.NumNavigation.IdCountrysNavigation != null ? o.NumNavigation.IdCountrysNavigation.CountryAbbrRu : null,
                                    CountryAbbrEn = o.NumNavigation != null && o.NumNavigation.IdCountrysNavigation != null ? o.NumNavigation.IdCountrysNavigation.CountryAbbrEn : null,
                                })
                                .FirstOrDefault(),
                             Arrival = db.ArrivalCars
                             .Where(a => a.Id == d.IdWagonInternalRoutesNavigation.IdArrivalCar)
                             .Select(ac => new
                             {
                                 IdArrivalCar = ac.Id,
                                 IdArrivalSostav = ac.IdArrivalNavigation.Id,
                                 DateAdoption = ac.IdArrivalNavigation.DateAdoption,
                                 DateAdoptionAct = ac.IdArrivalNavigation.DateAdoptionAct,

                                 //IdArrivalUzVagon = ac.IdArrivalUzVagonNavigation.Id,
                                 ConditionAbbrRu = ac.IdArrivalUzVagonNavigation != null && ac.IdArrivalUzVagonNavigation.IdConditionNavigation != null ? ac.IdArrivalUzVagonNavigation.IdConditionNavigation.ConditionAbbrRu : null,
                                 ConditionAbbrEn = ac.IdArrivalUzVagonNavigation != null && ac.IdArrivalUzVagonNavigation.IdConditionNavigation != null ? ac.IdArrivalUzVagonNavigation.IdConditionNavigation.ConditionAbbrEn : null,

                                 CargoNameRu = ac.IdArrivalUzVagonNavigation.IdCargoNavigation != null ? ac.IdArrivalUzVagonNavigation.IdCargoNavigation.CargoNameRu : null,
                                 CargoNameEn = ac.IdArrivalUzVagonNavigation.IdCargoNavigation != null ? ac.IdArrivalUzVagonNavigation.IdCargoNavigation.CargoNameEn : null,

                                 //IdArrivalUZDocument = ac.IdArrivalUzVagonNavigation.IdDocumentNavigation.Id,
                                 NomMainDoc = ac.IdArrivalUzVagonNavigation.IdDocumentNavigation.NomMainDoc,
                                 NomDoc = ac.IdArrivalUzVagonNavigation.IdDocumentNavigation.NomDoc,

                             })
                             .FirstOrDefault(),
                         })
                         .ToListAsync();

                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: WSD/view/vagons/remainder
        [HttpGet("view/vagons/remainder")]
        public async Task<ActionResult> GetViewRemainderWagons()
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                List<ViewOperatingBalanceRwCar> result = await db.getViewRemainderWagons().ToListAsync();
                db.Database.SetCommandTimeout(0);
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

        // Расчет платы за пользование по оперативномку остатку
        // GET: WSD/view/calc_wagon/balance
        [HttpGet("view/calc_wagon/balance")]
        public async Task<ActionResult> getCalcUsageFeeOfBalance()
        {
            try
            {
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                List<CalcWagonUsageFee> results = new List<CalcWagonUsageFee>();
                var result = await db.WagonInternalMovements
                         .AsNoTracking()
                         .Where(x => x.IdStation != 10 && (x.IdOuterWay == null && x.WayEnd == null) || (x.IdOuterWay != null && x.OuterWayStart != null && x.OuterWayEnd == null))
                         .Select(d => new
                         {
                             Id = d.IdWagonInternalRoutes,
                             Num = d.IdWagonInternalRoutesNavigation.Num,
                             //Paid = db.DirectoryWagonsRents.AsNoTracking().Where(r => r.Num == d.IdWagonInternalRoutesNavigation.Num && r.RentEnd == null).First().IdOperatorNavigation.Paid
                             //Paid = db.DirectoryWagonsRents.AsNoTracking().Where(r => r.Num == d.IdWagonInternalRoutesNavigation.Num && r.RentEnd == null).FirstOrDefault() != null ? db.DirectoryWagonsRents.AsNoTracking().Where(r => r.Num == d.IdWagonInternalRoutesNavigation.Num && r.RentEnd == null).FirstOrDefault().IdOperatorNavigation.Paid : false
                             DirectoryWagonsRent = (DirectoryWagonsRent?)db.DirectoryWagonsRents
                             .AsNoTracking()
                             .Include(op => op.IdOperatorNavigation)
                             .Where(r => r.Num == d.IdWagonInternalRoutesNavigation.Num && r.RentEnd == null)
                             .FirstOrDefault()
                         })
                         .ToListAsync();
                foreach (var wir in result.Where(w => w.DirectoryWagonsRent != null && w.DirectoryWagonsRent.IdOperatorNavigation != null && w.DirectoryWagonsRent.IdOperatorNavigation.Paid == true))
                {
                    results.Add(ids_wir.CalcUsageFeeOfWIR(wir.Id));
                }

                if (results == null) return NotFound();
                return new ObjectResult(results);
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

        // POST: WSD/operation/way/manual_position
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/way/manual_position")]
        public async Task<ActionResult<int>> postManualPosition([FromBody] OperationManualPosition value)
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
                int result = ids_wir.ManualPosition(value.id_way, value.positions, user);
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

        //// POST: WSD/add/filing/operation/processing
        //// BODY: WSD (JSON, XML)
        //[HttpPost("add/filing/operation/processing")]
        //public async Task<ActionResult<ResultUpdateIDWagon>> PostAddFilingProcessing([FromBody] OperationAddFilingProcessing value)
        //{
        //    try
        //    {
        //        string user = HttpContext.User.Identity.Name;
        //        bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

        //        if (value == null || !IsAuthenticated)
        //        {
        //            return BadRequest();
        //        }
        //        IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
        //        ResultUpdateIDWagon result = ids_wir.AddFiling(value.id_filing, null, value.type_filing, value.id_way, null, value.wagons, user);
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

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

        //// POST: WSD/update/filing/operation/processing
        //// BODY: WSD (JSON, XML)
        //[HttpPost("update/filing/operation/processing")]
        //public async Task<ActionResult<ResultUpdateIDWagon>> PostUpdateFilingOperationProcessing([FromBody] OperationUpdateFilingOperationProcessing value)
        //{
        //    try
        //    {
        //        string user = HttpContext.User.Identity.Name;
        //        bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

        //        if (value == null || !IsAuthenticated)
        //        {
        //            return BadRequest();
        //        }
        //        IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
        //        ResultUpdateIDWagon result = ids_wir.UpdateOperationFiling(value.id_filing, null, null, null, value.mode, value.wagons, user);
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}        

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

        #region ОПЕРАЦИЯ С ГРУППОЙ ВАГОНОВ
        // POST: WSD/view/wagon/nums [61236972;63679914;62853650;64053002;64175037;4772;4838;56968837;62976337;58583949;68026632]
        [HttpPost("view/wagon/nums")]
        public async Task<ActionResult<IEnumerable<ViewCarsGroup>>> postViewWagonsOfListNums([FromBody] List<int> nums)
        {
            try
            {
                string s_nums = "";
                foreach (int num in nums.ToList())
                {
                    s_nums += num.ToString() + ";";
                }
                s_nums = s_nums.TrimEnd(';');
                List<ViewCarsGroup> result = await db.getViewWagonsOfListNums(s_nums).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/update/group_wagon/note2
        // BODY: WSD (JSON, XML)
        [HttpPost("update/group_wagon/note2")]
        public async Task<ActionResult<ResultUpdateWagon>> UpdateNote2WagonsGroup([FromBody] OperationUpdateGroupWagonNote2 value)
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
                ResultUpdateWagon result = ids_wir.UpdateNoteGroupWagon(value.note_2, value.wagons, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #region СЕРВИСЫ
        // GET: WSD/view/arrival/documents/vagons/period/start/2025-04-01T00:00:00/stop/2025-04-30T00:00:00
        [HttpGet("view/arrival/documents/vagons/period/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult<IEnumerable<ViewArrivalDocumentsVagons>>> GetViewArrivalDocumentsVagonsOfPeriod(DateTime start, DateTime stop)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                List<ViewArrivalDocumentsVagons> result = await db.getViewArrivalDocumentsVagonsOfPeriod(start, stop).ToListAsync();
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


        #region ИНСТРУКТИВНЫЕ ПИСЬМА
        // GET: WSD/view/instructional_letters/list/period/start/2023-07-01T00:00:00/stop/2025-07-31T00:00:00
        [HttpGet("view/instructional_letters/list/period/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult> GetViewInstructionalLettersOfPeriod(DateTime start, DateTime stop)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                var result = await db.InstructionalLetters
                        .AsNoTracking()
                        .Where(x => x.Dt >= start && x.Dt <= stop)
                         .Select(l => new
                         {
                             l.Id,
                             l.Num,
                             l.Dt,
                             l.Owner,
                             l.DestinationStation,
                             StationNameRu = db.DirectoryExternalStations.Where(s => s.Code == l.DestinationStation).FirstOrDefault() != null ? db.DirectoryExternalStations.Where(s => s.Code == l.DestinationStation).FirstOrDefault().StationNameRu : null,
                             StationNameEn = db.DirectoryExternalStations.Where(s => s.Code == l.DestinationStation).FirstOrDefault() != null ? db.DirectoryExternalStations.Where(s => s.Code == l.DestinationStation).FirstOrDefault().StationNameEn : null,
                             l.Note,
                             l.Create,
                             l.CreateUser,
                             l.Change,
                             l.ChangeUser,
                             InstructionalLettersWagons = l.InstructionalLettersWagons.Select(
                                 w => new
                                 {
                                     w.Id,
                                     w.Num,
                                     w.Note,
                                     w.Status,
                                     w.Close,
                                     w.IdWir,
                                     WirNote = w.IdWirNavigation.Note,
                                     WirNote2 = w.IdWirNavigation.Note2,
                                     w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalNavigation.DateAdoption,
                                     w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalNavigation.DateAdoptionAct,
                                     ArrivalOperatorRu = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalUzVagonNavigation.IdWagonsRentArrivalNavigation.IdOperatorNavigation.OperatorsRu,
                                     ArrivalOperatorEn = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalUzVagonNavigation.IdWagonsRentArrivalNavigation.IdOperatorNavigation.OperatorsEn,
                                     ArrivalOperatorAbbrRu = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalUzVagonNavigation.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrRu,
                                     ArrivalOperatorAbbrEn = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalUzVagonNavigation.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrEn,
                                     w.IdWirNavigation.IdOutgoingCarNavigation.IdOutgoingNavigation.DateOutgoing,
                                     w.IdWirNavigation.IdOutgoingCarNavigation.IdOutgoingNavigation.DateOutgoingAct,
                                     w.IdWirNavigation.IdOutgoingCarNavigation.IdOutgoingNavigation.DateDepartureAmkr,
                                     w.Create,
                                     w.CreateUser,
                                     w.Change,
                                     w.ChangeUser,
                                     RentOperatorAbbrRu = db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= l.Dt && (r.RentEnd >= l.Dt || r.RentEnd == null)) != null ? db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= l.Dt && (r.RentEnd >= l.Dt || r.RentEnd == null)).First().IdOperatorNavigation.AbbrRu : null,
                                     RentOperatorAbbrEn = db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= l.Dt && (r.RentEnd >= l.Dt || r.RentEnd == null)) != null ? db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= l.Dt && (r.RentEnd >= l.Dt || r.RentEnd == null)).First().IdOperatorNavigation.AbbrEn : null,

                                 }),
                         })
                        .ToListAsync();
                db.Database.SetCommandTimeout(0);
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: WSD/view/instructional_letters_wagons/list/in_progress
        [HttpGet("view/instructional_letters_wagons/list/in_progress")]
        public async Task<ActionResult> GetViewInstructionalLettersWagonsInProgress()
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                var result = await db.InstructionalLettersWagons
                        .AsNoTracking()
                        .Where(x => x.Status < 2 || x.Status == 5)
                         .Select(w => new
                         {
                             w.Id,
                             IdInstructionalLetters = w.IdInstructionalLettersNavigation.Id,
                             InstructionalLettersNum = w.IdInstructionalLettersNavigation.Num,
                             InstructionalLettersDatetime = w.IdInstructionalLettersNavigation.Dt,
                             InstructionalLettersOwner = w.IdInstructionalLettersNavigation.Owner,
                             InstructionalLettersStationCode = w.IdInstructionalLettersNavigation.DestinationStation,
                             InstructionalLettersStationNameRu = db.DirectoryExternalStations.Where(s => s.Code == w.IdInstructionalLettersNavigation.DestinationStation).FirstOrDefault() != null ? db.DirectoryExternalStations.Where(s => s.Code == w.IdInstructionalLettersNavigation.DestinationStation).FirstOrDefault().StationNameRu : null,
                             InstructionalLettersStationNameEn = db.DirectoryExternalStations.Where(s => s.Code == w.IdInstructionalLettersNavigation.DestinationStation).FirstOrDefault() != null ? db.DirectoryExternalStations.Where(s => s.Code == w.IdInstructionalLettersNavigation.DestinationStation).FirstOrDefault().StationNameEn : null,
                             InstructionalLettersNote = w.IdInstructionalLettersNavigation.Note,
                             InstructionalLettersCreate = w.IdInstructionalLettersNavigation.Create,
                             InstructionalLettersCreateUser = w.IdInstructionalLettersNavigation.CreateUser,
                             InstructionalLettersChange = w.IdInstructionalLettersNavigation.Change,
                             InstructionalLettersChangeUser = w.IdInstructionalLettersNavigation.ChangeUser,
                             InstructionalLettersWagonsIdWir = w.IdWir,
                             InstructionalLettersWagonsNum = w.Num,
                             InstructionalLettersWagonsNote = w.Note,
                             InstructionalLettersWagonsStatus = w.Status,
                             InstructionalLettersWagonsClose = w.Close,
                             InstructionalLettersWagonsWirNote = w.IdWirNavigation.Note,
                             InstructionalLettersWagonsWirNote2 = w.IdWirNavigation.Note2,
                             InstructionalLettersWagonsDateAdoption = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalNavigation.DateAdoption,
                             InstructionalLettersWagonsDateAdoptionAct = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalNavigation.DateAdoptionAct,
                             InstructionalLettersWagonsOperatorsAbbrRu = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalUzVagonNavigation.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrRu,
                             InstructionalLettersWagonsOperatorsAbbrEn = w.IdWirNavigation.IdArrivalCarNavigation.IdArrivalUzVagonNavigation.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrEn,
                             InstructionalLettersWagonsRentOperatorAbbrRu = db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= w.IdInstructionalLettersNavigation.Dt && (r.RentEnd >= w.IdInstructionalLettersNavigation.Dt || r.RentEnd == null)) != null ? db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= w.IdInstructionalLettersNavigation.Dt && (r.RentEnd >= w.IdInstructionalLettersNavigation.Dt || r.RentEnd == null)).First().IdOperatorNavigation.AbbrRu : null,
                             InstructionalLettersWagonsRentOperatorAbbrEn = db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= w.IdInstructionalLettersNavigation.Dt && (r.RentEnd >= w.IdInstructionalLettersNavigation.Dt || r.RentEnd == null)) != null ? db.DirectoryWagonsRents.Where(r => r.Num == w.Num && r.RentStart <= w.IdInstructionalLettersNavigation.Dt && (r.RentEnd >= w.IdInstructionalLettersNavigation.Dt || r.RentEnd == null)).First().IdOperatorNavigation.AbbrEn : null,
                             InstructionalLettersWagonsDateOutgoing = w.IdWirNavigation.IdOutgoingCarNavigation.IdOutgoingNavigation.DateOutgoing,
                             InstructionalLettersWagonsDateOutgoingAct = w.IdWirNavigation.IdOutgoingCarNavigation.IdOutgoingNavigation.DateOutgoingAct,
                             InstructionalLettersWagonsDateDepartureAmkr = w.IdWirNavigation.IdOutgoingCarNavigation.IdOutgoingNavigation.DateDepartureAmkr,
                             InstructionalLettersWagonsCreate = w.Create,
                             InstructionalLettersWagonsCreateUser = w.CreateUser,
                             InstructionalLettersWagonsChange = w.Change,
                             InstructionalLettersWagonsChangeUser = w.ChangeUser,

                         })
                        .ToListAsync();
                db.Database.SetCommandTimeout(0);
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Получить статус вагонов в письме
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: WSD/view/instructional_letters_wagons
        // BODY: WSD (JSON, XML)
        [HttpPost("view/instructional_letters_wagons")]
        public async Task<ActionResult<IEnumerable<StatusInstructionalLettersWagon>>> PostStatusInstructionalLettersWagons([FromBody] StatusInstructionalLettersWagons value)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

                if (value == null || !IsAuthenticated)
                {
                    return BadRequest();
                }
                //if (user == "EUROPE\\ealevchenko" || user == "EUROPE\\lvgubarenko")
                //{
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                List<StatusInstructionalLettersWagon> result = ids_wir.GetStatusInstructionalLetterWagons(value.nums, value.date_lett);
                return Ok(result);
                //}
                //else
                //{
                //    return BadRequest();
                //}

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/instructional_letters/update
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/instructional_letters/update")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_LETTERS")]
        public async Task<ActionResult<ResultUpdateWagon>> PostUpdateInstructionalLetters([FromBody] OperationUpdateInstructionalLetters value)
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
                ResultUpdateWagon result = ids_wir.UpdateInstructionalLetters(value.id, value.num, value.dt, value.owner, value.destination_station, value.note, value.wagons, user);
                //ResultTransfer result = new ResultTransfer(value.wagons.Count());
                //foreach (UpdateInstructionalLettersWagons wag in value.wagons)
                //{
                //    result.SetMovedResult(-1, wag.num);
                //}
                //result.result = -1;
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/open_instructional_letters/update
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/open_instructional_letters/update")]
        //[Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_LETTERS")]
        public async Task<ActionResult<OperationResultID>> PostUpdateOpenInstructionalLetters([FromBody] List<int> value)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                OperationResultID result = ids_wir.UpdateOpenInstructionalLetter(user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE WSD/operation/instructional_letters/delete/1
        [HttpDelete("operation/instructional_letters/delete/{id}")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_LETTERS")]
        public async Task<ActionResult<ResultUpdateWagon>> DeleteInstructionalLetters(int id)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                if (!IsAuthenticated) { return BadRequest(); }
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                ResultUpdateWagon result = ids_wir.DeleteInstructionalLetters(id, user);
                return result;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion

        #region ПЛАТА ЗА ПОЛЬЗОВАНИЕ
        /// <summary>
        /// Получить расчитаные платы за пользование по номеру вагона
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        // GET: WSD/view/usage_fee/wagon/56291040
        [HttpGet("view/usage_fee/wagon/{num}")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<IEnumerable<ViewUsageFeeWagon>>> GetViewUsageFeeWagonOfNum(int num)
        {
            try
            {
                List<ViewUsageFeeWagon> result = await db.getViewUsageFeeWagonOfNum(num).ToListAsync(); ;
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
        /// Получить список периодов по оператору и типу вагона
        /// </summary>
        /// <param name="id_operator"></param>
        /// <param name="id_genus"></param>
        /// <returns></returns>
        // GET: WSD/view/usage_fee_period/operator/3/genus/22
        [HttpGet("view/usage_fee_period/operator/{id_operator}/genus/{id_genus}")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<IEnumerable<ViewUsageFeePeriod>>> GetViewUsageFeePeriodOfOperatorGenus(int id_operator, int id_genus)
        {
            try
            {
                List<ViewUsageFeePeriod> result = await db.getViewUsageFeePeriodOfOperatorGenus(id_operator, id_genus).ToListAsync(); ;
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
        /// Получить список дополнительных условий для указанного периода
        /// </summary>
        /// <param name="id_usage_fee_period"></param>
        /// <returns></returns>
        // GET: WSD/view/usage_fee_period_detali/id_usage_fee_period/2438
        [HttpGet("view/usage_fee_period_detali/id_usage_fee_period/{id_usage_fee_period}")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<IEnumerable<ViewUsageFeePeriodDetali>>> getViewUsageFeePeriodDetaliOfIdPeriod(int id_usage_fee_period)
        {
            try
            {

                List<ViewUsageFeePeriodDetali> result = await db.getViewUsageFeePeriodDetaliOfIdPeriod(id_usage_fee_period).ToListAsync(); ;
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
        /// Обновить строку платы за пользование по вагону
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: WSD/operation/usage_fee/update
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/usage_fee/update")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<int>> PostUpdateUsageFee([FromBody] OperationUpdateUsageFee value)
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
                int result = ids_wir.UpdateUpdateUsageFee(value.id, value.manual_time, value.manual_fee_amount, value.note, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // POST: WSD/operation/usage_fee_period/update
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/usage_fee_period/update")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<OperationResultID>> PostUpdateUsageFeePeriod([FromBody] OperationUpdateUsageFeePeriod value)
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
                OperationResultID result = ids_wir.UpdateUpdateUsageFeePeriod(value.id, value.start, value.stop, value.hour_after_30, value.id_currency, value.rate, value.id_currency_derailment,
             value.rate_derailment, value.coefficient_route, value.coefficient_not_route, value.grace_time_1, value.grace_time_2, value.note, value.list_period_edit, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/usage_fee_period/delete
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/usage_fee_period/delete")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<OperationResultID>> PostUpdateUsageFeePeriod([FromBody] List<int> value)
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
                OperationResultID result = ids_wir.DeleteUpdateUsageFeePeriod(value, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: WSD/operation/usage_fee_period_detali/update
        // BODY: WSD (JSON, XML)
        [HttpPost("operation/usage_fee_period_detali/update")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<int>> PostUpdateUsageFeePeriodDetali([FromBody] OperationUpdateUsageFeePeriodDetali value)
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
                int result = ids_wir.UpdateUpdateUsageFeePeriodDetali(value.id, value.id_usage_fee_period, value.code_stn_from, value.id_cargo_group_arrival, value.id_cargo_arrival, value.code_stn_to, value.id_cargo_group_outgoing, value.id_cargo_outgoing, value.grace_time, value.id_currency, value.rate, value.arrival_end_unload, value.outgoing_start_load, user);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE WSD/operation/instructional_letters/delete/1
        [HttpDelete("operation/usage_fee_period_detali/delete/{id}")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_PAY")]
        public async Task<ActionResult<int>> DeleteUsageFeePeriodDetali(int id)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                if (!IsAuthenticated) { return BadRequest(); }
                IDS_WIR ids_wir = new IDS_WIR(_logger, _configuration, _eventId_ids_wir);
                int result = ids_wir.DeleteUsageFeePeriodDetali(id, user);
                return result;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        #endregion

        #region АДМИНИСТРИРОВАНИЕ
        // POST: WSD/admin/change/division/outgoing
        // BODY: WSD (JSON, XML)
        [HttpPost("admin/change/division/outgoing")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN")]
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
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN")]
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

        // POST: WSD/admin/change/vesg/outgoing
        // BODY: WSD (JSON, XML)
        [HttpPost("admin/change/correct/arrival/document")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN")]
        public async Task<ActionResult<ResultTransfer>> PostAdmCorrectArrivalDocument([FromBody] AdmCorrectArrivalDocument value)
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
                    ResultCorrect result = ids_wir.CorrectArrivalDocument(value.num_doc, value.num_nakl, value.union, value.create_new, value.nums, value.correct_document, value.correct_vagons);
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

        // POST: WSD/admin/change/delete/car/amkr
        // BODY: WSD (JSON, XML)
        [HttpPost("admin/change/delete/cars/amkr")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN")]
        public async Task<ActionResult<ResultTransfer>> PostAdmDeleteWagonOfAMKR([FromBody] AdmDeleteWagonOfAMKR value)
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
                    ResultCorrect result = ids_wir.DeleteWagonOfAMKR(value.num_doc, value.nums);
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
