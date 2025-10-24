using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;
public class ViewUsageFeePeriod
{
    [Key]
    [Column("id_usage_fee_period")]
    public int IdUsageFeePeriod { get; set; }

    [Column("usage_fee_period_id_operator")]
    public int UsageFeePeriodIdOperator { get; set; }

    [Column("usage_fee_period_operator_abbr_ru")]
    [StringLength(20)]
    public string? UsageFeePeriodOperatorAbbrRu { get; set; }

    [Column("usage_fee_period_operator_ru")]
    [StringLength(100)]
    public string? UsageFeePeriodOperatorRu { get; set; }

    [Column("usage_fee_period_operator_abbr_en")]
    [StringLength(20)]
    public string? UsageFeePeriodOperatorAbbrEn { get; set; }

    [Column("usage_fee_period_operator_en")]
    [StringLength(100)]
    public string? UsageFeePeriodOperatorEn { get; set; }

    [Column("usage_fee_period_operators_paid")]
    public bool? UsageFeePeriodOperatorsPaid { get; set; }

    [Column("usage_fee_period_operators_rop")]
    public bool? UsageFeePeriodOperatorsRop { get; set; }

    [Column("usage_fee_period_operators_local_use")]
    public bool? UsageFeePeriodOperatorsLocalUse { get; set; }

    [Column("usage_fee_period_operators_color")]
    [StringLength(10)]
    public string? UsageFeePeriodOperatorsColor { get; set; }

    [Column("usage_fee_period_id_genus")]
    public int UsageFeePeriodIdGenus { get; set; }

    [Column("usage_fee_period_genus_ru")]
    [StringLength(50)]
    public string? UsageFeePeriodGenusRu { get; set; }

    [Column("usage_fee_period_genus_en")]
    [StringLength(50)]
    public string? UsageFeePeriodGenusEn { get; set; }

    [Column("usage_fee_period_genus_abbr_ru")]
    [StringLength(5)]
    public string? UsageFeePeriodGenusAbbrRu { get; set; }

    [Column("usage_fee_period_genus_abbr_en")]
    [StringLength(5)]
    public string? UsageFeePeriodGenusAbbrEn { get; set; }

    [Column("usage_fee_period_rod_uz")]
    public int? UsageFeePeriodRodUz { get; set; }

    [Column("usage_fee_period_start", TypeName = "datetime")]
    public DateTime UsageFeePeriodStart { get; set; }

    [Column("usage_fee_period_stop", TypeName = "datetime")]
    public DateTime UsageFeePeriodStop { get; set; }

    [Column("usage_fee_period_id_currency")]
    public int? UsageFeePeriodIdCurrency { get; set; }

    [Column("usage_fee_period_currency_ru")]
    [StringLength(50)]
    public string? UsageFeePeriodCurrencyRu { get; set; }

    [Column("usage_fee_period_currency_en")]
    [StringLength(50)]
    public string? UsageFeePeriodCurrencyEn { get; set; }

    [Column("usage_fee_period_code")]
    public int? UsageFeePeriodCode { get; set; }

    [Column("usage_fee_period_code_cc")]
    [StringLength(3)]
    public string? UsageFeePeriodCodeCc { get; set; }

    [Column("usage_fee_period_rate", TypeName = "money")]
    public decimal? UsageFeePeriodRate { get; set; }

    [Column("usage_fee_period_id_currency_derailment")]
    public int? UsageFeePeriodIdCurrencyDerailment { get; set; }

    [Column("usage_fee_period_derailment_currency_ru")]
    [StringLength(50)]
    public string? UsageFeePeriodDerailmentCurrencyRu { get; set; }

    [Column("usage_fee_period_derailment_currency_en")]
    [StringLength(50)]
    public string? UsageFeePeriodDerailmentCurrencyEn { get; set; }

    [Column("usage_fee_period_derailment_code")]
    public int? UsageFeePeriodDerailmentCode { get; set; }

    [Column("usage_fee_period_derailment_code_cc")]
    [StringLength(3)]
    public string? UsageFeePeriodDerailmentCodeCc { get; set; }

    [Column("usage_fee_period_rate_derailment", TypeName = "money")]
    public decimal? UsageFeePeriodRateDerailment { get; set; }

    [Column("usage_fee_period_coefficient_route")]
    public float? UsageFeePeriodCoefficientRoute { get; set; }

    [Column("usage_fee_period_coefficient_not_route")]
    public float? UsageFeePeriodCoefficientNotRoute { get; set; }

    [Column("usage_fee_period_grace_time_1")]
    public int? UsageFeePeriodGraceTime1 { get; set; }

    [Column("usage_fee_period_grace_time_2")]
    public int? UsageFeePeriodGraceTime2 { get; set; }

    [Column("usage_fee_period_note")]
    [StringLength(100)]
    public string? UsageFeePeriodNote { get; set; }

    [Column("usage_fee_period_create", TypeName = "datetime")]
    public DateTime UsageFeePeriodCreate { get; set; }

    [Column("usage_fee_period_create_user")]
    [StringLength(50)]
    public string UsageFeePeriodCreateUser { get; set; } = null!;

    [Column("usage_fee_period_change", TypeName = "datetime")]
    public DateTime? UsageFeePeriodChange { get; set; }

    [Column("usage_fee_period_change_user")]
    [StringLength(50)]
    public string? UsageFeePeriodChangeUser { get; set; }

    [Column("usage_fee_period_close", TypeName = "datetime")]
    public DateTime? UsageFeePeriodClose { get; set; }

    [Column("usage_fee_period_close_user")]
    [StringLength(50)]
    public string? UsageFeePeriodCloseUser { get; set; }

    [Column("usage_fee_period_parent_id")]
    public int? UsageFeePeriodParentId { get; set; }

    [Column("usage_fee_period_hour_after_30")]
    public bool? UsageFeePeriodHourAfter30 { get; set; }
}
