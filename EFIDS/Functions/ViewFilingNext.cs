using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;
public class ViewFilingNext
{
    [Key]
    [Column("id_wim")]
    public long IdWim { get; set; }

    [Column("id_wir")]
    public long? IdWir { get; set; }

    [Column("num")]
    public int? Num { get; set; }

    [Column("id_outgoing_car")]
    public long? IdOutgoingCar { get; set; }

    [Column("wir_close", TypeName = "datetime")]
    public DateTime? WirClose { get; set; }

    [Column("wir_close_user")]
    [StringLength(50)]
    public string? WirCloseUser { get; set; }

    [Column("id_filing")]
    public long? IdFiling { get; set; }

    [Column("num_filing")]
    [StringLength(50)]
    public string? NumFiling { get; set; }

    [Column("type_filing")]
    public int? TypeFiling { get; set; }

    [Column("id_division_filing")]
    public int? IdDivisionFiling { get; set; }

    [Column("filing_name_division_ru")]
    [StringLength(250)]
    public string? FilingNameDivisionRu { get; set; }

    [Column("filing_name_division_en")]
    [StringLength(250)]
    public string? FilingNameDivisionEn { get; set; }

    [Column("filing_division_abbr_ru")]
    [StringLength(50)]
    public string? FilingDivisionAbbrRu { get; set; }

    [Column("filing_division_abbr_en")]
    [StringLength(50)]
    public string? FilingDivisionAbbrEn { get; set; }

    [Column("filing_note")]
    [StringLength(250)]
    public string? FilingNote { get; set; }

    [Column("start_filing", TypeName = "datetime")]
    public DateTime? StartFiling { get; set; }

    [Column("end_filing", TypeName = "datetime")]
    public DateTime? EndFiling { get; set; }

    [Column("filing_id_station_from_amkr")]
    public int? FilingIdStationFromAmkr { get; set; }

    [Column("filing_station_name_ru")]
    [StringLength(50)]
    public string? FilingStationNameRu { get; set; }

    [Column("filing_station_name_en")]
    [StringLength(50)]
    public string? FilingStationNameEn { get; set; }

    [Column("filing_station_abbr_ru")]
    [StringLength(50)]
    public string? FilingStationAbbrRu { get; set; }

    [Column("filing_station_abbr_en")]
    [StringLength(50)]
    public string? FilingStationAbbrEn { get; set; }

    [Column("id_wim_next")]
    public long? IdWimNext { get; set; }

    [Column("id_filing_next")]
    public long? IdFilingNext { get; set; }

    [Column("num_filing_next")]
    [StringLength(50)]
    public string? NumFilingNext { get; set; }

    [Column("type_filing_next")]
    public int? TypeFilingNext { get; set; }

    [Column("id_division_filing_next")]
    public int? IdDivisionFilingNext { get; set; }

    [Column("filing_next_name_division_ru")]
    [StringLength(250)]
    public string? FilingNextNameDivisionRu { get; set; }

    [Column("filing_next_name_division_en")]
    [StringLength(250)]
    public string? FilingNextNameDivisionEn { get; set; }

    [Column("filing_next_division_abbr_ru")]
    [StringLength(50)]
    public string? FilingNextDivisionAbbrRu { get; set; }

    [Column("filing_next_division_abbr_en")]
    [StringLength(50)]
    public string? FilingNextDivisionAbbrEn { get; set; }

    [Column("filing_next_note")]
    [StringLength(250)]
    public string? FilingNextNote { get; set; }

    [Column("filing_next_start_filing", TypeName = "datetime")]
    public DateTime? FilingNextStartFiling { get; set; }

    [Column("filing_next_end_filing", TypeName = "datetime")]
    public DateTime? FilingNextEndFiling { get; set; }

    [Column("filing_next_id_station_from_amkr")]
    public int? FilingNextIdStationFromAmkr { get; set; }

    [Column("filing_next_station_name_ru")]
    [StringLength(50)]
    public string? FilingNextStationNameRu { get; set; }

    [Column("filing_next_station_name_en")]
    [StringLength(50)]
    public string? FilingNextStationNameEn { get; set; }

    [Column("filing_next_station_abbr_ru")]
    [StringLength(50)]
    public string? FilingNextStationAbbrRu { get; set; }

    [Column("filing_next_station_abbr_en")]
    [StringLength(50)]
    public string? FilingNextStationAbbrEn { get; set; }

    [Column("wio_old_id")]
    public long? WioOldId { get; set; }

    [Column("wio_old_id_operation")]
    public int? WioOldIdOperation { get; set; }

    [Column("wio_old_loading_status")]
    public int? WioOldLoadingStatus { get; set; }
}
