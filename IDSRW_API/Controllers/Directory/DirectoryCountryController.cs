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
    public class DirectoryCountryController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryCountryController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryCountry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryCountry>>> GetDirectoryCountry()
        {
            return await db.DirectoryCountrys.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryCountry/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryCountry>>> GetListDirectoryCountry()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryCountry> result = await db.DirectoryCountrys.FromSql($"select * from [IDS].[Directory_Countrys]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryCountry/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryCountry>> GetDirectoryCountry(int id)
        {
            DirectoryCountry? result = await db.DirectoryCountrys.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        // GET: DirectoryCountry/code_sng/[code]
        [HttpGet("code_sng/{code}")]
        public async Task<ActionResult<DirectoryCountry>> GetDirectoryCountry_Of_Code(int code)
        {
            DirectoryCountry? result = await db.DirectoryCountrys.AsNoTracking().FirstOrDefaultAsync(x => x.CodeSng == code);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }

        //// POST: DirectoryCountry
        //// BODY: DirectoryCountry (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryCountry>> PostDirectoryCountry([FromBody] DirectoryCountry obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryCountrys.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryCountry/
        //// BODY: DirectoryCountry (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryCountry>> PutDirectoryCountry(DirectoryCountry obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryCountrys.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryCountry/[num]
        //[HttpDelete("{num}")]
        //public async Task<ActionResult<DirectoryCountry>> DeleteDirectoryCountry(int num)
        //{
        //    DirectoryCountry result = db.DirectoryCountrys.FirstOrDefault(x => x.Num == num);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryCountrys.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
