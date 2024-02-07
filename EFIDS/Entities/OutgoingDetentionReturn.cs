using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("OutgoingDetentionReturn", Schema = "IDS")]
public partial class OutgoingDetentionReturn
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_detention_return")]
    public int IdDetentionReturn { get; set; }

    [Column("type_detention_return")]
    public int TypeDetentionReturn { get; set; }

    [Column("date_start", TypeName = "datetime")]
    public DateTime DateStart { get; set; }

    [Column("date_stop", TypeName = "datetime")]
    public DateTime? DateStop { get; set; }

    [Column("num_act")]
    [StringLength(20)]
    public string? NumAct { get; set; }

    [Column("date_act", TypeName = "datetime")]
    public DateTime? DateAct { get; set; }

    [Column("note")]
    [StringLength(200)]
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

    [ForeignKey("IdDetentionReturn")]
    [InverseProperty("OutgoingDetentionReturns")]
    public virtual DirectoryDetentionReturn IdDetentionReturnNavigation { get; set; } = null!;

    [InverseProperty("IdOutgoingDetentionNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCarIdOutgoingDetentionNavigations { get; } = new List<OutgoingCar>();

    [InverseProperty("IdOutgoingReturnStartNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCarIdOutgoingReturnStartNavigations { get; } = new List<OutgoingCar>();

    [InverseProperty("IdOutgoingReturnStopNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCarIdOutgoingReturnStopNavigations { get; } = new List<OutgoingCar>();
}
