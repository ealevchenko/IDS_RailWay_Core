using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_LocomotiveStatus", Schema = "IDS")]
public partial class DirectoryLocomotiveStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("locomotive_status_ru")]
    [StringLength(20)]
    public string LocomotiveStatusRu { get; set; } = null!;

    [Column("locomotive_status_en")]
    [StringLength(20)]
    public string LocomotiveStatusEn { get; set; } = null!;

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

    [InverseProperty("IdLocomotiveStatusNavigation")]
    public virtual ICollection<DirectoryLocomotive> DirectoryLocomotives { get; } = new List<DirectoryLocomotive>();
}
