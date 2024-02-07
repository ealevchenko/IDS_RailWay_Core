using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_LessorsWagons", Schema = "IDS")]
public partial class DirectoryLessorsWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("abbr_ru")]
    [StringLength(20)]
    public string AbbrRu { get; set; } = null!;

    [Column("lessors_ru")]
    [StringLength(100)]
    public string LessorsRu { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(20)]
    public string AbbrEn { get; set; } = null!;

    [Column("lessors_en")]
    [StringLength(100)]
    public string LessorsEn { get; set; } = null!;

    [Column("paid")]
    public bool Paid { get; set; }

    [Column("rop")]
    public bool Rop { get; set; }

    [Column("local_use")]
    public bool LocalUse { get; set; }

    [InverseProperty("IdLessorWagonNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();
}
