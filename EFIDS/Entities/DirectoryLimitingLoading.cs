using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_LimitingLoading", Schema = "IDS")]
public partial class DirectoryLimitingLoading
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("limiting_name_ru")]
    [StringLength(100)]
    public string LimitingNameRu { get; set; } = null!;

    [Column("limiting_name_en")]
    [StringLength(100)]
    public string LimitingNameEn { get; set; } = null!;

    [Column("limiting_abbr_ru")]
    [StringLength(30)]
    public string LimitingAbbrRu { get; set; } = null!;

    [Column("limiting_abbr_en")]
    [StringLength(30)]
    public string LimitingAbbrEn { get; set; } = null!;

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

    [InverseProperty("IdLimitingNavigation")]
    public virtual ICollection<DirectoryCarsKi> DirectoryCarsKis { get; } = new List<DirectoryCarsKi>();

    [InverseProperty("IdLimitingNavigation")]
    public virtual ICollection<DirectoryWagonsRent> DirectoryWagonsRents { get; } = new List<DirectoryWagonsRent>();
}
