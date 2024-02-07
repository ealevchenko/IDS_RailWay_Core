using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_DetentionReturn", Schema = "IDS")]
public partial class DirectoryDetentionReturn
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("cause_ru")]
    [StringLength(150)]
    public string CauseRu { get; set; } = null!;

    [Column("cause_en")]
    [StringLength(150)]
    public string CauseEn { get; set; } = null!;

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

    [InverseProperty("IdDetentionReturnNavigation")]
    public virtual ICollection<OutgoingDetentionReturn> OutgoingDetentionReturns { get; } = new List<OutgoingDetentionReturn>();
}
