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
    public class ArrivalUzVagonController : ControllerBase
    {
        private EFDbContext db;

        public ArrivalUzVagonController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: ArrivalUzVagon
        [HttpGet]
        public async Task<ActionResult<ArrivalUzVagon>> GetArrivalUzVagon()
        {
            try
            {
                IEnumerable<ArrivalUzVagon> result = await db.ArrivalUzVagons.AsNoTracking().ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //// GET: ArrivalUzVagon/list/main_doc/start/2025-03-01T00:00:00/stop/2025-03-30T00:00:00
        //[HttpGet("list/main_doc/start/{start:DateTime}/stop/{stop:DateTime}")]

        // GET: ArrivalUzVagon/list
        //[HttpGet("list")]
        //public async Task<ActionResult<IEnumerable<ArrivalUzVagon>>> GetListArrivalUzVagon()
        //{
        //    try
        //    {
        //        //db.Database.CommandTimeout = 100;
        //        List<ArrivalUzVagon> result = await db.ArrivalUzVagons.FromSql($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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

        // GET: ArrivalUzVagon/document/851564
        [HttpGet("document/{id}")]
        public async Task<ActionResult<ArrivalUzVagon>> GetArrivalUzVagonOfIdDocument(int id)
        {
            try
            {
                IEnumerable<ArrivalUzVagon> result = await db.ArrivalUzVagons
                    .AsNoTracking()
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_bc => code_bc.CodeBorderCheckpointNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_st_from => code_st_from.CodeStnFromNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_st_on => code_st_on.CodeStnToNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_cns => code_cns.CodeConsigneeNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_chp => code_chp.CodeShipperNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_ps => code_ps.CodePayerSenderNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_pa => code_pa.CodePayerArrivalNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(code_pl => code_pl.CodePayerLocalNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(doc_doc => doc_doc.ArrivalUzDocumentDocs)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(doc_act => doc_act.ArrivalUzDocumentActs)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(doc_pay => doc_pay.ArrivalUzDocumentPays)
                    .Include(arr_sost => arr_sost.IdArrivalNavigation)
                    .Include(vag_act => vag_act.ArrivalUzVagonActs)
                    .Include(vag_pay => vag_pay.ArrivalUzVagonPays)
                    .Include(wag_cont => wag_cont.ArrivalUzVagonConts)
                        .ThenInclude(cont_pay => cont_pay.ArrivalUzContPays)
                    .Include(wag_cargo => wag_cargo.IdCargoNavigation)
                    .Include(wag_rent => wag_rent.IdWagonsRentArrivalNavigation)
                        .ThenInclude(wag_oper => wag_oper.IdOperatorNavigation)
                    .Include(wag_div => wag_div.IdDivisionOnAmkrNavigation)
                    .Where(x => x.IdDocument == id)
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
        /// <summary>
        /// Сверка по актам накладных на прибывшие вагоны
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        // GET: ArrivalUzVagon/verification/start/2025-04-01T00:00:00/stop/2025-04-17T23:59:59
        [HttpGet("verification/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult> GetVerificationArrivalUzVagon(DateTime start, DateTime stop)
        {
            try
            {
                IEnumerable<long> id_sts = db.ArrivalSostavs.AsNoTracking().Where(s => s.DateAdoption >= start && s.DateAdoption <= stop).Select(c => c.Id).ToList();
                IEnumerable<long> id_vags = db.ArrivalUzVagons.Where(v => id_sts.Contains(v.IdArrivalNavigation.Id) && v.IdDocumentNavigation.NomDoc == null && v.IdDocumentNavigation.NomMainDoc>0).Select(c => c.Id).Distinct().ToList();

                var result = await db.ArrivalUzVagons
                        .AsNoTracking()
                        .Where(x => id_vags.Contains(x.Id))
                        .Select(d => new
                        {
                            Id = d.Id,
                            NomMainDoc = d.IdDocumentNavigation.NomMainDoc,
                            NomDoc = d.IdDocumentNavigation.NomDoc,
                            Num = d.Num,
                            TariffContract = d.IdDocumentNavigation.TariffContract,
                            PayerLocalCode = d.IdDocumentNavigation.CodePayerLocalNavigation.Code,
                            PayerLocalNameRu = d.IdDocumentNavigation.CodePayerLocalNavigation.PayerNameRu,
                            PayerLocalNameEn = d.IdDocumentNavigation.CodePayerLocalNavigation.PayerNameEn,
                            ArrivalUZDocumentPay = d.IdDocumentNavigation.ArrivalUzDocumentPays.Where(w => w.Kod == "001").Sum(p => p.Summa),
                            Vesg = d.Vesg,
                            ArrivalCodeStnFrom = d.IdDocumentNavigation.CodeStnFromNavigation.Code,
                            ArrivalNameStnFromRu = d.IdDocumentNavigation.CodeStnFromNavigation.StationNameRu,
                            ArrivalNameStnFromEn = d.IdDocumentNavigation.CodeStnFromNavigation.StationNameEn,
                            ArrivalCodeStnTo = d.IdDocumentNavigation.CodeStnToNavigation.Code,
                            ArrivalNameStnToRu = d.IdDocumentNavigation.CodeStnToNavigation.StationNameRu,
                            ArrivalNameStnToEn = d.IdDocumentNavigation.CodeStnToNavigation.StationNameEn,
                            ArrivalIdCargo = (int?)d.IdCargoNavigation.Id,
                            ArrivalCargoNameRu = d.IdCargoNavigation.CargoNameRu,
                            ArrivalCargoNameEn = d.IdCargoNavigation.CargoNameEn,
                            ArrivalIdOperator = (int?)d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.Id,
                            //ArrivalOperatorRu = d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.OperatorsRu,
                            //ArrivalOperatorEn = d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.OperatorsEn,
                            ArrivalOperatorAbbrRu = d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrRu,
                            ArrivalOperatorAbbrEn = d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrEn,
                            ArrivalIdDivision = (int?)d.IdDivisionOnAmkrNavigation.Id,
                            ArrivalNameDivisionRu = d.IdDivisionOnAmkrNavigation.NameDivisionRu,
                            ArrivalNameDivisionEn = d.IdDivisionOnAmkrNavigation.NameDivisionEn,
                            ArrivalDivisionAbbrRu = d.IdDivisionOnAmkrNavigation.DivisionAbbrRu,
                            ArrivalDivisionAbbrEn = d.IdDivisionOnAmkrNavigation.DivisionAbbrEn,
                            PayerSenderCode = d.IdDocumentNavigation.CodePayerSenderNavigation.Code,
                            PayerSenderNameRu = d.IdDocumentNavigation.CodePayerSenderNavigation.PayerNameRu,
                            PayerSenderNameEn = d.IdDocumentNavigation.CodePayerSenderNavigation.PayerNameEn,
                            PayerArrivalCode = d.IdDocumentNavigation.CodePayerArrivalNavigation.Code,
                            PayerArrivalNameRu = d.IdDocumentNavigation.CodePayerArrivalNavigation.PayerNameRu,
                            PayerArrivalNameEn = d.IdDocumentNavigation.CodePayerArrivalNavigation.PayerNameEn,
                            DateVid = d.IdDocumentNavigation.DateVid,
                            DateAdoption = d.IdArrivalNavigation.DateAdoption,
                            CalcPayer = d.IdDocumentNavigation.CalcPayer,
                            CalcPayerUser = d.IdDocumentNavigation.CalcPayerUser,
                            IdActServices1 = d.IdActServices1,
                            NumActServices1 = d.NumActServices1,
                            IdActServices2 = d.IdActServices2,
                            NumActServices2 = d.NumActServices2,
                            IdActServices3 = d.IdActServices3,
                            NumActServices3 = d.NumActServices3,
                            Verification = d.Verification,
                            VerificationUser = d.VerificationUser,
                        })
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



        //// POST: ArrivalUzVagon
        //// BODY: ArrivalUzVagon (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<ArrivalUzVagon>> PostArrivalUzVagon([FromBody] ArrivalUzVagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.ArrivalUzVagons.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT ArrivalUzVagon/
        //// BODY: ArrivalUzVagon (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<ArrivalUzVagon>> PutArrivalUzVagon(ArrivalUzVagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.ArrivalUzVagons.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE ArrivalUzVagon/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<ArrivalUzVagon>> DeleteArrivalUzVagon(int id)
        //{
        //    ArrivalUzVagon result = db.ArrivalUzVagons.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.ArrivalUzVagons.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
