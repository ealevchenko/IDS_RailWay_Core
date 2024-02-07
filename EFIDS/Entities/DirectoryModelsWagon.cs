using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_ModelsWagons", Schema = "IDS")]
public partial class DirectoryModelsWagon
{
    [Key]
    [Column("code")]
    [StringLength(20)]
    public string Code { get; set; } = null!;

    [Column("model_ru")]
    [StringLength(250)]
    public string ModelRu { get; set; } = null!;

    [Column("model_en")]
    [StringLength(250)]
    public string ModelEn { get; set; } = null!;

    [InverseProperty("CodeModelWagonNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();
}
