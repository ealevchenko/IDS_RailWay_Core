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
    public class DirectoryWayController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryWayController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryWay
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryWay>>> GetDirectoryWay()
        {
            return await db.DirectoryWays.ToListAsync();
        }
        // GET: DirectoryWay/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryWay>>> GetListDirectoryWay()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryWay> result = await db.DirectoryWays.FromSql($"select * from [IDS].[Directory_Ways]").ToListAsync();
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
        // GET: DirectoryWay/status/station/1
        [HttpGet("status/station/{id_station}")]
        public async Task<ActionResult<IEnumerable<ViewStatusWay>>> GetViewStatusAllWayOfStationId(int id_station)
        {
            try
            {
                List<ViewStatusWay> result = await db.getViewStatusAllWayOfStationId(id_station).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryWay/status/station/1/park/84
        [HttpGet("status/station/{id_station}/park/{id_park}")]
        public async Task<ActionResult<IEnumerable<ViewStatusWay>>> GetViewStatusAllWayOfStationParkId(int id_station, int id_park)
        {
            try
            {
                List<ViewStatusWay> result = await db.getViewStatusAllWayOfStationParkId(id_station, id_park).ToListAsync();
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryWay/way/1
        [HttpGet("status/way/{id_way}")]
        public async Task<ActionResult<ViewStatusWay>> GetViewStatusWayOfId(int id_way)
        {
            try
            {
                ViewStatusWay result = await db.getViewStatusWayOfId(id_way).FirstOrDefaultAsync();
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: DirectoryWay/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryWay>> GetDirectoryWay(int id)
        {
            DirectoryWay result = await db.DirectoryWays.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryWay
        //// BODY: DirectoryWay (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryWay>> PostDirectoryWay([FromBody] DirectoryWay obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryWays.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryWay/
        //// BODY: DirectoryWay (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryWay>> PutDirectoryWay(DirectoryWay obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryWays.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryWay/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryWay>> DeleteDirectoryWay(int id)
        //{
        //    DirectoryWay result = db.DirectoryWays.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryWays.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
