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
    public class DirectoryWagonsRentController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryWagonsRentController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryWagonsRent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryWagonsRent>>> GetDirectoryWagonsRent()
        {
            return await db.DirectoryWagonsRents.ToListAsync();
        }
        // GET: DirectoryWagonsRent
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryWagonsRent>>> GetListDirectoryWagonsRent()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryWagonsRent> result = await db.DirectoryWagonsRents.FromSql($"select * from [IDS].[Directory_WagonsRent]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryWagonsRent/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryWagonsRent>> GetDirectoryWagonsRent(int id)
        {
            DirectoryWagonsRent result = await db.DirectoryWagonsRents.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        // GET: DirectoryWagonsRent/num/[num]
        [HttpGet("num/{num}")]
        public async Task<ActionResult<IEnumerable<DirectoryWagonsRent>>> GetDirectoryWagonsRent_Of_Num(int num)
        {
            List<DirectoryWagonsRent> result = await db.DirectoryWagonsRents.Where(x => x.Num == num).ToListAsync();
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryWagonsRent
        //// BODY: DirectoryWagonsRent (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryWagonsRent>> PostDirectoryWagonsRent([FromBody] DirectoryWagonsRent obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryWagonsRents.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryWagonsRent/
        //// BODY: DirectoryWagonsRent (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryWagonsRent>> PutDirectoryWagonsRent(DirectoryWagonsRent obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryWagonsRents.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryWagonsRent/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryWagonsRent>> DeleteDirectoryWagonsRent(int num)
        //{
        //    DirectoryWagonsRent result = db.DirectoryWagonsRents.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryWagonsRents.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
