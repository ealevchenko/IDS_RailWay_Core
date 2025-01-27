using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_InternalCargo", Schema = "IDS")]
public partial class DirectoryInternalCargo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_group")]
    public int IdGroup { get; set; }

    [Column("cargo_name_ru")]
    [StringLength(50)]
    public string CargoNameRu { get; set; } = null!;

    [Column("cargo_name_en")]
    [StringLength(50)]
    public string CargoNameEn { get; set; } = null!;

    [Column("empty_weight")]
    public bool? EmptyWeight { get; set; }

    [Column("code_sap")]
    [StringLength(20)]
    public string? CodeSap { get; set; }

    [Column("material_class_sap")]
    [StringLength(50)]
    public string? MaterialClassSap { get; set; }

    [Column("id_cargo_ebd")]
    public int? IdCargoEbd { get; set; }

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

    [ForeignKey("IdGroup")]
    [InverseProperty("DirectoryInternalCargos")]
    public virtual DirectoryInternalCargoGroup IdGroupNavigation { get; set; } = null!;

    [InverseProperty("IdInternalCargoNavigation")]
    public virtual ICollection<WagonInternalMoveCargo> WagonInternalMoveCargos { get; set; } = new List<WagonInternalMoveCargo>();
}
