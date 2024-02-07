using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_HazardClass", Schema = "IDS")]
public partial class DirectoryHazardClass
{
    [Key]
    [Column("code")]
    [StringLength(3)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [Column("hazard_class_ru")]
    [StringLength(200)]
    public string HazardClassRu { get; set; } = null!;

    [Column("hazard_class_en")]
    [StringLength(200)]
    public string HazardClassEn { get; set; } = null!;

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

    [InverseProperty("DangerNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();
}
