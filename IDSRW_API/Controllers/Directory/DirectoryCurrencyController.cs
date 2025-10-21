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
    public class DirectoryCurrencyController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryCurrencyController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryCurrency
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryCurrency>>> GetDirectoryCurrency()
        {
            return await db.DirectoryCurrencies.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryCurrency/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryCurrency>>> GetListDirectoryCurrency()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryCurrency> result = await db.DirectoryCurrencies.FromSql($"select * from [IDS].[Directory_Currency]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryCurrency/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryCurrency>> GetDirectoryCurrency(int id)
        {
            DirectoryCurrency? result = await db.DirectoryCurrencies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        // GET: DirectoryCurrency/code/[code]
        [HttpGet("code/{code}")]
        public async Task<ActionResult<DirectoryCurrency>> GetDirectoryCurrency_Of_Code(int code)
        {
            DirectoryCurrency? result = await db.DirectoryCurrencies.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }

        //// POST: DirectoryCurrency
        //// BODY: DirectoryCurrency (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryCurrency>> PostDirectoryCurrency([FromBody] DirectoryCurrency obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryCurrencies.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryCurrency/
        //// BODY: DirectoryCurrency (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryCurrency>> PutDirectoryCurrency(DirectoryCurrency obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryCurrencies.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryCurrency/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryCurrency>> DeleteDirectoryCurrency(int num)
        //{
        //    DirectoryCurrency result = db.DirectoryCurrencies.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryCurrencies.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
