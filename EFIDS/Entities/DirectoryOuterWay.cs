using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_OuterWays", Schema = "IDS")]
public partial class DirectoryOuterWay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name_outer_way_ru")]
    [StringLength(150)]
    public string NameOuterWayRu { get; set; } = null!;

    [Column("name_outer_way_en")]
    [StringLength(150)]
    public string NameOuterWayEn { get; set; } = null!;

    [Column("id_station_from")]
    public int IdStationFrom { get; set; }

    [Column("id_park_from")]
    public int? IdParkFrom { get; set; }

    [Column("id_way_from")]
    public int? IdWayFrom { get; set; }

    [Column("side_from")]
    public bool? SideFrom { get; set; }

    [Column("id_station_on")]
    public int IdStationOn { get; set; }

    [Column("id_park_on")]
    public int? IdParkOn { get; set; }

    [Column("id_way_on")]
    public int? IdWayOn { get; set; }

    [Column("side_on")]
    public bool? SideOn { get; set; }

    [Column("exit_uz")]
    public bool ExitUz { get; set; }

    [Column("way_close", TypeName = "datetime")]
    public DateTime? WayClose { get; set; }

    [Column("way_delete", TypeName = "datetime")]
    public DateTime? WayDelete { get; set; }

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

    [ForeignKey("IdParkFrom")]
    [InverseProperty("DirectoryOuterWayIdParkFromNavigations")]
    public virtual DirectoryParkWay? IdParkFromNavigation { get; set; }

    [ForeignKey("IdParkOn")]
    [InverseProperty("DirectoryOuterWayIdParkOnNavigations")]
    public virtual DirectoryParkWay? IdParkOnNavigation { get; set; }

    [ForeignKey("IdStationFrom")]
    [InverseProperty("DirectoryOuterWayIdStationFromNavigations")]
    public virtual DirectoryStation IdStationFromNavigation { get; set; } = null!;

    [ForeignKey("IdStationOn")]
    [InverseProperty("DirectoryOuterWayIdStationOnNavigations")]
    public virtual DirectoryStation IdStationOnNavigation { get; set; } = null!;

    [ForeignKey("IdWayFrom")]
    [InverseProperty("DirectoryOuterWayIdWayFromNavigations")]
    public virtual DirectoryWay? IdWayFromNavigation { get; set; }

    [ForeignKey("IdWayOn")]
    [InverseProperty("DirectoryOuterWayIdWayOnNavigations")]
    public virtual DirectoryWay? IdWayOnNavigation { get; set; }

    [InverseProperty("IdOuterWayNavigation")]
    public virtual ICollection<WagonInternalMovement> WagonInternalMovements { get; } = new List<WagonInternalMovement>();
}
