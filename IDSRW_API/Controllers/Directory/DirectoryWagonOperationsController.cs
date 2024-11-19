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
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebAPI.Controllers.Directory
{

    [Route("[controller]")]
    [ApiController]
    public class DirectoryWagonOperationController : ControllerBase
    {
        private EFDbContext db;

        //JsonSerializerOptions options = new()
        //{
        //    ReferenceHandler = ReferenceHandler.Preserve,
        //    WriteIndented = true
        //};

        public DirectoryWagonOperationController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryWagonOperation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryWagonOperation>>> GetDirectoryWagonOperation()
        {
            return await db.DirectoryWagonOperations
                .AsNoTracking()
                .Include(dwols => dwols.DirectoryWagonOperationsLoadingStatuses)
                    .ThenInclude(ls => ls.IdWagonLoadingStatusNavigation)
                .ToListAsync();
        }
        // GET: DirectoryWagonOperation/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryWagonOperation>>> GetListDirectoryWagonOperation()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryWagonOperation> result = await db.DirectoryWagonOperations.FromSql($"select * from [IDS].[Directory_WagonOperations]").ToListAsync();    //i.SqlQuery<Directory_Cargo>($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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


        //// POST: DirectoryWagonOperation
        //// BODY: DirectoryWagonOperation (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryWagonOperation>> PostDirectoryWagonOperation([FromBody] DirectoryWagonOperation obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryWagonOperations.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryWagonOperation/
        //// BODY: DirectoryWagonOperation (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryWagonOperation>> PutDirectoryWagonOperation(DirectoryWagonOperation obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryWagonOperations.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryWagonOperation/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryWagonOperation>> DeleteDirectoryWagonOperation(int id)
        //{
        //    DirectoryWagonOperation result = db.DirectoryWagonOperations.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryWagonOperations.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
