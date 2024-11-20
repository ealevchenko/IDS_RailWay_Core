using EF_IDS.Concrete;
using EF_IDS.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Repositories;
using WebAPI.Repositories.Directory;

namespace WebAPI.Controllers.Directory
{

    [Route("[controller]")]
    [ApiController]
    public class DirectoryDivisionController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryDivisionController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryDivision
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryDivision>>> GetDirectoryDivision()
        {
            return await db.DirectoryDivisions.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryDivision/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryDivision>>> GetListDirectoryDivision()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryDivision> result = await db.DirectoryDivisions.FromSql($"select * from [IDS].[Directory_Divisions]").ToListAsync(); 
                if (result == null)
                    return NotFound();
                //db.Database.CommandTimeout = null;               
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryDivision/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryDivision>> GetDirectoryDivision(int id)
        {
            DirectoryDivision? result = await db.DirectoryDivisions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }

        //// POST: DirectoryDivision
        //// BODY: DirectoryDivision (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryDivision>> PostDirectoryDivision([FromBody] DirectoryDivision obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryDivisions.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryDivision/
        //// BODY: DirectoryDivision (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryDivision>> PutDirectoryDivision(DirectoryDivision obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryDivisions.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryDivision/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryDivision>> DeleteDirectoryDivision(int id)
        //{
        //    DirectoryDivision result = db.DirectoryDivisions.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryDivisions.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
