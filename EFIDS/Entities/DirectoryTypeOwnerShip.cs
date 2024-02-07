using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_TypeOwnerShip", Schema = "IDS")]
public partial class DirectoryTypeOwnerShip
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_ownership_ru")]
    [StringLength(50)]
    public string TypeOwnershipRu { get; set; } = null!;

    [Column("type_ownership_en")]
    [StringLength(50)]
    public string TypeOwnershipEn { get; set; } = null!;

    [InverseProperty("IdTypeOwnershipNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdTypeOwnershipNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();

    [InverseProperty("IdTypeOwnershipNavigation")]
    public virtual ICollection<DirectoryWagon> DirectoryWagons { get; } = new List<DirectoryWagon>();
}
