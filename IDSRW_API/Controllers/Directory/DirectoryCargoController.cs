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
    public class Directory_Cargo
    {
        public int Id { get; set; }
        public int IdGroup { get; set; }
        public int IdCargoEtsng { get; set; }
        public string CargoNameRu { get; set; } = null!;
        public string CargoNameEn { get; set; } = null!;
        public string? CodeSap { get; set; }
        public bool? Sending { get; set; }
        public DateTime Create { get; set; }
        public string CreateUser { get; set; } = null!;
        public DateTime? Change { get; set; }
        public string? ChangeUser { get; set; }
        public int? IdOutGroup { get; set; }
    }

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
            return await db.DirectoryCargos.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryCargo
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Directory_Cargo>>> GetListDirectoryCargo()
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
            DirectoryCargo? result = await db.DirectoryCargos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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
