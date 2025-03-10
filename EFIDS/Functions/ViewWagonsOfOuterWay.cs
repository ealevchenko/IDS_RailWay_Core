using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public partial class ViewWagonsOfOuterWay
{
    [Key]
    [Column("from_id_wim")]
    public long FromIdWim { get; set; }

    [Column("id_wir")]
    public long IdWir { get; set; }

    [Column("from_id_wio")]
    public long? FromIdWio { get; set; }

    [Column("on_id_wim")]
    public long? OnIdWim { get; set; }

    [Column("on_id_wio")]
    public long? OnIdWio { get; set; }

    [Column("outer_way_num_sostav")]
    ////[StringLength(50)]
    public string? OuterWayNumSostav { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("outer_way_position")]
    public int OuterWayPosition { get; set; }

    [Column("arrival_nom_doc")]
    public int? ArrivalNomDoc { get; set; }

    [Column("arrival_nom_main_doc")]
    public int? ArrivalNomMainDoc { get; set; }

    [Column("wagon_adm")]
    public int? WagonAdm { get; set; }

    [Column("wagon_adm_name_ru")]
    ////[StringLength(100)]
    public string? WagonAdmNameRu { get; set; }

    [Column("wagon_adm_name_en")]
    ////[StringLength(100)]
    public string? WagonAdmNameEn { get; set; }

    [Column("wagon_adm_abbr_ru")]
    ////[StringLength(10)]
    public string? WagonAdmAbbrRu { get; set; }

    [Column("wagon_adm_abbr_en")]
    ////[StringLength(10)]
    public string? WagonAdmAbbrEn { get; set; }

    [Column("wagon_rod")]
    public int? WagonRod { get; set; }

    [Column("wagon_rod_name_ru")]
    ////[StringLength(50)]
    public string? WagonRodNameRu { get; set; }

    [Column("wagon_rod_name_en")]
    ////[StringLength(50)]
    public string? WagonRodNameEn { get; set; }

    [Column("wagon_rod_abbr_ru")]
    ////[StringLength(5)]
    public string? WagonRodAbbrRu { get; set; }

    [Column("wagon_rod_abbr_en")]
    ////[StringLength(5)]
    public string? WagonRodAbbrEn { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("operators_ru")]
    ////[StringLength(100)]
    public string? OperatorsRu { get; set; }

    [Column("operators_en")]
    ////[StringLength(100)]
    public string? OperatorsEn { get; set; }

    [Column("operator_abbr_ru")]
    ////[StringLength(20)]
    public string? OperatorAbbrRu { get; set; }

    [Column("operator_abbr_en")]
    ////[StringLength(20)]
    public string? OperatorAbbrEn { get; set; }

    [Column("operator_rent_start", TypeName = "datetime")]
    public DateTime? OperatorRentStart { get; set; }

    [Column("operator_rent_end", TypeName = "datetime")]
    public DateTime? OperatorRentEnd { get; set; }

    [Column("operator_paid")]
    public bool? OperatorPaid { get; set; }

    [Column("operator_color")]
    ////[StringLength(10)]
    public string? OperatorColor { get; set; }

    [Column("operator_monitoring_idle_time")]
    public bool? OperatorMonitoringIdleTime { get; set; }

    [Column("id_limiting_loading")]
    public int? IdLimitingLoading { get; set; }

    [Column("limiting_name_ru")]
    ////[StringLength(100)]
    public string? LimitingNameRu { get; set; }

    [Column("limiting_name_en")]
    ////[StringLength(100)]
    public string? LimitingNameEn { get; set; }

    [Column("limiting_abbr_ru")]
    ////[StringLength(30)]
    public string? LimitingAbbrRu { get; set; }

    [Column("limiting_abbr_en")]
    ////[StringLength(30)]
    public string? LimitingAbbrEn { get; set; }

    [Column("arrival_condition_name_ru")]
    ////[StringLength(100)]
    public string? ArrivalConditionNameRu { get; set; }

    [Column("arrival_condition_name_en")]
    ////[StringLength(100)]
    public string? ArrivalConditionNameEn { get; set; }

    [Column("arrival_condition_abbr_ru")]
    ////[StringLength(20)]
    public string? ArrivalConditionAbbrRu { get; set; }

    [Column("arrival_condition_abbr_en")]
    ////[StringLength(20)]
    public string? ArrivalConditionAbbrEn { get; set; }

    [Column("arrival_condition_red")]
    public bool? ArrivalConditionRed { get; set; }

    [Column("arrival_cargo_group_name_ru")]
    ////[StringLength(50)]
    public string? ArrivalCargoGroupNameRu { get; set; }

    [Column("arrival_cargo_group_name_en")]
    ////[StringLength(50)]
    public string? ArrivalCargoGroupNameEn { get; set; }

    [Column("arrival_cargo_name_ru")]
    //[StringLength(50)]
    public string? ArrivalCargoNameRu { get; set; }

    [Column("arrival_cargo_name_en")]
    ////[StringLength(50)]
    public string? ArrivalCargoNameEn { get; set; }

    [Column("arrival_id_sertification_data")]
    public int? ArrivalIdSertificationData { get; set; }

    [Column("arrival_sertification_data_ru")]
    ////[StringLength(50)]
    public string? ArrivalSertificationDataRu { get; set; }

    [Column("arrival_sertification_data_en")]
    ////[StringLength(50)]
    public string? ArrivalSertificationDataEn { get; set; }

    [Column("arrival_division_amkr_code")]
    ////[StringLength(5)]
    public string? ArrivalDivisionAmkrCode { get; set; }

    [Column("arrival_division_amkr_name_ru")]
    ////[StringLength(250)]
    public string? ArrivalDivisionAmkrNameRu { get; set; }

    [Column("arrival_division_amkr_name_en")]
    ////[StringLength(250)]
    public string? ArrivalDivisionAmkrNameEn { get; set; }

    [Column("arrival_division_amkr_abbr_ru")]
    ////[StringLength(50)]
    public string? ArrivalDivisionAmkrAbbrRu { get; set; }

    [Column("arrival_division_amkr_abbr_en")]
    ////[StringLength(50)]
    public string? ArrivalDivisionAmkrAbbrEn { get; set; }

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

    [Column("wir_note")]
    ////[StringLength(250)]
    public string? WirNote { get; set; }

    [Column("wir_create", TypeName = "datetime")]
    public DateTime WirCreate { get; set; }

    [Column("wir_create_user")]
    ////[StringLength(50)]
    public string WirCreateUser { get; set; } = null!;

    [Column("wir_close", TypeName = "datetime")]
    public DateTime? WirClose { get; set; }

    [Column("wir_close_user")]
    ////[StringLength(50)]
    public string? WirCloseUser { get; set; }

    [Column("wir_parent_id")]
    public long? WirParentId { get; set; }

    [Column("from_id_operation")]
    public int? FromIdOperation { get; set; }

    [Column("from_operation_name_ru")]
    //[StringLength(50)]
    public string? FromOperationNameRu { get; set; }

    [Column("from_operation_name_en")]
    ////[StringLength(50)]
    public string? FromOperationNameEn { get; set; }

    [Column("from_busy")]
    public bool? FromBusy { get; set; }

    [Column("from_operation_start", TypeName = "datetime")]
    public DateTime? FromOperationStart { get; set; }

    [Column("from_operation_end", TypeName = "datetime")]
    public DateTime? FromOperationEnd { get; set; }

    [Column("from_operation_id_condition")]
    public int? FromOperationIdCondition { get; set; }

    [Column("from_operation_condition_name_ru")]
    ////[StringLength(100)]
    public string? FromOperationConditionNameRu { get; set; }

    [Column("from_operation_condition_name_en")]
    ////[StringLength(100)]
    public string? FromOperationConditionNameEn { get; set; }

    [Column("from_operation_condition_abbr_ru")]
    ////[StringLength(20)]
    public string? FromOperationConditionAbbrRu { get; set; }

    [Column("from_operation_condition_abbr_en")]
    ////[StringLength(20)]
    public string? FromOperationConditionAbbrEn { get; set; }

    [Column("from_operation_id_loading_status")]
    public int? FromOperationIdLoadingStatus { get; set; }

    [Column("from_operation_loading_status_ru")]
    ////[StringLength(30)]
    public string? FromOperationLoadingStatusRu { get; set; }

    [Column("from_operation_loading_status_en")]
    ////[StringLength(30)]
    public string? FromOperationLoadingStatusEn { get; set; }

    [Column("from_operation_locomotive1")]
    ////[StringLength(20)]
    public string? FromOperationLocomotive1 { get; set; }

    [Column("from_operation_locomotive2")]
    ////[StringLength(20)]
    public string? FromOperationLocomotive2 { get; set; }

    [Column("from_operation_note")]
    ////[StringLength(250)]
    public string? FromOperationNote { get; set; }

    [Column("from_operation_create", TypeName = "datetime")]
    public DateTime? FromOperationCreate { get; set; }

    [Column("from_operation_create_user")]
    ////[StringLength(50)]
    public string? FromOperationCreateUser { get; set; }

    [Column("from_operation_close", TypeName = "datetime")]
    public DateTime? FromOperationClose { get; set; }

    [Column("from_operation_close_user")]
    ////[StringLength(50)]
    public string? FromOperationCloseUser { get; set; }

    [Column("from_operation_parent_id")]
    public long? FromOperationParentId { get; set; }

    [Column("from_id_station")]
    public int FromIdStation { get; set; }

    [Column("from_station_name_ru")]
    ////[StringLength(50)]
    public string? FromStationNameRu { get; set; }

    [Column("from_station_name_en")]
    ////[StringLength(50)]
    public string? FromStationNameEn { get; set; }

    [Column("from_station_abbr_ru")]
    ////[StringLength(50)]
    public string? FromStationAbbrRu { get; set; }

    [Column("from_station_abbr_en")]
    ////[StringLength(50)]
    public string? FromStationAbbrEn { get; set; }

    [Column("from_id_way")]
    public int FromIdWay { get; set; }

    [Column("from_id_park")]
    public int? FromIdPark { get; set; }

    [Column("from_way_num_ru")]
    ////[StringLength(20)]
    public string? FromWayNumRu { get; set; }

    [Column("from_way_num_en")]
    ////[StringLength(20)]
    public string? FromWayNumEn { get; set; }

    [Column("from_way_name_ru")]
    ////[StringLength(100)]
    public string? FromWayNameRu { get; set; }

    [Column("from_way_name_en")]
    ////[StringLength(100)]
    public string? FromWayNameEn { get; set; }

    [Column("from_way_abbr_ru")]
    ////[StringLength(50)]
    public string? FromWayAbbrRu { get; set; }

    [Column("from_way_abbr_en")]
    ////[StringLength(50)]
    public string? FromWayAbbrEn { get; set; }

    [Column("from_way_capacity")]
    public int? FromWayCapacity { get; set; }

    [Column("from_way_close", TypeName = "datetime")]
    public DateTime? FromWayClose { get; set; }

    [Column("from_way_delete", TypeName = "datetime")]
    public DateTime? FromWayDelete { get; set; }

    [Column("from_way_note")]
    ////[StringLength(100)]
    public string? FromWayNote { get; set; }

    [Column("from_way_start", TypeName = "datetime")]
    public DateTime FromWayStart { get; set; }

    [Column("from_way_end", TypeName = "datetime")]
    public DateTime? FromWayEnd { get; set; }

    [Column("id_outer_way")]
    public int? IdOuterWay { get; set; }

    [Column("name_outer_way_ru")]
    ////[StringLength(150)]
    public string? NameOuterWayRu { get; set; }

    [Column("name_outer_way_en")]
    ////[StringLength(150)]
    public string? NameOuterWayEn { get; set; }

    [Column("outer_way_close", TypeName = "datetime")]
    public DateTime? OuterWayClose { get; set; }

    [Column("outer_way_delete", TypeName = "datetime")]
    public DateTime? OuterWayDelete { get; set; }

    [Column("outer_way_note")]
    ////[StringLength(200)]
    public string? OuterWayNote { get; set; }

    [Column("outer_way_start", TypeName = "datetime")]
    public DateTime? OuterWayStart { get; set; }

    [Column("outer_way_end", TypeName = "datetime")]
    public DateTime? OuterWayEnd { get; set; }

    [Column("from_wim_note")]
    ////[StringLength(250)]
    public string? FromWimNote { get; set; }

    [Column("from_wim_create", TypeName = "datetime")]
    public DateTime FromWimCreate { get; set; }

    [Column("from_wim_create_user")]
    ////[StringLength(50)]
    public string FromWimCreateUser { get; set; } = null!;

    [Column("from_wim_close", TypeName = "datetime")]
    public DateTime? FromWimClose { get; set; }

    [Column("from_wim_close_user")]
    ////[StringLength(50)]
    public string? FromWimCloseUser { get; set; }

    [Column("from_wim_parent_id")]
    public long? FromWimParentId { get; set; }

    [Column("on_id_station")]
    public int? OnIdStation { get; set; }

    [Column("on_station_name_ru")]
    ////[StringLength(50)]
    public string? OnStationNameRu { get; set; }

    [Column("on_station_name_en")]
    ////[StringLength(50)]
    public string? OnStationNameEn { get; set; }

    [Column("on_station_abbr_ru")]
    ////[StringLength(50)]
    public string? OnStationAbbrRu { get; set; }

    [Column("on_station_abbr_en")]
    ////[StringLength(50)]
    public string? OnStationAbbrEn { get; set; }

    [Column("arrival_id_station")]
    public int? ArrivalIdStation { get; set; }

    [Column("arrival_station_name_ru")]
    ////[StringLength(50)]
    public string? ArrivalStationNameRu { get; set; }

    [Column("arrival_station_name_en")]
    ////[StringLength(50)]
    public string? ArrivalStationNameEn { get; set; }

    [Column("arrival_station_abbr_ru")]
    ////[StringLength(50)]
    public string? ArrivalStationAbbrRu { get; set; }

    [Column("arrival_station_abbr_en")]
    ////[StringLength(50)]
    public string? ArrivalStationAbbrEn { get; set; }

    [Column("on_id_way")]
    public int? OnIdWay { get; set; }

    [Column("on_id_park")]
    public int? OnIdPark { get; set; }

    [Column("on_way_num_ru")]
    ////[StringLength(20)]
    public string? OnWayNumRu { get; set; }

    [Column("on_way_num_en")]
    ////[StringLength(20)]
    public string? OnWayNumEn { get; set; }

    [Column("on_way_name_ru")]
    ////[StringLength(100)]
    public string? OnWayNameRu { get; set; }

    [Column("on_way_name_en")]
    ////[StringLength(100)]
    public string? OnWayNameEn { get; set; }

    [Column("on_way_abbr_ru")]
    ////[StringLength(50)]
    public string? OnWayAbbrRu { get; set; }

    [Column("on_way_abbr_en")]
    ////[StringLength(50)]
    public string? OnWayAbbrEn { get; set; }

    [Column("on_way_capacity")]
    public int? OnWayCapacity { get; set; }

    [Column("on_way_close", TypeName = "datetime")]
    public DateTime? OnWayClose { get; set; }

    [Column("on_way_delete", TypeName = "datetime")]
    public DateTime? OnWayDelete { get; set; }

    [Column("on_way_note")]
    //[StringLength(100)]
    public string? OnWayNote { get; set; }

    [Column("on_way_start", TypeName = "datetime")]
    public DateTime? OnWayStart { get; set; }

    [Column("on_way_end", TypeName = "datetime")]
    public DateTime? OnWayEnd { get; set; }

    [Column("on_way_position")]
    public int? OnWayPosition { get; set; }

    [Column("on_wim_note")]
    //[StringLength(250)]
    public string? OnWimNote { get; set; }

    [Column("on_wim_create", TypeName = "datetime")]
    public DateTime? OnWimCreate { get; set; }

    [Column("on_wim_create_user")]
    //[StringLength(50)]
    public string? OnWimCreateUser { get; set; }

    [Column("on_wim_close", TypeName = "datetime")]
    public DateTime? OnWimClose { get; set; }

    [Column("on_wim_close_user")]
    //[StringLength(50)]
    public string? OnWimCloseUser { get; set; }

    [Column("on_wim_parent_id")]
    public long? OnWimParentId { get; set; }

    [Column("on_id_operation")]
    public int? OnIdOperation { get; set; }

    [Column("on_operation_name_ru")]
    //[StringLength(50)]
    public string? OnOperationNameRu { get; set; }

    [Column("on_operation_name_en")]
    //[StringLength(50)]
    public string? OnOperationNameEn { get; set; }

    [Column("on_busy")]
    public bool? OnBusy { get; set; }

    [Column("on_operation_start", TypeName = "datetime")]
    public DateTime? OnOperationStart { get; set; }

    [Column("on_operation_end", TypeName = "datetime")]
    public DateTime? OnOperationEnd { get; set; }

    [Column("on_operation_id_condition")]
    public int? OnOperationIdCondition { get; set; }

    [Column("on_operation_condition_name_ru")]
    //[StringLength(100)]
    public string? OnOperationConditionNameRu { get; set; }

    [Column("on_operation_condition_name_en")]
    //[StringLength(100)]
    public string? OnOperationConditionNameEn { get; set; }

    [Column("on_operation_condition_abbr_ru")]
    //[StringLength(20)]
    public string? OnOperationConditionAbbrRu { get; set; }

    [Column("on_operation_condition_abbr_en")]
    //[StringLength(20)]
    public string? OnOperationConditionAbbrEn { get; set; }

    [Column("on_operation_id_loading_status")]
    public int? OnOperationIdLoadingStatus { get; set; }

    [Column("on_operation_loading_status_ru")]
    //[StringLength(30)]
    public string? OnOperationLoadingStatusRu { get; set; }

    [Column("on_operation_loading_status_en")]
    //[StringLength(30)]
    public string? OnOperationLoadingStatusEn { get; set; }

    [Column("on_operation_locomotive1")]
    //[StringLength(20)]
    public string? OnOperationLocomotive1 { get; set; }

    [Column("on_operation_locomotive2")]
    //[StringLength(20)]
    public string? OnOperationLocomotive2 { get; set; }

    [Column("on_operation_note")]
    //[StringLength(250)]
    public string? OnOperationNote { get; set; }

    [Column("on_operation_create", TypeName = "datetime")]
    public DateTime? OnOperationCreate { get; set; }

    [Column("on_operation_create_user")]
    //[StringLength(50)]
    public string? OnOperationCreateUser { get; set; }

    [Column("on_operation_close", TypeName = "datetime")]
    public DateTime? OnOperationClose { get; set; }

    [Column("on_operation_close_user")]
    //[StringLength(50)]
    public string? OnOperationCloseUser { get; set; }

    [Column("on_operation_parent_id")]
    public long? OnOperationParentId { get; set; }
}
