﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewWagonsFiling
{
    [Key]
    [Column("id_wim")]
    public long IdWim { get; set; }

    [Column("id_wir")]
    public long IdWir { get; set; }

    [Column("is_moving")]
    public bool? IsMoving { get; set; }

    [Column("id_filing")]
    public long IdFiling { get; set; }

    [Column("num_filing")]
    [StringLength(50)]
    public string NumFiling { get; set; } = null!;

    [Column("type_filing")]
    public int TypeFiling { get; set; }

    [Column("id_division_filing")]
    public int IdDivisionFiling { get; set; }

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
    public DateTime CreateFiling { get; set; }

    [Column("create_user_filing")]
    [StringLength(50)]
    public string CreateUserFiling { get; set; } = null!;

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

    [Column("num")]
    public int Num { get; set; }

    [Column("arrival_nom_doc")]
    public int? ArrivalNomDoc { get; set; }

    [Column("arrival_nom_main_doc")]
    public int? ArrivalNomMainDoc { get; set; }

    [Column("position")]
    public int Position { get; set; }

    [Column("filing_way_start", TypeName = "datetime")]
    public DateTime FilingWayStart { get; set; }

    [Column("filing_way_end", TypeName = "datetime")]
    public DateTime? FilingWayEnd { get; set; }

    [Column("filing_start", TypeName = "datetime")]
    public DateTime? FilingStart { get; set; }

    [Column("filing_end", TypeName = "datetime")]
    public DateTime? FilingEnd { get; set; }

    [Column("filing_wim_create", TypeName = "datetime")]
    public DateTime FilingWimCreate { get; set; }

    [Column("filing_wim_create_user")]
    [StringLength(50)]
    public string FilingWimCreateUser { get; set; } = null!;

    [Column("filing_wim_close", TypeName = "datetime")]
    public DateTime? FilingWimClose { get; set; }

    [Column("filing_wim_close_user")]
    [StringLength(50)]
    public string? FilingWimCloseUser { get; set; }

    [Column("filing_id_station")]
    public int FilingIdStation { get; set; }

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

    [Column("filing_id_park")]
    public int? FilingIdPark { get; set; }

    [Column("filing_park_name_ru")]
    [StringLength(100)]
    public string? FilingParkNameRu { get; set; }

    [Column("filing_park_name_en")]
    [StringLength(100)]
    public string? FilingParkNameEn { get; set; }

    [Column("filing_park_abbr_ru")]
    [StringLength(50)]
    public string? FilingParkAbbrRu { get; set; }

    [Column("filing_park_abbr_en")]
    [StringLength(50)]
    public string? FilingParkAbbrEn { get; set; }

    [Column("filing_id_way")]
    public int FilingIdWay { get; set; }

    [Column("filing_way_num_ru")]
    [StringLength(20)]
    public string? FilingWayNumRu { get; set; }

    [Column("filing_way_num_en")]
    [StringLength(20)]
    public string? FilingWayNumEn { get; set; }

    [Column("filing_way_name_ru")]
    [StringLength(100)]
    public string? FilingWayNameRu { get; set; }

    [Column("filing_way_name_en")]
    [StringLength(100)]
    public string? FilingWayNameEn { get; set; }

    [Column("filing_way_abbr_ru")]
    [StringLength(50)]
    public string? FilingWayAbbrRu { get; set; }

    [Column("filing_way_abbr_en")]
    [StringLength(50)]
    public string? FilingWayAbbrEn { get; set; }

    [Column("filing_way_capacity")]
    public int? FilingWayCapacity { get; set; }

    [Column("filing_way_id_devision")]
    public int? FilingWayIdDevision { get; set; }

    [Column("filing_way_close", TypeName = "datetime")]
    public DateTime? FilingWayClose { get; set; }

    [Column("filing_way_delete", TypeName = "datetime")]
    public DateTime? FilingWayDelete { get; set; }

    [Column("filing_way_note")]
    [StringLength(100)]
    public string? FilingWayNote { get; set; }

    [Column("filing_division_id_division")]
    public int FilingDivisionIdDivision { get; set; }

    [Column("filing_division_code")]
    [StringLength(5)]
    public string? FilingDivisionCode { get; set; }

    [Column("filing_division_name_ru")]
    [StringLength(250)]
    public string? FilingDivisionNameRu { get; set; }

    [Column("filing_division_name_en")]
    [StringLength(250)]
    public string? FilingDivisionNameEn { get; set; }

    [Column("filing_division_abbr_ru")]
    [StringLength(50)]
    public string? FilingDivisionAbbrRu { get; set; }

    [Column("filing_division_abbr_en")]
    [StringLength(50)]
    public string? FilingDivisionAbbrEn { get; set; }

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

    [Column("wagon_gruzp")]
    public double? WagonGruzp { get; set; }

    [Column("wagon_tara")]
    public double? WagonTara { get; set; }

    [Column("wagon_kol_os")]
    public int? WagonKolOs { get; set; }

    [Column("wagon_usl_tip")]
    [StringLength(10)]
    public string? WagonUslTip { get; set; }

    [Column("wagon_date_rem_uz", TypeName = "datetime")]
    public DateTime? WagonDateRemUz { get; set; }

    [Column("wagon_date_rem_vag", TypeName = "datetime")]
    public DateTime? WagonDateRemVag { get; set; }

    [Column("wagon_sign")]
    public int? WagonSign { get; set; }

    [Column("wagon_factory_number")]
    [StringLength(10)]
    public string? WagonFactoryNumber { get; set; }

    [Column("wagon_inventory_number")]
    [StringLength(10)]
    public string? WagonInventoryNumber { get; set; }

    [Column("wagon_year_built")]
    public int? WagonYearBuilt { get; set; }

    [Column("wagon_exit_ban")]
    public bool? WagonExitBan { get; set; }

    [Column("wagon_note")]
    [StringLength(1000)]
    public string? WagonNote { get; set; }

    [Column("wagon_closed_route")]
    public bool? WagonClosedRoute { get; set; }

    [Column("wagon_new_construction")]
    [StringLength(200)]
    public string? WagonNewConstruction { get; set; }

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

    [Column("arrival_condition_id_condition")]
    public int? ArrivalConditionIdCondition { get; set; }

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

    [Column("current_condition_id_condition")]
    public int? CurrentConditionIdCondition { get; set; }

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

    [Column("arrival_cargo_group_id_group")]
    public int? ArrivalCargoGroupIdGroup { get; set; }

    [Column("arrival_cargo_group_name_ru")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameRu { get; set; }

    [Column("arrival_cargo_group_name_en")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameEn { get; set; }

    [Column("arrival_cargo_id_cargo")]
    public int? ArrivalCargoIdCargo { get; set; }

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

    [Column("arrival_station_from_code")]
    public int? ArrivalStationFromCode { get; set; }

    [Column("arrival_station_from_name_ru")]
    [StringLength(50)]
    public string? ArrivalStationFromNameRu { get; set; }

    [Column("arrival_station_from_name_en")]
    [StringLength(50)]
    public string? ArrivalStationFromNameEn { get; set; }

    [Column("arrival_station_amkr_id_station")]
    public int? ArrivalStationAmkrIdStation { get; set; }

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

    [Column("arrival_division_amkr_id_division")]
    public int? ArrivalDivisionAmkrIdDivision { get; set; }

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

    [Column("filing_condition_id_condition")]
    public int? FilingConditionIdCondition { get; set; }

    [Column("filing_condition_name_ru")]
    [StringLength(100)]
    public string? FilingConditionNameRu { get; set; }

    [Column("filing_condition_name_en")]
    [StringLength(100)]
    public string? FilingConditionNameEn { get; set; }

    [Column("filing_condition_abbr_ru")]
    [StringLength(20)]
    public string? FilingConditionAbbrRu { get; set; }

    [Column("filing_condition_abbr_en")]
    [StringLength(20)]
    public string? FilingConditionAbbrEn { get; set; }

    [Column("filing_condition_red")]
    public bool? FilingConditionRed { get; set; }

    [Column("filing_id_loading_status")]
    public int? FilingIdLoadingStatus { get; set; }

    [Column("filing_loading_status_ru")]
    [StringLength(30)]
    public string? FilingLoadingStatusRu { get; set; }

    [Column("filing_loading_status_en")]
    [StringLength(30)]
    public string? FilingLoadingStatusEn { get; set; }

    [Column("filing_id_operation")]
    public int? FilingIdOperation { get; set; }

    [Column("filing_operation_name_ru")]
    [StringLength(50)]
    public string? FilingOperationNameRu { get; set; }

    [Column("filing_operation_name_en")]
    [StringLength(50)]
    public string? FilingOperationNameEn { get; set; }

    [Column("filing_operation_start", TypeName = "datetime")]
    public DateTime? FilingOperationStart { get; set; }

    [Column("filing_operation_end", TypeName = "datetime")]
    public DateTime? FilingOperationEnd { get; set; }

    [Column("filing_id_organization_service")]
    public int? FilingIdOrganizationService { get; set; }

    [Column("filing_organization_service_ru")]
    [StringLength(50)]
    public string? FilingOrganizationServiceRu { get; set; }

    [Column("filing_organization_service_en")]
    [StringLength(50)]
    public string? FilingOrganizationServiceEn { get; set; }

    [Column("filing_internal_doc_num")]
    [StringLength(20)]
    public string? FilingInternalDocNum { get; set; }

    [Column("filing_id_weighing_num")]
    public int? FilingIdWeighingNum { get; set; }

    [Column("filing_move_cargo_doc_received", TypeName = "datetime")]
    public DateTime? FilingMoveCargoDocReceived { get; set; }

    [Column("filing_cargo_id_group")]
    public int? FilingCargoIdGroup { get; set; }

    [Column("filing_cargo_group_name_ru")]
    [StringLength(50)]
    public string? FilingCargoGroupNameRu { get; set; }

    [Column("filing_cargo_group_name_en")]
    [StringLength(50)]
    public string? FilingCargoGroupNameEn { get; set; }

    [Column("filing_cargo_id_cargo")]
    public int? FilingCargoIdCargo { get; set; }

    [Column("filing_cargo_name_ru")]
    [StringLength(50)]
    public string? FilingCargoNameRu { get; set; }

    [Column("filing_cargo_name_en")]
    [StringLength(50)]
    public string? FilingCargoNameEn { get; set; }

    [Column("filing_internal_cargo_id_group")]
    public int? FilingInternalCargoIdGroup { get; set; }

    [Column("filing_internal_cargo_group_name_ru")]
    [StringLength(50)]
    public string? FilingInternalCargoGroupNameRu { get; set; }

    [Column("filing_internal_cargo_group_name_en")]
    [StringLength(50)]
    public string? FilingInternalCargoGroupNameEn { get; set; }

    [Column("filing_internal_cargo_id_internal_cargo")]
    public int? FilingInternalCargoIdInternalCargo { get; set; }

    [Column("filing_internal_cargo_name_ru")]
    [StringLength(50)]
    public string? FilingInternalCargoNameRu { get; set; }

    [Column("filing_internal_cargo_name_en")]
    [StringLength(50)]
    public string? FilingInternalCargoNameEn { get; set; }

    [Column("filing_vesg")]
    public int? FilingVesg { get; set; }

    [Column("filing_id_station_from_amkr")]
    public int? FilingIdStationFromAmkr { get; set; }

    [Column("filing_station_from_amkr_name_ru")]
    [StringLength(50)]
    public string? FilingStationFromAmkrNameRu { get; set; }

    [Column("filing_station_from_amkr_name_en")]
    [StringLength(50)]
    public string? FilingStationFromAmkrNameEn { get; set; }

    [Column("filing_station_from_amkr_abbr_ru")]
    [StringLength(50)]
    public string? FilingStationFromAmkrAbbrRu { get; set; }

    [Column("filing_station_from_amkr_abbr_en")]
    [StringLength(50)]
    public string? FilingStationFromAmkrAbbrEn { get; set; }

    [Column("filing_id_division_from")]
    public int? FilingIdDivisionFrom { get; set; }

    [Column("filing_division_from_code")]
    [StringLength(5)]
    public string? FilingDivisionFromCode { get; set; }

    [Column("filing_division_from_name_ru")]
    [StringLength(250)]
    public string? FilingDivisionFromNameRu { get; set; }

    [Column("filing_division_from_name_en")]
    [StringLength(250)]
    public string? FilingDivisionFromNameEn { get; set; }

    [Column("filing_division_from_abbr_ru")]
    [StringLength(50)]
    public string? FilingDivisionFromAbbrRu { get; set; }

    [Column("filing_division_from_abbr_en")]
    [StringLength(50)]
    public string? FilingDivisionFromAbbrEn { get; set; }

    [Column("filing_id_wim_load")]
    public long? FilingIdWimLoad { get; set; }

    [Column("filing_id_wim_redirection")]
    public long? FilingIdWimRedirection { get; set; }

    [Column("filing_code_external_station")]
    public int? FilingCodeExternalStation { get; set; }

    [Column("filing_external_station_on_name_ru")]
    [StringLength(50)]
    public string? FilingExternalStationOnNameRu { get; set; }

    [Column("filing_external_station_on_name_en")]
    [StringLength(50)]
    public string? FilingExternalStationOnNameEn { get; set; }

    [Column("filing_id_station_on_amkr")]
    public int? FilingIdStationOnAmkr { get; set; }

    [Column("filing_station_on_amkr_name_ru")]
    [StringLength(50)]
    public string? FilingStationOnAmkrNameRu { get; set; }

    [Column("filing_station_on_amkr_name_en")]
    [StringLength(50)]
    public string? FilingStationOnAmkrNameEn { get; set; }

    [Column("filing_station_on_amkr_abbr_ru")]
    [StringLength(50)]
    public string? FilingStationOnAmkrAbbrRu { get; set; }

    [Column("filing_station_on_amkr_abbr_en")]
    [StringLength(50)]
    public string? FilingStationOnAmkrAbbrEn { get; set; }

    [Column("filing_id_division_on")]
    public int? FilingIdDivisionOn { get; set; }

    [Column("filing_division_on_code")]
    [StringLength(5)]
    public string? FilingDivisionOnCode { get; set; }

    [Column("filing_division_on_name_ru")]
    [StringLength(250)]
    public string? FilingDivisionOnNameRu { get; set; }

    [Column("filing_division_on_name_en")]
    [StringLength(250)]
    public string? FilingDivisionOnNameEn { get; set; }

    [Column("filing_division_on_abbr_ru")]
    [StringLength(50)]
    public string? FilingDivisionOnAbbrRu { get; set; }

    [Column("filing_division_on_abbr_en")]
    [StringLength(50)]
    public string? FilingDivisionOnAbbrEn { get; set; }

    [Column("filing_move_cargo_create", TypeName = "datetime")]
    public DateTime? FilingMoveCargoCreate { get; set; }

    [Column("filing_move_cargo_create_user")]
    [StringLength(50)]
    public string? FilingMoveCargoCreateUser { get; set; }

    [Column("filing_move_cargo_change", TypeName = "datetime")]
    public DateTime? FilingMoveCargoChange { get; set; }

    [Column("filing_move_cargo_change_user")]
    [StringLength(50)]
    public string? FilingMoveCargoChangeUser { get; set; }

    [Column("filing_move_cargo_close", TypeName = "datetime")]
    public DateTime? FilingMoveCargoClose { get; set; }

    [Column("filing_move_cargo_close_user")]
    [StringLength(50)]
    public string? FilingMoveCargoCloseUser { get; set; }

}
