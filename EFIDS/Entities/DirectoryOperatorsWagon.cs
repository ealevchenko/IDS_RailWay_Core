using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_OperatorsWagons", Schema = "IDS")]
public partial class DirectoryOperatorsWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("abbr_ru")]
    [StringLength(20)]
    public string AbbrRu { get; set; } = null!;

    [Column("operators_ru")]
    [StringLength(100)]
    public string OperatorsRu { get; set; } = null!;

    [Column("abbr_en")]
    [StringLength(20)]
    public string AbbrEn { get; set; } = null!;

    [Column("operators_en")]
    [StringLength(100)]
    public string OperatorsEn { get; set; } = null!;

    [Column("paid")]
    public bool Paid { get; set; }

    [Column("rop")]
    public bool Rop { get; set; }

    [Column("local_use")]
    public bool LocalUse { get; set; }

    [Column("color")]
    [StringLength(10)]
    public string? Color { get; set; }

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

    [Column("monitoring_idle_time")]
    public bool? MonitoringIdleTime { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [InverseProperty("IdOperatorWagonNavigation")]
    public virtual ICollection<CardsWagon> CardsWagons { get; } = new List<CardsWagon>();

    [InverseProperty("IdOperatorNavigation")]
    public virtual ICollection<DirectoryCarsKi> DirectoryCarsKis { get; } = new List<DirectoryCarsKi>();

    [InverseProperty("IdOperatorNavigation")]
    public virtual ICollection<DirectoryWagon> DirectoryWagons { get; } = new List<DirectoryWagon>();

    [InverseProperty("IdOperatorNavigation")]
    public virtual ICollection<DirectoryWagonsRent> DirectoryWagonsRents { get; } = new List<DirectoryWagonsRent>();

    [InverseProperty("Parent")]
    public virtual ICollection<DirectoryOperatorsWagon> InverseParent { get; } = new List<DirectoryOperatorsWagon>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual DirectoryOperatorsWagon? Parent { get; set; }

    [InverseProperty("IdOperatorNavigation")]
    public virtual ICollection<UsageFeePeriod> UsageFeePeriods { get; } = new List<UsageFeePeriod>();
}
