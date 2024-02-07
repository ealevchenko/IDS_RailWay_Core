using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Locomotive", Schema = "IDS")]
public partial class DirectoryLocomotive
{
    [Key]
    [Column("locomotive")]
    [StringLength(20)]
    public string Locomotive { get; set; } = null!;

    [Column("id_locomotive_status")]
    public int IdLocomotiveStatus { get; set; }

    [Column("factory_number")]
    [StringLength(20)]
    public string? FactoryNumber { get; set; }

    [Column("inventory_number")]
    [StringLength(20)]
    public string? InventoryNumber { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }

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

    [ForeignKey("IdLocomotiveStatus")]
    [InverseProperty("DirectoryLocomotives")]
    public virtual DirectoryLocomotiveStatus IdLocomotiveStatusNavigation { get; set; } = null!;

    [InverseProperty("Locomotive1Navigation")]
    public virtual ICollection<WagonInternalOperation> WagonInternalOperationLocomotive1Navigations { get; } = new List<WagonInternalOperation>();

    [InverseProperty("Locomotive2Navigation")]
    public virtual ICollection<WagonInternalOperation> WagonInternalOperationLocomotive2Navigations { get; } = new List<WagonInternalOperation>();
}
