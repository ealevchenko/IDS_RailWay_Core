using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_TypeWagons", Schema = "IDS")]
public partial class DirectoryTypeWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_ru")]
    [StringLength(50)]
    public string TypeRu { get; set; } = null!;

    [Column("type_en")]
    [StringLength(50)]
    public string TypeEn { get; set; } = null!;

    [InverseProperty("IdTypeNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdTypeWagonNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();
}
