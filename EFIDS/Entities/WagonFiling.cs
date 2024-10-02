using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("WagonFiling", Schema = "IDS")]
public partial class WagonFiling
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("num_filing")]
    [StringLength(50)]
    public string NumFiling { get; set; } = null!;

    [Column("id_wio")]
    public long IdWio { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }

    [Column("start_filing", TypeName = "datetime")]
    public DateTime StartFiling { get; set; }

    [Column("end_filing", TypeName = "datetime")]
    public DateTime? EndFiling { get; set; }

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

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_user")]
    [StringLength(50)]
    public string? CloseUser { get; set; }

    [ForeignKey("IdWio")]
    [InverseProperty("WagonFilings")]
    public virtual WagonInternalOperation IdWioNavigation { get; set; } = null!;

    [InverseProperty("IdFilingNavigation")]
    public virtual ICollection<WagonInternalMovement> WagonInternalMovements { get; set; } = new List<WagonInternalMovement>();
}
