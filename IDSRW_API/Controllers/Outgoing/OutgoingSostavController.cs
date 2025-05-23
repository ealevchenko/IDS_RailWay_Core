using EF_IDS.Concrete;
using EF_IDS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;
using WebAPI.Repositories.Arrival;

namespace WebAPI.Controllers.Arrival
{
    [Route("[controller]")]
    [ApiController]
    public class OutgoingSostavController : ControllerBase
    {
        private EFDbContext db;
        public OutgoingSostavController(EFDbContext db)
        {
            this.db = db;
        }

        // GET: OutgoingSostav
        [HttpGet]
        public async Task<ActionResult<OutgoingSostav>> GetArrivalSostav()
        {
            try
            {
                IEnumerable<OutgoingSostav> result = await db.OutgoingSostavs.AsNoTracking().ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Отчеты.  Gjkexbnm в "Отчетной документации" -  "Реєстр передач документів"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: OutgoingSostav/register/document/transfer/id/396422
        [HttpGet("register/document/transfer/id/{id}")]
        public async Task<ActionResult> GetRegisterDocumentTransferOutgoingSostavOfId(int id)
        {
            try
            {
                var result = await db.OutgoingSostavs
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(d => new
                    {
                        Id = d.Id,
                        CompositionIndex = d.CompositionIndex,
                        IdStationFrom = d.IdStationFromNavigation.Id,
                        StationFromNameRu = d.IdStationFromNavigation.StationNameRu,
                        StationFromNameEn = d.IdStationFromNavigation.StationNameEn,
                        IdStationOn = d.IdStationOnNavigation != null ? (int?)d.IdStationOnNavigation.Id : null,
                        StationOnNameRu = d.IdStationOnNavigation != null ? d.IdStationOnNavigation.StationNameRu : null,
                        StationOnNameEn = d.IdStationOnNavigation != null ? d.IdStationOnNavigation.StationNameEn : null,
                        //IdWayOn = d.IdWayNavigation != null ? (int?)d.IdWayNavigation.Id : null,
                        //WayOnNameRu = d.IdWayNavigation != null ?  d.IdWayNavigation.WayNumRu + "-" + d.IdWayNavigation.WayAbbrRu : null,
                        //WayOnNameEn = d.IdWayNavigation != null ?  d.IdWayNavigation.WayNumEn + "-" + d.IdWayNavigation.WayAbbrEn : null,
                        DateReadinessUz = d.DateReadinessUz,
                        DateReadinessAmkr = d.DateReadinessAmkr,
                        DateOutgoing = d.DateOutgoing,
                        DateOutgoingAct = d.DateOutgoingAct,
                        DateDepartureAmkr = d.DateDepartureAmkr,
                        Status = d.Status,
                        Create = d.Create,
                        CreateUser = d.CreateUser,
                        OutgoingCars = d.OutgoingCars.Select(w => new
                        {
                            Id = w.Id,
                            Num = w.Num,
                            Position = w.Position,
                            PositionOutgoing = w.PositionOutgoing,
                            NumDoc = w.NumDocNavigation != null ? w.NumDocNavigation.NumUz : null,
                            WagonsRent = db.DirectoryWagonsRents
                                .AsNoTracking()
                                .Where(r => r.Num == w.Num && r.RentEnd == null)
                                .Select(o => new
                                {
                                    Id = o.Id,
                                    IdOperator = o.IdOperator,
                                    OperatorAbbrRu = o.IdOperatorNavigation != null ? o.IdOperatorNavigation.AbbrRu : null,
                                    OperatorAbbrEn = o.IdOperatorNavigation != null ? o.IdOperatorNavigation.AbbrEn : null,
                                    CountrysCodeSng = o.NumNavigation != null && o.NumNavigation.IdCountrysNavigation != null ? o.NumNavigation.IdCountrysNavigation.CodeSng : null,
                                })
                                .FirstOrDefault()
                        })
                    })
                    .FirstOrDefaultAsync();
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
