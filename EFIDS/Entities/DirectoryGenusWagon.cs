using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_GenusWagons", Schema = "IDS")]
public partial class DirectoryGenusWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("abbr_ru")]
    [StringLength(5)]
    public string AbbrRu { get; set; } = null!;

    [Column("genus_ru")]
    [StringLength(50)]
    public string GenusRu { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(5)]
    public string AbbrEn { get; set; } = null!;

    [Column("genus_en")]
    [StringLength(50)]
    public string GenusEn { get; set; } = null!;

    [Column("rod_uz")]
    public int? RodUz { get; set; }

    [Column("rod_default")]
    public bool? RodDefault { get; set; }

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

    [InverseProperty("IdGenusNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdGenusWagonNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();

    [InverseProperty("IdGenusNavigation")]
    public virtual ICollection<DirectoryCarsKi> DirectoryCarsKis { get; } = new List<DirectoryCarsKi>();

    [InverseProperty("IdGenusNavigation")]
    public virtual ICollection<DirectoryWagon> DirectoryWagons { get; } = new List<DirectoryWagon>();

    [InverseProperty("IdGenusNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();

    [InverseProperty("IdGenusNavigation")]
    public virtual ICollection<UsageFeePeriod> UsageFeePeriods { get; } = new List<UsageFeePeriod>();
}
