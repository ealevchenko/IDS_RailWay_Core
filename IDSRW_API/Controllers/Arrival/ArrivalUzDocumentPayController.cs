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
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Collections.Generic;

namespace WebAPI.Controllers.Directory
{
    [Route("[controller]")]
    [ApiController]
    public class ArrivalUzDocumentPayController : ControllerBase
    {
        private EFDbContext db;

        public ArrivalUzDocumentPayController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: ArrivalUzDocumentPay/44
        [HttpGet("{id}")]
        public async Task<ActionResult<ArrivalUzDocumentPay>> GetArrivalUzDocumentPay(int id)
        {
            try
            {
                IEnumerable<ArrivalUzDocumentPay> result = await db.ArrivalUzDocumentPays
                    .AsNoTracking()
                    .Where(d=>d.Id == id)
                    .ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        // GET: ArrivalUzDocumentPay/document/57
        [HttpGet("document/{id}")]
        public async Task<ActionResult<ArrivalUzDocumentPay>> GetArrivalUzDocumentPayOfDocument(long id)
        {
            try
            {
                IEnumerable<ArrivalUzDocumentPay> result = await db.ArrivalUzDocumentPays
                    .AsNoTracking()
                    .Where(d=>d.IdDocument == id)
                    .ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        //// POST: ArrivalUzDocumentPay
        //// BODY: ArrivalUzDocumentPay (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<ArrivalUzDocumentPay>> PostArrivalUzDocumentPay([FromBody] ArrivalUzDocumentPay obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.ArrivalUzDocumentPays.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT ArrivalUzDocumentPay/
        //// BODY: ArrivalUzDocumentPay (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<ArrivalUzDocumentPay>> PutArrivalUzDocumentPay(ArrivalUzDocumentPay obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.ArrivalUzDocumentPays.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE ArrivalUzDocumentPay/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ArrivalUzDocumentPay>> DeleteArrivalUzDocumentPay(int id)
        //{
        //    ArrivalUzDocumentPay result = db.ArrivalUzDocumentPays.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.ArrivalUzDocumentPays.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
