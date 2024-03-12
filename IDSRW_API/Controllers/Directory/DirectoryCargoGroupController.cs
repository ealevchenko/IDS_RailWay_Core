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
    public class DirectoryCargoGroupController : ControllerBase
    {
        private IRepository<DirectoryCargoGroup> repo;

        // конструктор вводит зарегистрированный репозиторий
        public DirectoryCargoGroupController(IRepository<DirectoryCargoGroup> repo)
        {
            this.repo = repo;
        }

        // GET: api/DirectoryCargoGroup
        [HttpGet]
        public async Task<IEnumerable<DirectoryCargoGroup>> GetDirectoryCargoGroup()
        {
            return await repo.RetrieveAllAsync();
        }
        // GET: api/DirectoryCargoGroup/[id]
        [HttpGet("{id}", Name = "GetDirectoryCargoGroup")]
        public async Task<IActionResult> GetDirectoryCargoGroup(int id)
        {
            DirectoryCargoGroup c = await repo.RetrieveAsync(id);
            if (c == null)
            {
                return NotFound(); // 404 Resource not found
            }
            return new ObjectResult(c); // 200 OK
        }

        // POST: api/DirectoryCargoGroup
        // BODY: DirectoryCargoGroup (JSON, XML)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DirectoryCargoGroup c)
        {
            if (c == null)
            {
                return BadRequest(); // 400 Bad request
            }
            DirectoryCargoGroup added = await repo.CreateAsync(c);
            return CreatedAtRoute("GetDirectoryCargoGroup", new { id = added.Id }, c); // 201 Created
        }

        // PUT: api/DirectoryCargoGroup/[id]
        // BODY: DirectoryCargoGroup (JSON, XML)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DirectoryCargoGroup c)
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

        // DELETE: api/DirectoryCargoGroup/[id]
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
