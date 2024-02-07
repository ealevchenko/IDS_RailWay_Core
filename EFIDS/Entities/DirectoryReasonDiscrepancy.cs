using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Reason_Discrepancy", Schema = "IDS")]
public partial class DirectoryReasonDiscrepancy
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("reason_discrepancy_name_ru")]
    [StringLength(100)]
    public string ReasonDiscrepancyNameRu { get; set; } = null!;

    [Column("reason_discrepancy_name_en")]
    [StringLength(100)]
    public string ReasonDiscrepancyNameEn { get; set; } = null!;

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

    [InverseProperty("IdReasonDiscrepancyAmkrNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCarIdReasonDiscrepancyAmkrNavigations { get; } = new List<OutgoingCar>();

    [InverseProperty("IdReasonDiscrepancyUzNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCarIdReasonDiscrepancyUzNavigations { get; } = new List<OutgoingCar>();
}
