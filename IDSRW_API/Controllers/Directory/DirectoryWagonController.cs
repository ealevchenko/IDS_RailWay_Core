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
    public class DirectoryWagonController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryWagonController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryWagon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryWagon>>> GetDirectoryWagon()
        {
            return await db.DirectoryWagons.ToListAsync();
        }
        // GET: DirectoryWagon
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryWagon>>> GetListDirectoryWagon()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryWagon> result = await db.DirectoryWagons.FromSql($"select * from [IDS].[Directory_Wagons]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryWagon/[num]
        [HttpGet("num/{num}")]
        public async Task<ActionResult<DirectoryWagon>> GetDirectoryWagon(int num)
        {
            DirectoryWagon result = await db.DirectoryWagons.FirstOrDefaultAsync(x => x.Num == num);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryWagon
        //// BODY: DirectoryWagon (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryWagon>> PostDirectoryWagon([FromBody] DirectoryWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryWagons.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryWagon/
        //// BODY: DirectoryWagon (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryWagon>> PutDirectoryWagon(DirectoryWagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryWagons.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryWagon/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryWagon>> DeleteDirectoryWagon(int num)
        //{
        //    DirectoryWagon result = db.DirectoryWagons.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryWagons.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
