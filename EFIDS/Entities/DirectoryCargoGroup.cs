using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_CargoGroup", Schema = "IDS")]
public partial class DirectoryCargoGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cargo_group_name_ru")]
    [StringLength(50)]
    public string CargoGroupNameRu { get; set; } = null!;

    [Column("cargo_group_name_en")]
    [StringLength(50)]
    public string CargoGroupNameEn { get; set; } = null!;

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

    [InverseProperty("IdGroupNavigation")]
    public virtual ICollection<DirectoryCargo> DirectoryCargos { get; } = new List<DirectoryCargo>();
}
