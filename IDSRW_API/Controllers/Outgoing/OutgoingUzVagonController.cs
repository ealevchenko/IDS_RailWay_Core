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
    public class OutgoingUzVagonController : ControllerBase
    {
        private EFDbContext db;

        public OutgoingUzVagonController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: OutgoingUzVagon
        [HttpGet]
        public async Task<ActionResult<OutgoingUzVagon>> GetOutgoingUzVagon()
        {
            try
            {
                IEnumerable<OutgoingUzVagon> result = await db.OutgoingUzVagons.AsNoTracking().ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        // GET: OutgoingUzVagon/document/536848
        [HttpGet("document/{id}")]
        public async Task<ActionResult<OutgoingUzVagon>> GetOutgoingUzVagonOfIdDocument(int id)
        {
            try
            {
                IEnumerable<OutgoingUzVagon> result = await db.OutgoingUzVagons
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
                        .ThenInclude(code_ps => code_ps.CodePayerNavigation)
                    .Include(doc => doc.IdDocumentNavigation)
                        .ThenInclude(doc_pay => doc_pay.OutgoingUzDocumentPays)
                    .Include(arr_sost => arr_sost.IdOutgoingNavigation)
                    .Include(vag_act => vag_act.OutgoingUzVagonActs)
                    .Include(vag_pay => vag_pay.OutgoingUzVagonPays)
                    .Include(wag_cont => wag_cont.OutgoingUzVagonConts)
                        .ThenInclude(cont_pay => cont_pay.OutgoingUzContPays)
                    .Include(wag_cargo => wag_cargo.IdCargoNavigation)
                    .Include(wag_arr_rent => wag_arr_rent.IdWagonsRentArrivalNavigation)
                    .Include(wag_out_rent => wag_out_rent.IdWagonsRentOutgoingNavigation)
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
        /// Реестр отправленных вагонов
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        // GET: OutgoingUzVagon/register/start/2025-04-01T00:00:00/stop/2025-04-30T23:59:59
        [HttpGet("register/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult> GetRegisterOutgoingUzVagon(DateTime start, DateTime stop)
        {
            try
            {
                IEnumerable<long> id_sts = db.OutgoingSostavs.AsNoTracking().Where(s => s.DateOutgoing >= start && s.DateOutgoing <= stop).Select(c => c.Id).ToList();
                IEnumerable<long> id_vags = db.OutgoingUzVagons.Where(v => v.IdDocument != null && id_sts.Contains(v.IdOutgoingNavigation.Id) && v.IdDocumentNavigation.NomDoc > 0).Select(c => c.Id).Distinct().ToList();

                var result = await db.OutgoingUzVagons
                        .AsNoTracking()
                        .Where(x => id_vags.Contains(x.Id))
                        .Select(d => new
                        {
                            Id = d.Id,
                            NomDoc = d.IdDocumentNavigation.NomDoc,
                            Num = d.Num,
                            //TariffContract = d.IdDocumentNavigation.TariffContract,
                            PayerSenderCode = d.IdDocumentNavigation.CodePayerNavigation.Code,
                            PayerSenderNameRu = d.IdDocumentNavigation.CodePayerNavigation.PayerNameRu,
                            PayerSenderNameEn = d.IdDocumentNavigation.CodePayerNavigation.PayerNameEn,
                            OutgoingUZDocumentPay = d.IdDocumentNavigation.OutgoingUzDocumentPays.Where(w => w.Kod == "001").Sum(p => p.Summa),
                            OutgoingUZDocumentPayAdd = d.IdDocumentNavigation.OutgoingUzDocumentPays.Where(w => w.Kod != "001").Sum(p => p.Summa),
                            Vesg = d.Vesg,
                            OutgoingCodeStnFrom = (int?)(d.IdDocumentNavigation.CodeStnFromNavigation != null ? d.IdDocumentNavigation.CodeStnFromNavigation.Code : null),
                            OutgoingNameStnFromRu = d.IdDocumentNavigation.CodeStnFromNavigation != null ? d.IdDocumentNavigation.CodeStnFromNavigation.StationNameRu : null,
                            OutgoingNameStnFromEn = d.IdDocumentNavigation.CodeStnFromNavigation != null ? d.IdDocumentNavigation.CodeStnFromNavigation.StationNameEn : null,
                            OutgoingCodeStnTo = (int?)(d.IdDocumentNavigation.CodeStnToNavigation != null ? d.IdDocumentNavigation.CodeStnToNavigation.Code : null),
                            OutgoingNameStnToRu = d.IdDocumentNavigation.CodeStnToNavigation != null ? d.IdDocumentNavigation.CodeStnToNavigation.StationNameRu : null,
                            OutgoingNameStnToEn = d.IdDocumentNavigation.CodeStnToNavigation != null ? d.IdDocumentNavigation.CodeStnToNavigation.StationNameEn : null,
                            OutgoingIdCargo = (int?)d.IdCargoNavigation.Id,
                            OutgoingCargoNameRu = d.IdCargoNavigation.CargoNameRu,
                            OutgoingCargoNameEn = d.IdCargoNavigation.CargoNameEn,
                            ArrivalIdOperator = (int?)d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.Id,
                            ArrivalOperatorAbbrRu = d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrRu,
                            ArrivalOperatorAbbrEn = d.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrEn,
                            OutgoingIdOperator = (int?)d.IdWagonsRentOutgoingNavigation.IdOperatorNavigation.Id,
                            OutgoingOperatorAbbrRu = d.IdWagonsRentOutgoingNavigation.IdOperatorNavigation.AbbrRu,
                            OutgoingOperatorAbbrEn = d.IdWagonsRentOutgoingNavigation.IdOperatorNavigation.AbbrEn,
                            DateVid = d.IdDocumentNavigation.DateVid,
                            DateOutgoing = d.IdOutgoingNavigation.DateOutgoing,
                            DateOutgoingAct = d.IdOutgoingNavigation.DateOutgoingAct,
                            //CalcPayer = d.IdDocumentNavigation.CalcPayer,
                            //CalcPayerUser = d.IdDocumentNavigation.CalcPayerUser,

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



        //// POST: OutgoingUzVagon
        //// BODY: OutgoingUzVagon (JSON, XML)
        //[HttpPost]
        //public async Task<ActionResult<OutgoingUzVagon>> PostOutgoingUzVagon([FromBody] OutgoingUzVagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    db.OutgoingUzVagons.Add(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// PUT OutgoingUzVagon/
        //// BODY: OutgoingUzVagon (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<OutgoingUzVagon>> PutOutgoingUzVagon(OutgoingUzVagon obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.OutgoingUzVagons.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE OutgoingUzVagon/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<OutgoingUzVagon>> DeleteOutgoingUzVagon(int id)
        //{
        //    OutgoingUzVagon result = db.OutgoingUzVagons.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.OutgoingUzVagons.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
