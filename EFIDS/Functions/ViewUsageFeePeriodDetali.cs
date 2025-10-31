using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public partial class ViewUsageFeePeriodDetali
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_usage_fee_period")]
    public int? IdUsageFeePeriod { get; set; }

    [Column("code_stn_from")]
    public int? CodeStnFrom { get; set; }

    [Column("from_station_name_ru")]
    [StringLength(50)]
    public string? FromStationNameRu { get; set; }

    [Column("from_station_name_en")]
    [StringLength(50)]
    public string? FromStationNameEn { get; set; }

    [Column("id_cargo_arrival")]
    public int? IdCargoArrival { get; set; }

    [Column("arrival_cargo_name_ru")]
    [StringLength(50)]
    public string? ArrivalCargoNameRu { get; set; }

    [Column("arrival_cargo_name_en")]
    [StringLength(50)]
    public string? ArrivalCargoNameEn { get; set; }

    [Column("code_stn_to")]
    public int? CodeStnTo { get; set; }

    [Column("to_station_name_ru")]
    [StringLength(50)]
    public string? ToStationNameRu { get; set; }

    [Column("to_station_name_en")]
    [StringLength(50)]
    public string? ToStationNameEn { get; set; }

    [Column("id_cargo_outgoing")]
    public int? IdCargoOutgoing { get; set; }

    [Column("outgoing_cargo_name_ru")]
    [StringLength(50)]
    public string? OutgoingCargoNameRu { get; set; }

    [Column("outgoing_cargo_name_en")]
    [StringLength(50)]
    public string? OutgoingCargoNameEn { get; set; }

    [Column("grace_time")]
    public int? GraceTime { get; set; }

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
}
