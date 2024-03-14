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
    public class DirectoryCargoGroupController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryCargoGroupController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryCargoGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryCargoGroup>>> GetDirectoryCargoGroup()
        {
            return await db.DirectoryCargoGroups.ToListAsync();
        }
        // GET: DirectoryCargoGroup
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryCargoGroup>>> GetListDirectoryCargoGroup()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryCargoGroup> result = await db.DirectoryCargoGroups.FromSql($"select * from [IDS].[Directory_CargoGroup]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: DirectoryCargoGroup/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryCargoGroup>> GetDirectoryCargoGroup(int id)
        {
            DirectoryCargoGroup result = await db.DirectoryCargoGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        // POST: DirectoryCargoGroup
        // BODY: DirectoryCargoGroup (JSON, XML)
        [HttpPost]
        public async Task<ActionResult<DirectoryCargoGroup>> PostDirectoryCargoGroup([FromBody] DirectoryCargoGroup obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            db.DirectoryCargoGroups.Add(obj);
            await db.SaveChangesAsync();
            return Ok(obj);
        }

        // PUT DirectoryCargoGroup/
        // BODY: DirectoryCargoGroup (JSON, XML)
        [HttpPut]
        public async Task<ActionResult<DirectoryCargoGroup>> PutDirectoryCargoGroup(DirectoryCargoGroup obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }
            if (!db.DirectoryCargoGroups.Any(x => x.Id == obj.Id))
            {
                return NotFound();
            }

            db.Update(obj);
            await db.SaveChangesAsync();
            return Ok(obj);
        }

        // DELETE DirectoryCargoGroup/[id]
        [HttpDelete("{id}")]
        public async Task<ActionResult<DirectoryCargoGroup>> DeleteDirectoryCargoGroup(int id)
        {
            DirectoryCargoGroup result = db.DirectoryCargoGroups.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            db.DirectoryCargoGroups.Remove(result);
            await db.SaveChangesAsync();
            return Ok(result);
        }
    }
}
