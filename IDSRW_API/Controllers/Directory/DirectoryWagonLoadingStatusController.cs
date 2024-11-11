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
using System.Collections.Generic;

namespace WebAPI.Controllers.Directory
{

    [Route("[controller]")]
    [ApiController]
    public class DirectoryWagonLoadingStatusController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryWagonLoadingStatusController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryWagonLoadingStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryWagonLoadingStatus>>> GetDirectoryWagonLoadingStatus()
        {
            return await db.DirectoryWagonLoadingStatuses.AsNoTracking().ToListAsync();
        }
        // GET: DirectoryWagonLoadingStatus/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Directory_Cargo>>> GetListDirectoryWagonLoadingStatus()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryWagonLoadingStatus> result = await db.DirectoryWagonLoadingStatuses.FromSql($"select * from [IDS].[Directory_WagonLoadingStatus]").ToListAsync(); 
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
        // GET: DirectoryWagonLoadingStatus/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryWagonLoadingStatus>> GetDirectoryWagonLoadingStatus(int id)
        {
            DirectoryWagonLoadingStatus? result = await db.DirectoryWagonLoadingStatuses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }

        // GET: DirectoryWagonLoadingStatus/wagon_operations/13
        [HttpGet("wagon_operations/{id}")]
        public async Task<ActionResult<IEnumerable<DirectoryWagonLoadingStatus>>> GetDirectoryWagonLoadingStatusOfWagonOperations(int id)
        {
            List<DirectoryWagonLoadingStatus> result = await db.DirectoryWagonOperationsLoadingStatuses
                .AsNoTracking()
                .Where(s=>s.IdWagonOperations == id)
                .Select(x=>x.IdWagonLoadingStatusNavigation)
                .ToListAsync();
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }


        //// POST: DirectoryWagonLoadingStatus
        //// BODY: DirectoryWagonLoadingStatus (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryWagonLoadingStatus>> PostDirectoryWagonLoadingStatus([FromBody] DirectoryWagonLoadingStatus obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryWagonLoadingStatuss.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryWagonLoadingStatus/
        //// BODY: DirectoryWagonLoadingStatus (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryWagonLoadingStatus>> PutDirectoryWagonLoadingStatus(DirectoryWagonLoadingStatus obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryWagonLoadingStatuss.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryWagonLoadingStatus/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryWagonLoadingStatus>> DeleteDirectoryWagonLoadingStatus(int id)
        //{
        //    DirectoryWagonLoadingStatus result = db.DirectoryWagonLoadingStatuss.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryWagonLoadingStatuss.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
