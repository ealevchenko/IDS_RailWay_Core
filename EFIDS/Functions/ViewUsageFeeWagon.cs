using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;
public class ViewUsageFeeWagon
{
    [Key]
    [Column("outgoing_car_id")]
    public long OutgoingCarId { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("outgoing_car_position")]
    public int OutgoingCarPosition { get; set; }

    [Column("id_wir")]
    public long? IdWir { get; set; }

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

    [Column("wagon_gruzp")]
    public double? WagonGruzp { get; set; }

    [Column("wagon_tara")]
    public double? WagonTara { get; set; }

    [Column("wagon_kol_os")]
    public int? WagonKolOs { get; set; }

    [Column("wagon_usl_tip")]
    [StringLength(10)]
    public string? WagonUslTip { get; set; }

    [Column("outgoing_uz_vagon_outgoing_id_wagons_rent")]
    public int? OutgoingUzVagonOutgoingIdWagonsRent { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_id_operator")]
    public int? OutgoingUzVagonOutgoingWagonsRentIdOperator { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_operators_ru")]
    [StringLength(100)]
    public string? OutgoingUzVagonOutgoingWagonsRentOperatorsRu { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_operators_en")]
    [StringLength(100)]
    public string? OutgoingUzVagonOutgoingWagonsRentOperatorsEn { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_operator_abbr_ru")]
    [StringLength(20)]
    public string? OutgoingUzVagonOutgoingWagonsRentOperatorAbbrRu { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_operator_abbr_en")]
    [StringLength(20)]
    public string? OutgoingUzVagonOutgoingWagonsRentOperatorAbbrEn { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_start", TypeName = "datetime")]
    public DateTime? OutgoingUzVagonOutgoingWagonsRentStart { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_end", TypeName = "datetime")]
    public DateTime? OutgoingUzVagonOutgoingWagonsRentEnd { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_operator_paid")]
    public bool? OutgoingUzVagonOutgoingWagonsRentOperatorPaid { get; set; }

    [Column("outgoing_uz_vagon_outgoing_wagons_rent_operator_color")]
    [StringLength(10)]
    public string? OutgoingUzVagonOutgoingWagonsRentOperatorColor { get; set; }

    [Column("arrival_sostav_id")]
    public long? ArrivalSostavId { get; set; }

    [Column("arrival_sostav_num_doc")]
    public int? ArrivalSostavNumDoc { get; set; }

    [Column("arrival_sostav_date_arrival", TypeName = "datetime")]
    public DateTime? ArrivalSostavDateArrival { get; set; }

    [Column("arrival_sostav_date_adoption", TypeName = "datetime")]
    public DateTime? ArrivalSostavDateAdoption { get; set; }

    [Column("arrival_sostav_date_adoption_act", TypeName = "datetime")]
    public DateTime? ArrivalSostavDateAdoptionAct { get; set; }

    [Column("arrival_uz_vagon_id_cargo")]
    public int? ArrivalUzVagonIdCargo { get; set; }

    [Column("arrival_uz_vagon_cargo_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoNameRu { get; set; }

    [Column("arrival_uz_vagon_cargo_name_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoNameEn { get; set; }

    [Column("arrival_uz_vagon_cargo_empty_weight")]
    public bool? ArrivalUzVagonCargoEmptyWeight { get; set; }

    [Column("arrival_uz_vagon_id_group")]
    public int? ArrivalUzVagonIdGroup { get; set; }

    [Column("arrival_uz_vagon_cargo_group_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoGroupNameRu { get; set; }

    [Column("arrival_uz_vagon_cargo_group_name_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoGroupNameEn { get; set; }

    [Column("arrival_uz_vagon_cargo_returns")]
    public bool? ArrivalUzVagonCargoReturns { get; set; }

    [Column("arrival_uz_document_id")]
    public long? ArrivalUzDocumentId { get; set; }

    [Column("arrival_uz_document_id_doc_uz")]
    [StringLength(50)]
    public string? ArrivalUzDocumentIdDocUz { get; set; }

    [Column("arrival_uz_document_nom_doc")]
    public int? ArrivalUzDocumentNomDoc { get; set; }

    [Column("arrival_uz_document_nom_main_doc")]
    public int? ArrivalUzDocumentNomMainDoc { get; set; }

    [Column("arrival_uz_document_code_stn_from")]
    public int? ArrivalUzDocumentCodeStnFrom { get; set; }

    [Column("arrival_uz_document_station_from_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzDocumentStationFromNameRu { get; set; }

    [Column("arrival_uz_document_station_from_name_en")]
    [StringLength(50)]
    public string? ArrivalUzDocumentStationFromNameEn { get; set; }

    [Column("outgoing_sostav_id")]
    public long? OutgoingSostavId { get; set; }

    [Column("outgoing_sostav_num_doc")]
    public int? OutgoingSostavNumDoc { get; set; }

    [Column("outgoing_sostav_date_outgoing", TypeName = "datetime")]
    public DateTime? OutgoingSostavDateOutgoing { get; set; }

    [Column("outgoing_sostav_date_outgoing_act", TypeName = "datetime")]
    public DateTime? OutgoingSostavDateOutgoingAct { get; set; }

    [Column("outgoing_sostav_date_departure_amkr", TypeName = "datetime")]
    public DateTime? OutgoingSostavDateDepartureAmkr { get; set; }

    [Column("outgoing_uz_vagon_id")]
    public long? OutgoingUzVagonId { get; set; }

    [Column("outgoing_uz_vagon_id_condition")]
    public int? OutgoingUzVagonIdCondition { get; set; }

    [Column("outgoing_uz_vagon_condition_name_ru")]
    [StringLength(100)]
    public string? OutgoingUzVagonConditionNameRu { get; set; }

    [Column("outgoing_uz_vagon_condition_name_en")]
    [StringLength(100)]
    public string? OutgoingUzVagonConditionNameEn { get; set; }

    [Column("outgoing_uz_vagon_condition_abbr_ru")]
    [StringLength(20)]
    public string? OutgoingUzVagonConditionAbbrRu { get; set; }

    [Column("outgoing_uz_vagon_condition_abbr_en")]
    [StringLength(20)]
    public string? OutgoingUzVagonConditionAbbrEn { get; set; }

    [Column("outgoing_uz_vagon_id_cargo")]
    public int? OutgoingUzVagonIdCargo { get; set; }

    [Column("outgoing_uz_vagon_cargo_name_ru")]
    [StringLength(50)]
    public string? OutgoingUzVagonCargoNameRu { get; set; }

    [Column("outgoing_uz_vagon_cargo_name_en")]
    [StringLength(50)]
    public string? OutgoingUzVagonCargoNameEn { get; set; }

    [Column("outgoing_uz_vagon_cargo_empty_weight")]
    public bool? OutgoingUzVagonCargoEmptyWeight { get; set; }

    [Column("outgoing_uz_vagon_id_group")]
    public int? OutgoingUzVagonIdGroup { get; set; }

    [Column("outgoing_uz_vagon_cargo_group_name_ru")]
    [StringLength(50)]
    public string? OutgoingUzVagonCargoGroupNameRu { get; set; }

    [Column("outgoing_uz_vagon_cargo_group_name_en")]
    [StringLength(50)]
    public string? OutgoingUzVagonCargoGroupNameEn { get; set; }

    [Column("outgoing_uz_document_id")]
    public long? OutgoingUzDocumentId { get; set; }

    [Column("outgoing_uz_document_id_doc_uz")]
    [StringLength(50)]
    public string? OutgoingUzDocumentIdDocUz { get; set; }

    [Column("outgoing_uz_document_nom_doc")]
    public int? OutgoingUzDocumentNomDoc { get; set; }

    [Column("outgoing_uz_document_code_stn_from")]
    public int? OutgoingUzDocumentCodeStnFrom { get; set; }

    [Column("outgoing_uz_document_code_stn_to")]
    public int? OutgoingUzDocumentCodeStnTo { get; set; }

    [Column("outgoing_uz_document_station_to_name_ru")]
    [StringLength(50)]
    public string? OutgoingUzDocumentStationToNameRu { get; set; }

    [Column("outgoing_uz_document_station_to_name_en")]
    [StringLength(50)]
    public string? OutgoingUzDocumentStationToNameEn { get; set; }

    [Column("usage_fee_date_adoption", TypeName = "datetime")]
    public DateTime? UsageFeeDateAdoption { get; set; }

    [Column("usage_fee_date_outgoing", TypeName = "datetime")]
    public DateTime? UsageFeeDateOutgoing { get; set; }

    [Column("usage_fee_route")]
    public bool? UsageFeeRoute { get; set; }

    [Column("usage_fee_derailment")]
    public bool? UsageFeeDerailment { get; set; }

    [Column("usage_fee_inp_cargo")]
    public bool? UsageFeeInpCargo { get; set; }

    [Column("usage_fee_id_cargo_arr")]
    public int? UsageFeeIdCargoArr { get; set; }

    [Column("usage_fee_arrival_cargo_name_ru")]
    [StringLength(50)]
    public string? UsageFeeArrivalCargoNameRu { get; set; }

    [Column("usage_fee_arrival_cargo_name_en")]
    [StringLength(50)]
    public string? UsageFeeArrivalCargoNameEn { get; set; }

    [Column("usage_fee_arrival_cargo_empty_weight")]
    public bool? UsageFeeArrivalCargoEmptyWeight { get; set; }

    [Column("usage_fee_id_cargo_group_arr")]
    public int? UsageFeeIdCargoGroupArr { get; set; }

    [Column("usage_fee_arrival_cargo_group_name_ru")]
    [StringLength(50)]
    public string? UsageFeeArrivalCargoGroupNameRu { get; set; }

    [Column("usage_fee_arrival_cargo_group_name_en")]
    [StringLength(50)]
    public string? UsageFeeArrivalCargoGroupNameEn { get; set; }

    [Column("usage_fee_date_start_unload", TypeName = "datetime")]
    public DateTime? UsageFeeDateStartUnload { get; set; }

    [Column("usage_fee_date_end_unload", TypeName = "datetime")]
    public DateTime? UsageFeeDateEndUnload { get; set; }

    [Column("usage_fee_out_cargo")]
    public bool? UsageFeeOutCargo { get; set; }

    [Column("usage_fee_id_cargo_out")]
    public int? UsageFeeIdCargoOut { get; set; }

    [Column("usage_fee_outgoing_cargo_name_ru")]
    [StringLength(50)]
    public string? UsageFeeOutgoingCargoNameRu { get; set; }

    [Column("usage_fee_outgoing_cargo_name_en")]
    [StringLength(50)]
    public string? UsageFeeOutgoingCargoNameEn { get; set; }

    [Column("usage_fee_outgoing_cargo_empty_weight")]
    public bool? UsageFeeOutgoingCargoEmptyWeight { get; set; }

    [Column("usage_fee_id_cargo_group_out")]
    public int? UsageFeeIdCargoGroupOut { get; set; }

    [Column("usage_fee_outgoing_cargo_group_name_ru")]
    [StringLength(50)]
    public string? UsageFeeOutgoingCargoGroupNameRu { get; set; }

    [Column("usage_fee_outgoing_cargo_group_name_en")]
    [StringLength(50)]
    public string? UsageFeeOutgoingCargoGroupNameEn { get; set; }

    [Column("usage_fee_date_start_load", TypeName = "datetime")]
    public DateTime? UsageFeeDateStartLoad { get; set; }

    [Column("usage_fee_date_end_load", TypeName = "datetime")]
    public DateTime? UsageFeeDateEndLoad { get; set; }

    [Column("usage_fee_code_stn_from")]
    public int? UsageFeeCodeStnFrom { get; set; }

    [Column("usage_fee_station_from_name_ru")]
    [StringLength(50)]
    public string? UsageFeeStationFromNameRu { get; set; }

    [Column("usage_fee_station_from_name_en")]
    [StringLength(50)]
    public string? UsageFeeStationFromNameEn { get; set; }

    [Column("usage_fee_code_stn_to")]
    public int? UsageFeeCodeStnTo { get; set; }

    [Column("usage_fee_station_to_name_ru")]
    [StringLength(50)]
    public string? UsageFeeStationToNameRu { get; set; }

    [Column("usage_fee_station_to_name_en")]
    [StringLength(50)]
    public string? UsageFeeStationToNameEn { get; set; }

    [Column("usage_fee_count_stage")]
    public int? UsageFeeCountStage { get; set; }

    [Column("usage_fee_id_ufp")]
    [StringLength(50)]
    public string? UsageFeeIdUfp { get; set; }

    [Column("usage_fee_id_upfpd")]
    public int? UsageFeeIdUpfpd { get; set; }

    [Column("usage_fee_id_currency")]
    public int? UsageFeeIdCurrency { get; set; }

    [Column("usage_fee_currency_ru")]
    [StringLength(50)]
    public string? UsageFeeCurrencyRu { get; set; }

    [Column("usage_fee_currency_en")]
    [StringLength(50)]
    public string? UsageFeeCurrencyEn { get; set; }

    [Column("usage_fee_currency_code")]
    public int? UsageFeeCurrencyCode { get; set; }

    [Column("usage_fee_currency_code_cc")]
    [StringLength(3)]
    public string? UsageFeeCurrencyCodeCc { get; set; }

    [Column("usage_fee_rate", TypeName = "money")]
    public decimal? UsageFeeRate { get; set; }

    [Column("usage_fee_exchange_rate", TypeName = "money")]
    public decimal? UsageFeeExchangeRate { get; set; }

    [Column("usage_fee_coefficient")]
    public double? UsageFeeCoefficient { get; set; }

    [Column("usage_fee_grace_time")]
    public int? UsageFeeGraceTime { get; set; }

    [Column("usage_fee_calc_date_start", TypeName = "datetime")]
    public DateTime? UsageFeeCalcDateStart { get; set; }

    [Column("usage_fee_calc_date_end", TypeName = "datetime")]
    public DateTime? UsageFeeCalcDateEnd { get; set; }

    [Column("usage_fee_downtime")]
    public int? UsageFeeDowntime { get; set; }

    [Column("usage_fee_use_time")]
    public int? UsageFeeUseTime { get; set; }

    [Column("usage_fee_calc_time")]
    public int? UsageFeeCalcTime { get; set; }

    [Column("usage_fee_calc_fee_amount", TypeName = "money")]
    public decimal? UsageFeeCalcFeeAmount { get; set; }

    [Column("usage_fee_manual_time")]
    public int? UsageFeeManualTime { get; set; }

    [Column("usage_fee_manual_fee_amount", TypeName = "money")]
    public decimal? UsageFeeManualFeeAmount { get; set; }

    [Column("usage_fee_note")]
    [StringLength(100)]
    public string? UsageFeeNote { get; set; }

    [Column("usage_fee_create", TypeName = "datetime")]
    public DateTime? UsageFeeCreate { get; set; }

    [Column("usage_fee_create_user")]
    [StringLength(50)]
    public string? UsageFeeCreateUser { get; set; }

    [Column("usage_fee_change", TypeName = "datetime")]
    public DateTime? UsageFeeChange { get; set; }

    [Column("usage_fee_change_user")]
    [StringLength(50)]
    public string? UsageFeeChangeUser { get; set; }

    [Column("usage_fee_error")]
    public int? UsageFeeError { get; set; }
}
