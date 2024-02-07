using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_SpecialConditions", Schema = "IDS")]
public partial class DirectorySpecialCondition
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("special_conditions_ru")]
    [StringLength(50)]
    public string SpecialConditionsRu { get; set; } = null!;

    [Column("special_conditions_en")]
    [StringLength(50)]
    public string SpecialConditionsEn { get; set; } = null!;

    [InverseProperty("IdSpecialConditionsNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();
}
