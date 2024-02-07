using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_CargoGNG", Schema = "IDS")]
public partial class DirectoryCargoGng
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("cargo_gng_name_ru")]
    [StringLength(250)]
    public string CargoGngNameRu { get; set; } = null!;

    [Column("cargo_gng_name_en")]
    [StringLength(250)]
    public string CargoGngNameEn { get; set; } = null!;

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

    [InverseProperty("IdCargoGngNavigation")]
    public virtual ICollection<ArrivalUzVagonCont> ArrivalUzVagonConts { get; } = new List<ArrivalUzVagonCont>();

    [InverseProperty("IdCargoGngNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdCargoGngNavigation")]
    public virtual ICollection<OutgoingUzVagonCont> OutgoingUzVagonConts { get; } = new List<OutgoingUzVagonCont>();

    [InverseProperty("IdCargoGngNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();
}
