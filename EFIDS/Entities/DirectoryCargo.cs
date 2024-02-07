using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Cargo", Schema = "IDS")]
public partial class DirectoryCargo
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_group")]
    public int IdGroup { get; set; }

    [Column("id_cargo_etsng")]
    public int IdCargoEtsng { get; set; }

    [Column("cargo_name_ru")]
    [StringLength(50)]
    public string CargoNameRu { get; set; } = null!;

    [Column("cargo_name_en")]
    [StringLength(50)]
    public string CargoNameEn { get; set; } = null!;

    [Column("code_sap")]
    [StringLength(20)]
    public string? CodeSap { get; set; }

    [Column("sending")]
    public bool? Sending { get; set; }

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

    [Column("id_out_group")]
    public int? IdOutGroup { get; set; }

    [InverseProperty("IdCargoNavigation")]
    public virtual ICollection<ArrivalUzVagonCont> ArrivalUzVagonConts { get; } = new List<ArrivalUzVagonCont>();

    [InverseProperty("IdCargoNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [ForeignKey("IdCargoEtsng")]
    [InverseProperty("DirectoryCargos")]
    public virtual DirectoryCargoEtsng IdCargoEtsngNavigation { get; set; } = null!;

    [ForeignKey("IdGroup")]
    [InverseProperty("DirectoryCargos")]
    public virtual DirectoryCargoGroup IdGroupNavigation { get; set; } = null!;

    [ForeignKey("IdOutGroup")]
    [InverseProperty("DirectoryCargos")]
    public virtual DirectoryCargoOutGroup? IdOutGroupNavigation { get; set; }

    [InverseProperty("IdCargoNavigation")]
    public virtual ICollection<OutgoingUzVagonCont> OutgoingUzVagonConts { get; } = new List<OutgoingUzVagonCont>();

    [InverseProperty("IdCargoNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();
}
