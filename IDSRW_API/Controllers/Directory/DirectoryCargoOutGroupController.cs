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
    public class DirectoryCargoOutGroupController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryCargoOutGroupController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryCargoOutGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryCargoOutGroup>>> GetDirectoryCargoOutGroup()
        {
            return await db.DirectoryCargoOutGroups.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryCargoOutGroup
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryCargoOutGroup>>> GetListDirectoryCargoOutGroup()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryCargoOutGroup> result = await db.DirectoryCargoOutGroups.FromSql($"select * from [IDS].[Directory_CargoOutGroup]").ToListAsync();
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
        // GET: DirectoryCargoOutGroup/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryCargoOutGroup>> GetDirectoryCargoOutGroup(int id)
        {
            DirectoryCargoOutGroup? result = await db.DirectoryCargoOutGroups.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryCargoOutGroup
        //// BODY: DirectoryCargoOutGroup (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryCargoOutGroup>> PostDirectoryCargoOutGroup([FromBody] DirectoryCargoOutGroup obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryCargoOutGroups.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryCargoOutGroup/
        //// BODY: DirectoryCargoOutGroup (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryCargoOutGroup>> PutDirectoryCargoOutGroup(DirectoryCargoOutGroup obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryCargoOutGroups.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryCargoOutGroup/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryCargoOutGroup>> DeleteDirectoryCargoOutGroup(int id)
        //{
        //    DirectoryCargoOutGroup result = db.DirectoryCargoOutGroups.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryCargoOutGroups.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
