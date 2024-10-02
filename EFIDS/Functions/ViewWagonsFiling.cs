using System;
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

    [Column("id_wf")]
    public long IdWf { get; set; }

    [Column("num_filing")]
    [StringLength(50)]
    public string NumFiling { get; set; } = null!;

    [Column("id_wio")]
    public long IdWio { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }

    [Column("start_filing", TypeName = "datetime")]
    public DateTime StartFiling { get; set; }

    [Column("end_filing", TypeName = "datetime")]
    public DateTime? EndFiling { get; set; }

    [Column("filing_create", TypeName = "datetime")]
    public DateTime FilingCreate { get; set; }

    [Column("filing_create_user")]
    [StringLength(50)]
    public string FilingCreateUser { get; set; } = null!;

    [Column("filing_change", TypeName = "datetime")]
    public DateTime? FilingChange { get; set; }

    [Column("filing_change_user")]
    [StringLength(50)]
    public string? FilingChangeUser { get; set; }

    [Column("filing_close", TypeName = "datetime")]
    public DateTime? FilingClose { get; set; }

    [Column("filing_close_user")]
    [StringLength(50)]
    public string? FilingCloseUser { get; set; }

    [Column("num")]
    public int Num { get; set; }

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

    [Column("filing_way_close", TypeName = "datetime")]
    public DateTime? FilingWayClose { get; set; }

    [Column("filing_way_delete", TypeName = "datetime")]
    public DateTime? FilingWayDelete { get; set; }

    [Column("filing_way_note")]
    [StringLength(100)]
    public string? FilingWayNote { get; set; }

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

    [Column("arrival_station_from_code")]
    public int? ArrivalStationFromCode { get; set; }

    [Column("arrival_station_from_name_ru")]
    [StringLength(50)]
    public string? ArrivalStationFromNameRu { get; set; }

    [Column("arrival_station_from_name_en")]
    [StringLength(50)]
    public string? ArrivalStationFromNameEn { get; set; }

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

    [Column("current_cargo_group_name_ru")]
    public int? CurrentCargoGroupNameRu { get; set; }

    [Column("current_cargo_group_name_en")]
    public int? CurrentCargoGroupNameEn { get; set; }

    [Column("current_cargo_name_ru")]
    public int? CurrentCargoNameRu { get; set; }

    [Column("current_cargo_name_en")]
    public int? CurrentCargoNameEn { get; set; }

    [Column("current_division_amkr_code")]
    public int? CurrentDivisionAmkrCode { get; set; }

    [Column("current_division_amkr_name_ru")]
    public int? CurrentDivisionAmkrNameRu { get; set; }

    [Column("current_division_amkr_name_en")]
    public int? CurrentDivisionAmkrNameEn { get; set; }

    [Column("current_division_amkr_abbr_ru")]
    public int? CurrentDivisionAmkrAbbrRu { get; set; }

    [Column("current_division_amkr_abbr_en")]
    public int? CurrentDivisionAmkrAbbrEn { get; set; }

    [Column("current_station_amkr_name_ru")]
    public int? CurrentStationAmkrNameRu { get; set; }

    [Column("current_station_amkr_name_en")]
    public int? CurrentStationAmkrNameEn { get; set; }

    [Column("current_station_amkr_abbr_ru")]
    public int? CurrentStationAmkrAbbrRu { get; set; }

    [Column("current_station_amkr_abbr_en")]
    public int? CurrentStationAmkrAbbrEn { get; set; }
}
