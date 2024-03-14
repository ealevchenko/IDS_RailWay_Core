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
    public class DirectoryCargoEtsngController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryCargoEtsngController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryCargoEtsng
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryCargoEtsng>>> GetDirectoryCargoEtsng()
        {
            return await db.DirectoryCargoEtsngs.ToListAsync();
        }
        // GET: DirectoryCargoEtsng
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryCargoEtsng>>> GetListDirectoryCargoEtsng()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryCargoEtsng> result = await db.DirectoryCargoEtsngs.FromSql($"select * from [IDS].[Directory_CargoETSNG]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryCargoEtsng/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryCargoEtsng>> GetDirectoryCargoEtsng(int id)
        {
            DirectoryCargoEtsng result = await db.DirectoryCargoEtsngs.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        // POST: DirectoryCargoEtsng
        // BODY: DirectoryCargoEtsng (JSON, XML)
        [HttpPost]
        public async Task<ActionResult<DirectoryCargoEtsng>> PostDirectoryCargoEtsng([FromBody] DirectoryCargoEtsng obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            db.DirectoryCargoEtsngs.Add(obj);
            await db.SaveChangesAsync();
            return Ok(obj);
        }

        // PUT DirectoryCargoEtsng/
        // BODY: DirectoryCargoEtsng (JSON, XML)
        [HttpPut]
        public async Task<ActionResult<DirectoryCargoEtsng>> PutDirectoryCargoEtsng(DirectoryCargoEtsng obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            if (!db.DirectoryCargoEtsngs.Any(x => x.Id == obj.Id))
            {
                return NotFound();
            }

            db.Update(obj);
            await db.SaveChangesAsync();
            return Ok(obj);
        }

        // DELETE DirectoryCargoEtsng/[id]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DirectoryCargoEtsng>> DeleteDirectoryCargoEtsng(int id)
        {
            DirectoryCargoEtsng result = db.DirectoryCargoEtsngs.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            db.DirectoryCargoEtsngs.Remove(result);
            await db.SaveChangesAsync();
            return Ok(result);
        }
    }
}
