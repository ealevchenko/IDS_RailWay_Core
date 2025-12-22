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

    public class UpdatePay
    {
        public long id_document { get; set; }
        public int? type { get; set; }
        public long? value { get; set; }
    }

    public class UpdateVerificationOutgoing
    {
        public List<long> id_docs { get; set; }
        public int? num_list { get; set; }
        public DateTime? date_list { get; set; }
    }

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
                db.Database.SetCommandTimeout(300);
                IEnumerable<long> id_sts = db.OutgoingSostavs.AsNoTracking().Where(s => s.DateOutgoing >= start && s.DateOutgoing <= stop).Select(c => c.Id).ToList();
                IEnumerable<long?> id_docs = db.OutgoingUzVagons.Where(v => id_sts.Contains(v.IdOutgoingNavigation.Id)).Select(c => c.IdDocument).Distinct().ToList();
                var result = await db.OutgoingUzDocuments
                        .AsNoTracking()
                        .Where(x => id_docs.Contains(x.Id))
                        .Select(d => new { d.Id, d.NomDoc, d.CodePayer })
                        .ToListAsync();
                db.Database.SetCommandTimeout(0);
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
                db.Database.SetCommandTimeout(300);
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
                            Vesg = d.OutgoingUzVagons.Where(w => w.Vesg != null).Sum(p => p.Vesg),
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
                db.Database.SetCommandTimeout(0);
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: OutgoingUzDocument/register/id/536848
        [HttpGet("register/id/{id}")]
        public async Task<ActionResult> GetRegisterOutgoingUzDocumentOfId(int id)
        {
            try
            {
                var result = await db.OutgoingUzDocuments
                         //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                         .AsNoTracking()
                         .Where(x => x.Id == id)
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
                             Vesg = d.OutgoingUzVagons.Where(w => w.Vesg != null).Sum(p => p.Vesg),
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
                         .FirstOrDefaultAsync();
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: OutgoingUzDocument/verification/start/2025-05-15T00:00:00/stop/2025-05-15T23:59:59
        [HttpGet("verification/start/{start:DateTime}/stop/{stop:DateTime}")]
        public async Task<ActionResult> GetVerificationOutgoingUzDocument(DateTime start, DateTime stop)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                IEnumerable<long> id_sts = db.OutgoingSostavs.AsNoTracking().Where(s => s.DateOutgoing >= start && s.DateOutgoing <= stop).Select(c => c.Id).ToList();
                IEnumerable<long?> id_docs = db.OutgoingUzVagons.AsNoTracking().Where(v => id_sts.Contains(v.IdOutgoingNavigation.Id)).Select(c => c.IdDocument).Distinct().ToList();
                var result = await db.OutgoingUzDocuments
                        .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
                        .Include(code_st_from => code_st_from.CodeStnFromNavigation)
                        .Include(code_st_on => code_st_on.CodeStnToNavigation)
                        .Include(code_cns => code_cns.CodeConsigneeNavigation)
                        .Include(code_chp => code_chp.CodeShipperNavigation)
                        .Include(code_pa => code_pa.CodePayerNavigation)
                        .Include(pays => pays.OutgoingUzDocumentPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(out_sost => out_sost.IdOutgoingNavigation)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_acts => wag_acts.OutgoingUzVagonActs)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_cont => wag_cont.OutgoingUzVagonConts)
                                .ThenInclude(wag_cont_pays => wag_cont_pays.OutgoingUzContPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_pays => wag_pays.OutgoingUzVagonPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_cargo => wag_cargo.IdCargoNavigation)
                                .ThenInclude(wag_cargo_etsng => wag_cargo_etsng.IdCargoEtsngNavigation)
                        //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                        //    .ThenInclude(wag_rent_arr => wag_rent_arr.IdWagonsRentArrivalNavigation)
                        //        .ThenInclude(wag_oper_arr => wag_oper_arr.IdOperatorNavigation)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_rent_out => wag_rent_out.IdWagonsRentOutgoingNavigation)
                                .ThenInclude(wag_oper_out => wag_oper_out.IdOperatorNavigation)
                        .AsNoTracking()
                        .Where(x => id_docs.Contains(x.Id) && x.NomDoc > 0 && x.CalcPayer != null)
                        .ToListAsync();
                db.Database.SetCommandTimeout(0);
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: OutgoingUzDocument/verification/id/536848
        [HttpGet("verification/id/{id}")]
        public async Task<ActionResult> GetVerificationOutgoingUzDocumentOfId(int id)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                var result = await db.OutgoingUzDocuments
                        .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
                        .Include(code_st_from => code_st_from.CodeStnFromNavigation)
                        .Include(code_st_on => code_st_on.CodeStnToNavigation)
                        .Include(code_cns => code_cns.CodeConsigneeNavigation)
                        .Include(code_chp => code_chp.CodeShipperNavigation)
                        .Include(code_pa => code_pa.CodePayerNavigation)
                           .Include(pays => pays.OutgoingUzDocumentPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(out_sost => out_sost.IdOutgoingNavigation)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_acts => wag_acts.OutgoingUzVagonActs)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_cont => wag_cont.OutgoingUzVagonConts)
                                .ThenInclude(wag_cont_pays => wag_cont_pays.OutgoingUzContPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_pays => wag_pays.OutgoingUzVagonPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_cargo => wag_cargo.IdCargoNavigation)
                                .ThenInclude(wag_cargo_etsng => wag_cargo_etsng.IdCargoEtsngNavigation)
                        //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                        //    .ThenInclude(wag_rent_arr => wag_rent_arr.IdWagonsRentArrivalNavigation)
                        //        .ThenInclude(wag_oper_arr => wag_oper_arr.IdOperatorNavigation)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_rent_out => wag_rent_out.IdWagonsRentOutgoingNavigation)
                                .ThenInclude(wag_oper_out => wag_oper_out.IdOperatorNavigation)
                        .AsNoTracking()
                         .Where(x => x.Id == id)
                         .FirstOrDefaultAsync();
                db.Database.SetCommandTimeout(0);
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        // GET: OutgoingUzDocument/verification/num/46228342
        [HttpGet("verification/num/{num}")]
        public async Task<ActionResult> GetVerificationOutgoingUzDocumentOfNum(int num)
        {
            try
            {
                db.Database.SetCommandTimeout(300);
                var result = await db.OutgoingUzDocuments
                        .Include(code_bc => code_bc.CodeBorderCheckpointNavigation)
                        .Include(code_st_from => code_st_from.CodeStnFromNavigation)
                        .Include(code_st_on => code_st_on.CodeStnToNavigation)
                        .Include(code_cns => code_cns.CodeConsigneeNavigation)
                        .Include(code_chp => code_chp.CodeShipperNavigation)
                        .Include(code_pa => code_pa.CodePayerNavigation)
                           .Include(pays => pays.OutgoingUzDocumentPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(out_sost => out_sost.IdOutgoingNavigation)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_acts => wag_acts.OutgoingUzVagonActs)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_cont => wag_cont.OutgoingUzVagonConts)
                                .ThenInclude(wag_cont_pays => wag_cont_pays.OutgoingUzContPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_pays => wag_pays.OutgoingUzVagonPays)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_cargo => wag_cargo.IdCargoNavigation)
                                .ThenInclude(wag_cargo_etsng => wag_cargo_etsng.IdCargoEtsngNavigation)
                        //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                        //    .ThenInclude(wag_rent_arr => wag_rent_arr.IdWagonsRentArrivalNavigation)
                        //        .ThenInclude(wag_oper_arr => wag_oper_arr.IdOperatorNavigation)
                        .Include(wag_doc => wag_doc.OutgoingUzVagons)
                            .ThenInclude(wag_rent_out => wag_rent_out.IdWagonsRentOutgoingNavigation)
                                .ThenInclude(wag_oper_out => wag_oper_out.IdOperatorNavigation)
                        .AsNoTracking()
                         .Where(x => x.NomDoc == num)
                         .ToListAsync();
                db.Database.SetCommandTimeout(0);
                if (result == null)
                    return NotFound();
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

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

        // POST: OutgoingUzDocument/update/pay
        // BODY: OutgoingUzDocument/update/pay (JSON, XML)
        [HttpPost("update/pay")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_TRF_SEND")]
        public async Task<ActionResult<int>> PostUpdatePayOutgoingUzDocument([FromBody] UpdatePay value)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;

                if (value == null || !IsAuthenticated)
                {
                    return Unauthorized();
                }

                //OutgoingUzDocument? result = await db.OutgoingUzDocuments
                //    .Include(pays => pays.OutgoingUzDocumentPays)
                //    //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                //    .FirstOrDefaultAsync(x => x.Id == value.id_document);
                OutgoingUzDocument? result = db.OutgoingUzDocuments
                    .Include(pays => pays.OutgoingUzDocumentPays)
                    //.Include(wag_doc => wag_doc.OutgoingUzVagons)
                    .FirstOrDefault(x => x.Id == value.id_document);

                if (result != null)
                {
                    // обновить OutgoingUzDocumentPays
                    if (value.type == 0)
                    {
                        if (String.IsNullOrWhiteSpace(result.CodePayer)) return (int)errors_base.error_out_uz_doc_not_code_payer;

                        //IEnumerable<OutgoingUzDocumentPay> pays = result.OutgoingUzDocumentPays
                        //    .Where(d => d.Kod == "001")
                        //    .ToList();

                        IEnumerable<OutgoingUzDocumentPay> pays = db.OutgoingUzDocumentPays
                            .Where(d => d.Kod == "001")
                            .ToList();

                        OutgoingUzDocumentPay pay_new = new OutgoingUzDocumentPay()
                        {
                            Id = 0,
                            IdDocument = value.id_document,
                            CodePayer = int.Parse(result.CodePayer),
                            Kod = "001",
                            Summa = value.value != null ? (long)value.value : 0,
                            TypePayer = 0
                        };
                        // Удалим старую запись
                        if (pays != null && pays.Count() > 0)
                        {
                            // Удалить
                            foreach (OutgoingUzDocumentPay pay in pays)
                            {
                                pay.IdDocumentNavigation = null;
                                //result.OutgoingUzDocumentPays.Remove(pay);
                                db.OutgoingUzDocumentPays.Remove(pay);
                            }

                        }
                        if (value.value != null)
                        {
                            //result.OutgoingUzDocumentPays.Add(pay_new);
                            db.OutgoingUzDocumentPays.Add(pay_new);
                        }
                        result.Change = DateTime.Now;
                        result.ChangeUser = user;
                        db.OutgoingUzDocuments.Update(result);
                        return await db.SaveChangesAsync();
                    }
                    // обновить тариф
                    if (value.type == 1 || value.type == 2)
                    {
                        result.TariffContract = (int?)value.value;
                        result.CalcPayerUser = user;
                        result.CalcPayer = DateTime.Now;
                        return await db.SaveChangesAsync();
                    }
                    return 0;

                }
                else
                {
                    return (int)errors_base.not_out_uz_doc_db;
                }
            }
            catch (Exception e)
            {
                return (int)errors_base.global;
            }
        }

        #region СВЕРКА ДОКУМЕНТОВ ПО ОТПРАВКЕ
        // POST: OutgoingUzDocument/update/verification
        // BODY: OutgoingUzDocument/update/verification (JSON, XML)
        [HttpPost("update/verification")]
        [Authorize(Roles = "KRR-LG_TD-IDSRW_ADMIN, KRR-LG_TD-IDSRW_DOK_SEND")]
        public async Task<int> PostVerificationOutgoingUzDocument([FromBody] UpdateVerificationOutgoing value)
        {
            try
            {
                string user = HttpContext.User.Identity.Name;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                // Пользователь непрошел интендификацию!
                if (value == null || !IsAuthenticated) { return -1; }
                //if (value.presented < 1 || value.presented > 3) return (int)errors_base.error_input_value;
                List<OutgoingUzDocument>? list = await db.OutgoingUzDocuments
                    .Where(d => value.id_docs.Contains(d.Id))
                    .ToListAsync();
                if (list != null)
                {
                    foreach (OutgoingUzDocument doc in list)
                    {
                        doc.NumList = value.num_list;
                        doc.DateList = value.date_list;
                        // если сбоасываем пустую строку, тогда не делаем отметку
                        if ((value.num_list == null && doc.Verification != null) || value.num_list != null)
                        {
                            doc.Verification = DateTime.Now;
                            doc.VerificationUser = user;
                        }

                    }
                    return await db.SaveChangesAsync();
                }
                else
                {
                    return (int)errors_base.not_inp_uz_doc_db;
                }
            }
            catch (Exception e)
            {
                return (int)errors_base.global;
            }
        }
        #endregion

    }
}
