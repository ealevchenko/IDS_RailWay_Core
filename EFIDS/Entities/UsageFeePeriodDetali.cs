using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Usage_Fee_Period_Detali", Schema = "IDS")]
public partial class UsageFeePeriodDetali
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_usage_fee_period")]
    public int? IdUsageFeePeriod { get; set; }

    [Column("code_stn_from")]
    public int? CodeStnFrom { get; set; }

    [Column("id_cargo_arrival")]
    public int? IdCargoArrival { get; set; }

    [Column("code_stn_to")]
    public int? CodeStnTo { get; set; }

    [Column("id_cargo_outgoing")]
    public int? IdCargoOutgoing { get; set; }

    [Column("grace_time")]
    public int? GraceTime { get; set; }

    [Column("id_currency")]
    public int? IdCurrency { get; set; }

    [Column("rate", TypeName = "money")]
    public decimal? Rate { get; set; }

    [Column("end_unload")]
    public bool? EndUnload { get; set; }

    [Column("start_load")]
    public bool? StartLoad { get; set; }

    [ForeignKey("CodeStnFrom")]
    [InverseProperty("UsageFeePeriodDetaliCodeStnFromNavigations")]
    public virtual DirectoryExternalStation? CodeStnFromNavigation { get; set; }

    [ForeignKey("CodeStnTo")]
    [InverseProperty("UsageFeePeriodDetaliCodeStnToNavigations")]
    public virtual DirectoryExternalStation? CodeStnToNavigation { get; set; }

    [ForeignKey("IdCargoArrival")]
    [InverseProperty("UsageFeePeriodDetaliIdCargoArrivalNavigations")]
    public virtual DirectoryCargo? IdCargoArrivalNavigation { get; set; }

    [ForeignKey("IdCargoOutgoing")]
    [InverseProperty("UsageFeePeriodDetaliIdCargoOutgoingNavigations")]
    public virtual DirectoryCargo? IdCargoOutgoingNavigation { get; set; }

    [ForeignKey("IdUsageFeePeriod")]
    [InverseProperty("UsageFeePeriodDetalis")]
    public virtual UsageFeePeriod? IdUsageFeePeriodNavigation { get; set; }
}
