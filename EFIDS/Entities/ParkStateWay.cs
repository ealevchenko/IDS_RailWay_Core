using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("ParkState_Way", Schema = "IDS")]
public partial class ParkStateWay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_park_state_station")]
    public int IdParkStateStation { get; set; }

    [Column("id_way")]
    public int IdWay { get; set; }

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

    [ForeignKey("IdParkStateStation")]
    [InverseProperty("ParkStateWays")]
    public virtual ParkStateStation IdParkStateStationNavigation { get; set; } = null!;

    [ForeignKey("IdWay")]
    [InverseProperty("ParkStateWays")]
    public virtual DirectoryWay IdWayNavigation { get; set; } = null!;

    [InverseProperty("IdParkStateWayNavigation")]
    public virtual ICollection<ParkStateWagon> ParkStateWagons { get; } = new List<ParkStateWagon>();
}
