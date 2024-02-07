using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_WagonManufacturers", Schema = "IDS")]
public partial class DirectoryWagonManufacturer
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name_ru")]
    [StringLength(250)]
    public string NameRu { get; set; } = null!;

    [Column("abbr_ru")]
    [StringLength(10)]
    public string AbbrRu { get; set; } = null!;

    [Column("name_en")]
    [StringLength(250)]
    public string NameEn { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(10)]
    public string AbbrEn { get; set; } = null!;

    [InverseProperty("IdWagonManufacturerNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();
}
