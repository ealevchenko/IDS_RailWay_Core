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
    public class DirectoryExternalStationController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryExternalStationController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryExternalStation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryExternalStation>>> GetDirectoryExternalStation()
        {
            return await db.DirectoryExternalStations
                .AsNoTracking()
                .Include(ir => ir.CodeInlandrailwayNavigation)
                    .ThenInclude(rw => rw.CodeRailwayNavigation)
                        .ThenInclude(contrys => contrys.IdCountrysNavigation)
                .ToListAsync();
        }
        // GET: DirectoryExternalStation/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryExternalStation>>> GetListDirectoryExternalStation()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryExternalStation> result = await db.DirectoryExternalStations.FromSql($"select * from [IDS].[Directory_ExternalStation]").ToListAsync();
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
        // GET: DirectoryExternalStation/[code]
        [HttpGet("{code}")]
        public async Task<ActionResult<DirectoryExternalStation>> GetDirectoryExternalStation(int code)
        {
            DirectoryExternalStation? result = await db.DirectoryExternalStations
                .AsNoTracking()
                .Include(ir => ir.CodeInlandrailwayNavigation)
                    .ThenInclude(rw => rw.CodeRailwayNavigation)
                        .ThenInclude(contrys => contrys.IdCountrysNavigation)
                .FirstOrDefaultAsync(x => x.Code == code);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryExternalStation
        //// BODY: DirectoryExternalStation (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryExternalStation>> PostDirectoryExternalStation([FromBody] DirectoryExternalStation obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryExternalStations.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryExternalStation/
        //// BODY: DirectoryExternalStation (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryExternalStation>> PutDirectoryExternalStation(DirectoryExternalStation obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryExternalStations.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryExternalStation/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryExternalStation>> DeleteDirectoryExternalStation(int id)
        //{
        //    DirectoryExternalStation result = db.DirectoryExternalStations.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryExternalStations.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
