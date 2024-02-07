using EF_IDS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories.Arrival;

namespace WebAPI.Controllers.Arrival
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalSostavController : ControllerBase
    {
        private ILongRepository<ArrivalSostav> repo;

        // конструктор вводит зарегистрированный репозиторий
        public ArrivalSostavController(ILongRepository<ArrivalSostav> repo)
        {
            this.repo = repo;
        }

        // GET: api/arrivalsostav
        [HttpGet]
        public async Task<IEnumerable<ArrivalSostav>> GetArrivalSostav()
        {
            return await repo.RetrieveAllAsync();
        }

        // GET: api/arrivalsostav/[id]
        [HttpGet("{id}", Name = "GetArrivalSostav")]
        public async Task<IActionResult> GetArrivalSostav(long id)
        {
            ArrivalSostav c = await repo.RetrieveAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(c); // 200 OK
        }

        // POST: api/arrivalsostav
        // BODY: ArrivalSostav (JSON, XML)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArrivalSostav c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            ArrivalSostav added = await repo.CreateAsync(c);
            return CreatedAtRoute("GetArrivalSostav", new { id = added.Id }, c); // 201 Created
        }

        // PUT: api/arrivalsostav/[id]
        // BODY: ArrivalSostav (JSON, XML)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ArrivalSostav c)
        {
            if (c == null || c.Id != id)
            {
                return BadRequest(); // 400 Bad request
            }
            var existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            await repo.UpdateAsync(id, c);
            return new NoContentResult(); // 204 No content
        }

        // DELETE: api/arrivalsostav/[id]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await repo.RetrieveAsync(id);
            if (existing == null)
            {
                return NotFound(); // 404 Resource not found
            }
            bool deleted = await repo.DeleteAsync(id);
            if (deleted)
            {
                return new NoContentResult(); // 204 No content
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
