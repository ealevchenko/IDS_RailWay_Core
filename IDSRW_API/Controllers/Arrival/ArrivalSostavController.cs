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
    public class ArrivalSostavController : ControllerBase
    {
        //private ILongRepository<ArrivalSostav> repo;
        private EFDbContext db;

        // конструктор вводит зарегистрированный репозиторий
        //public ArrivalSostavController(ILongRepository<ArrivalSostav> repo)
        //{
        //    this.repo = repo;
        //}
        public ArrivalSostavController(EFDbContext db)
        {
            this.db = db;
        }

        // GET: ArrivalSostav
        [HttpGet]
        public async Task<ActionResult<ArrivalSostav>> GetArrivalSostav()
        {
            try
            {
                IEnumerable<ArrivalSostav> result = await db.ArrivalSostavs.AsNoTracking().ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Отчеты. Получить черновик документа на состав по прибытию
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: ArrivalSostav/document/draft/id/398431
        [HttpGet("document/draft/id/{id}")]
        public async Task<ActionResult<ArrivalSostav>> GetDocumentDraftArrivalSostavOfId(int id)
        {
            try
            {
                var result = await db.ArrivalSostavs
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Select(d => new
                    {
                        Id = d.Id,
                        CompositionIndex = d.CompositionIndex,
                        IdStationFrom = d.IdStationFromNavigation.Id,
                        StationFromNameRu = d.IdStationFromNavigation.StationNameRu,
                        StationFromNameEn = d.IdStationFromNavigation.StationNameEn,
                        IdStationOn = d.IdStationOnNavigation.Id,
                        StationOnNameRu = d.IdStationOnNavigation.StationNameRu,
                        StationOnNameEn = d.IdStationOnNavigation.StationNameEn,
                        IdWayOn = d.IdWayNavigation.Id,
                        WayOnNameRu = d.IdWayNavigation.WayNumRu + "-"+ d.IdWayNavigation.WayAbbrRu,
                        WayOnNameEn = d.IdWayNavigation.WayNumEn + "-"+ d.IdWayNavigation.WayAbbrEn,
                        Numeration = d.Numeration,
                        Train = d.Train,
                        DateArrival = d.DateArrival,
                        DateAdoption = d.DateAdoption,
                        DateAdoptionAct = d.DateAdoptionAct,
                        Status = d.Status,
                        Create = d.Create,
                        CreateUser = d.CreateUser,
                        ArrivalCars = d.ArrivalCars.Select(w => new
                        {
                            Id = w.Id,
                            Num = w.Num,
                            Position = w.Position,
                            PositionArrival = w.PositionArrival,
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

        //// GET: api/arrivalsostav/[id]
        //[HttpGet("{id}", Name = "GetArrivalSostav")]
        //public async Task<IActionResult> GetArrivalSostav(long id)
        //{
        //    ArrivalSostav c = await repo.RetrieveAsync(id);
        //    if (c == null)
        //    {
        //        return NotFound(); // 404 Resource not found
        //    }
        //    return new ObjectResult(c); // 200 OK
        //}

        //// POST: api/arrivalsostav
        //// BODY: ArrivalSostav (JSON, XML)
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ArrivalSostav c)
        //{
        //    if (c == null)
        //    {
        //        return BadRequest(); // 400 Bad request
        //    }
        //    ArrivalSostav added = await repo.CreateAsync(c);
        //    return CreatedAtRoute("GetArrivalSostav", new { id = added.Id }, c); // 201 Created
        //}

        //// PUT: api/arrivalsostav/[id]
        //// BODY: ArrivalSostav (JSON, XML)
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(long id, [FromBody] ArrivalSostav c)
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

        //// DELETE: api/arrivalsostav/[id]
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(long id)
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
