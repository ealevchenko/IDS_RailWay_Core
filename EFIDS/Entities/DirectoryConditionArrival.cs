using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_ConditionArrival", Schema = "IDS")]
public partial class DirectoryConditionArrival
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("condition_name_ru")]
    [StringLength(100)]
    public string ConditionNameRu { get; set; } = null!;

    [Column("condition_name_en")]
    [StringLength(100)]
    public string ConditionNameEn { get; set; } = null!;

    [Column("condition_abbr_ru")]
    [StringLength(20)]
    public string ConditionAbbrRu { get; set; } = null!;

    [Column("condition_abbr_en")]
    [StringLength(20)]
    public string ConditionAbbrEn { get; set; } = null!;

    [Column("red")]
    public bool? Red { get; set; }

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

    [Column("delete", TypeName = "datetime")]
    public DateTime? Delete { get; set; }

    [Column("delete_user")]
    [StringLength(50)]
    public string? DeleteUser { get; set; }

    [Column("repairs")]
    public bool? Repairs { get; set; }

    [InverseProperty("IdConditionNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdConditionNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();

    [InverseProperty("IdConditionNavigation")]
    public virtual ICollection<WagonInternalOperation> WagonInternalOperations { get; } = new List<WagonInternalOperation>();
}
