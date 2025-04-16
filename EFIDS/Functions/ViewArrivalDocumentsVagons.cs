using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewArrivalDocumentsVagons
{
    [Key]
    [Column("arrival_car_id")]
    public long? ArrivalCarId { get; set; }

    [Column("num")]
    public int? Num { get; set; }

    [Column("arrival_car_position_arrival")]
    public int? ArrivalCarPositionArrival { get; set; }

    [Column("arrival_car_position")]
    public int? ArrivalCarPosition { get; set; }

    [Column("arrival_car_consignee")]
    public int? ArrivalCarConsignee { get; set; }

    [Column("arrival_car_num_doc")]
    [StringLength(50)]
    public string? ArrivalCarNumDoc { get; set; }

    [Column("arrival_car_id_transfer")]
    public long? ArrivalCarIdTransfer { get; set; }

    [Column("arrival_car_note")]
    [StringLength(200)]
    public string? ArrivalCarNote { get; set; }

    [Column("arrival_car_date_adoption_act", TypeName = "datetime")]
    public DateTime? ArrivalCarDateAdoptionAct { get; set; }

    [Column("arrival_car_arrival", TypeName = "datetime")]
    public DateTime? ArrivalCarArrival { get; set; }

    [Column("arrival_car_arrival_user")]
    [StringLength(50)]
    public string? ArrivalCarArrivalUser { get; set; }

    [Column("arrival_car_create", TypeName = "datetime")]
    public DateTime? ArrivalCarCreate { get; set; }

    [Column("arrival_car_create_user")]
    [StringLength(50)]
    public string? ArrivalCarCreateUser { get; set; }

    [Column("arrival_car_change", TypeName = "datetime")]
    public DateTime? ArrivalCarChange { get; set; }

    [Column("arrival_car_change_user")]
    [StringLength(50)]
    public string? ArrivalCarChangeUser { get; set; }

    [Column("arrival_sostav_id")]
    public long? ArrivalSostavId { get; set; }

    [Column("arrival_sostav_id_arrived")]
    public long? ArrivalSostavIdArrived { get; set; }

    [Column("arrival_sostav_id_sostav")]
    public long? ArrivalSostavIdSostav { get; set; }

    [Column("arrival_sostav_train")]
    public int? ArrivalSostavTrain { get; set; }

    [Column("arrival_sostav_composition_index")]
    [StringLength(50)]
    public string? ArrivalSostavCompositionIndex { get; set; }

    [Column("arrival_sostav_date_arrival", TypeName = "datetime")]
    public DateTime? ArrivalSostavDateArrival { get; set; }

    [Column("arrival_sostav_date_adoption", TypeName = "datetime")]
    public DateTime? ArrivalSostavDateAdoption { get; set; }

    [Column("arrival_sostav_date_adoption_act", TypeName = "datetime")]
    public DateTime? ArrivalSostavDateAdoptionAct { get; set; }

    [Column("arrival_sostav_id_station_from")]
    public int? ArrivalSostavIdStationFrom { get; set; }

    [Column("arrival_sostav_station_from_name_ru")]
    [StringLength(50)]
    public string? ArrivalSostavStationFromNameRu { get; set; }

    [Column("arrival_sostav_station_from_name_en")]
    [StringLength(50)]
    public string? ArrivalSostavStationFromNameEn { get; set; }

    [Column("arrival_sostav_station_from_abbr_ru")]
    [StringLength(50)]
    public string? ArrivalSostavStationFromAbbrRu { get; set; }

    [Column("arrival_sostav_station_from_abbr_en")]
    [StringLength(50)]
    public string? ArrivalSostavStationFromAbbrEn { get; set; }

    [Column("arrival_sostav_id_station_on")]
    public int? ArrivalSostavIdStationOn { get; set; }

    [Column("arrival_sostav_station_on_name_ru")]
    [StringLength(50)]
    public string? ArrivalSostavStationOnNameRu { get; set; }

    [Column("arrival_sostav_station_on_name_en")]
    [StringLength(50)]
    public string? ArrivalSostavStationOnNameEn { get; set; }

    [Column("arrival_sostav_station_on_abbr_ru")]
    [StringLength(50)]
    public string? ArrivalSostavStationOnAbbrRu { get; set; }

    [Column("arrival_sostav_station_on_abbr_en")]
    [StringLength(50)]
    public string? ArrivalSostavStationOnAbbrEn { get; set; }

    [Column("arrival_sostav_id_way")]
    public int? ArrivalSostavIdWay { get; set; }

    [Column("arrival_sostav_way_on_id_park")]
    public int? ArrivalSostavWayOnIdPark { get; set; }

    [Column("arrival_sostav_way_on_num_ru")]
    [StringLength(20)]
    public string? ArrivalSostavWayOnNumRu { get; set; }

    [Column("arrival_sostav_way_on_num_en")]
    [StringLength(20)]
    public string? ArrivalSostavWayOnNumEn { get; set; }

    [Column("arrival_sostav_way_on_name_ru")]
    [StringLength(100)]
    public string? ArrivalSostavWayOnNameRu { get; set; }

    [Column("arrival_sostav_way_on_name_en")]
    [StringLength(100)]
    public string? ArrivalSostavWayOnNameEn { get; set; }

    [Column("arrival_sostav_way_on_abbr_ru")]
    [StringLength(50)]
    public string? ArrivalSostavWayOnAbbrRu { get; set; }

    [Column("arrival_sostav_way_on_abbr_en")]
    [StringLength(50)]
    public string? ArrivalSostavWayOnAbbrEn { get; set; }

    [Column("arrival_sostav_numeration")]
    public bool? ArrivalSostavNumeration { get; set; }

    [Column("arrival_sostav_num_doc")]
    public int? ArrivalSostavNumDoc { get; set; }

    [Column("arrival_sostav_count")]
    public int? ArrivalSostavCount { get; set; }

    [Column("arrival_sostav_status")]
    public int? ArrivalSostavStatus { get; set; }

    [Column("arrival_sostav_note")]
    [StringLength(200)]
    public string? ArrivalSostavNote { get; set; }

    [Column("arrival_sostav_create", TypeName = "datetime")]
    public DateTime? ArrivalSostavCreate { get; set; }

    [Column("arrival_sostav_create_user")]
    [StringLength(50)]
    public string? ArrivalSostavCreateUser { get; set; }

    [Column("arrival_sostav_change", TypeName = "datetime")]
    public DateTime? ArrivalSostavChange { get; set; }

    [Column("arrival_sostav_change_user")]
    [StringLength(50)]
    public string? ArrivalSostavChangeUser { get; set; }

    [Column("arrival_uz_vagon_id")]
    public long ArrivalUzVagonId { get; set; }

    [Column("arrival_uz_vagon_id_owner")]
    public int? ArrivalUzVagonIdOwner { get; set; }

    [Column("arrival_uz_vagon_owner_wagon_ru")]
    [StringLength(100)]
    public string? ArrivalUzVagonOwnerWagonRu { get; set; }

    [Column("arrival_uz_vagon_owner_wagon_en")]
    [StringLength(100)]
    public string? ArrivalUzVagonOwnerWagonEn { get; set; }

    [Column("arrival_uz_vagon_owner_wagon_abbr_ru")]
    [StringLength(20)]
    public string? ArrivalUzVagonOwnerWagonAbbrRu { get; set; }

    [Column("arrival_uz_vagon_owner_wagon_abbr_en")]
    [StringLength(20)]
    public string? ArrivalUzVagonOwnerWagonAbbrEn { get; set; }

    [Column("arrival_uz_vagon_id_type_ownership")]
    public int? ArrivalUzVagonIdTypeOwnership { get; set; }

    [Column("arrival_uz_vagon_type_ownership_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonTypeOwnershipRu { get; set; }

    [Column("arrival_uz_vagon_type_ownership_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonTypeOwnershipEn { get; set; }

    [Column("arrival_uz_vagon_id_countrys")]
    public int? ArrivalUzVagonIdCountrys { get; set; }

    [Column("arrival_uz_vagon_wagon_adm")]
    public int? ArrivalUzVagonWagonAdm { get; set; }

    [Column("arrival_uz_vagon_wagon_adm_name_ru")]
    [StringLength(100)]
    public string? ArrivalUzVagonWagonAdmNameRu { get; set; }

    [Column("arrival_uz_vagon_wagon_adm_name_en")]
    [StringLength(100)]
    public string? ArrivalUzVagonWagonAdmNameEn { get; set; }

    [Column("arrival_uz_vagon_wagon_adm_abbr_ru")]
    [StringLength(10)]
    public string? ArrivalUzVagonWagonAdmAbbrRu { get; set; }

    [Column("arrival_uz_vagon_wagon_adm_abbr_en")]
    [StringLength(10)]
    public string? ArrivalUzVagonWagonAdmAbbrEn { get; set; }

    [Column("arrival_uz_vagon_id_genus")]
    public int? ArrivalUzVagonIdGenus { get; set; }

    [Column("arrival_uz_vagon_rod")]
    public int? ArrivalUzVagonRod { get; set; }

    [Column("arrival_uz_vagon_rod_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonRodNameRu { get; set; }

    [Column("arrival_uz_vagon_rod_name_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonRodNameEn { get; set; }

    [Column("arrival_uz_vagon_rod_abbr_ru")]
    [StringLength(5)]
    public string? ArrivalUzVagonRodAbbrRu { get; set; }

    [Column("arrival_uz_vagon_rod_abbr_en")]
    [StringLength(5)]
    public string? ArrivalUzVagonRodAbbrEn { get; set; }

    [Column("arrival_uz_vagon_wagon_kol_os")]
    public int? ArrivalUzVagonWagonKolOs { get; set; }

    [Column("arrival_uz_vagon_wagon_usl_tip")]
    [StringLength(10)]
    public string? ArrivalUzVagonWagonUslTip { get; set; }

    [Column("arrival_uz_vagon_wagon_date_rem_uz", TypeName = "datetime")]
    public DateTime? ArrivalUzVagonWagonDateRemUz { get; set; }

    [Column("arrival_uz_vagon_wagon_date_rem_vag", TypeName = "datetime")]
    public DateTime? ArrivalUzVagonWagonDateRemVag { get; set; }

    [Column("arrival_uz_vagon_id_condition")]
    public int? ArrivalUzVagonIdCondition { get; set; }

    [Column("arrival_uz_vagon_condition_name_ru")]
    [StringLength(100)]
    public string? ArrivalUzVagonConditionNameRu { get; set; }

    [Column("arrival_uz_vagon_condition_name_en")]
    [StringLength(100)]
    public string? ArrivalUzVagonConditionNameEn { get; set; }

    [Column("arrival_uz_vagon_condition_abbr_ru")]
    [StringLength(20)]
    public string? ArrivalUzVagonConditionAbbrRu { get; set; }

    [Column("arrival_uz_vagon_condition_abbr_en")]
    [StringLength(20)]
    public string? ArrivalUzVagonConditionAbbrEn { get; set; }

    [Column("arrival_uz_vagon_condition_repairs")]
    public bool? ArrivalUzVagonConditionRepairs { get; set; }

    [Column("arrival_uz_vagon_id_wagons_rent_arrival")]
    public int? ArrivalUzVagonIdWagonsRentArrival { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_id_operator")]
    public int? ArrivalUzVagonArrivalWagonsRentIdOperator { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_operators_ru")]
    [StringLength(100)]
    public string? ArrivalUzVagonArrivalWagonsRentOperatorsRu { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_operators_en")]
    [StringLength(100)]
    public string? ArrivalUzVagonArrivalWagonsRentOperatorsEn { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_operator_abbr_ru")]
    [StringLength(20)]
    public string? ArrivalUzVagonArrivalWagonsRentOperatorAbbrRu { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_operator_abbr_en")]
    [StringLength(20)]
    public string? ArrivalUzVagonArrivalWagonsRentOperatorAbbrEn { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_start", TypeName = "datetime")]
    public DateTime? ArrivalUzVagonArrivalWagonsRentStart { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_end", TypeName = "datetime")]
    public DateTime? ArrivalUzVagonArrivalWagonsRentEnd { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_operator_paid")]
    public bool? ArrivalUzVagonArrivalWagonsRentOperatorPaid { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_operator_color")]
    [StringLength(10)]
    public string? ArrivalUzVagonArrivalWagonsRentOperatorColor { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_id_limiting")]
    public int? ArrivalUzVagonArrivalWagonsRentIdLimiting { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_limiting_name_ru")]
    [StringLength(100)]
    public string? ArrivalUzVagonArrivalWagonsRentLimitingNameRu { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_limiting_name_en")]
    [StringLength(100)]
    public string? ArrivalUzVagonArrivalWagonsRentLimitingNameEn { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_limiting_abbr_ru")]
    [StringLength(30)]
    public string? ArrivalUzVagonArrivalWagonsRentLimitingAbbrRu { get; set; }

    [Column("arrival_uz_vagon_arrival_wagons_rent_limiting_abbr_en")]
    [StringLength(30)]
    public string? ArrivalUzVagonArrivalWagonsRentLimitingAbbrEn { get; set; }

    [Column("arrival_uz_vagon_id_type")]
    public int? ArrivalUzVagonIdType { get; set; }

    [Column("arrival_uz_vagon_type_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonTypeRu { get; set; }

    [Column("arrival_uz_vagon_type_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonTypeEn { get; set; }

    [Column("arrival_uz_vagon_gruzp")]
    public double? ArrivalUzVagonGruzp { get; set; }

    [Column("arrival_uz_vagon_u_tara")]
    public int? ArrivalUzVagonUTara { get; set; }

    [Column("arrival_uz_vagon_ves_tary_arc")]
    public int? ArrivalUzVagonVesTaryArc { get; set; }

    [Column("arrival_uz_vagon_gruzp_uz")]
    public double? ArrivalUzVagonGruzpUz { get; set; }

    [Column("arrival_uz_vagon_tara_uz")]
    public double? ArrivalUzVagonTaraUz { get; set; }

    [Column("arrival_uz_vagon_route")]
    public bool? ArrivalUzVagonRoute { get; set; }

    [Column("arrival_uz_vagon_note_vagon")]
    [StringLength(200)]
    public string? ArrivalUzVagonNoteVagon { get; set; }

    [Column("arrival_uz_vagon_id_cargo")]
    public int? ArrivalUzVagonIdCargo { get; set; }

    [Column("arrival_uz_vagon_cargo_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoNameRu { get; set; }

    [Column("arrival_uz_vagon_cargo_name_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoNameEn { get; set; }

    [Column("arrival_uz_vagon_id_group")]
    public int? ArrivalUzVagonIdGroup { get; set; }

    [Column("arrival_uz_vagon_cargo_group_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoGroupNameRu { get; set; }

    [Column("arrival_uz_vagon_cargo_group_name_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonCargoGroupNameEn { get; set; }

    [Column("arrival_uz_vagon_id_cargo_etsng")]
    public int? ArrivalUzVagonIdCargoEtsng { get; set; }

    [Column("arrival_uz_vagon_cargo_etsng_code")]
    public int? ArrivalUzVagonCargoEtsngCode { get; set; }

    [Column("arrival_uz_vagon_cargo_etsng_name_ru")]
    [StringLength(250)]
    public string? ArrivalUzVagonCargoEtsngNameRu { get; set; }

    [Column("arrival_uz_vagon_cargo_etsng_name_en")]
    [StringLength(250)]
    public string? ArrivalUzVagonCargoEtsngNameEn { get; set; }

    [Column("arrival_uz_vagon_id_cargo_gng")]
    public int? ArrivalUzVagonIdCargoGng { get; set; }

    [Column("arrival_uz_vagon_cargo_gng_code")]
    public int? ArrivalUzVagonCargoGngCode { get; set; }

    [Column("arrival_uz_vagon_cargo_gng_name_ru")]
    [StringLength(250)]
    public string? ArrivalUzVagonCargoGngNameRu { get; set; }

    [Column("arrival_uz_vagon_cargo_gng_name_en")]
    [StringLength(250)]
    public string? ArrivalUzVagonCargoGngNameEn { get; set; }

    [Column("arrival_uz_vagon_id_certification_data")]
    public int? ArrivalUzVagonIdCertificationData { get; set; }

    [Column("arrival_uz_vagon_sertification_data_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonSertificationDataRu { get; set; }

    [Column("arrival_uz_vagon_sertification_data_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonSertificationDataEn { get; set; }

    [Column("arrival_uz_vagon_id_commercial_condition")]
    public int? ArrivalUzVagonIdCommercialCondition { get; set; }

    [Column("arrival_uz_vagon_commercial_condition_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonCommercialConditionRu { get; set; }

    [Column("arrival_uz_vagon_commercial_condition_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonCommercialConditionEn { get; set; }

    [Column("arrival_uz_vagon_zayava")]
    [StringLength(100)]
    public string? ArrivalUzVagonZayava { get; set; }

    [Column("arrival_uz_vagon_kol_pac")]
    public int? ArrivalUzVagonKolPac { get; set; }

    [Column("arrival_uz_vagon_pac")]
    [StringLength(3)]
    public string? ArrivalUzVagonPac { get; set; }

    [Column("arrival_uz_vagon_vesg")]
    public int? ArrivalUzVagonVesg { get; set; }

    [Column("arrival_uz_vagon_vesg_reweighing")]
    public double? ArrivalUzVagonVesgReweighing { get; set; }

    [Column("arrival_uz_vagon_nom_zpu")]
    [StringLength(20)]
    public string? ArrivalUzVagonNomZpu { get; set; }

    [Column("arrival_uz_vagon_danger")]
    [StringLength(3)]
    [Unicode(false)]
    public string? ArrivalUzVagonDanger { get; set; }

    [Column("arrival_uz_vagon_danger_kod")]
    [StringLength(4)]
    [Unicode(false)]
    public string? ArrivalUzVagonDangerKod { get; set; }

    [Column("arrival_uz_vagon_cargo_returns")]
    public bool? ArrivalUzVagonCargoReturns { get; set; }

    [Column("arrival_uz_vagon_id_station_on_amkr")]
    public int? ArrivalUzVagonIdStationOnAmkr { get; set; }

    [Column("arrival_uz_vagon_station_amkr_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonStationAmkrNameRu { get; set; }

    [Column("arrival_uz_vagon_station_amkr_name_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonStationAmkrNameEn { get; set; }

    [Column("arrival_uz_vagon_station_amkr_abbr_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonStationAmkrAbbrRu { get; set; }

    [Column("arrival_uz_vagon_station_amkr_abbr_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonStationAmkrAbbrEn { get; set; }

    [Column("arrival_uz_vagon_id_division_on_amkr")]
    public int? ArrivalUzVagonIdDivisionOnAmkr { get; set; }

    [Column("arrival_uz_vagon_division_code")]
    [StringLength(5)]
    public string? ArrivalUzVagonDivisionCode { get; set; }

    [Column("arrival_uz_vagon_name_division_ru")]
    [StringLength(250)]
    public string? ArrivalUzVagonNameDivisionRu { get; set; }

    [Column("arrival_uz_vagon_name_division_en")]
    [StringLength(250)]
    public string? ArrivalUzVagonNameDivisionEn { get; set; }

    [Column("arrival_uz_vagon_division_abbr_ru")]
    [StringLength(50)]
    public string? ArrivalUzVagonDivisionAbbrRu { get; set; }

    [Column("arrival_uz_vagon_division_abbr_en")]
    [StringLength(50)]
    public string? ArrivalUzVagonDivisionAbbrEn { get; set; }

    [Column("arrival_uz_vagon_id_type_devision")]
    public int? ArrivalUzVagonIdTypeDevision { get; set; }

    [Column("arrival_uz_vagon_empty_car")]
    public bool? ArrivalUzVagonEmptyCar { get; set; }

    [Column("arrival_uz_vagon_kol_conductor")]
    public int? ArrivalUzVagonKolConductor { get; set; }

    [Column("arrival_uz_vagon_manual")]
    public bool? ArrivalUzVagonManual { get; set; }

    [Column("arrival_uz_vagon_pay_summa")]
    public int? ArrivalUzVagonPaySumma { get; set; }

    [Column("arrival_uz_vagon_summa_001")]
    public long? ArrivalUzVagonSumma001 { get; set; }

    [Column("arrival_uz_vagon_id_act_services1")]
    public int? ArrivalUzVagonIdActServices1 { get; set; }

    [Column("arrival_uz_vagon_id_act_services2")]
    public int? ArrivalUzVagonIdActServices2 { get; set; }

    [Column("arrival_uz_vagon_id_act_services3")]
    public int? ArrivalUzVagonIdActServices3 { get; set; }

    [Column("arrival_uz_vagon_num_act_services1")]
    [StringLength(20)]
    public string? ArrivalUzVagonNumActServices1 { get; set; }

    [Column("arrival_uz_vagon_num_act_services2")]
    [StringLength(20)]
    public string? ArrivalUzVagonNumActServices2 { get; set; }

    [Column("arrival_uz_vagon_num_act_services3")]
    [StringLength(20)]
    public string? ArrivalUzVagonNumActServices3 { get; set; }

    [Column("arrival_uz_vagon_create", TypeName = "datetime")]
    public DateTime ArrivalUzVagonCreate { get; set; }

    [Column("arrival_uz_vagon_create_user")]
    [StringLength(50)]
    public string ArrivalUzVagonCreateUser { get; set; } = null!;

    [Column("arrival_uz_vagon_change", TypeName = "datetime")]
    public DateTime? ArrivalUzVagonChange { get; set; }

    [Column("arrival_uz_vagon_change_user")]
    [StringLength(50)]
    public string? ArrivalUzVagonChangeUser { get; set; }

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

    [Column("arrival_uz_document_from_code_inlandrailway")]
    public int? ArrivalUzDocumentFromCodeInlandrailway { get; set; }

    [Column("arrival_uz_document_from_inlandrailway_name_ru")]
    [StringLength(150)]
    public string? ArrivalUzDocumentFromInlandrailwayNameRu { get; set; }

    [Column("arrival_uz_document_from_inlandrailway_name_en")]
    [StringLength(150)]
    public string? ArrivalUzDocumentFromInlandrailwayNameEn { get; set; }

    [Column("arrival_uz_document_from_inlandrailway_abbr_ru")]
    [StringLength(20)]
    public string? ArrivalUzDocumentFromInlandrailwayAbbrRu { get; set; }

    [Column("arrival_uz_document_from_inlandrailway_abbr_en")]
    [StringLength(20)]
    public string? ArrivalUzDocumentFromInlandrailwayAbbrEn { get; set; }

    [Column("arrival_uz_document_from_code_railway")]
    public int? ArrivalUzDocumentFromCodeRailway { get; set; }

    [Column("arrival_uz_document_code_stn_to")]
    public int? ArrivalUzDocumentCodeStnTo { get; set; }

    [Column("arrival_uz_document_station_to_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzDocumentStationToNameRu { get; set; }

    [Column("arrival_uz_document_station_to_name_en")]
    [StringLength(50)]
    public string? ArrivalUzDocumentStationToNameEn { get; set; }

    [Column("arrival_uz_document_to_code_inlandrailway")]
    public int? ArrivalUzDocumentToCodeInlandrailway { get; set; }

    [Column("arrival_uz_document_to_inlandrailway_name_ru")]
    [StringLength(150)]
    public string? ArrivalUzDocumentToInlandrailwayNameRu { get; set; }

    [Column("arrival_uz_document_to_inlandrailway_name_en")]
    [StringLength(150)]
    public string? ArrivalUzDocumentToInlandrailwayNameEn { get; set; }

    [Column("arrival_uz_document_to_inlandrailway_abbr_ru")]
    [StringLength(20)]
    public string? ArrivalUzDocumentToInlandrailwayAbbrRu { get; set; }

    [Column("arrival_uz_document_to_inlandrailway_abbr_en")]
    [StringLength(20)]
    public string? ArrivalUzDocumentToInlandrailwayAbbrEn { get; set; }

    [Column("arrival_uz_document_to_code_railway")]
    public int? ArrivalUzDocumentToCodeRailway { get; set; }

    [Column("arrival_uz_document_code_border_checkpoint")]
    public int? ArrivalUzDocumentCodeBorderCheckpoint { get; set; }

    [Column("arrival_uz_document_border_checkpoint_station_name_ru")]
    [StringLength(50)]
    public string? ArrivalUzDocumentBorderCheckpointStationNameRu { get; set; }

    [Column("arrival_uz_document_border_checkpoint_station_name_en")]
    [StringLength(50)]
    public string? ArrivalUzDocumentBorderCheckpointStationNameEn { get; set; }

    [Column("arrival_uz_document_border_checkpoint_code_inlandrailway")]
    public int? ArrivalUzDocumentBorderCheckpointCodeInlandrailway { get; set; }

    [Column("arrival_uz_document_cross_time", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentCrossTime { get; set; }

    [Column("arrival_uz_document_code_shipper")]
    public int? ArrivalUzDocumentCodeShipper { get; set; }

    [Column("arrival_uz_document_shipper_name_ru")]
    [StringLength(100)]
    public string? ArrivalUzDocumentShipperNameRu { get; set; }

    [Column("arrival_uz_document_shipper_name_en")]
    [StringLength(100)]
    public string? ArrivalUzDocumentShipperNameEn { get; set; }

    [Column("arrival_uz_document_code_consignee")]
    public int? ArrivalUzDocumentCodeConsignee { get; set; }

    [Column("arrival_uz_document_name_consignee")]
    [StringLength(50)]
    public string? ArrivalUzDocumentNameConsignee { get; set; }

    [Column("arrival_uz_document_klient")]
    public bool? ArrivalUzDocumentKlient { get; set; }

    [Column("arrival_uz_document_code_payer_sender")]
    [StringLength(20)]
    public string? ArrivalUzDocumentCodePayerSender { get; set; }

    [Column("arrival_uz_document_payer_sender_name_ru")]
    [StringLength(100)]
    public string? ArrivalUzDocumentPayerSenderNameRu { get; set; }

    [Column("arrival_uz_document_payer_sender_name_en")]
    [StringLength(100)]
    public string? ArrivalUzDocumentPayerSenderNameEn { get; set; }

    [Column("arrival_uz_document_code_payer_arrival")]
    [StringLength(20)]
    public string? ArrivalUzDocumentCodePayerArrival { get; set; }

    [Column("arrival_uz_document_payer_arrival_name_ru")]
    [StringLength(100)]
    public string? ArrivalUzDocumentPayerArrivalNameRu { get; set; }

    [Column("arrival_uz_document_payer_arrival_name_en")]
    [StringLength(100)]
    public string? ArrivalUzDocumentPayerArrivalNameEn { get; set; }

    [Column("arrival_uz_document_distance_way")]
    public int? ArrivalUzDocumentDistanceWay { get; set; }

    [Column("arrival_uz_document_note")]
    [StringLength(200)]
    public string? ArrivalUzDocumentNote { get; set; }

    [Column("arrival_uz_document_parent_id")]
    public long? ArrivalUzDocumentParentId { get; set; }

    [Column("arrival_uz_document_manual")]
    public bool? ArrivalUzDocumentManual { get; set; }

    [Column("arrival_uz_document_date_otpr", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentDateOtpr { get; set; }

    [Column("arrival_uz_document_srok_end", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentSrokEnd { get; set; }

    [Column("arrival_uz_document_date_grpol", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentDateGrpol { get; set; }

    [Column("arrival_uz_document_date_pr", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentDatePr { get; set; }

    [Column("arrival_uz_document_date_vid", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentDateVid { get; set; }

    [Column("arrival_uz_document_code_payer_local")]
    [StringLength(20)]
    public string? ArrivalUzDocumentCodePayerLocal { get; set; }

    [Column("arrival_uz_document_payer_local_name_ru")]
    [StringLength(100)]
    public string? ArrivalUzDocumentPayerLocalNameRu { get; set; }

    [Column("arrival_uz_document_payer_local_name_en")]
    [StringLength(100)]
    public string? ArrivalUzDocumentPayerLocalNameEn { get; set; }

    [Column("arrival_uz_document_tariff_contract", TypeName = "money")]
    public decimal? ArrivalUzDocumentTariffContract { get; set; }

    [Column("arrival_uz_document_calc_payer", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentCalcPayer { get; set; }

    [Column("arrival_uz_document_calc_payer_user")]
    [StringLength(50)]
    public string? ArrivalUzDocumentCalcPayerUser { get; set; }

    [Column("arrival_uz_document_pay_summa")]
    public long? ArrivalUzDocumentPaySumma { get; set; }

    [Column("arrival_uz_document_create", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentCreate { get; set; }

    [Column("arrival_uz_document_create_user")]
    [StringLength(50)]
    public string? ArrivalUzDocumentCreateUser { get; set; }

    [Column("arrival_uz_document_change", TypeName = "datetime")]
    public DateTime? ArrivalUzDocumentChange { get; set; }

    [Column("arrival_uz_document_change_user")]
    [StringLength(50)]
    public string? ArrivalUzDocumentChangeUser { get; set; }

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

    [Column("sap_incoming_supply_warehouse_code_10")]
    [StringLength(4)]
    public string? SapIncomingSupplyWarehouseCode10 { get; set; }

    [Column("sap_incoming_supply_warehouse_name_10")]
    [StringLength(16)]
    public string? SapIncomingSupplyWarehouseName10 { get; set; }

    [Column("sap_incoming_supply_cargo_code")]
    [StringLength(18)]
    public string? SapIncomingSupplyCargoCode { get; set; }

    [Column("sap_incoming_supply_cargo_name")]
    [StringLength(40)]
    public string? SapIncomingSupplyCargoName { get; set; }

    [Column("sap_incoming_supply_works")]
    [StringLength(4)]
    public string? SapIncomingSupplyWorks { get; set; }

    [Column("sap_incoming_supply_ship")]
    [StringLength(35)]
    public string? SapIncomingSupplyShip { get; set; }

    [Column("sap_incoming_supply_ban")]
    [StringLength(4)]
    public string? SapIncomingSupplyBan { get; set; }
}
