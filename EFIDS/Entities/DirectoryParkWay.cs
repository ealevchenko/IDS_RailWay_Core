using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_ParkWays", Schema = "IDS")]
public partial class DirectoryParkWay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("park_name_ru")]
    [StringLength(100)]
    public string ParkNameRu { get; set; } = null!;

    [Column("park_name_en")]
    [StringLength(100)]
    public string ParkNameEn { get; set; } = null!;

    [Column("park_abbr_ru")]
    [StringLength(50)]
    public string ParkAbbrRu { get; set; } = null!;

    [Column("park_abbr_en")]
    [StringLength(50)]
    public string ParkAbbrEn { get; set; } = null!;

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

    [InverseProperty("IdParkFromNavigation")]
    public virtual ICollection<DirectoryOuterWay> DirectoryOuterWayIdParkFromNavigations { get; } = new List<DirectoryOuterWay>();

    [InverseProperty("IdParkOnNavigation")]
    public virtual ICollection<DirectoryOuterWay> DirectoryOuterWayIdParkOnNavigations { get; } = new List<DirectoryOuterWay>();

    [InverseProperty("IdParkNavigation")]
    public virtual ICollection<DirectoryWay> DirectoryWays { get; } = new List<DirectoryWay>();
}
