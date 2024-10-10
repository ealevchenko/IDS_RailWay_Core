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
    public class DirectoryGenusWagonController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryGenusWagonController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryGenusWagon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryGenusWagon>>> GetDirectoryGenusWagon()
        {
            return await db.DirectoryGenusWagons.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryGenusWagon
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryGenusWagon>>> GetListDirectoryGenusWagon()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryGenusWagon> result = await db.DirectoryGenusWagons.FromSql($"select * from [IDS].[Directory_GenusWagons]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryGenusWagon/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryGenusWagon>> GetDirectoryGenusWagon(int id)
        {
            DirectoryGenusWagon? result = await db.DirectoryGenusWagons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryGenusWagon
        //// BODY: DirectoryGenusWagon (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryGenusWagon>> PostDirectoryGenusWagon([FromBody] DirectoryGenusWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryGenusWagons.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryGenusWagon/
        //// BODY: DirectoryGenusWagon (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryGenusWagon>> PutDirectoryGenusWagon(DirectoryGenusWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryGenusWagons.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryGenusWagon/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryGenusWagon>> DeleteDirectoryGenusWagon(int num)
        //{
        //    DirectoryGenusWagon result = db.DirectoryGenusWagons.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryGenusWagons.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
