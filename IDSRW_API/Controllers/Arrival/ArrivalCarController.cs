using EF_IDS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories.Arrival;

namespace WebAPI.Controllers.Arrival
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivalCarController : ControllerBase
    {
        private ILongRepository<ArrivalCar> repo;

        // конструктор вводит зарегистрированный репозиторий
        public ArrivalCarController(ILongRepository<ArrivalCar> repo)
        {
            this.repo = repo;
        }

        // GET: api/arrivalcar
        [HttpGet]
        public async Task<IEnumerable<ArrivalCar>> GetArrivalCar()
        {
            return await repo.RetrieveAllAsync();
        }
        // GET: api/arrivalcar/[id]
        [HttpGet("{id}", Name = "GetArrivalCar")]
        public async Task<IActionResult> GetArrivalCar(long id)
        {
            ArrivalCar c = await repo.RetrieveAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(c); // 200 OK
        }

        // POST: api/arrivalcar
        // BODY: ArrivalCar (JSON, XML)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ArrivalCar c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            ArrivalCar added = await repo.CreateAsync(c);
            return CreatedAtRoute("GetArrivalCar", new { id = added.Id }, c); // 201 Created
        }

        // PUT: api/arrivalcar/[id]
        // BODY: ArrivalCar (JSON, XML)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ArrivalCar c)
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

        // DELETE: api/arrivalcar/[id]
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
