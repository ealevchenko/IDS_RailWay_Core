using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Usage_Fee_Period", Schema = "IDS")]
public partial class UsageFeePeriod
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_operator")]
    public int IdOperator { get; set; }

    [Column("id_genus")]
    public int IdGenus { get; set; }

    [Column("start", TypeName = "datetime")]
    public DateTime Start { get; set; }

    [Column("stop", TypeName = "datetime")]
    public DateTime Stop { get; set; }

    [Column("id_currency")]
    public int? IdCurrency { get; set; }

    [Column("rate", TypeName = "money")]
    public decimal? Rate { get; set; }

    [Column("id_currency_derailment")]
    public int? IdCurrencyDerailment { get; set; }

    [Column("rate_derailment", TypeName = "money")]
    public decimal? RateDerailment { get; set; }

    [Column("coefficient_route")]
    public float? CoefficientRoute { get; set; }

    [Column("coefficient_not_route")]
    public float? CoefficientNotRoute { get; set; }

    [Column("grace_time_1")]
    public int? GraceTime1 { get; set; }

    [Column("grace_time_2")]
    public int? GraceTime2 { get; set; }

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

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_user")]
    [StringLength(50)]
    public string? CloseUser { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [Column("hour_after_30")]
    public bool? HourAfter30 { get; set; }

    [ForeignKey("IdCurrencyDerailment")]
    [InverseProperty("UsageFeePeriodIdCurrencyDerailmentNavigations")]
    public virtual DirectoryCurrency? IdCurrencyDerailmentNavigation { get; set; }

    [ForeignKey("IdCurrency")]
    [InverseProperty("UsageFeePeriodIdCurrencyNavigations")]
    public virtual DirectoryCurrency? IdCurrencyNavigation { get; set; }

    [ForeignKey("IdGenus")]
    [InverseProperty("UsageFeePeriods")]
    public virtual DirectoryGenusWagon IdGenusNavigation { get; set; } = null!;

    [ForeignKey("IdOperator")]
    [InverseProperty("UsageFeePeriods")]
    public virtual DirectoryOperatorsWagon IdOperatorNavigation { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<UsageFeePeriod> InverseParent { get; } = new List<UsageFeePeriod>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual UsageFeePeriod? Parent { get; set; }
}
