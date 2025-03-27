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

namespace WebAPI.Controllers.Directory
{
    [Route("[controller]")]
    [ApiController]
    public class ArrivalUzDocumentController : ControllerBase
    {
        private EFDbContext db;

        public ArrivalUzDocumentController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: ArrivalUzDocument
        [HttpGet]
        public async Task<ActionResult<ArrivalUzDocument>> GetArrivalUzDocument()
        {
            try
            {
                IEnumerable<ArrivalUzDocument> result = await db.ArrivalUzDocuments.AsNoTracking().ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: ArrivalUzDocument/list/main_doc/start/2025-03-01T00:00:00/stop/2025-03-30T00:00:00
        [HttpGet("list/main_doc/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult<ArrivalUzDocument>> GetListMainDocArrivalUzDocument(DateTime start, DateTime stop)
        {
            try
            {
                IEnumerable<long> id_sts = db.ArrivalSostavs.AsNoTracking().Where(s => s.DateAdoption >= start && s.DateAdoption <= stop).Select(c => c.Id).ToList();
                IEnumerable<long> id_docs = db.ArrivalUzVagons.Where(v => id_sts.Contains(v.IdArrivalNavigation.Id)).Select(c => c.IdDocument).Distinct().ToList();
                var result = await db.ArrivalUzDocuments
                        .AsNoTracking()
                        .Where(x => id_docs.Contains(x.Id))
                        .Select(d => new { d.Id, d.NomMainDoc, d.CalcPayer })
                        .ToListAsync();
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: ArrivalUzDocument/list
        //[HttpGet("list")]
        //public async Task<ActionResult<IEnumerable<ArrivalUzDocument>>> GetListArrivalUzDocument()
        //{
        //    try
        //    {
        //        //db.Database.CommandTimeout = 100;
        //        List<ArrivalUzDocument> result = await db.ArrivalUzDocuments.FromSql($"select * from [IDS].[Directory_Cargo]").ToListAsync();
        //        if (result == null)
        //            return NotFound();
        //        //db.Database.CommandTimeout = null;               
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        // GET: ArrivalUzDocument/851564
        [HttpGet("{id}")]
        public async Task<ActionResult<ArrivalUzDocument>> GetArrivalUzDocument(int id)
        {
            try
            {
                ArrivalUzDocument? result = await db.ArrivalUzDocuments
                    .AsNoTracking()
                    .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
                    .Include(code_st_from => code_st_from.CodeStnFromNavigation)
                    .Include(code_st_on => code_st_on.CodeStnToNavigation)
                    .Include(code_cns => code_cns.CodeConsigneeNavigation)
                    .Include(code_chp => code_chp.CodeShipperNavigation)
                    .Include(code_ps => code_ps.CodePayerSenderNavigation)
                    .Include(code_pa => code_pa.CodePayerArrivalNavigation)
                    .Include(code_pl => code_pl.CodePayerLocalNavigation)
                    .Include(doc => doc.ArrivalUzDocumentDocs)
                    .Include(act => act.ArrivalUzDocumentActs)
                    .Include(pays => pays.ArrivalUzDocumentPays)
                    .Include(wag_doc => wag_doc.ArrivalUzVagons)
                        .ThenInclude(wag_cargo => wag_cargo.IdCargoNavigation)
                    .Include(wag_doc => wag_doc.ArrivalUzVagons)
                        .ThenInclude(wag_rent => wag_rent.IdWagonsRentArrivalNavigation)
                            .ThenInclude(wag_oper => wag_oper.IdOperatorNavigation)
                    .Include(wag_doc => wag_doc.ArrivalUzVagons)
                        .ThenInclude(wag_div => wag_div.IdDivisionOnAmkrNavigation)
                    .Include(wag_doc => wag_doc.ArrivalUzVagons)
                        .ThenInclude(arr_sost => arr_sost.IdArrivalNavigation)
                    .Include(wag_doc => wag_doc.ArrivalUzVagons)
                        .ThenInclude(wag_acts => wag_acts.ArrivalUzVagonActs)
                    .Include(wag_doc => wag_doc.ArrivalUzVagons)
                        .ThenInclude(wag_cont => wag_cont.ArrivalUzVagonConts)
                            .ThenInclude(wag_cont_pays => wag_cont_pays.ArrivalUzContPays)
                    .Include(wag_doc => wag_doc.ArrivalUzVagons)
                        .ThenInclude(wag_pays => wag_pays.ArrivalUzVagonPays)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: ArrivalUzDocument/accepted/start/2025-03-01T00:00:00/stop/2025-03-30T00:00:00
        [HttpGet("accepted/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult<ArrivalUzDocument>> GetArrivalUzDocument(DateTime start, DateTime stop)
        {
            try
            {
                IEnumerable<long> id_sts = db.ArrivalSostavs.AsNoTracking().Where(s => s.DateAdoption >= start && s.DateAdoption <= stop).Select(c => c.Id).ToList();
                IEnumerable<long> id_docs = db.ArrivalUzVagons.Where(v => id_sts.Contains(v.IdArrivalNavigation.Id)).Select(c => c.IdDocument).Distinct().ToList();
                var result = await db.ArrivalUzDocuments
                        .AsNoTracking()
                        .Where(x => id_docs.Contains(x.Id))
                        .Select(d=> new { d.Id, d.NomMainDoc, d.CalcPayer})
                        .ToListAsync();
                
                //IEnumerable<ArrivalUzDocument> result = await db.ArrivalUzDocuments
                //        .AsNoTracking()
                //        .Where(x => id_docs.Contains(x.Id))
                //        .ToListAsync();

                //IEnumerable<ArrivalUzDocument> result = await db.ArrivalUzDocuments
                //        .AsNoTracking()
                //        .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
                //        .Include(code_st_from => code_st_from.CodeStnFromNavigation)
                //        .Include(code_st_on => code_st_on.CodeStnToNavigation)
                //        .Include(code_cns => code_cns.CodeConsigneeNavigation)
                //        .Include(code_chp => code_chp.CodeShipperNavigation)
                //        .Include(code_ps => code_ps.CodePayerSenderNavigation)
                //        .Include(code_pa => code_pa.CodePayerArrivalNavigation)
                //        .Include(code_pl => code_pl.CodePayerLocalNavigation)
                //        .Include(doc => doc.ArrivalUzDocumentDocs)
                //        .Include(act => act.ArrivalUzDocumentActs)
                //        .Include(pays => pays.ArrivalUzDocumentPays)
                //        .Include(wag_doc => wag_doc.ArrivalUzVagons)
                //            .ThenInclude(arr_sost => arr_sost.IdArrivalNavigation)
                //        .Include(wag_doc => wag_doc.ArrivalUzVagons)
                //            .ThenInclude(wag_acts => wag_acts.ArrivalUzVagonActs)
                //        .Include(wag_doc => wag_doc.ArrivalUzVagons)
                //            .ThenInclude(wag_cont => wag_cont.ArrivalUzVagonConts)
                //                .ThenInclude(wag_cont_pays => wag_cont_pays.ArrivalUzContPays)
                //        .Include(wag_doc => wag_doc.ArrivalUzVagons)
                //            .ThenInclude(wag_pays => wag_pays.ArrivalUzVagonPays)
                //        .Where(x => id_docs.Contains(x.Id))
                //        .ToListAsync();

                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        //// POST: ArrivalUzDocument
        //// BODY: ArrivalUzDocument (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<ArrivalUzDocument>> PostArrivalUzDocument([FromBody] ArrivalUzDocument obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.ArrivalUzDocuments.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT ArrivalUzDocument/
        //// BODY: ArrivalUzDocument (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<ArrivalUzDocument>> PutArrivalUzDocument(ArrivalUzDocument obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.ArrivalUzDocuments.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE ArrivalUzDocument/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ArrivalUzDocument>> DeleteArrivalUzDocument(int id)
        //{
        //    ArrivalUzDocument result = db.ArrivalUzDocuments.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.ArrivalUzDocuments.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
