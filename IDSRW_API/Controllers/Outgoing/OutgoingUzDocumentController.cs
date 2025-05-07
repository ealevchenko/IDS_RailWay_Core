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
using IDS_;

namespace WebAPI.Controllers.Directory
{
    #region 
    public class UpdateOutgoingUzDocumentPay
    {
        public long id_document { get; set; }
        public int summa { get; set; }
        public string kod { get; set; }
    }
    //public class UpdatePayerLocal
    //{
    //    public long id_document { get; set; }
    //    public string code_payer_local { get; set; }
    //    public decimal? tariff_contract { get; set; }
    //}

    //public class UpdateVerification
    //{
    //    public List<long> id_docs { get; set; }
    //    public int presented { get; set; }
    //    public string? num_act { get; set; }
    //}

    #endregion

    [Route("[controller]")]
    [ApiController]
    public class OutgoingUzDocumentController : ControllerBase
    {
        private EFDbContext db;

        public OutgoingUzDocumentController(EFDbContext db)
        {
            this.db = db;
        }
        // GET: OutgoingUzDocument
        [HttpGet]
        public async Task<ActionResult<OutgoingUzDocument>> GetOutgoingUzDocument()
        {
            try
            {
                IEnumerable<OutgoingUzDocument> result = await db.OutgoingUzDocuments.AsNoTracking().ToListAsync();
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        // GET: OutgoingUzDocument/list/main_doc/start/2025-04-01T00:00:00/stop/2025-04-30T00:00:00
        [HttpGet("list/main_doc/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult> GetListMainDocOutgoingUzDocument(DateTime start, DateTime stop)
        {
            try
            {
                IEnumerable<long> id_sts = db.OutgoingSostavs.AsNoTracking().Where(s => s.DateOutgoing >= start && s.DateOutgoing <= stop).Select(c => c.Id).ToList();
                IEnumerable<long?> id_docs = db.OutgoingUzVagons.Where(v => id_sts.Contains(v.IdOutgoingNavigation.Id)).Select(c => c.IdDocument).Distinct().ToList();
                var result = await db.OutgoingUzDocuments
                        .AsNoTracking()
                        .Where(x => id_docs.Contains(x.Id))
                        .Select(d => new { d.Id, d.NomDoc, d.CodePayer })
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

        // GET: OutgoingUzDocument/register/start/2025-05-05T00:00:00/stop/2025-05-05T00:00:00
        [HttpGet("register/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult> GetRegisterOutgoingUzDocument(DateTime start, DateTime stop)
        {
            try
            {
                IEnumerable<long> id_sts = db.OutgoingSostavs.AsNoTracking().Where(s => s.DateOutgoing >= start && s.DateOutgoing <= stop).Select(c => c.Id).ToList();
                IEnumerable<long?> id_docs = db.OutgoingUzVagons.AsNoTracking().Where(v => id_sts.Contains(v.IdOutgoingNavigation.Id)).Select(c => c.IdDocument).Distinct().ToList();
                var result = await db.OutgoingUzDocuments
                        //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .AsNoTracking()
                        .Where(x => id_docs.Contains(x.Id) && x.NomDoc > 0)
                        .Select(d => new
                        {
                            Id = d.Id,
                            NomDoc = d.NomDoc,
                            OutgoingUzVagons = d.OutgoingUzVagons.Select(w => new
                            {
                                Id = w.Id,
                                Num = w.Num,
                                OutgoingIdCargo = (int?)w.IdCargoNavigation.Id,
                                OutgoingCargoNameRu = w.IdCargoNavigation.CargoNameRu,
                                OutgoingCargoNameEn = w.IdCargoNavigation.CargoNameEn,
                                Vesg = w.Vesg,
                                ArrivalIdOperator = (int?)w.IdWagonsRentArrivalNavigation.IdOperatorNavigation.Id,
                                ArrivalOperatorAbbrRu = w.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrRu,
                                ArrivalOperatorAbbrEn = w.IdWagonsRentArrivalNavigation.IdOperatorNavigation.AbbrEn,
                                OutgoingIdOperator = (int?)w.IdWagonsRentOutgoingNavigation.IdOperatorNavigation.Id,
                                OutgoingOperatorAbbrRu = w.IdWagonsRentOutgoingNavigation.IdOperatorNavigation.AbbrRu,
                                OutgoingOperatorAbbrEn = w.IdWagonsRentOutgoingNavigation.IdOperatorNavigation.AbbrEn,
                                RodUz = w.IdGenusNavigation.RodUz,
                                RodAbbrRu = w.IdGenusNavigation.AbbrRu,
                                RodAbbrEn = w.IdGenusNavigation.AbbrEn,
                                OutgoingUzVagonPays = w.OutgoingUzVagonPays.Where(w => w.Kod == "001").Sum(p => p.Summa),
                                OutgoingUzVagonPaysAdd = w.OutgoingUzVagonPays.Where(w => w.Kod != "001").Sum(p => p.Summa),
                                DateReadinessUz = w.IdOutgoingNavigation.DateReadinessUz,
                                DateReadinessAmkr = w.IdOutgoingNavigation.DateReadinessAmkr,
                                DateOutgoing = w.IdOutgoingNavigation.DateOutgoing,
                                DateOutgoingAct = w.IdOutgoingNavigation.DateOutgoingAct,
                                DateDepartureAmkr = w.IdOutgoingNavigation.DateDepartureAmkr,
                                KolConductor = w.KolConductor,
                            }),
                            Vesg = d.OutgoingUzVagons.Where(w=>w.Vesg != null).Sum(p=>p.Vesg),
                            PayerSenderCode = d.CodePayerNavigation.Code,
                            PayerSenderNameRu = d.CodePayerNavigation.PayerNameRu,
                            PayerSenderNameEn = d.CodePayerNavigation.PayerNameEn,
                            OutgoingUZDocumentPay = d.OutgoingUzDocumentPays.Where(w => w.Kod == "001").Sum(p => p.Summa),
                            OutgoingUZDocumentPayAdd = d.OutgoingUzDocumentPays.Where(w => w.Kod != "001").Sum(p => p.Summa),
                            //OutgoingUzVagonPays = d.OutgoingUzVagons.Where(w => w.OutgoingUzVagonPays != null).Sum(p => p.OutgoingUzVagonPays),
                            //OutgoingUzVagonPaysAdd = d.OutgoingUzVagons.Where(w => w.OutgoingUzVagonPaysAdd != null).Sum(p => p.OutgoingUzVagonPaysAdd),
                            OutgoingCodeStnFrom = (int?)(d.CodeStnFromNavigation != null ? d.CodeStnFromNavigation.Code : null),
                            OutgoingNameStnFromRu = d.CodeStnFromNavigation != null ? d.CodeStnFromNavigation.StationNameRu : null,
                            OutgoingNameStnFromEn = d.CodeStnFromNavigation != null ? d.CodeStnFromNavigation.StationNameEn : null,
                            OutgoingCodeStnTo = (int?)(d.CodeStnToNavigation != null ? d.CodeStnToNavigation.Code : null),
                            OutgoingNameStnToRu = d.CodeStnToNavigation != null ? d.CodeStnToNavigation.StationNameRu : null,
                            OutgoingNameStnToEn = d.CodeStnToNavigation != null ? d.CodeStnToNavigation.StationNameEn : null,
                            InlandrailwayCode = d.CodeStnToNavigation.CodeInlandrailwayNavigation.Code,
                            InlandrailwayAbbrRu = d.CodeStnToNavigation.CodeInlandrailwayNavigation.InlandrailwayAbbrRu,
                            InlandrailwayAbbrEn = d.CodeStnToNavigation.CodeInlandrailwayNavigation.InlandrailwayAbbrEn,
                            DistanceWay = d.DistanceWay,
                            TariffContract = d.TariffContract,
                            CalcPayer = d.CalcPayer,
                            CalcPayerUser = d.CalcPayerUser,
                            NumList = d.NumList,
                            DateList = d.DateList,
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

        // GET: OutgoingUzDocument/list
        //[HttpGet("list")]
        //public async Task<ActionResult<IEnumerable<OutgoingUzDocument>>> GetListOutgoingUzDocument()
        //{
        //    try
        //    {
        //        //db.Database.CommandTimeout = 100;
        //        List<OutgoingUzDocument> result = await db.OutgoingUzDocuments.FromSql($"select * from [IDS].[Directory_Cargo]").ToListAsync();
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
        // GET: OutgoingUzDocument/536848
        [HttpGet("{id}")]
        public async Task<ActionResult<OutgoingUzDocument>> GetOutgoingUzDocument(int id)
        {
            try
            {
                OutgoingUzDocument? result = await db.OutgoingUzDocuments
                    .AsNoTracking()
                    .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
                    .Include(code_st_from => code_st_from.CodeStnFromNavigation)
                    .Include(code_st_on => code_st_on.CodeStnToNavigation)
                    .Include(code_cns => code_cns.CodeConsigneeNavigation)
                    .Include(code_chp => code_chp.CodeShipperNavigation)
                    .Include(code_ps => code_ps.CodePayerNavigation)
                    .Include(pays => pays.OutgoingUzDocumentPays)
                    .Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .ThenInclude(out_sost => out_sost.IdOutgoingNavigation)
                    .Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .ThenInclude(wag_cargo => wag_cargo.IdCargoNavigation)
                    .Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .ThenInclude(wag_arr_rent => wag_arr_rent.IdWagonsRentArrivalNavigation)
                            .ThenInclude(wag_arr_oper => wag_arr_oper.IdOperatorNavigation)
                    .Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .ThenInclude(wag_out_rent => wag_out_rent.IdWagonsRentOutgoingNavigation)
                            .ThenInclude(wag_out_oper => wag_out_oper.IdOperatorNavigation)
                    .Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .ThenInclude(wag_acts => wag_acts.OutgoingUzVagonActs)
                    .Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .ThenInclude(wag_cont => wag_cont.OutgoingUzVagonConts)
                            .ThenInclude(wag_cont_pays => wag_cont_pays.OutgoingUzContPays)
                    .Include(wag_doc => wag_doc.OutgoingUzVagons)
                        .ThenInclude(wag_pays => wag_pays.OutgoingUzVagonPays)
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

        //#region СВЕРКА ДОКУМЕНТОВ ПО ПРИБЫТИЮ
        ///// <summary>
        ///// Сверака документов по прибытию получение документа по id 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //// GET: OutgoingUzDocument/verification/id/932003
        //[HttpGet("verification/id/{id}")]
        //public async Task<ActionResult<OutgoingUzDocument>> GetVerificationOutgoingUzDocument(long id)
        //{
        //    try
        //    {
        //        var result = await db.OutgoingUzDocuments
        //                .AsNoTracking()
        //                .Where(x => x.Id == id)
        //                .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
        //                .Include(code_st_from => code_st_from.CodeStnFromNavigation)
        //                .Include(code_st_on => code_st_on.CodeStnToNavigation)
        //                .Include(code_cns => code_cns.CodeConsigneeNavigation)
        //                .Include(code_chp => code_chp.CodeShipperNavigation)
        //                .Include(code_ps => code_ps.CodePayerSenderNavigation)
        //                .Include(code_pa => code_pa.CodePayerArrivalNavigation)
        //                .Include(code_pl => code_pl.CodePayerLocalNavigation)
        //                .Include(doc => doc.OutgoingUzDocumentDocs)
        //                .Include(act => act.OutgoingUzDocumentActs)
        //                .Include(pays => pays.OutgoingUzDocumentPays)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(out_sost => out_sost.IdArrivalNavigation)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_acts => wag_acts.OutgoingUzVagonActs)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_cont => wag_cont.OutgoingUzVagonConts)
        //                        .ThenInclude(wag_cont_pays => wag_cont_pays.OutgoingUzContPays)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_pays => wag_pays.OutgoingUzVagonPays)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_cargo => wag_cargo.IdCargoNavigation)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_rent => wag_rent.IdWagonsRentArrivalNavigation)
        //                        .ThenInclude(wag_oper => wag_oper.IdOperatorNavigation)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_div => wag_div.IdDivisionOnAmkrNavigation)
        //                .FirstOrDefaultAsync();

        //        if (result == null)
        //            return NotFound();
        //        return new ObjectResult(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
        ///// <summary>
        ///// Сверака документов по прибытию получение документа за период 
        ///// </summary>
        ///// <param name="start"></param>
        ///// <param name="stop"></param>
        ///// <returns></returns>
        //// GET: OutgoingUzDocument/verification/start/2025-03-01T00:00:00/stop/2025-03-30T00:00:00
        //[HttpGet("verification/start/{start:DateTime}/stop/{stop:DateTime}")]
        //public async Task<ActionResult<OutgoingUzDocument>> GetVerificationOutgoingUzDocument(DateTime start, DateTime stop)
        //{
        //    try
        //    {
        //        IEnumerable<long> id_sts = db.OutgoingSostavs.AsNoTracking().Where(s => s.DateAdoption >= start && s.DateAdoption <= stop).Select(c => c.Id).ToList();
        //        IEnumerable<long> id_docs = db.OutgoingUzVagons.Where(v => id_sts.Contains(v.IdArrivalNavigation.Id)).Select(c => c.IdDocument).Distinct().ToList();
        //        var result = await db.OutgoingUzDocuments
        //                .AsNoTracking()
        //                .Where(x => id_docs.Contains(x.Id) && x.CalcPayer!=null)
        //                .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
        //                .Include(code_st_from => code_st_from.CodeStnFromNavigation)
        //                .Include(code_st_on => code_st_on.CodeStnToNavigation)
        //                .Include(code_cns => code_cns.CodeConsigneeNavigation)
        //                .Include(code_chp => code_chp.CodeShipperNavigation)
        //                .Include(code_ps => code_ps.CodePayerSenderNavigation)
        //                .Include(code_pa => code_pa.CodePayerArrivalNavigation)
        //                .Include(code_pl => code_pl.CodePayerLocalNavigation)
        //                .Include(doc => doc.OutgoingUzDocumentDocs)
        //                .Include(act => act.OutgoingUzDocumentActs)
        //                .Include(pays => pays.OutgoingUzDocumentPays)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(out_sost => out_sost.IdArrivalNavigation)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_acts => wag_acts.OutgoingUzVagonActs)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_cont => wag_cont.OutgoingUzVagonConts)
        //                        .ThenInclude(wag_cont_pays => wag_cont_pays.OutgoingUzContPays)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_pays => wag_pays.OutgoingUzVagonPays)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_cargo => wag_cargo.IdCargoNavigation)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_rent => wag_rent.IdWagonsRentArrivalNavigation)
        //                        .ThenInclude(wag_oper => wag_oper.IdOperatorNavigation)
        //                .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //                    .ThenInclude(wag_div => wag_div.IdDivisionOnAmkrNavigation)
        //                .ToListAsync();

        //        if (result == null)
        //            return NotFound();
        //        return new ObjectResult(result);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //// POST: OutgoingUzDocument/update/verification
        //// BODY: OutgoingUzDocument/update/verification (JSON, XML)
        //[HttpPost("update/verification")]
        //public async Task<int> PostVerificationOutgoingUzDocument([FromBody] UpdateVerification value)
        //{
        //    try
        //    {
        //        string user = HttpContext.User.Identity.Name;
        //        bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
        //        // Пользователь непрошел интендификацию!
        //        if (value == null || !IsAuthenticated) { return -1; }
        //        if (value.presented < 1 || value.presented > 3) return (int)errors_base.error_input_value;
        //        List<OutgoingUzDocument>? list = await db.OutgoingUzDocuments
        //            .Where(d => value.id_docs.Contains(d.Id))
        //            .ToListAsync();
        //        if (list != null)
        //        {
        //            foreach (OutgoingUzDocument doc in list) {

        //                if (value.presented == 1) {
        //                    doc.NumActServices1 = value.num_act;
        //                }
        //                if (value.presented == 2) {
        //                    doc.NumActServices2 = value.num_act;
        //                }
        //                if (value.presented == 3) {
        //                    doc.NumActServices3 = value.num_act;
        //                }
        //                doc.Verification = DateTime.Now;
        //                doc.VerificationUser = user;
        //            }
        //            return await db.SaveChangesAsync();
        //        }
        //        else
        //        {
        //            return (int)errors_base.not_inp_uz_doc_db;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return (int)errors_base.global;
        //    }
        //}
        //#endregion

        // POST: OutgoingUzDocument/update/pay
        // BODY: OutgoingUzDocument/update/pay (JSON, XML)
        //[HttpPost("update/pay")]
        //public async Task<int> PostOutgoingUzDocumentPay([FromBody] UpdateOutgoingUzDocumentPay value)
        //{
        //    try
        //    {
        //        OutgoingUzDocument? result = await db.OutgoingUzDocuments
        //            .Include(doc => doc.OutgoingUzDocumentDocs)
        //            .Include(act => act.OutgoingUzDocumentActs)
        //            .Include(pays => pays.OutgoingUzDocumentPays)
        //            .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //            .FirstOrDefaultAsync(x => x.Id == value.id_document);
        //        if (result != null)
        //        {
        //            IEnumerable<OutgoingUzDocumentPay> pays = result.OutgoingUzDocumentPays
        //                .Where(d => d.Kod == value.kod)
        //                .ToList();

        //            OutgoingUzDocumentPay new_pay = new OutgoingUzDocumentPay()
        //            {
        //                Id = 0,
        //                IdDocument = value.id_document,
        //                CodePayer = result.CodePayerSender != null ? int.Parse(result.CodePayerSender) : 0,
        //                Kod = value.kod,
        //                Summa = value.summa,
        //                TypePayer = 0
        //            };

        //            if (pays == null || pays.Count() == 0)
        //            {
        //                result.OutgoingUzDocumentPays.Add(new_pay);
        //            }
        //            else
        //            {
        //                if (pays.Count() > 2)
        //                {
        //                    // Удалить
        //                    foreach (OutgoingUzDocumentPay pay in pays)
        //                    {
        //                        result.OutgoingUzDocumentPays.Remove(pay);
        //                    }
        //                    result.OutgoingUzDocumentPays.Add(new_pay);
        //                }
        //                else
        //                {
        //                    pays.ToList()[0].Summa = value.summa;
        //                }
        //            }
        //            return await db.SaveChangesAsync();

        //        }
        //        else
        //        {
        //            return (int)errors_base.not_inp_uz_vag_db;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return (int)errors_base.global;
        //    }
        //}

        //// POST: OutgoingUzDocument/update/payer_local
        //// BODY: OutgoingUzDocument/update/payer_local (JSON, XML)
        //[HttpPost("update/payer_local")]
        //public async Task<ActionResult<int>> PostOutgoingUzDocumentPayerLocal([FromBody] UpdatePayerLocal value)
        //{
        //    try
        //    {
        //        string user = HttpContext.User.Identity.Name;
        //        bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

        //        if (value == null || !IsAuthenticated)
        //        {
        //            //return (int)errors_base.error_authenticated;
        //            return Unauthorized();
        //        }

        //        OutgoingUzDocument? result = await db.OutgoingUzDocuments
        //            .Include(doc => doc.OutgoingUzDocumentDocs)
        //            .Include(act => act.OutgoingUzDocumentActs)
        //            .Include(pays => pays.OutgoingUzDocumentPays)
        //            .Include(wag_doc => wag_doc.OutgoingUzVagons)
        //            .FirstOrDefaultAsync(x => x.Id == value.id_document);
        //        if (result != null)
        //        {
        //            result.CodePayerLocal = value.code_payer_local;
        //            result.TariffContract = value.tariff_contract;
        //            result.CalcPayerUser = user;
        //            result.CalcPayer = DateTime.Now;

        //            return await db.SaveChangesAsync();

        //        }
        //        else
        //        {
        //            return (int)errors_base.not_inp_uz_vag_db;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return (int)errors_base.global;
        //    }
        //}


        //// PUT OutgoingUzDocument/
        //// BODY: OutgoingUzDocument (JSON, XML)
        //[HttpPut]
        //public async Task<ActionResult<OutgoingUzDocument>> PutOutgoingUzDocument(OutgoingUzDocument obj)
        //{
        //    if (obj == null)
        //    {
        //        return BadRequest();
        //    }
        //    if (!db.OutgoingUzDocuments.Any(x => x.Id == obj.Id))
        //    {
        //        return NotFound();
        //    }

        //    db.Update(obj);
        //    await db.SaveChangesAsync();
        //    return Ok(obj);
        //}

        //// DELETE OutgoingUzDocument/[id]
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<OutgoingUzDocument>> DeleteOutgoingUzDocument(int id)
        //{
        //    OutgoingUzDocument result = db.OutgoingUzDocuments.FirstOrDefault(x => x.Id == id);
        //    if (result == null)
        //    {
        //        return NotFound();
        //    }
        //    db.OutgoingUzDocuments.Remove(result);
        //    await db.SaveChangesAsync();
        //    return Ok(result);
        //}
    }
}
