using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewOperatingBalanceRwCar
{
    [Key]
    [Column("id")]
    public long? Id { get; set; }

    [Column("wim_id")]
    public long WimId { get; set; }

    [Column("wio_id")]
    public long? WioId { get; set; }

    [Column("num")]
    public int? Num { get; set; }

    [Column("current_datatime", TypeName = "datetime")]
    public DateTime CurrentDatatime { get; set; }

    [Column("arrival_nom_main_doc")]
    public int? ArrivalNomMainDoc { get; set; }

    [Column("arrival_nom_doc")]
    public int? ArrivalNomDoc { get; set; }

    [Column("arrival_date_adoption", TypeName = "datetime")]
    public DateTime? ArrivalDateAdoption { get; set; }

    [Column("arrival_klient")]
    public bool? ArrivalKlient { get; set; }

    [Column("arrival_id_condition")]
    public int? ArrivalIdCondition { get; set; }

    [Column("arrival_condition_abbr_ru")]
    [StringLength(20)]
    public string? ArrivalConditionAbbrRu { get; set; }

    [Column("arrival_condition_abbr_en")]
    [StringLength(20)]
    public string? ArrivalConditionAbbrEn { get; set; }

    [Column("arrival_condition_red")]
    public bool? ArrivalConditionRed { get; set; }

    [Column("arrival_id_cargo_group")]
    public int? ArrivalIdCargoGroup { get; set; }

    [Column("arrival_cargo_group_name_ru")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameRu { get; set; }

    [Column("arrival_cargo_group_name_en")]
    [StringLength(50)]
    public string? ArrivalCargoGroupNameEn { get; set; }

    [Column("arrival_id_cargo")]
    public int? ArrivalIdCargo { get; set; }

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

    [Column("arrival_vesg")]
    public int? ArrivalVesg { get; set; }

    [Column("arrival_station_from_code")]
    public int? ArrivalStationFromCode { get; set; }

    [Column("arrival_station_from_name_ru")]
    [StringLength(50)]
    public string? ArrivalStationFromNameRu { get; set; }

    [Column("arrival_station_from_name_en")]
    [StringLength(50)]
    public string? ArrivalStationFromNameEn { get; set; }

    [Column("arrival_id_station_amkr")]
    public int? ArrivalIdStationAmkr { get; set; }

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

    [Column("arrival_id_division_amkr")]
    public int? ArrivalIdDivisionAmkr { get; set; }

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

    [Column("id_station_amkr")]
    public int IdStationAmkr { get; set; }

    [Column("station_amkr_name_ru")]
    [StringLength(50)]
    public string? StationAmkrNameRu { get; set; }

    [Column("station_amkr_name_en")]
    [StringLength(50)]
    public string? StationAmkrNameEn { get; set; }

    [Column("station_amkr_abbr_ru")]
    [StringLength(50)]
    public string? StationAmkrAbbrRu { get; set; }

    [Column("station_amkr_abbr_en")]
    [StringLength(50)]
    public string? StationAmkrAbbrEn { get; set; }

    [Column("id_way")]
    public int IdWay { get; set; }

    [Column("id_park")]
    public int? IdPark { get; set; }

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
    public DateTime WayStart { get; set; }

    [Column("way_end", TypeName = "datetime")]
    public DateTime? WayEnd { get; set; }

    [Column("wim_note")]
    [StringLength(250)]
    public string? WimNote { get; set; }

    [Column("id_outer_way")]
    public int? IdOuterWay { get; set; }

    [Column("outer_way_name_ru")]
    [StringLength(150)]
    public string? OuterWayNameRu { get; set; }

    [Column("outer_way_name_en")]
    [StringLength(150)]
    public string? OuterWayNameEn { get; set; }

    [Column("outer_way_start", TypeName = "datetime")]
    public DateTime? OuterWayStart { get; set; }

    [Column("outer_way_end", TypeName = "datetime")]
    public DateTime? OuterWayEnd { get; set; }

    [Column("view_type_way")]
    [StringLength(9)]
    [Unicode(false)]
    public string ViewTypeWay { get; set; } = null!;

    [Column("view_name_way_ru")]
    [StringLength(150)]
    public string? ViewNameWayRu { get; set; }

    [Column("view_name_way_en")]
    [StringLength(150)]
    public string? ViewNameWayEn { get; set; }

    [Column("wir_note")]
    [StringLength(250)]
    public string? WirNote { get; set; }

    [Column("wir_note2")]
    [StringLength(250)]
    public string? WirNote2 { get; set; }

    [Column("wir_highlight_color")]
    [StringLength(10)]
    public string? WirHighlightColor { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

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

    [Column("limiting_abbr_ru")]
    [StringLength(30)]
    public string? LimitingAbbrRu { get; set; }

    [Column("limiting_abbr_en")]
    [StringLength(30)]
    public string? LimitingAbbrEn { get; set; }

    [Column("wagon_adm")]
    public int? WagonAdm { get; set; }

    [Column("wagon_adm_abbr_ru")]
    [StringLength(100)]
    public string? WagonAdmAbbrRu { get; set; }

    [Column("wagon_adm_abbr_en")]
    [StringLength(100)]
    public string? WagonAdmAbbrEn { get; set; }

    [Column("wagon_rod")]
    public int? WagonRod { get; set; }

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

    [Column("current_condition_abbr_ru")]
    [StringLength(20)]
    public string? CurrentConditionAbbrRu { get; set; }

    [Column("current_condition_abbr_en")]
    [StringLength(20)]
    public string? CurrentConditionAbbrEn { get; set; }

    [Column("current_condition_red")]
    public bool? CurrentConditionRed { get; set; }

    [Column("current_condition_repairs")]
    public bool? CurrentConditionRepairs { get; set; }

    [Column("wagon_date_rem_uz", TypeName = "datetime")]
    public DateTime? WagonDateRemUz { get; set; }

    [Column("wagon_gruzp_doc")]
    public double? WagonGruzpDoc { get; set; }

    [Column("wagon_gruzp_uz")]
    public double? WagonGruzpUz { get; set; }

    [Column("id_loading_status")]
    public int? IdLoadingStatus { get; set; }

    [Column("loading_status_ru")]
    [StringLength(30)]
    public string? LoadingStatusRu { get; set; }

    [Column("loading_status_en")]
    [StringLength(30)]
    public string? LoadingStatusEn { get; set; }

    [Column("id_operation")]
    public int? IdOperation { get; set; }

    [Column("operation_name_ru")]
    [StringLength(50)]
    public string? OperationNameRu { get; set; }

    [Column("operation_name_en")]
    [StringLength(50)]
    public string? OperationNameEn { get; set; }

    [Column("operation_start", TypeName = "datetime")]
    public DateTime? OperationStart { get; set; }

    [Column("operation_end", TypeName = "datetime")]
    public DateTime? OperationEnd { get; set; }

    [Column("move_cargo_id")]
    public long? MoveCargoId { get; set; }

    [Column("move_cargo_internal_doc_num")]
    [StringLength(20)]
    public string? MoveCargoInternalDocNum { get; set; }

    [Column("move_cargo_id_weighing_num")]
    public int? MoveCargoIdWeighingNum { get; set; }

    [Column("move_cargo_doc_received", TypeName = "datetime")]
    public DateTime? MoveCargoDocReceived { get; set; }

    [Column("move_cargo_id_cargo")]
    public int? MoveCargoIdCargo { get; set; }

    [Column("move_cargo_id_internal_cargo")]
    public int? MoveCargoIdInternalCargo { get; set; }

    [Column("move_cargo_id_division_from")]
    public int? MoveCargoIdDivisionFrom { get; set; }

    [Column("move_cargo_id_division_on")]
    public int? MoveCargoIdDivisionOn { get; set; }

    [Column("move_cargo_code_external_station")]
    public int? MoveCargoCodeExternalStation { get; set; }

    [Column("move_cargo_empty")]
    public bool? MoveCargoEmpty { get; set; }

    [Column("move_cargo_vesg")]
    public int? MoveCargoVesg { get; set; }

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

    [Column("view_cargo_name_ru")]
    [StringLength(50)]
    public string? ViewCargoNameRu { get; set; }

    [Column("view_cargo_name_en")]
    [StringLength(50)]
    public string? ViewCargoNameEn { get; set; }

    [Column("view_division_from_abbr_ru")]
    [StringLength(50)]
    public string? ViewDivisionFromAbbrRu { get; set; }

    [Column("view_division_from_abbr_en")]
    [StringLength(50)]
    public string? ViewDivisionFromAbbrEn { get; set; }

    [Column("view_division_on_abbr_ru")]
    [StringLength(50)]
    public string? ViewDivisionOnAbbrRu { get; set; }

    [Column("view_division_on_abbr_en")]
    [StringLength(50)]
    public string? ViewDivisionOnAbbrEn { get; set; }

    [Column("view_external_station_on_name_ru")]
    [StringLength(50)]
    public string? ViewExternalStationOnNameRu { get; set; }

    [Column("view_external_station_on_name_en")]
    [StringLength(50)]
    public string? ViewExternalStationOnNameEn { get; set; }

    [Column("view_station_from_amkr_abbr_ru")]
    [StringLength(50)]
    public string? ViewStationFromAmkrAbbrRu { get; set; }

    [Column("view_station_from_amkr_abbr_en")]
    [StringLength(50)]
    public string? ViewStationFromAmkrAbbrEn { get; set; }

    [Column("view_station_on_amkr_abbr_ru")]
    [StringLength(50)]
    public string? ViewStationOnAmkrAbbrRu { get; set; }

    [Column("view_station_on_amkr_abbr_en")]
    [StringLength(50)]
    public string? ViewStationOnAmkrAbbrEn { get; set; }

    [Column("view_vesg")]
    public int? ViewVesg { get; set; }

    [Column("wim_unload_id")]
    public long? WimUnloadId { get; set; }

    [Column("wim_unload_id_filing")]
    public long? WimUnloadIdFiling { get; set; }

    [Column("wim_unload_filing_start", TypeName = "datetime")]
    public DateTime? WimUnloadFilingStart { get; set; }

    [Column("wim_unload_filing_end", TypeName = "datetime")]
    public DateTime? WimUnloadFilingEnd { get; set; }

    [Column("wim_unload_id_wio")]
    public long? WimUnloadIdWio { get; set; }

    [Column("wim_load_id")]
    public long? WimLoadId { get; set; }

    [Column("wim_load_id_filing")]
    public long? WimLoadIdFiling { get; set; }

    [Column("wim_load_filing_start", TypeName = "datetime")]
    public DateTime? WimLoadFilingStart { get; set; }

    [Column("wim_load_filing_end", TypeName = "datetime")]
    public DateTime? WimLoadFilingEnd { get; set; }

    [Column("wim_load_id_wio")]
    public long? WimLoadIdWio { get; set; }

    [Column("wim_clear_id")]
    public long? WimClearId { get; set; }

    [Column("wim_clear_id_filing")]
    public long? WimClearIdFiling { get; set; }

    [Column("wim_clear_filing_start", TypeName = "datetime")]
    public DateTime? WimClearFilingStart { get; set; }

    [Column("wim_clear_filing_end", TypeName = "datetime")]
    public DateTime? WimClearFilingEnd { get; set; }

    [Column("wim_clear_id_wio")]
    public long? WimClearIdWio { get; set; }

    [Column("current_wagon_busy")]
    public int CurrentWagonBusy { get; set; }

    [Column("current_move_busy")]
    public int CurrentMoveBusy { get; set; }

    [Column("exist_load_document")]
    public int ExistLoadDocument { get; set; }

    [Column("outgoing_date", TypeName = "datetime")]
    public DateTime? OutgoingDate { get; set; }

    [Column("date_outgoing_act", TypeName = "datetime")]
    public DateTime? DateOutgoingAct { get; set; }

    [Column("outgoing_sostav_status")]
    public int? OutgoingSostavStatus { get; set; }

    [Column("outgoing_return_cause_ru")]
    [StringLength(150)]
    public string? OutgoingReturnCauseRu { get; set; }

    [Column("outgoing_return_cause_en")]
    [StringLength(150)]
    public string? OutgoingReturnCauseEn { get; set; }

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
