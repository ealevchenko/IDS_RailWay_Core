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
    public class DirectoryCargoController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryCargoController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryCargo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryCargo>>> GetDirectoryCargo()
        {
            return await db.DirectoryCargos
                .AsNoTracking()
                .Include(group => group.IdGroupNavigation)
                .Include(etsng => etsng.IdCargoEtsngNavigation)
                .ToListAsync();
        }
        // GET: DirectoryCargo/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryCargo>>> GetListDirectoryCargo()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryCargo> result = await db.DirectoryCargos.FromSql($"select * from [IDS].[Directory_Cargo]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryCargo/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryCargo>> GetDirectoryCargo(int id)
        {
            DirectoryCargo? result = await db.DirectoryCargos
                .AsNoTracking()
                .Include(group => group.IdGroupNavigation)
                .Include(etsng => etsng.IdCargoEtsngNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryCargo
        //// BODY: DirectoryCargo (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryCargo>> PostDirectoryCargo([FromBody] DirectoryCargo obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryCargos.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryCargo/
        //// BODY: DirectoryCargo (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryCargo>> PutDirectoryCargo(DirectoryCargo obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryCargos.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryCargo/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryCargo>> DeleteDirectoryCargo(int id)
        //{
        //    DirectoryCargo result = db.DirectoryCargos.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryCargos.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
