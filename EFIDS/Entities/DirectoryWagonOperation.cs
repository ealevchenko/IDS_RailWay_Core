using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_WagonOperations", Schema = "IDS")]
public partial class DirectoryWagonOperation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("operation_name_ru")]
    [StringLength(20)]
    public string OperationNameRu { get; set; } = null!;

    [Column("operation_name_en")]
    [StringLength(20)]
    public string OperationNameEn { get; set; } = null!;

    [Column("busy")]
    public bool Busy { get; set; }

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

    [InverseProperty("IdOperationNavigation")]
    public virtual ICollection<WagonInternalOperation> WagonInternalOperations { get; } = new List<WagonInternalOperation>();
}
