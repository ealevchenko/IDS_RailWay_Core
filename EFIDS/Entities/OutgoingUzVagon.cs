using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Outgoing_UZ_Vagon", Schema = "IDS")]
public partial class OutgoingUzVagon
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_document")]
    public long? IdDocument { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_outgoing")]
    public long IdOutgoing { get; set; }

    [Column("id_car")]
    public long IdCar { get; set; }

    [Column("id_condition")]
    public int? IdCondition { get; set; }

    [Column("id_wagons_rent_arrival")]
    public int? IdWagonsRentArrival { get; set; }

    [Column("id_wagons_rent_outgoing")]
    public int? IdWagonsRentOutgoing { get; set; }

    [Column("id_countrys")]
    public int IdCountrys { get; set; }

    [Column("id_genus")]
    public int IdGenus { get; set; }

    [Column("id_owner")]
    public int IdOwner { get; set; }

    [Column("gruzp_uz")]
    public double? GruzpUz { get; set; }

    [Column("tara_uz")]
    public double? TaraUz { get; set; }

    [Column("note_uz")]
    [StringLength(1000)]
    public string? NoteUz { get; set; }

    [Column("gruzp")]
    public double? Gruzp { get; set; }

    [Column("u_tara")]
    public int? UTara { get; set; }

    [Column("ves_tary_arc")]
    public int? VesTaryArc { get; set; }

    [Column("id_warehouse")]
    public int? IdWarehouse { get; set; }

    [Column("id_division")]
    public int? IdDivision { get; set; }

    [Column("laden")]
    public bool? Laden { get; set; }

    [Column("id_cargo")]
    public int? IdCargo { get; set; }

    [Column("id_cargo_gng")]
    public int? IdCargoGng { get; set; }

    [Column("vesg")]
    public int? Vesg { get; set; }

    [Column("code_stn_to")]
    public int? CodeStnTo { get; set; }

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

    [ForeignKey("CodeStnTo")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryExternalStation? CodeStnToNavigation { get; set; }

    [ForeignKey("IdCargoGng")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryCargoGng? IdCargoGngNavigation { get; set; }

    [ForeignKey("IdCargo")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryCargo? IdCargoNavigation { get; set; }

    [ForeignKey("IdCondition")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryConditionArrival? IdConditionNavigation { get; set; }

    [ForeignKey("IdCountrys")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryCountry IdCountrysNavigation { get; set; } = null!;

    [ForeignKey("IdDivision")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryDivision? IdDivisionNavigation { get; set; }

    [ForeignKey("IdDocument")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual OutgoingUzDocument? IdDocumentNavigation { get; set; }

    [ForeignKey("IdGenus")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryGenusWagon IdGenusNavigation { get; set; } = null!;

    [ForeignKey("IdOutgoing")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual OutgoingSostav IdOutgoingNavigation { get; set; } = null!;

    [ForeignKey("IdOwner")]
    [InverseProperty("OutgoingUzVagons")]
    public virtual DirectoryOwnersWagon IdOwnerNavigation { get; set; } = null!;

    [ForeignKey("IdWagonsRentArrival")]
    [InverseProperty("OutgoingUzVagonIdWagonsRentArrivalNavigations")]
    public virtual DirectoryWagonsRent? IdWagonsRentArrivalNavigation { get; set; }

    [ForeignKey("IdWagonsRentOutgoing")]
    [InverseProperty("OutgoingUzVagonIdWagonsRentOutgoingNavigations")]
    public virtual DirectoryWagonsRent? IdWagonsRentOutgoingNavigation { get; set; }

    [InverseProperty("IdOutgoingUzVagonNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCars { get; } = new List<OutgoingCar>();

    [InverseProperty("IdVagonNavigation")]
    public virtual ICollection<OutgoingUzVagonAct> OutgoingUzVagonActs { get; } = new List<OutgoingUzVagonAct>();

    [InverseProperty("IdVagonNavigation")]
    public virtual ICollection<OutgoingUzVagonCont> OutgoingUzVagonConts { get; } = new List<OutgoingUzVagonCont>();

    [InverseProperty("IdVagonNavigation")]
    public virtual ICollection<OutgoingUzVagonPay> OutgoingUzVagonPays { get; } = new List<OutgoingUzVagonPay>();
}
