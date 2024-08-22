using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewWagonDislocation
{
    [Key]
    [Column("id_wir")]
    public long IdWir { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_arrival_car")]
    public long? IdArrivalCar { get; set; }

    [Column("id_sap_incoming_supply")]
    public long? IdSapIncomingSupply { get; set; }

    [Column("doc_outgoing_car")]
    public bool? DocOutgoingCar { get; set; }

    [Column("id_outgoing_car")]
    public long? IdOutgoingCar { get; set; }

    [Column("id_sap_outbound_supply")]
    public long? IdSapOutboundSupply { get; set; }

    [Column("note_wir")]
    [StringLength(250)]
    public string? NoteWir { get; set; }

    [Column("create_wir", TypeName = "datetime")]
    public DateTime CreateWir { get; set; }

    [Column("create_user_wir")]
    [StringLength(50)]
    public string CreateUserWir { get; set; } = null!;

    [Column("close_wir", TypeName = "datetime")]
    public DateTime? CloseWir { get; set; }

    [Column("close_user_wir")]
    [StringLength(50)]
    public string? CloseUserWir { get; set; }

    [Column("parent_id_wir")]
    public long? ParentIdWir { get; set; }

    [Column("id_wim")]
    public long? IdWim { get; set; }

    [Column("id_wagon_internal_routes")]
    public long? IdWagonInternalRoutes { get; set; }

    [Column("id_station")]
    public int? IdStation { get; set; }

    [Column("station_name_ru")]
    [StringLength(50)]
    public string? StationNameRu { get; set; }

    [Column("station_name_en")]
    [StringLength(50)]
    public string? StationNameEn { get; set; }

    [Column("station_abbr_ru")]
    [StringLength(50)]
    public string? StationAbbrRu { get; set; }

    [Column("station_abbr_en")]
    [StringLength(50)]
    public string? StationAbbrEn { get; set; }

    [Column("id_way")]
    public int? IdWay { get; set; }

    [Column("way_num_ru")]
    [StringLength(20)]
    public string? WayNumRu { get; set; }

    [Column("way_num_en")]
    [StringLength(20)]
    public string? WayNumEn { get; set; }

    [Column("way_name_ru")]
    [StringLength(100)]
    public string? WayNameRu { get; set; }

    [Column("way_name_en")]
    [StringLength(100)]
    public string? WayNameEn { get; set; }

    [Column("way_abbr_ru")]
    [StringLength(50)]
    public string? WayAbbrRu { get; set; }

    [Column("way_abbr_en")]
    [StringLength(50)]
    public string? WayAbbrEn { get; set; }

    [Column("way_start", TypeName = "datetime")]
    public DateTime? WayStart { get; set; }

    [Column("way_end", TypeName = "datetime")]
    public DateTime? WayEnd { get; set; }

    [Column("id_outer_way")]
    public int? IdOuterWay { get; set; }

    [Column("name_outer_way_ru")]
    [StringLength(150)]
    public string? NameOuterWayRu { get; set; }

    [Column("name_outer_way_en")]
    [StringLength(150)]
    public string? NameOuterWayEn { get; set; }

    [Column("outer_way_start", TypeName = "datetime")]
    public DateTime? OuterWayStart { get; set; }

    [Column("outer_way_end", TypeName = "datetime")]
    public DateTime? OuterWayEnd { get; set; }

    [Column("position")]
    public int? Position { get; set; }

    [Column("note_wim")]
    [StringLength(250)]
    public string? NoteWim { get; set; }

    [Column("create_wim", TypeName = "datetime")]
    public DateTime? CreateWim { get; set; }

    [Column("create_user_wim")]
    [StringLength(50)]
    public string? CreateUserWim { get; set; }

    [Column("close_wim", TypeName = "datetime")]
    public DateTime? CloseWim { get; set; }

    [Column("close_user_wim")]
    [StringLength(50)]
    public string? CloseUserWim { get; set; }

    [Column("parent_id_wim")]
    public long? ParentIdWim { get; set; }

    [Column("id_operation_wagon")]
    public int? IdOperationWagon { get; set; }

    [Column("operation_wagon_name_ru")]
    [StringLength(20)]
    public string? OperationWagonNameRu { get; set; }

    [Column("operation_wagon_name_en")]
    [StringLength(20)]
    public string? OperationWagonNameEn { get; set; }

    [Column("operation_wagon_start", TypeName = "datetime")]
    public DateTime? OperationWagonStart { get; set; }

    [Column("operation_wagon_end", TypeName = "datetime")]
    public DateTime? OperationWagonEnd { get; set; }

    [Column("operation_wagon_busy")]
    public bool? OperationWagonBusy { get; set; }

    [Column("operation_wagon_create", TypeName = "datetime")]
    public DateTime? OperationWagonCreate { get; set; }

    [Column("operation_wagon_create_user")]
    [StringLength(50)]
    public string? OperationWagonCreateUser { get; set; }

    [Column("operation_wagon_close", TypeName = "datetime")]
    public DateTime? OperationWagonClose { get; set; }

    [Column("operation_wagon_close_user")]
    [StringLength(50)]
    public string? OperationWagonCloseUser { get; set; }

    [Column("date_outgoing", TypeName = "datetime")]
    public DateTime? DateOutgoing { get; set; }

    [Column("date_outgoing_act", TypeName = "datetime")]
    public DateTime? DateOutgoingAct { get; set; }

    [Column("date_departure_amkr", TypeName = "datetime")]
    public DateTime? DateDepartureAmkr { get; set; }

}
