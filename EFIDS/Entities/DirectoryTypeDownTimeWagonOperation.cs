using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_TypeDownTimeWagonOperations", Schema = "IDS")]
public partial class DirectoryTypeDownTimeWagonOperation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_downtime_ru")]
    [StringLength(20)]
    public string TypeDowntimeRu { get; set; } = null!;

    [Column("type_downtime_en")]
    [StringLength(20)]
    public string TypeDowntimeEn { get; set; } = null!;

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

    [InverseProperty("IdTypeDownTimeNavigation")]
    public virtual ICollection<DirectoryWagonOperation> DirectoryWagonOperations { get; set; } = new List<DirectoryWagonOperation>();
}
