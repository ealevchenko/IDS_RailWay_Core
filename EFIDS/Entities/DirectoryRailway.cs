using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Railway", Schema = "IDS")]
public partial class DirectoryRailway
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("railway_name_ru")]
    [StringLength(150)]
    public string RailwayNameRu { get; set; } = null!;

    [Column("railway_name_en")]
    [StringLength(150)]
    public string RailwayNameEn { get; set; } = null!;

    [Column("railway_abbr_ru")]
    [StringLength(10)]
    public string RailwayAbbrRu { get; set; } = null!;

    [Column("railway_abbr_en")]
    [StringLength(10)]
    public string RailwayAbbrEn { get; set; } = null!;

    [Column("id_countrys")]
    public int IdCountrys { get; set; }

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

    [InverseProperty("CodeRailwayNavigation")]
    public virtual ICollection<DirectoryInlandRailway> DirectoryInlandRailways { get; } = new List<DirectoryInlandRailway>();

    [ForeignKey("IdCountrys")]
    [InverseProperty("DirectoryRailways")]
    public virtual DirectoryCountry IdCountrysNavigation { get; set; } = null!;
}
