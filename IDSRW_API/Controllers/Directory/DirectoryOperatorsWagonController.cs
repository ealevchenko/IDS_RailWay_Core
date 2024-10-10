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
    public class DirectoryOperatorsWagonController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryOperatorsWagonController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryOperatorsWagon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryOperatorsWagon>>> GetDirectoryOperatorsWagon()
        {
            return await db.DirectoryOperatorsWagons.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryOperatorsWagon
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryOperatorsWagon>>> GetListDirectoryOperatorsWagon()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryOperatorsWagon> result = await db.DirectoryOperatorsWagons.FromSql($"select * from [IDS].[Directory_OperatorsWagons]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryOperatorsWagon/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryOperatorsWagon>> GetDirectoryOperatorsWagon(int id)
        {
            DirectoryOperatorsWagon? result = await db.DirectoryOperatorsWagons.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryOperatorsWagon
        //// BODY: DirectoryOperatorsWagon (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryOperatorsWagon>> PostDirectoryOperatorsWagon([FromBody] DirectoryOperatorsWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryOperatorsWagons.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryOperatorsWagon/
        //// BODY: DirectoryOperatorsWagon (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryOperatorsWagon>> PutDirectoryOperatorsWagon(DirectoryOperatorsWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryOperatorsWagons.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryOperatorsWagon/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryOperatorsWagon>> DeleteDirectoryOperatorsWagon(int num)
        //{
        //    DirectoryOperatorsWagon result = db.DirectoryOperatorsWagons.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryOperatorsWagons.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
