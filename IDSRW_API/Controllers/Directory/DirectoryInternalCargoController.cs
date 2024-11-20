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
    public class DirectoryInternalCargoController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryInternalCargoController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryInternalCargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryInternalCargo>>> GetDirectoryInternalCargo()
        {
            return await db.DirectoryInternalCargos
                .AsNoTracking()
                .Include(group => group.IdGroupNavigation)
                .ToListAsync();
        }
        // GET: DirectoryInternalCargo/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryInternalCargo>>> GetListDirectoryInternalCargo()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryInternalCargo> result = await db.DirectoryInternalCargos.FromSql($"select * from [IDS].[Directory_InternalCargo]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryInternalCargo/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryInternalCargo>> GetDirectoryInternalCargo(int id)
        {
            DirectoryInternalCargo? result = await db.DirectoryInternalCargos
                .AsNoTracking()
                .Include(group => group.IdGroupNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryInternalCargo
        //// BODY: DirectoryInternalCargo (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryInternalCargo>> PostDirectoryInternalCargo([FromBody] DirectoryInternalCargo obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryInternalCargos.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryInternalCargo/
        //// BODY: DirectoryInternalCargo (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryInternalCargo>> PutDirectoryInternalCargo(DirectoryInternalCargo obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryInternalCargos.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryInternalCargo/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryInternalCargo>> DeleteDirectoryInternalCargo(int id)
        //{
        //    DirectoryInternalCargo result = db.DirectoryInternalCargos.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryInternalCargos.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
