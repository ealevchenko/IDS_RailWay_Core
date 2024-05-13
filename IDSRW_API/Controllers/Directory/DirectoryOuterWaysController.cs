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
    public class DirectoryOuterWayController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryOuterWayController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryOuterWay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryOuterWay>>> GetDirectoryOuterWay()
        {
            return await db.DirectoryOuterWays.ToListAsync();
        }
        // GET: DirectoryOuterWay/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryOuterWay>>> GetListDirectoryOuterWay()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryOuterWay> result = await db.DirectoryOuterWays.FromSql($"select * from [IDS].[Directory_OuterWays]").ToListAsync();
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
        // GET: DirectoryOuterWay/from/station/1
        [HttpGet("from/station/{id_station}")]
        public async Task<ActionResult<IEnumerable<DirectoryOuterWay>>> GetDirectoryOuterWayOfFromStationId(int id_station)
        {
            try
            {
                List<DirectoryOuterWay> result = await db.DirectoryOuterWays.Where(o=>o.IdStationFrom == id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryOuterWay/on/station/1
        [HttpGet("on/station/{id_station}")]
        public async Task<ActionResult<IEnumerable<DirectoryOuterWay>>> GetDirectoryOuterWayOfOnStationId(int id_station)
        {
            try
            {
                List<DirectoryOuterWay> result = await db.DirectoryOuterWays.Where(o=>o.IdStationOn == id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// POST: DirectoryOuterWays
        //// BODY: DirectoryOuterWays (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryOuterWays>> PostDirectoryOuterWay([FromBody] DirectoryOuterWays obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryOuterWays.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryOuterWays/
        //// BODY: DirectoryOuterWays (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryOuterWays>> PutDirectoryOuterWay(DirectoryOuterWays obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryOuterWays.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryOuterWays/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryOuterWays>> DeleteDirectoryOuterWay(int id)
        //{
        //    DirectoryOuterWays result = db.DirectoryOuterWays.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryOuterWays.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
