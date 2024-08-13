using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewOutgoingSostav
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("num_doc")]
    public int NumDoc { get; set; }

    [Column("id_station_from")]
    public int IdStationFrom { get; set; }

    [Column("station_from_name_ru")]
    [StringLength(50)]
    public string? StationFromNameRu { get; set; }

    [Column("station_from_name_en")]
    [StringLength(50)]
    public string? StationFromNameEn { get; set; }

    [Column("station_from_abbr_ru")]
    [StringLength(50)]
    public string? StationFromAbbrRu { get; set; }

    [Column("station_from_abbr_en")]
    [StringLength(50)]
    public string? StationFromAbbrEn { get; set; }

    [Column("id_way_from")]
    public int IdWayFrom { get; set; }

    [Column("way_from_num_ru")]
    [StringLength(20)]
    public string? WayFromNumRu { get; set; }

    [Column("way_from_num_en")]
    [StringLength(20)]
    public string? WayFromNumEn { get; set; }

    [Column("way_from_name_ru")]
    [StringLength(100)]
    public string? WayFromNameRu { get; set; }

    [Column("way_from_name_en")]
    [StringLength(100)]
    public string? WayFromNameEn { get; set; }

    [Column("id_station_on")]
    public int? IdStationOn { get; set; }

    [Column("station_on_name_ru")]
    [StringLength(50)]
    public string? StationOnNameRu { get; set; }

    [Column("station_on_name_en")]
    [StringLength(50)]
    public string? StationOnNameEn { get; set; }

    [Column("station_on_abbr_ru")]
    [StringLength(50)]
    public string? StationOnAbbrRu { get; set; }

    [Column("station_on_abbr_en")]
    [StringLength(50)]
    public string? StationOnAbbrEn { get; set; }

    [Column("date_readiness_amkr", TypeName = "datetime")]
    public DateTime DateReadinessAmkr { get; set; }

    [Column("date_end_inspection_acceptance_delivery", TypeName = "datetime")]
    public DateTime? DateEndInspectionAcceptanceDelivery { get; set; }

    [Column("date_end_inspection_loader", TypeName = "datetime")]
    public DateTime? DateEndInspectionLoader { get; set; }

    [Column("date_end_inspection_vagonnik", TypeName = "datetime")]
    public DateTime? DateEndInspectionVagonnik { get; set; }

    [Column("vagonnik_user")]
    [StringLength(50)]
    public string? VagonnikUser { get; set; }

    [Column("date_show_wagons", TypeName = "datetime")]
    public DateTime? DateShowWagons { get; set; }

    [Column("date_readiness_uz", TypeName = "datetime")]
    public DateTime? DateReadinessUz { get; set; }

    [Column("date_outgoing", TypeName = "datetime")]
    public DateTime? DateOutgoing { get; set; }

    [Column("date_outgoing_act", TypeName = "datetime")]
    public DateTime? DateOutgoingAct { get; set; }

    [Column("date_departure_amkr", TypeName = "datetime")]
    public DateTime? DateDepartureAmkr { get; set; }

    [Column("composition_index")]
    [StringLength(50)]
    public string? CompositionIndex { get; set; }

    [Column("status")]
    public int Status { get; set; }

    [Column("note")]
    [StringLength(200)]
    public string? Note { get; set; }

    [Column("route_sign")]
    public bool? RouteSign { get; set; }

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

    [Column("count_all")]
    public int? CountAll { get; set; }

    [Column("count_outgoing")]
    public int? CountOutgoing { get; set; }

    [Column("count_not_outgoing")]
    public int? CountNotOutgoing { get; set; }

    [Column("count_return")]
    public int? CountReturn { get; set; }

    [Column("count_detention")]
    public int? CountDetention { get; set; }

    [Column("count_vagonnik")]
    public int? CountVagonnik { get; set; }
}
