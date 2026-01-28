using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("WagonUsageFee", Schema = "IDS")]
public partial class WagonUsageFee
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_wir")]
    public long IdWir { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_operator")]
    public int IdOperator { get; set; }

    [Column("id_genus")]
    public int IdGenus { get; set; }

    [Column("date_adoption", TypeName = "datetime")]
    public DateTime DateAdoption { get; set; }

    [Column("date_outgoing", TypeName = "datetime")]
    public DateTime DateOutgoing { get; set; }

    [Column("route")]
    public bool Route { get; set; }

    [Column("inp_cargo")]
    public bool InpCargo { get; set; }

    [Column("out_cargo")]
    public bool OutCargo { get; set; }

    [Column("derailment")]
    public bool Derailment { get; set; }

    [Column("count_stage")]
    public int CountStage { get; set; }

    [Column("id_currency")]
    public int IdCurrency { get; set; }

    [Column("rate", TypeName = "money")]
    public decimal Rate { get; set; }

    [Column("exchange_rate", TypeName = "money")]
    public decimal ExchangeRate { get; set; }

    [Column("coefficient")]
    public double Coefficient { get; set; }

    [Column("use_time")]
    public int UseTime { get; set; }

    [Column("grace_time")]
    public int GraceTime { get; set; }

    [Column("calc_time")]
    public int CalcTime { get; set; }

    [Column("calc_fee_amount", TypeName = "money")]
    public decimal CalcFeeAmount { get; set; }

    [Column("manual_time")]
    public int? ManualTime { get; set; }

    [Column("manual_fee_amount", TypeName = "money")]
    public decimal? ManualFeeAmount { get; set; }

    [Column("note")]
    [StringLength(100)]
    public string? Note { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("change", TypeName = "datetime")]
    public DateTime? Change { get; set; }

    [Column("change_user")]
    [StringLength(50)]
    public string? ChangeUser { get; set; }

    [Column("downtime")]
    public int? Downtime { get; set; }

    [Column("date_start_unload", TypeName = "datetime")]
    public DateTime? DateStartUnload { get; set; }

    [Column("date_end_unload", TypeName = "datetime")]
    public DateTime? DateEndUnload { get; set; }

    [Column("date_start_load", TypeName = "datetime")]
    public DateTime? DateStartLoad { get; set; }

    [Column("date_end_load", TypeName = "datetime")]
    public DateTime? DateEndLoad { get; set; }

    [Column("id_cargo_arr")]
    public int? IdCargoArr { get; set; }

    [Column("id_cargo_group_arr")]
    public int? IdCargoGroupArr { get; set; }

    [Column("id_cargo_out")]
    public int? IdCargoOut { get; set; }

    [Column("id_cargo_group_out")]
    public int? IdCargoGroupOut { get; set; }

    [Column("code_stn_from")]
    public int? CodeStnFrom { get; set; }

    [Column("code_stn_to")]
    public int? CodeStnTo { get; set; }

    [Column("id_ufp")]
    [StringLength(50)]
    public string? IdUfp { get; set; }

    [Column("id_upfpd")]
    public int? IdUpfpd { get; set; }

    [Column("calc_date_start", TypeName = "datetime")]
    public DateTime? CalcDateStart { get; set; }

    [Column("calc_date_end", TypeName = "datetime")]
    public DateTime? CalcDateEnd { get; set; }

    [Column("error")]
    public int? Error { get; set; }

    [InverseProperty("IdUsageFeeNavigation")]
    public virtual ICollection<WagonInternalRoute> WagonInternalRoutes { get; set; } = new List<WagonInternalRoute>();
}
