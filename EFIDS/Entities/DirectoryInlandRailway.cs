using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_InlandRailway", Schema = "IDS")]
public partial class DirectoryInlandRailway
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("inlandrailway_name_ru")]
    [StringLength(150)]
    public string InlandrailwayNameRu { get; set; } = null!;

    [Column("inlandrailway_name_en")]
    [StringLength(150)]
    public string InlandrailwayNameEn { get; set; } = null!;

    [Column("inlandrailway_abbr_ru")]
    [StringLength(20)]
    public string InlandrailwayAbbrRu { get; set; } = null!;

    [Column("inlandrailway_abbr_en")]
    [StringLength(20)]
    public string InlandrailwayAbbrEn { get; set; } = null!;

    [Column("code_railway")]
    public int CodeRailway { get; set; }

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

    [ForeignKey("CodeRailway")]
    [InverseProperty("DirectoryInlandRailways")]
    public virtual DirectoryRailway CodeRailwayNavigation { get; set; } = null!;

    [InverseProperty("CodeInlandrailwayNavigation")]
    public virtual ICollection<DirectoryBorderCheckpoint> DirectoryBorderCheckpoints { get; } = new List<DirectoryBorderCheckpoint>();

    [InverseProperty("CodeInlandrailwayNavigation")]
    public virtual ICollection<DirectoryExternalStation> DirectoryExternalStations { get; } = new List<DirectoryExternalStation>();
}
