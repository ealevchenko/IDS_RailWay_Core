using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Arrival_UZ_Vagon", Schema = "IDS")]
[Index("CargoReturns", Name = "NCI_Arr_uz_vag_cargo_ret")]
public partial class ArrivalUzVagon
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_document")]
    public long IdDocument { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_arrival")]
    public long IdArrival { get; set; }

    [Column("id_car")]
    public int IdCar { get; set; }

    [Column("id_condition")]
    public int? IdCondition { get; set; }

    [Column("id_type")]
    public int? IdType { get; set; }

    [Column("gruzp")]
    public double? Gruzp { get; set; }

    [Column("u_tara")]
    public int? UTara { get; set; }

    [Column("ves_tary_arc")]
    public int? VesTaryArc { get; set; }

    [Column("route")]
    public bool? Route { get; set; }

    [Column("note_vagon")]
    [StringLength(200)]
    public string? NoteVagon { get; set; }

    [Column("id_cargo")]
    public int? IdCargo { get; set; }

    [Column("id_cargo_gng")]
    public int? IdCargoGng { get; set; }

    [Column("id_certification_data")]
    public int? IdCertificationData { get; set; }

    [Column("id_commercial_condition")]
    public int? IdCommercialCondition { get; set; }

    [Column("kol_pac")]
    public int? KolPac { get; set; }

    [Column("pac")]
    [StringLength(3)]
    public string? Pac { get; set; }

    [Column("vesg")]
    public int? Vesg { get; set; }

    [Column("vesg_reweighing")]
    public double? VesgReweighing { get; set; }

    [Column("nom_zpu")]
    [StringLength(20)]
    public string? NomZpu { get; set; }

    [Column("danger")]
    [StringLength(3)]
    [Unicode(false)]
    public string? Danger { get; set; }

    [Column("danger_kod")]
    [StringLength(4)]
    [Unicode(false)]
    public string? DangerKod { get; set; }

    [Column("cargo_returns")]
    public bool? CargoReturns { get; set; }

    [Column("id_station_on_amkr")]
    public int? IdStationOnAmkr { get; set; }

    [Column("id_division_on_amkr")]
    public int? IdDivisionOnAmkr { get; set; }

    [Column("empty_car")]
    public bool? EmptyCar { get; set; }

    [Column("kol_conductor")]
    public int? KolConductor { get; set; }

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

    [Column("id_owner")]
    public int? IdOwner { get; set; }

    [Column("id_countrys")]
    public int? IdCountrys { get; set; }

    [Column("id_genus")]
    public int? IdGenus { get; set; }

    [Column("kol_os")]
    public int? KolOs { get; set; }

    [Column("usl_tip")]
    [StringLength(10)]
    public string? UslTip { get; set; }

    [Column("date_rem_uz", TypeName = "datetime")]
    public DateTime? DateRemUz { get; set; }

    [Column("date_rem_vag", TypeName = "datetime")]
    public DateTime? DateRemVag { get; set; }

    [Column("id_type_ownership")]
    public int? IdTypeOwnership { get; set; }

    [Column("gruzp_uz")]
    public double? GruzpUz { get; set; }

    [Column("tara_uz")]
    public double? TaraUz { get; set; }

    [Column("zayava")]
    [StringLength(100)]
    public string? Zayava { get; set; }

    [Column("manual")]
    public bool? Manual { get; set; }

    [Column("pay_summa")]
    public int? PaySumma { get; set; }

    [Column("id_wagons_rent_arrival")]
    public int? IdWagonsRentArrival { get; set; }

    [InverseProperty("IdArrivalUzVagonNavigation")]
    public virtual ICollection<ArrivalCar> ArrivalCars { get; } = new List<ArrivalCar>();

    [InverseProperty("IdVagonNavigation")]
    public virtual ICollection<ArrivalUzVagonAct> ArrivalUzVagonActs { get; } = new List<ArrivalUzVagonAct>();

    [InverseProperty("IdVagonNavigation")]
    public virtual ICollection<ArrivalUzVagonCont> ArrivalUzVagonConts { get; } = new List<ArrivalUzVagonCont>();

    [InverseProperty("IdVagonNavigation")]
    public virtual ICollection<ArrivalUzVagonPay> ArrivalUzVagonPays { get; } = new List<ArrivalUzVagonPay>();

    [ForeignKey("Danger")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryHazardClass? DangerNavigation { get; set; }

    [ForeignKey("IdArrival")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual ArrivalSostav IdArrivalNavigation { get; set; } = null!;

    [ForeignKey("IdCargoGng")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryCargoGng? IdCargoGngNavigation { get; set; }

    [ForeignKey("IdCargo")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryCargo? IdCargoNavigation { get; set; }

    [ForeignKey("IdCertificationData")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryCertificationDatum? IdCertificationDataNavigation { get; set; }

    [ForeignKey("IdCommercialCondition")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryCommercialCondition? IdCommercialConditionNavigation { get; set; }

    [ForeignKey("IdCondition")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryConditionArrival? IdConditionNavigation { get; set; }

    [ForeignKey("IdCountrys")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryCountry? IdCountrysNavigation { get; set; }

    [ForeignKey("IdDivisionOnAmkr")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryDivision? IdDivisionOnAmkrNavigation { get; set; }

    [ForeignKey("IdDocument")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual ArrivalUzDocument IdDocumentNavigation { get; set; } = null!;

    [ForeignKey("IdGenus")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryGenusWagon? IdGenusNavigation { get; set; }

    [ForeignKey("IdOwner")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryOwnersWagon? IdOwnerNavigation { get; set; }

    [ForeignKey("IdStationOnAmkr")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryStation? IdStationOnAmkrNavigation { get; set; }

    [ForeignKey("IdType")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryTypeWagon? IdTypeNavigation { get; set; }

    [ForeignKey("IdTypeOwnership")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryTypeOwnerShip? IdTypeOwnershipNavigation { get; set; }

    [ForeignKey("IdWagonsRentArrival")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryWagonsRent? IdWagonsRentArrivalNavigation { get; set; }

    [ForeignKey("Num")]
    [InverseProperty("ArrivalUzVagons")]
    public virtual DirectoryWagon NumNavigation { get; set; } = null!;
}
