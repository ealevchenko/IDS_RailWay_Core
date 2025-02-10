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
    public class DirectoryOrganizationServiceController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryOrganizationServiceController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryOrganizationService
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryOrganizationService>>> GetDirectoryOrganizationService()
        {
            return await db.DirectoryOrganizationServices
                .AsNoTracking()
                .ToListAsync();
        }
        // GET: DirectoryOrganizationService/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryOrganizationService>>> GetListDirectoryOrganizationService()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryOrganizationService> result = await db.DirectoryOrganizationServices.FromSql($"select * from [IDS].[Directory_OrganizationService]").ToListAsync();
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
        // GET: DirectoryOrganizationService/[id]
        [HttpGet("{id}")]
        public async Task<ActionResult<DirectoryOrganizationService>> GetDirectoryOrganizationService(int id)
        {
            DirectoryOrganizationService? result = await db.DirectoryOrganizationServices
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryOrganizationService
        //// BODY: DirectoryOrganizationService (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryOrganizationService>> PostDirectoryOrganizationService([FromBody] DirectoryOrganizationService obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryOrganizationServices.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryOrganizationService/
        //// BODY: DirectoryOrganizationService (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryOrganizationService>> PutDirectoryOrganizationService(DirectoryOrganizationService obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryOrganizationServices.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryOrganizationService/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryOrganizationService>> DeleteDirectoryOrganizationService(int id)
        //{
        //    DirectoryOrganizationService result = db.DirectoryOrganizationServices.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryOrganizationServices.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
