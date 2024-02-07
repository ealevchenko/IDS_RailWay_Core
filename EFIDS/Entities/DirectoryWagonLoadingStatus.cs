using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_WagonLoadingStatus", Schema = "IDS")]
public partial class DirectoryWagonLoadingStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("loading_status_ru")]
    [StringLength(30)]
    public string LoadingStatusRu { get; set; } = null!;

    [Column("loading_status_en")]
    [StringLength(30)]
    public string LoadingStatusEn { get; set; } = null!;

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

    [InverseProperty("IdLoadingStatusNavigation")]
    public virtual ICollection<WagonInternalOperation> WagonInternalOperations { get; } = new List<WagonInternalOperation>();
}
