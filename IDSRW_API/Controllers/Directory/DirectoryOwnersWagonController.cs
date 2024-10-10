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
    public class DirectoryOwnersWagonController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryOwnersWagonController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryOwnersWagon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryOwnersWagon>>> GetDirectoryOwnersWagon()
        {
            return await db.DirectoryOwnersWagons.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryOwnersWagon
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryOwnersWagon>>> GetListDirectoryOwnersWagon()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryOwnersWagon> result = await db.DirectoryOwnersWagons.FromSql($"select * from [IDS].[Directory_OwnersWagons]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryOwnersWagon/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryOwnersWagon>> GetDirectoryOwnersWagon(int id)
        {
            DirectoryOwnersWagon? result = await db.DirectoryOwnersWagons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryOwnersWagon
        //// BODY: DirectoryOwnersWagon (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryOwnersWagon>> PostDirectoryOwnersWagon([FromBody] DirectoryOwnersWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryOwnersWagons.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryOwnersWagon/
        //// BODY: DirectoryOwnersWagon (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryOwnersWagon>> PutDirectoryOwnersWagon(DirectoryOwnersWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryOwnersWagons.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryOwnersWagon/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryOwnersWagon>> DeleteDirectoryOwnersWagon(int num)
        //{
        //    DirectoryOwnersWagon result = db.DirectoryOwnersWagons.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryOwnersWagons.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
