using EF_IDS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Repositories;
using WebAPI.Repositories.Directory;

namespace WebAPI.Controllers.Directory
{
    [Route("[controller]")]
    [ApiController]
    public class DirectoryCargoController : ControllerBase
    {
        private IRepository<DirectoryCargo> repo;

        // конструктор вводит зарегистрированный репозиторий
        public DirectoryCargoController(IRepository<DirectoryCargo> repo)
        {
            this.repo = repo;
        }

        // GET: api/DirectoryCargo
        [HttpGet]
        public async Task<IEnumerable<DirectoryCargo>> GetDirectoryCargo()
        {
            return await repo.RetrieveAllAsync();
        }
        // GET: api/DirectoryCargo/[id]
        [HttpGet("{id}", Name = "GetDirectoryCargo")]
        public async Task<IActionResult> GetDirectoryCargo(int id)
        {
            DirectoryCargo c = await repo.RetrieveAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(c); // 200 OK
        }

        // POST: api/DirectoryCargo
        // BODY: DirectoryCargo (JSON, XML)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DirectoryCargo c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            DirectoryCargo added = await repo.CreateAsync(c);
            return CreatedAtRoute("GetDirectoryCargo", new { id = added.Id }, c); // 201 Created
        }

        // PUT: api/DirectoryCargo/[id]
        // BODY: DirectoryCargo (JSON, XML)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DirectoryCargo c)
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

        // DELETE: api/DirectoryCargo/[id]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
