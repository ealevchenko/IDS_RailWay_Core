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

    [Column("id_cargo_group_arrival")]
    public int? IdCargoGroupArrival { get; set; }

    [Column("arrival_cargo_group_name_ru")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameRu { get; set; }

    [Column("arrival_cargo_group_name_en")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameEn { get; set; }

    [Column("id_cargo_arrival")]
    public int? IdCargoArrival { get; set; }

    [Column("arrival_cargo_name_ru")]
    [StringLength(50)]
    public string? ArrivalCargoNameRu { get; set; }

    [Column("arrival_cargo_name_en")]
    [StringLength(50)]
    public string? ArrivalCargoNameEn { get; set; }

    [Column("arrival_end_unload")]
    public bool? ArrivalEndUnload { get; set; }

    [Column("code_stn_to")]
    public int? CodeStnTo { get; set; }

    [Column("to_station_name_ru")]
    [StringLength(50)]
    public string? ToStationNameRu { get; set; }

    [Column("to_station_name_en")]
    [StringLength(50)]
    public string? ToStationNameEn { get; set; }

    [Column("id_cargo_group_outgoing")]
    public int? IdCargoGroupOutgoing { get; set; }

    [Column("outgoing_cargo_group_name_ru")]
    [StringLength(50)]
    public string? OutgoingCargoGroupNameRu { get; set; }

    [Column("outgoing_cargo_group_name_en")]
    [StringLength(50)]
    public string? OutgoingCargoGroupNameEn { get; set; }

    [Column("id_cargo_outgoing")]
    public int? IdCargoOutgoing { get; set; }

    [Column("outgoing_cargo_name_ru")]
    [StringLength(50)]
    public string? OutgoingCargoNameRu { get; set; }
 
    [Column("outgoing_cargo_name_en")]
    [StringLength(50)]
    public string? OutgoingCargoNameEn { get; set; }

    [Column("outgoing_start_load")]
    public bool? OutgoingStartLoad { get; set; }

    [Column("grace_time")]
    public int? GraceTime { get; set; }

    [Column("id_currency")]
    public int? IdCurrency { get; set; }

    [Column("currency_ru")]
    [StringLength(50)]
    public string? CurrencyRu { get; set; }

    [Column("currency_en")]
    [StringLength(50)]
    public string? CurrencyEn { get; set; }

    [Column("currency_code")]
    public int? CurrencyCode { get; set; }

    [Column("currency_code_cc")]
    [StringLength(3)]
    public string? CurrencyCodeCc { get; set; }

    [Column("rate", TypeName = "money")]
    public decimal? Rate { get; set; }
}
