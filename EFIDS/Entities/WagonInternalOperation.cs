using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("WagonInternalOperation", Schema = "IDS")]
[Index("IdWagonInternalRoutes", "OperationEnd", Name = "CNI_wio_wirid_end")]
[Index("IdWagonInternalRoutes", Name = "NCI_wio_wir_id")]
public partial class WagonInternalOperation
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_wagon_internal_routes")]
    public long IdWagonInternalRoutes { get; set; }

    [Column("id_operation")]
    public int IdOperation { get; set; }

    [Column("operation_start", TypeName = "datetime")]
    public DateTime OperationStart { get; set; }

    [Column("operation_end", TypeName = "datetime")]
    public DateTime? OperationEnd { get; set; }

    [Column("id_condition")]
    public int IdCondition { get; set; }

    [Column("id_loading_status")]
    public int IdLoadingStatus { get; set; }

    [Column("locomotive1")]
    [StringLength(20)]
    public string? Locomotive1 { get; set; }

    [Column("locomotive2")]
    [StringLength(20)]
    public string? Locomotive2 { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_user")]
    [StringLength(50)]
    public string? CloseUser { get; set; }

    [Column("parent_id")]
    public long? ParentId { get; set; }

    [Column("con_change", TypeName = "datetime")]
    public DateTime? ConChange { get; set; }

    [Column("con_change_user")]
    [StringLength(50)]
    public string? ConChangeUser { get; set; }

    [ForeignKey("IdCondition")]
    [InverseProperty("WagonInternalOperations")]
    public virtual DirectoryConditionArrival IdConditionNavigation { get; set; } = null!;

    [ForeignKey("IdLoadingStatus")]
    [InverseProperty("WagonInternalOperations")]
    public virtual DirectoryWagonLoadingStatus IdLoadingStatusNavigation { get; set; } = null!;

    [ForeignKey("IdOperation")]
    [InverseProperty("WagonInternalOperations")]
    public virtual DirectoryWagonOperation IdOperationNavigation { get; set; } = null!;

    [ForeignKey("IdWagonInternalRoutes")]
    [InverseProperty("WagonInternalOperations")]
    public virtual WagonInternalRoute IdWagonInternalRoutesNavigation { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<WagonInternalOperation> InverseParent { get; } = new List<WagonInternalOperation>();

    [ForeignKey("Locomotive1")]
    [InverseProperty("WagonInternalOperationLocomotive1Navigations")]
    public virtual DirectoryLocomotive? Locomotive1Navigation { get; set; }

    [ForeignKey("Locomotive2")]
    [InverseProperty("WagonInternalOperationLocomotive2Navigations")]
    public virtual DirectoryLocomotive? Locomotive2Navigation { get; set; }

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual WagonInternalOperation? Parent { get; set; }

    [InverseProperty("IdWioNavigation")]
    public virtual ICollection<WagonInternalMovement> WagonInternalMovements { get; } = new List<WagonInternalMovement>();
}
