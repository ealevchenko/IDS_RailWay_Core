using EF_IDS.Concrete;
using EF_IDS.Entities;
using EF_IDS.Functions;
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
    public class DirectoryLocomotiveController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryLocomotiveController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryLocomotive
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryLocomotive>>> GetDirectoryLocomotive()
        {
            return await db.DirectoryLocomotives
                .AsNoTracking()
                //.Include(l => l.IdLocomotiveStatus)
                .ToListAsync();
        }
        // GET: DirectoryLocomotive/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryLocomotive>>> GetListDirectoryLocomotive()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryLocomotive> result = await db.DirectoryLocomotives.FromSql($"select * from [IDS].[Directory_Locomotive]").ToListAsync();
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
        // GET: DirectoryLocomotive/locomotive/TЭM15-043
        [HttpGet("locomotive/{locomotive}")]
        public async Task<ActionResult<DirectoryLocomotive>> GetDirectoryLocomotiveOfLocomotive(string locomotive)
        {
            try
            {
               DirectoryLocomotive result = await db.DirectoryLocomotives.Where(l=>l.Locomotive==locomotive).FirstOrDefaultAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryLocomotive/status/2
        [HttpGet("status/{id_locomotive_status}")]
        public async Task<ActionResult<IEnumerable<DirectoryLocomotive>>> GetDirectoryLocomotiveOfLocomotive(int id_locomotive_status)
        {
            try
            {
                List<DirectoryLocomotive> result = await db.DirectoryLocomotives.Where(o => o.IdLocomotiveStatus == id_locomotive_status).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// POST: DirectoryLocomotives
        //// BODY: DirectoryLocomotives (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryLocomotives>> PostDirectoryLocomotive([FromBody] DirectoryLocomotives obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryLocomotives.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryLocomotives/
        //// BODY: DirectoryLocomotives (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryLocomotives>> PutDirectoryLocomotive(DirectoryLocomotives obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryLocomotives.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryLocomotives/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryLocomotives>> DeleteDirectoryLocomotive(int id)
        //{
        //    DirectoryLocomotives result = db.DirectoryLocomotives.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryLocomotives.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
