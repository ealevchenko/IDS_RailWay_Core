using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_WagonsCondition", Schema = "IDS")]
public partial class DirectoryWagonsCondition
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("abbr_ru")]
    [StringLength(20)]
    public string AbbrRu { get; set; } = null!;

    [Column("condition_ru")]
    [StringLength(100)]
    public string ConditionRu { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(20)]
    public string AbbrEn { get; set; } = null!;

    [Column("condition_en")]
    [StringLength(100)]
    public string ConditionEn { get; set; } = null!;

    [Column("red")]
    public bool Red { get; set; }

    [InverseProperty("IdWagonsConditionNavigation")]
    public virtual ICollection<CardsWagonsRepair> CardsWagonsRepairs { get; } = new List<CardsWagonsRepair>();
}
