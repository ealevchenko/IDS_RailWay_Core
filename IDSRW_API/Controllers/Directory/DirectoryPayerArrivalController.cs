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
    public class DirectoryPayerArrivalController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryPayerArrivalController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryPayerArrival
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryPayerArrival>>> GetDirectoryPayerArrival()
        {
            return await db.DirectoryPayerArrivals
                .AsNoTracking()
                .ToListAsync();
        }
        // GET: DirectoryPayerArrival/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryPayerArrival>>> GetListDirectoryPayerArrival()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryPayerArrival> result = await db.DirectoryPayerArrivals.FromSql($"select * from [IDS].[Directory_PayerArrival]").ToListAsync();
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
        // GET: DirectoryPayerArrival/[code]
        [HttpGet("{code}")]
        public async Task<ActionResult<DirectoryPayerArrival>> GetDirectoryPayerArrival(string code)
        {
            DirectoryPayerArrival? result = await db.DirectoryPayerArrivals
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == code);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryPayerArrival
        //// BODY: DirectoryPayerArrival (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryPayerArrival>> PostDirectoryPayerArrival([FromBody] DirectoryPayerArrival obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryPayerArrivals.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryPayerArrival/
        //// BODY: DirectoryPayerArrival (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryPayerArrival>> PutDirectoryPayerArrival(DirectoryPayerArrival obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryPayerArrivals.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryPayerArrival/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryPayerArrival>> DeleteDirectoryPayerArrival(int id)
        //{
        //    DirectoryPayerArrival result = db.DirectoryPayerArrivals.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryPayerArrivals.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
