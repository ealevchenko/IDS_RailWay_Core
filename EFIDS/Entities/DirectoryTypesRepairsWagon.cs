using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_TypesRepairsWagons", Schema = "IDS")]
public partial class DirectoryTypesRepairsWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("abbr_ru")]
    [StringLength(10)]
    public string AbbrRu { get; set; } = null!;

    [Column("type_repairs_ru")]
    [StringLength(50)]
    public string TypeRepairsRu { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(10)]
    public string AbbrEn { get; set; } = null!;

    [Column("type_repairs_en")]
    [StringLength(50)]
    public string TypeRepairsEn { get; set; } = null!;

    [InverseProperty("IdTypeRepairsNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();

    [InverseProperty("IdTypeRepairWagonNavigation")]
    public virtual ICollection<CardsWagonsRepair> CardsWagonsRepairs { get; } = new List<CardsWagonsRepair>();
}
