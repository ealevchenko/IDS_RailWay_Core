using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_OwnersWagons", Schema = "IDS")]
public partial class DirectoryOwnersWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("abbr_ru")]
    [StringLength(20)]
    public string AbbrRu { get; set; } = null!;

    [Column("owner_ru")]
    [StringLength(100)]
    public string OwnerRu { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(20)]
    public string AbbrEn { get; set; } = null!;

    [Column("owner_en")]
    [StringLength(100)]
    public string OwnerEn { get; set; } = null!;

    [Column("local_use")]
    public bool LocalUse { get; set; }

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

    [InverseProperty("IdOwnerNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdOwnerWagonNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();

    [InverseProperty("IdOwnerNavigation")]
    public virtual ICollection<DirectoryWagon> DirectoryWagons { get; } = new List<DirectoryWagon>();

    [InverseProperty("IdOwnerNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();
}
