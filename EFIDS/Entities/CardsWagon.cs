using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("CardsWagons", Schema = "IDS")]
public partial class CardsWagon
{
    [Key]
    [Column("num")]
    public int Num { get; set; }

    [Column("id_genus_wagon")]
    public int IdGenusWagon { get; set; }

    [Column("id_state")]
    public int IdState { get; set; }

    [Column("id_wagon_manufacturer")]
    public int? IdWagonManufacturer { get; set; }

    [Column("year_wagon_create")]
    public int? YearWagonCreate { get; set; }

    [Column("code_station")]
    public int? CodeStation { get; set; }

    [Column("carrying_capacity")]
    public float? CarryingCapacity { get; set; }

    [Column("tara")]
    public float? Tara { get; set; }

    [Column("id_type_repairs")]
    public int? IdTypeRepairs { get; set; }

    [Column("date_type_repairs", TypeName = "datetime")]
    public DateTime? DateTypeRepairs { get; set; }

    [Column("code_model_wagon")]
    [StringLength(20)]
    public string? CodeModelWagon { get; set; }

    [Column("id_type_wagon")]
    public int? IdTypeWagon { get; set; }

    [Column("axis_length")]
    public float? AxisLength { get; set; }

    [Column("body_volume")]
    public float? BodyVolume { get; set; }

    [Column("id_type_ownership")]
    public int IdTypeOwnership { get; set; }

    [Column("id_owner_wagon")]
    public int IdOwnerWagon { get; set; }

    [Column("date_registration", TypeName = "datetime")]
    public DateTime? DateRegistration { get; set; }

    [Column("id_lessor_wagon")]
    public int? IdLessorWagon { get; set; }

    [Column("id_operator_wagon")]
    public int? IdOperatorWagon { get; set; }

    [Column("id_poligon_travel_wagon")]
    public int? IdPoligonTravelWagon { get; set; }

    [Column("id_special_conditions")]
    public int? IdSpecialConditions { get; set; }

    [Column("sap")]
    public int? Sap { get; set; }

    [Column("note")]
    [StringLength(500)]
    public string? Note { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("change", TypeName = "datetime")]
    public DateTime Change { get; set; }

    [Column("change_user")]
    [StringLength(50)]
    public string ChangeUser { get; set; } = null!;

    [InverseProperty("NumNavigation")]
    public virtual ICollection<CardsWagonsRepair> CardsWagonsRepairs { get; } = new List<CardsWagonsRepair>();

    [ForeignKey("CodeModelWagon")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryModelsWagon? CodeModelWagonNavigation { get; set; }

    [ForeignKey("IdGenusWagon")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryGenusWagon IdGenusWagonNavigation { get; set; } = null!;

    [ForeignKey("IdLessorWagon")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryLessorsWagon? IdLessorWagonNavigation { get; set; }

    [ForeignKey("IdOperatorWagon")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryOperatorsWagon? IdOperatorWagonNavigation { get; set; }

    [ForeignKey("IdOwnerWagon")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryOwnersWagon IdOwnerWagonNavigation { get; set; } = null!;

    [ForeignKey("IdPoligonTravelWagon")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryPoligonTravelWagon? IdPoligonTravelWagonNavigation { get; set; }

    [ForeignKey("IdSpecialConditions")]
    [InverseProperty("CardsWagons")]
    public virtual DirectorySpecialCondition? IdSpecialConditionsNavigation { get; set; }

    [ForeignKey("IdTypeOwnership")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryTypeOwnerShip IdTypeOwnershipNavigation { get; set; } = null!;

    [ForeignKey("IdTypeRepairs")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryTypesRepairsWagon? IdTypeRepairsNavigation { get; set; }

    [ForeignKey("IdTypeWagon")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryTypeWagon? IdTypeWagonNavigation { get; set; }

    [ForeignKey("IdWagonManufacturer")]
    [InverseProperty("CardsWagons")]
    public virtual DirectoryWagonManufacturer? IdWagonManufacturerNavigation { get; set; }

    [InverseProperty("NumNavigation")]
    public virtual ICollection<ParksListWagon> ParksListWagons { get; } = new List<ParksListWagon>();
}
