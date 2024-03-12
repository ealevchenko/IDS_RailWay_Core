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
    public class DirectoryCargoEtsngController : ControllerBase
    {
        private IRepository<DirectoryCargoEtsng> repo;

        // конструктор вводит зарегистрированный репозиторий
        public DirectoryCargoEtsngController(IRepository<DirectoryCargoEtsng> repo)
        {
            this.repo = repo;
        }

        // GET: api/DirectoryCargoEtsng
        [HttpGet]
        public async Task<IEnumerable<DirectoryCargoEtsng>> GetDirectoryCargoEtsng()
        {
            return await repo.RetrieveAllAsync();
        }
        // GET: api/DirectoryCargoEtsng/[id]
        [HttpGet("{id}", Name = "GetDirectoryCargoEtsng")]
        public async Task<IActionResult> GetDirectoryCargoEtsng(int id)
        {
            DirectoryCargoEtsng c = await repo.RetrieveAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(c); // 200 OK
        }

        // POST: api/DirectoryCargoEtsng
        // BODY: DirectoryCargoEtsng (JSON, XML)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DirectoryCargoEtsng c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            DirectoryCargoEtsng added = await repo.CreateAsync(c);
            return CreatedAtRoute("GetDirectoryCargoEtsng", new { id = added.Id }, c); // 201 Created
        }

        // PUT: api/DirectoryCargoEtsng/[id]
        // BODY: DirectoryCargoEtsng (JSON, XML)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DirectoryCargoEtsng c)
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

        // DELETE: api/DirectoryCargoEtsng/[id]
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
