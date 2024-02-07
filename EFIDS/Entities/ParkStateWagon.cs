using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("ParkState_Wagon", Schema = "IDS")]
public partial class ParkStateWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_park_state_way")]
    public int IdParkStateWay { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("position")]
    public int Position { get; set; }

    [Column("note")]
    [StringLength(100)]
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

    [Column("delete", TypeName = "datetime")]
    public DateTime? Delete { get; set; }

    [Column("delete_user")]
    [StringLength(50)]
    public string? DeleteUser { get; set; }

    [ForeignKey("IdParkStateWay")]
    [InverseProperty("ParkStateWagons")]
    public virtual ParkStateWay IdParkStateWayNavigation { get; set; } = null!;
}
