using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("ParksWagons", Schema = "IDS")]
public partial class ParksWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name_park_ru")]
    [StringLength(250)]
    public string NameParkRu { get; set; } = null!;

    [Column("name_park_en")]
    [StringLength(250)]
    public string NameParkEn { get; set; } = null!;

    [InverseProperty("IdParkWagonNavigation")]
    public virtual ICollection<ParksListWagon> ParksListWagons { get; } = new List<ParksListWagon>();
}
