using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("ParkState_Station", Schema = "IDS")]
public partial class ParkStateStation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_station")]
    public int IdStation { get; set; }

    [Column("state_on", TypeName = "datetime")]
    public DateTime StateOn { get; set; }

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

    [Column("applied", TypeName = "datetime")]
    public DateTime? Applied { get; set; }

    [Column("applied_user")]
    [StringLength(50)]
    public string? AppliedUser { get; set; }

    [ForeignKey("IdStation")]
    [InverseProperty("ParkStateStations")]
    public virtual DirectoryStation IdStationNavigation { get; set; } = null!;

    [InverseProperty("IdParkStateStationNavigation")]
    public virtual ICollection<ParkStateWay> ParkStateWays { get; } = new List<ParkStateWay>();
}
