﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewCarsGroup
{
    [Key]
    [Column("wir_id")]
    public long WirId { get; set; }

    [Column("wim_id")]
    public long? WimId { get; set; }

    [Column("wio_id")]
    public long? WioId { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_filing")]
    public long? IdFiling { get; set; }

    [Column("num_filing")]
    [StringLength(50)]
    public string? NumFiling { get; set; }

    [Column("type_filing")]
    public int? TypeFiling { get; set; }

    [Column("id_division_filing")]
    public int? IdDivisionFiling { get; set; }

    [Column("vesg_filing")]
    public int? VesgFiling { get; set; }

    [Column("note_filing")]
    [StringLength(250)]
    public string? NoteFiling { get; set; }

    [Column("start_filing", TypeName = "datetime")]
    public DateTime? StartFiling { get; set; }

    [Column("end_filing", TypeName = "datetime")]
    public DateTime? EndFiling { get; set; }

    [Column("doc_received_filing", TypeName = "datetime")]
    public DateTime? DocReceivedFiling { get; set; }

    [Column("create_filing", TypeName = "datetime")]
    public DateTime? CreateFiling { get; set; }

    [Column("create_user_filing")]
    [StringLength(50)]
    public string? CreateUserFiling { get; set; }

    [Column("change_filing", TypeName = "datetime")]
    public DateTime? ChangeFiling { get; set; }

    [Column("change_user_filing")]
    [StringLength(50)]
    public string? ChangeUserFiling { get; set; }

    [Column("close_filing", TypeName = "datetime")]
    public DateTime? CloseFiling { get; set; }

    [Column("close_user_filing")]
    [StringLength(50)]
    public string? CloseUserFiling { get; set; }

    [Column("id_previous_filing")]
    public long? IdPreviousFiling { get; set; }

    [Column("num_previous_filing")]
    [StringLength(50)]
    public string? NumPreviousFiling { get; set; }

    [Column("type_previous_filing")]
    public int? TypePreviousFiling { get; set; }

    [Column("id_previous_division_filing")]
    public int? IdPreviousDivisionFiling { get; set; }

    [Column("vesg_previous_filing")]
    public int? VesgPreviousFiling { get; set; }

    [Column("note_previous_filing")]
    [StringLength(250)]
    public string? NotePreviousFiling { get; set; }

    [Column("start_previous_filing", TypeName = "datetime")]
    public DateTime? StartPreviousFiling { get; set; }

    [Column("end_previous_filing", TypeName = "datetime")]
    public DateTime? EndPreviousFiling { get; set; }

    [Column("doc_received_previous_filing", TypeName = "datetime")]
    public DateTime? DocReceivedPreviousFiling { get; set; }

    [Column("create_previous_filing", TypeName = "datetime")]
    public DateTime? CreatePreviousFiling { get; set; }

    [Column("create_user_previous_filing")]
    [StringLength(50)]
    public string? CreateUserPreviousFiling { get; set; }

    [Column("change_previous_filing", TypeName = "datetime")]
    public DateTime? ChangePreviousFiling { get; set; }

    [Column("change_user_previous_filing")]
    [StringLength(50)]
    public string? ChangeUserPreviousFiling { get; set; }

    [Column("close_previous_filing", TypeName = "datetime")]
    public DateTime? ClosePreviousFiling { get; set; }

    [Column("close_user_previous_filing")]
    [StringLength(50)]
    public string? CloseUserPreviousFiling { get; set; }

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

    [Column("way_filing_start", TypeName = "datetime")]
    public DateTime? WayFilingStart { get; set; }

    [Column("way_filing_end", TypeName = "datetime")]
    public DateTime? WayFilingEnd { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("operators_ru")]
    [StringLength(100)]
    public string? OperatorsRu { get; set; }

    [Column("operators_en")]
    [StringLength(100)]
    public string? OperatorsEn { get; set; }

    [Column("operator_abbr_ru")]
    [StringLength(20)]
    public string? OperatorAbbrRu { get; set; }

    [Column("operator_abbr_en")]
    [StringLength(20)]
    public string? OperatorAbbrEn { get; set; }

    [Column("operator_rent_start", TypeName = "datetime")]
    public DateTime? OperatorRentStart { get; set; }

    [Column("operator_rent_end", TypeName = "datetime")]
    public DateTime? OperatorRentEnd { get; set; }

    [Column("operator_paid")]
    public bool? OperatorPaid { get; set; }

    [Column("operator_color")]
    [StringLength(10)]
    public string? OperatorColor { get; set; }

    [Column("operator_monitoring_idle_time")]
    public bool? OperatorMonitoringIdleTime { get; set; }

    [Column("operator_group")]
    [StringLength(20)]
    public string? OperatorGroup { get; set; }

    [Column("id_limiting_loading")]
    public int? IdLimitingLoading { get; set; }

    [Column("limiting_name_ru")]
    [StringLength(100)]
    public string? LimitingNameRu { get; set; }

    [Column("limiting_name_en")]
    [StringLength(100)]
    public string? LimitingNameEn { get; set; }

    [Column("limiting_abbr_ru")]
    [StringLength(30)]
    public string? LimitingAbbrRu { get; set; }

    [Column("limiting_abbr_en")]
    [StringLength(30)]
    public string? LimitingAbbrEn { get; set; }

    [Column("id_owner_wagon")]
    public int? IdOwnerWagon { get; set; }

    [Column("owner_wagon_ru")]
    [StringLength(100)]
    public string? OwnerWagonRu { get; set; }

    [Column("owner_wagon_en")]
    [StringLength(100)]
    public string? OwnerWagonEn { get; set; }

    [Column("owner_wagon_abbr_ru")]
    [StringLength(20)]
    public string? OwnerWagonAbbrRu { get; set; }

    [Column("owner_wagon_abbr_en")]
    [StringLength(20)]
    public string? OwnerWagonAbbrEn { get; set; }

    [Column("wagon_adm")]
    public int? WagonAdm { get; set; }

    [Column("wagon_adm_name_ru")]
    [StringLength(100)]
    public string? WagonAdmNameRu { get; set; }

    [Column("wagon_adm_name_en")]
    [StringLength(100)]
    public string? WagonAdmNameEn { get; set; }

    [Column("wagon_adm_abbr_ru")]
    [StringLength(10)]
    public string? WagonAdmAbbrRu { get; set; }

    [Column("wagon_adm_abbr_en")]
    [StringLength(10)]
    public string? WagonAdmAbbrEn { get; set; }

    [Column("wagon_rod")]
    public int? WagonRod { get; set; }

    [Column("wagon_rod_name_ru")]
    [StringLength(50)]
    public string? WagonRodNameRu { get; set; }

    [Column("wagon_rod_name_en")]
    [StringLength(50)]
    public string? WagonRodNameEn { get; set; }

    [Column("wagon_rod_abbr_ru")]
    [StringLength(5)]
    public string? WagonRodAbbrRu { get; set; }

    [Column("wagon_rod_abbr_en")]
    [StringLength(5)]
    public string? WagonRodAbbrEn { get; set; }

    [Column("wagon_type_ru")]
    [StringLength(50)]
    public string? WagonTypeRu { get; set; }

    [Column("wagon_type_en")]
    [StringLength(50)]
    public string? WagonTypeEn { get; set; }

    [Column("arrival_condition_name_ru")]
    [StringLength(100)]
    public string? ArrivalConditionNameRu { get; set; }

    [Column("arrival_condition_name_en")]
    [StringLength(100)]
    public string? ArrivalConditionNameEn { get; set; }

    [Column("arrival_condition_abbr_ru")]
    [StringLength(20)]
    public string? ArrivalConditionAbbrRu { get; set; }

    [Column("arrival_condition_abbr_en")]
    [StringLength(20)]
    public string? ArrivalConditionAbbrEn { get; set; }

    [Column("arrival_condition_red")]
    public bool? ArrivalConditionRed { get; set; }

    [Column("current_condition_name_ru")]
    [StringLength(100)]
    public string? CurrentConditionNameRu { get; set; }

    [Column("current_condition_name_en")]
    [StringLength(100)]
    public string? CurrentConditionNameEn { get; set; }

    [Column("current_condition_abbr_ru")]
    [StringLength(20)]
    public string? CurrentConditionAbbrRu { get; set; }

    [Column("current_condition_abbr_en")]
    [StringLength(20)]
    public string? CurrentConditionAbbrEn { get; set; }

    [Column("current_condition_red")]
    public bool? CurrentConditionRed { get; set; }

    [Column("wagon_date_rem_uz", TypeName = "datetime")]
    public DateTime? WagonDateRemUz { get; set; }

    [Column("wagon_gruzp_doc")]
    public double? WagonGruzpDoc { get; set; }

    [Column("wagon_gruzp_uz")]
    public double? WagonGruzpUz { get; set; }

    [Column("arrival_cargo_group_name_ru")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameRu { get; set; }

    [Column("arrival_cargo_group_name_en")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameEn { get; set; }

    [Column("arrival_cargo_name_ru")]
    [StringLength(50)]
    public string? ArrivalCargoNameRu { get; set; }

    [Column("arrival_cargo_name_en")]
    [StringLength(50)]
    public string? ArrivalCargoNameEn { get; set; }

    [Column("arrival_id_sertification_data")]
    public int? ArrivalIdSertificationData { get; set; }

    [Column("arrival_sertification_data_ru")]
    [StringLength(50)]
    public string? ArrivalSertificationDataRu { get; set; }

    [Column("arrival_sertification_data_en")]
    [StringLength(50)]
    public string? ArrivalSertificationDataEn { get; set; }

    [Column("arrival_id_commercial_condition")]
    public int? ArrivalIdCommercialCondition { get; set; }

    [Column("arrival_commercial_condition_ru")]
    [StringLength(50)]
    public string? ArrivalCommercialConditionRu { get; set; }

    [Column("arrival_commercial_condition_en")]
    [StringLength(50)]
    public string? ArrivalCommercialConditionEn { get; set; }

    [Column("arrival_station_from_code")]
    public int? ArrivalStationFromCode { get; set; }

    [Column("arrival_station_from_name_ru")]
    [StringLength(50)]
    public string? ArrivalStationFromNameRu { get; set; }

    [Column("arrival_station_from_name_en")]
    [StringLength(50)]
    public string? ArrivalStationFromNameEn { get; set; }

    [Column("arrival_shipper_code")]
    public int? ArrivalShipperCode { get; set; }

    [Column("arrival_shipper_name_ru")]
    [StringLength(100)]
    public string? ArrivalShipperNameRu { get; set; }

    [Column("arrival_shipper_name_en")]
    [StringLength(100)]
    public string? ArrivalShipperNameEn { get; set; }

    [Column("arrival_station_amkr_name_ru")]
    [StringLength(50)]
    public string? ArrivalStationAmkrNameRu { get; set; }

    [Column("arrival_station_amkr_name_en")]
    [StringLength(50)]
    public string? ArrivalStationAmkrNameEn { get; set; }

    [Column("arrival_station_amkr_abbr_ru")]
    [StringLength(50)]
    public string? ArrivalStationAmkrAbbrRu { get; set; }

    [Column("arrival_station_amkr_abbr_en")]
    [StringLength(50)]
    public string? ArrivalStationAmkrAbbrEn { get; set; }

    [Column("arrival_division_amkr_code")]
    [StringLength(5)]
    public string? ArrivalDivisionAmkrCode { get; set; }

    [Column("arrival_division_amkr_name_ru")]
    [StringLength(250)]
    public string? ArrivalDivisionAmkrNameRu { get; set; }

    [Column("arrival_division_amkr_name_en")]
    [StringLength(250)]
    public string? ArrivalDivisionAmkrNameEn { get; set; }

    [Column("arrival_division_amkr_abbr_ru")]
    [StringLength(50)]
    public string? ArrivalDivisionAmkrAbbrRu { get; set; }

    [Column("arrival_division_amkr_abbr_en")]
    [StringLength(50)]
    public string? ArrivalDivisionAmkrAbbrEn { get; set; }

    [Column("current_id_loading_status")]
    public int? CurrentIdLoadingStatus { get; set; }

    [Column("current_loading_status_ru")]
    [StringLength(30)]
    public string? CurrentLoadingStatusRu { get; set; }

    [Column("current_loading_status_en")]
    [StringLength(30)]
    public string? CurrentLoadingStatusEn { get; set; }

    [Column("current_wagon_busy")]
    public int CurrentWagonBusy { get; set; }

    [Column("current_move_busy")]
    public int CurrentMoveBusy { get; set; }

    [Column("current_load_busy")]
    public int CurrentLoadBusy { get; set; }

    [Column("current_unload_busy")]
    public int CurrentUnloadBusy { get; set; }

    [Column("exist_load_document")]
    public int ExistLoadDocument { get; set; }

    [Column("current_processing_busy")]
    public int CurrentProcessingBusy { get; set; }

    [Column("current_id_operation")]
    public int? CurrentIdOperation { get; set; }

    [Column("current_operation_name_ru")]
    [StringLength(50)]
    public string? CurrentOperationNameRu { get; set; }

    [Column("current_operation_name_en")]
    [StringLength(50)]
    public string? CurrentOperationNameEn { get; set; }

    [Column("current_operation_start", TypeName = "datetime")]
    public DateTime? CurrentOperationStart { get; set; }

    [Column("current_operation_end", TypeName = "datetime")]
    public DateTime? CurrentOperationEnd { get; set; }

    [Column("current_id_organization_service")]
    public int? CurrentIdOrganizationService { get; set; }

    [Column("current_organization_service_ru")]
    [StringLength(50)]
    public string? CurrentOrganizationServiceRu { get; set; }

    [Column("current_organization_service_en")]
    [StringLength(50)]
    public string? CurrentOrganizationServiceEn { get; set; }

    [Column("view_current_cargo_name_ru")]
    [StringLength(50)]
    public string? ViewCurrentCargoNameRu { get; set; }

    [Column("view_current_cargo_name_en")]
    [StringLength(50)]
    public string? ViewCurrentCargoNameEn { get; set; }

    [Column("view_current_division_from_abbr_ru")]
    [StringLength(50)]
    public string? ViewCurrentDivisionFromAbbrRu { get; set; }

    [Column("view_current_division_from_abbr_en")]
    [StringLength(50)]
    public string? ViewCurrentDivisionFromAbbrEn { get; set; }

    [Column("view_current_division_on_abbr_ru")]
    [StringLength(50)]
    public string? ViewCurrentDivisionOnAbbrRu { get; set; }

    [Column("view_current_division_on_abbr_en")]
    [StringLength(50)]
    public string? ViewCurrentDivisionOnAbbrEn { get; set; }

    [Column("view_current_external_station_on_name_ru")]
    [StringLength(50)]
    public string? ViewCurrentExternalStationOnNameRu { get; set; }

    [Column("view_current_external_station_on_name_en")]
    [StringLength(50)]
    public string? ViewCurrentExternalStationOnNameEn { get; set; }

    [Column("view_current_station_from_amkr_abbr_ru")]
    [StringLength(50)]
    public string? ViewCurrentStationFromAmkrAbbrRu { get; set; }

    [Column("view_current_station_from_amkr_abbr_en")]
    [StringLength(50)]
    public string? ViewCurrentStationFromAmkrAbbrEn { get; set; }

    [Column("view_current_station_on_amkr_abbr_ru")]
    [StringLength(50)]
    public string? ViewCurrentStationOnAmkrAbbrRu { get; set; }

    [Column("view_current_station_on_amkr_abbr_en")]
    [StringLength(50)]
    public string? ViewCurrentStationOnAmkrAbbrEn { get; set; }

    [Column("internal_doc_num")]
    [StringLength(20)]
    public string? InternalDocNum { get; set; }

    [Column("id_weighing_num")]
    public int? IdWeighingNum { get; set; }

    [Column("move_cargo_doc_received", TypeName = "datetime")]
    public DateTime? MoveCargoDocReceived { get; set; }

    [Column("current_cargo_id_group")]
    public int? CurrentCargoIdGroup { get; set; }

    [Column("current_cargo_group_name_ru")]
    [StringLength(50)]
    public string? CurrentCargoGroupNameRu { get; set; }

    [Column("current_cargo_group_name_en")]
    [StringLength(50)]
    public string? CurrentCargoGroupNameEn { get; set; }

    [Column("current_cargo_id_cargo")]
    public int? CurrentCargoIdCargo { get; set; }

    [Column("current_cargo_name_ru")]
    [StringLength(50)]
    public string? CurrentCargoNameRu { get; set; }

    [Column("current_cargo_name_en")]
    [StringLength(50)]
    public string? CurrentCargoNameEn { get; set; }

    [Column("current_internal_cargo_id_group")]
    public int? CurrentInternalCargoIdGroup { get; set; }

    [Column("current_internal_cargo_group_name_ru")]
    [StringLength(50)]
    public string? CurrentInternalCargoGroupNameRu { get; set; }

    [Column("current_internal_cargo_group_name_en")]
    [StringLength(50)]
    public string? CurrentInternalCargoGroupNameEn { get; set; }

    [Column("current_internal_cargo_id_internal_cargo")]
    public int? CurrentInternalCargoIdInternalCargo { get; set; }

    [Column("current_internal_cargo_name_ru")]
    [StringLength(50)]
    public string? CurrentInternalCargoNameRu { get; set; }

    [Column("current_internal_cargo_name_en")]
    [StringLength(50)]
    public string? CurrentInternalCargoNameEn { get; set; }

    [Column("current_vesg")]
    public int? CurrentVesg { get; set; }

    [Column("id_station_from_amkr")]
    public int? IdStationFromAmkr { get; set; }

    [Column("current_station_from_amkr_name_ru")]
    [StringLength(50)]
    public string? CurrentStationFromAmkrNameRu { get; set; }

    [Column("current_station_from_amkr_name_en")]
    [StringLength(50)]
    public string? CurrentStationFromAmkrNameEn { get; set; }

    [Column("current_station_from_amkr_abbr_ru")]
    [StringLength(50)]
    public string? CurrentStationFromAmkrAbbrRu { get; set; }

    [Column("current_station_from_amkr_abbr_en")]
    [StringLength(50)]
    public string? CurrentStationFromAmkrAbbrEn { get; set; }

    [Column("id_division_from")]
    public int? IdDivisionFrom { get; set; }

    [Column("current_division_from_code")]
    [StringLength(5)]
    public string? CurrentDivisionFromCode { get; set; }

    [Column("current_division_from_name_ru")]
    [StringLength(250)]
    public string? CurrentDivisionFromNameRu { get; set; }

    [Column("current_division_from_name_en")]
    [StringLength(250)]
    public string? CurrentDivisionFromNameEn { get; set; }

    [Column("current_division_from_abbr_ru")]
    [StringLength(50)]
    public string? CurrentDivisionFromAbbrRu { get; set; }

    [Column("current_division_from_abbr_en")]
    [StringLength(50)]
    public string? CurrentDivisionFromAbbrEn { get; set; }

    [Column("id_wim_load")]
    public long? IdWimLoad { get; set; }

    [Column("id_wim_redirection")]
    public long? IdWimRedirection { get; set; }

    [Column("code_external_station")]
    public int? CodeExternalStation { get; set; }

    [Column("current_external_station_on_name_ru")]
    [StringLength(50)]
    public string? CurrentExternalStationOnNameRu { get; set; }

    [Column("current_external_station_on_name_en")]
    [StringLength(50)]
    public string? CurrentExternalStationOnNameEn { get; set; }

    [Column("id_station_on_amkr")]
    public int? IdStationOnAmkr { get; set; }

    [Column("current_station_on_amkr_name_ru")]
    [StringLength(50)]
    public string? CurrentStationOnAmkrNameRu { get; set; }

    [Column("current_station_on_amkr_name_en")]
    [StringLength(50)]
    public string? CurrentStationOnAmkrNameEn { get; set; }

    [Column("current_station_on_amkr_abbr_ru")]
    [StringLength(50)]
    public string? CurrentStationOnAmkrAbbrRu { get; set; }

    [Column("current_station_on_amkr_abbr_en")]
    [StringLength(50)]
    public string? CurrentStationOnAmkrAbbrEn { get; set; }

    [Column("id_division_on")]
    public int? IdDivisionOn { get; set; }

    [Column("current_division_on_code")]
    [StringLength(5)]
    public string? CurrentDivisionOnCode { get; set; }

    [Column("current_division_on_name_ru")]
    [StringLength(250)]
    public string? CurrentDivisionOnNameRu { get; set; }

    [Column("current_division_on_name_en")]
    [StringLength(250)]
    public string? CurrentDivisionOnNameEn { get; set; }

    [Column("current_division_on_abbr_ru")]
    [StringLength(50)]
    public string? CurrentDivisionOnAbbrRu { get; set; }

    [Column("current_division_on_abbr_en")]
    [StringLength(50)]
    public string? CurrentDivisionOnAbbrEn { get; set; }

    [Column("move_cargo_create", TypeName = "datetime")]
    public DateTime? MoveCargoCreate { get; set; }

    [Column("move_cargo_create_user")]
    [StringLength(50)]
    public string? MoveCargoCreateUser { get; set; }

    [Column("move_cargo_change", TypeName = "datetime")]
    public DateTime? MoveCargoChange { get; set; }

    [Column("move_cargo_change_user")]
    [StringLength(50)]
    public string? MoveCargoChangeUser { get; set; }

    [Column("move_cargo_close", TypeName = "datetime")]
    public DateTime? MoveCargoClose { get; set; }

    [Column("move_cargo_close_user")]
    [StringLength(50)]
    public string? MoveCargoCloseUser { get; set; }

    [Column("arrival_duration")]
    public int? ArrivalDuration { get; set; }

    [Column("arrival_idle_time")]
    public int? ArrivalIdleTime { get; set; }

    [Column("arrival_usage_fee", TypeName = "numeric(2, 2)")]
    public decimal ArrivalUsageFee { get; set; }

    [Column("current_station_duration")]
    public int? CurrentStationDuration { get; set; }

    [Column("current_way_duration")]
    public int? CurrentWayDuration { get; set; }

    [Column("current_station_idle_time")]
    public int? CurrentStationIdleTime { get; set; }

    [Column("sap_incoming_supply_num")]
    [StringLength(10)]
    public string? SapIncomingSupplyNum { get; set; }

    [Column("sap_incoming_supply_pos")]
    [StringLength(10)]
    public string? SapIncomingSupplyPos { get; set; }

    [Column("sap_incoming_supply_date", TypeName = "date")]
    public DateTime? SapIncomingSupplyDate { get; set; }

    [Column("sap_incoming_supply_time")]
    public TimeSpan? SapIncomingSupplyTime { get; set; }

    [Column("sap_incoming_supply_warehouse_code")]
    [StringLength(4)]
    public string? SapIncomingSupplyWarehouseCode { get; set; }

    [Column("sap_incoming_supply_warehouse_name")]
    [StringLength(16)]
    public string? SapIncomingSupplyWarehouseName { get; set; }

    [Column("sap_incoming_supply_cargo_code")]
    [StringLength(18)]
    public string? SapIncomingSupplyCargoCode { get; set; }

    [Column("sap_incoming_supply_cargo_name")]
    [StringLength(40)]
    public string? SapIncomingSupplyCargoName { get; set; }

    [Column("sap_incoming_supply_cargo_ban")]
    [StringLength(4)]
    public string? SapIncomingSupplyCargoBan { get; set; }

    [Column("sap_outgoing_supply_num")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SapOutgoingSupplyNum { get; set; }

    [Column("sap_outgoing_supply_date", TypeName = "date")]
    public DateTime? SapOutgoingSupplyDate { get; set; }

    [Column("sap_outgoing_supply_cargo_name")]
    [StringLength(160)]
    public string? SapOutgoingSupplyCargoName { get; set; }

    [Column("sap_outgoing_supply_cargo_code")]
    [StringLength(17)]
    [Unicode(false)]
    public string? SapOutgoingSupplyCargoCode { get; set; }

    [Column("sap_outgoing_supply_shipper_name")]
    [StringLength(150)]
    public string? SapOutgoingSupplyShipperName { get; set; }

    [Column("sap_outgoing_supply_shipper_code")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SapOutgoingSupplyShipperCode { get; set; }

    [Column("sap_outgoing_supply_destination_station_name")]
    [StringLength(30)]
    public string? SapOutgoingSupplyDestinationStationName { get; set; }

    [Column("sap_outgoing_supply_destination_station_code")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SapOutgoingSupplyDestinationStationCode { get; set; }

    [Column("sap_outgoing_supply_border_checkpoint_name")]
    [StringLength(30)]
    public string? SapOutgoingSupplyBorderCheckpointName { get; set; }

    [Column("sap_outgoing_supply_border_checkpoint_code")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SapOutgoingSupplyBorderCheckpointCode { get; set; }

    [Column("sap_outgoing_supply_netto")]
    public double? SapOutgoingSupplyNetto { get; set; }

    [Column("sap_outgoing_supply_warehouse_code")]
    [StringLength(4)]
    [Unicode(false)]
    public string? SapOutgoingSupplyWarehouseCode { get; set; }

    [Column("sap_outgoing_supply_warehouse_name")]
    [StringLength(20)]
    public string? SapOutgoingSupplyWarehouseName { get; set; }

    [Column("sap_outgoing_supply_responsible_post")]
    [StringLength(50)]
    public string? SapOutgoingSupplyResponsiblePost { get; set; }

    [Column("sap_outgoing_supply_responsible_fio")]
    [StringLength(50)]
    public string? SapOutgoingSupplyResponsibleFio { get; set; }

    [Column("sap_outgoing_supply_payer_code")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SapOutgoingSupplyPayerCode { get; set; }

    [Column("sap_outgoing_supply_payer_name")]
    [StringLength(50)]
    public string? SapOutgoingSupplyPayerName { get; set; }

    [Column("instructional_letters_num")]
    [StringLength(20)]
    public string? InstructionalLettersNum { get; set; }

    [Column("instructional_letters_datetime", TypeName = "datetime")]
    public DateTime? InstructionalLettersDatetime { get; set; }

    [Column("instructional_letters_station_code")]
    public int? InstructionalLettersStationCode { get; set; }

    [Column("instructional_letters_station_name")]
    [StringLength(50)]
    public string? InstructionalLettersStationName { get; set; }

    [Column("instructional_letters_note")]
    [StringLength(500)]
    public string? InstructionalLettersNote { get; set; }

    [Column("wagon_brutto_doc")]
    public int? WagonBruttoDoc { get; set; }

    [Column("wagon_brutto_amkr")]
    public int WagonBruttoAmkr { get; set; }

    [Column("wagon_tara_doc")]
    public int? WagonTaraDoc { get; set; }

    [Column("wagon_tara_uz")]
    public double? WagonTaraUz { get; set; }

    [Column("wagon_tara_arc_doc")]
    public int? WagonTaraArcDoc { get; set; }

    [Column("wagon_vesg_doc")]
    public int? WagonVesgDoc { get; set; }

    [Column("wagon_vesg_amkr")]
    public int WagonVesgAmkr { get; set; }

    [Column("diff_vesg")]
    public int DiffVesg { get; set; }

    [Column("doc_outgoing_car")]
    public bool? DocOutgoingCar { get; set; }

    [Column("arrival_nom_doc")]
    public int? ArrivalNomDoc { get; set; }

    [Column("arrival_nom_main_doc")]
    public int? ArrivalNomMainDoc { get; set; }

    [Column("arrival_klient")]
    public bool? ArrivalKlient { get; set; }

    [Column("arrival_composition_index")]
    [StringLength(50)]
    public string? ArrivalCompositionIndex { get; set; }

    [Column("arrival_date_adoption", TypeName = "datetime")]
    public DateTime? ArrivalDateAdoption { get; set; }

    [Column("outgoing_id_return")]
    public int? OutgoingIdReturn { get; set; }

    [Column("outgoing_return_cause_ru")]
    [StringLength(150)]
    public string? OutgoingReturnCauseRu { get; set; }

    [Column("outgoing_return_cause_en")]
    [StringLength(150)]
    public string? OutgoingReturnCauseEn { get; set; }

    [Column("outgoing_date", TypeName = "datetime")]
    public DateTime? OutgoingDate { get; set; }

    [Column("outgoing_sostav_status")]
    public int? OutgoingSostavStatus { get; set; }

    [Column("wagon_ban_uz")]
    [StringLength(1000)]
    public string? WagonBanUz { get; set; }

    [Column("wagon_closed_route")]
    public bool? WagonClosedRoute { get; set; }

    [Column("wir_note")]
    [StringLength(250)]
    public string? WirNote { get; set; }

    [Column("wir_note2")]
    [StringLength(250)]
    public string? WirNote2 { get; set; }

    [Column("wir_highlight_color")]
    [StringLength(10)]
    public string? WirHighlightColor { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_user")]
    [StringLength(50)]
    public string? CloseUser { get; set; }

    [Column("old_arrival_car_id_outgoing_car")]
    public long? OldArrivalCarIdOutgoingCar { get; set; }

    [Column("old_arrival_car_id_outgoing_uz_vagon")]
    public long? OldArrivalCarIdOutgoingUzVagon { get; set; }

    [Column("old_date_outgoing", TypeName = "datetime")]
    public DateTime? OldDateOutgoing { get; set; }

    [Column("old_date_outgoing_act", TypeName = "datetime")]
    public DateTime? OldDateOutgoingAct { get; set; }

    [Column("old_outgoing_uz_vagon_id_cargo")]
    public int? OldOutgoingUzVagonIdCargo { get; set; }

    [Column("old_outgoing_uz_vagon_cargo_name_ru")]
    [StringLength(50)]
    public string? OldOutgoingUzVagonCargoNameRu { get; set; }

    [Column("old_outgoing_uz_vagon_cargo_name_en")]
    [StringLength(50)]
    public string? OldOutgoingUzVagonCargoNameEn { get; set; }

    [Column("old_outgoingl_uz_document_code_stn_to")]
    public int? OldOutgoinglUzDocumentCodeStnTo { get; set; }

    [Column("old_outgoing_uz_document_station_to_name_ru")]
    [StringLength(50)]
    public string? OldOutgoingUzDocumentStationToNameRu { get; set; }

    [Column("old_outgoing_uz_document_station_to_name_en")]
    [StringLength(50)]
    public string? OldOutgoingUzDocumentStationToNameEn { get; set; }
}
