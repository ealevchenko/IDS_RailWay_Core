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
    public class DirectoryPayerSenderController : ControllerBase
    {
        private EFDbContext db;

        public DirectoryPayerSenderController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: DirectoryPayerSender
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DirectoryPayerSender>>> GetDirectoryPayerSender()
        {
            return await db.DirectoryPayerSenders
                .AsNoTracking()
                .ToListAsync();
        }
        // GET: DirectoryPayerSender/list
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<DirectoryPayerSender>>> GetListDirectoryPayerSender()
        {
            try
            {
                //db.Database.CommandTimeout = 100;
                List<DirectoryPayerSender> result = await db.DirectoryPayerSenders.FromSql($"select * from [IDS].[Directory_PayerSender]").ToListAsync();
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
        // GET: DirectoryPayerSender/[code]
        [HttpGet("{code}")]
        public async Task<ActionResult<DirectoryPayerSender>> GetDirectoryPayerSender(string code)
        {
            DirectoryPayerSender? result = await db.DirectoryPayerSenders
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Code == code);
            if (result == null)
                return NotFound();
            return new ObjectResult(result);
        }
        //// POST: DirectoryPayerSender
        //// BODY: DirectoryPayerSender (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<DirectoryPayerSender>> PostDirectoryPayerSender([FromBody] DirectoryPayerSender obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.DirectoryPayerSenders.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT DirectoryPayerSender/
        //// BODY: DirectoryPayerSender (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<DirectoryPayerSender>> PutDirectoryPayerSender(DirectoryPayerSender obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.DirectoryPayerSenders.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE DirectoryPayerSender/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DirectoryPayerSender>> DeleteDirectoryPayerSender(int id)
        //{
        //    DirectoryPayerSender result = db.DirectoryPayerSenders.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.DirectoryPayerSenders.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
