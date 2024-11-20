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
    public class DirectoryWagonOperationsUzController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryWagonOperationsUzController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryWagonOperationsUz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryWagonOperationsUz>>> GetDirectoryWagonOperationsUz()
        {
            return await db.DirectoryWagonOperationsUzs.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryWagonOperationsUz
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryWagonOperationsUz>>> GetListDirectoryWagonOperationsUz()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryWagonOperationsUz> result = await db.DirectoryWagonOperationsUzs.FromSql($"select * from [IDS].[Directory_WagonOperationsUz]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryWagonOperationsUz/3/PrOp/true
        [HttpGet("{kod_pp}/PrOp/{pr_op}")]
        public async Task<ActionResult<DirectoryWagonOperationsUz>> GetDirectoryWagonOperationsUz(int kod_pp, bool pr_op)
        {
            DirectoryWagonOperationsUz? result = await db.DirectoryWagonOperationsUzs.AsNoTracking().FirstOrDefaultAsync(x => x.KodOp == kod_pp && x.PrOp == pr_op);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryWagonOperationsUz
        //// BODY: DirectoryWagonOperationsUz (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryWagonOperationsUz>> PostDirectoryWagonOperationsUz([FromBody] DirectoryWagonOperationsUz obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryWagonOperationsUzs.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryWagonOperationsUz/
        //// BODY: DirectoryWagonOperationsUz (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryWagonOperationsUz>> PutDirectoryWagonOperationsUz(DirectoryWagonOperationsUz obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryWagonOperationsUzs.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryWagonOperationsUz/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryWagonOperationsUz>> DeleteDirectoryWagonOperationsUz(int id)
        //{
        //    DirectoryWagonOperationsUz result = db.DirectoryWagonOperationsUzs.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryWagonOperationsUzs.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
