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
    public class DirectoryParkWayController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryParkWayController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryParkWay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryParkWay>>> GetDirectoryParkWay()
        {
            return await db.DirectoryParkWays.ToListAsync();
        }
        // GET: DirectoryParkWay/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryParkWay>>> GetListDirectoryParkWay()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryParkWay> result = await db.DirectoryParkWays.FromSql($"select * from [IDS].[Directory_ParkWays]").ToListAsync();    //i.SqlQuery<Directory_Station>($"select * from [IDS].[Directory_Station]").ToListAsync();
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
        // GET: DirectoryParkWay/status/station/1
        [HttpGet("status/station/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewStatusParkWay>>> getViewStatusAllParkOfStationId(int id_station)
        {
            try
            {
                List<ViewStatusParkWay> result = await db.getViewStatusAllParkOfStationId(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryParkWay/station/1/park/84
        [HttpGet("status/station/{id_station}/park/{id_park}")]
        public async Task<ActionResult<ViewStatusParkWay>> getViewStatusParkOfId(int id_station, int id_park)
        {
            try
            {
                ViewStatusParkWay result = await db.getViewStatusParkOfId(id_station, id_park).FirstOrDefaultAsync();
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryParkWay/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryParkWay>> GetDirectoryParkWay(int id)
        {
            DirectoryParkWay result = await db.DirectoryParkWays.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryParkWay
        //// BODY: DirectoryParkWay (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryParkWay>> PostDirectoryParkWay([FromBody] DirectoryParkWay obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryParkWays.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryParkWay/
        //// BODY: DirectoryParkWay (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryParkWay>> PutDirectoryParkWay(DirectoryParkWay obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryParkWays.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryParkWay/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryParkWay>> DeleteDirectoryParkWay(int id)
        //{
        //    DirectoryParkWay result = db.DirectoryParkWays.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryParkWays.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
