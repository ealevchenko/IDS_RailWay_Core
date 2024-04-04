﻿using EF_IDS.Concrete;
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
    public class DirectoryStationController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryStationController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryStation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryStation>>> GetDirectoryStation()
        {
            return await db.DirectoryStations.ToListAsync();
        }
        // GET: DirectoryStation
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryStation>>> GetListDirectoryStation()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryStation> result = await db.DirectoryStations.FromSql($"select * from [IDS].[Directory_Station]").ToListAsync();    //i.SqlQuery<Directory_Station>($"select * from [IDS].[Directory_Station]").ToListAsync();
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
        // GET: DirectoryStation/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryStation>> GetDirectoryStation(int id)
        {
            DirectoryStation result = await db.DirectoryStations.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryStation
        //// BODY: DirectoryStation (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryStation>> PostDirectoryStation([FromBody] DirectoryStation obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryStations.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryStation/
        //// BODY: DirectoryStation (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryStation>> PutDirectoryStation(DirectoryStation obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryStations.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryStation/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryStation>> DeleteDirectoryStation(int id)
        //{
        //    DirectoryStation result = db.DirectoryStations.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryStations.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
