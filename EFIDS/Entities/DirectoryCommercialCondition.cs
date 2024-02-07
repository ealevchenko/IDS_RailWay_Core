using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_CommercialCondition", Schema = "IDS")]
public partial class DirectoryCommercialCondition
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("commercial_condition_ru")]
    [StringLength(50)]
    public string CommercialConditionRu { get; set; } = null!;

    [Column("commercial_condition_en")]
    [StringLength(50)]
    public string CommercialConditionEn { get; set; } = null!;

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("change", TypeName = "datetime")]
    public DateTime? Change { get; set; }

    [Column("change_user")]
    [StringLength(50)]
    public string? ChangeUser { get; set; }

    [InverseProperty("IdCommercialConditionNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();
}
