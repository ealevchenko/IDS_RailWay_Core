using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_PoligonTravelWagons", Schema = "IDS")]
public partial class DirectoryPoligonTravelWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("abbr_ru")]
    [StringLength(20)]
    public string AbbrRu { get; set; } = null!;

    [Column("poligon_travel_ru")]
    [StringLength(100)]
    public string PoligonTravelRu { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(20)]
    public string AbbrEn { get; set; } = null!;

    [Column("poligon_travel_en")]
    [StringLength(100)]
    public string PoligonTravelEn { get; set; } = null!;

    [InverseProperty("IdPoligonTravelWagonNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();
}
