using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_CargoETSNG", Schema = "IDS")]
public partial class DirectoryCargoEtsng
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("cargo_etsng_name_ru")]
    [StringLength(250)]
    public string CargoEtsngNameRu { get; set; } = null!;

    [Column("cargo_etsng_name_en")]
    [StringLength(250)]
    public string CargoEtsngNameEn { get; set; } = null!;

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

    [InverseProperty("IdCargoEtsngNavigation")]
    public virtual ICollection<DirectoryCargo> DirectoryCargos { get; } = new List<DirectoryCargo>();
}
