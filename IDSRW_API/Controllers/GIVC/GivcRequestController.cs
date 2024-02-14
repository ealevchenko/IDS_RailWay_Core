using EF_IDS.Abstract;
using EF_IDS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers.GIVC
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class GivcRequestController : ControllerBase
    //{
    //    private IRepository<GivcRequest> repo;

    //    // конструктор вводит зарегистрированный репозиторий
    //    public GivcRequestController(IRepository<GivcRequest> repo)
    //    {
    //        this.repo = repo;
    //    }

    //    // GET: api/GivcRequest
    //    [HttpGet]
    //    public async Task<IEnumerable<GivcRequest>> GetGivcRequest()
    //    {
    //        return await repo.Context.ToListAsync();
    //    }
    //    // GET: api/GivcRequest/[id]
    //    [HttpGet("{id}", Name = "GetGivcRequest")]
    //    public async Task<IActionResult> GetGivcRequest(int id)
    //    {
    //        GivcRequest c = await repo.Context.FirstOrDefaultAsync(x=>x.Id == id);
    //        if (c == null)
    //        {
    //            return NotFound(); // 404 Resource not found
    //        }
    //        return new ObjectResult(c); // 200 OK
    //    }

    //    //// POST: api/GivcRequest
    //    //// BODY: GivcRequest (JSON, XML)
    //    //[HttpPost]
    //    //public async Task<IActionResult> Create([FromBody] GivcRequest c)
    //    //{
    //    //    if (c == null)
    //    //    {
    //    //        return BadRequest(); // 400 Bad request
    //    //    }
    //    //    GivcRequest added = await repo.CreateAsync(c);
    //    //    return CreatedAtRoute("GetGivcRequest", new { id = added.Id }, c); // 201 Created
    //    //}

    //    //// PUT: api/GivcRequest/[id]
    //    //// BODY: GivcRequest (JSON, XML)
    //    //[HttpPut("{id}")]
    //    //public async Task<IActionResult> Update(int id, [FromBody] GivcRequest c)
    //    //{
    //    //    if (c == null || c.Id != id)
    //    //    {
    //    //        return BadRequest(); // 400 Bad request
    //    //    }
    //    //    var existing = await repo.RetrieveAsync(id);
    //    //    if (existing == null)
    //    //    {
    //    //        return NotFound(); // 404 Resource not found
    //    //    }
    //    //    await repo.UpdateAsync(id, c);
    //    //    return new NoContentResult(); // 204 No content
    //    //}

    //    //// DELETE: api/GivcRequest/[id]
    //    //[HttpDelete("{id}")]
    //    //public async Task<IActionResult> Delete(int id)
    //    //{
    //    //    var existing = await repo.RetrieveAsync(id);
    //    //    if (existing == null)
    //    //    {
    //    //        return NotFound(); // 404 Resource not found
    //    //    }
    //    //    bool deleted = await repo.DeleteAsync(id);
    //    //    if (deleted)
    //    //    {
    //    //        return new NoContentResult(); // 204 No content
    //    //    }
    //    //    else
    //    //    {
    //    //        return BadRequest();
    //    //    }
    //    //}

    //}
}
