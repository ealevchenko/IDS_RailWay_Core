using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Ways", Schema = "IDS")]
[Index("IdStation", "WayDelete", "Capacity", Name = "NCI_station_waydel_capacity")]
public partial class DirectoryWay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_station")]
    public int IdStation { get; set; }

    [Column("id_park")]
    public int IdPark { get; set; }

    [Column("position_park")]
    public int PositionPark { get; set; }

    [Column("position_way")]
    public int PositionWay { get; set; }

    [Column("way_num_ru")]
    [StringLength(20)]
    public string WayNumRu { get; set; } = null!;

    [Column("way_num_en")]
    [StringLength(20)]
    public string WayNumEn { get; set; } = null!;

    [Column("way_name_ru")]
    [StringLength(100)]
    public string WayNameRu { get; set; } = null!;

    [Column("way_name_en")]
    [StringLength(100)]
    public string WayNameEn { get; set; } = null!;

    [Column("way_abbr_ru")]
    [StringLength(50)]
    public string WayAbbrRu { get; set; } = null!;

    [Column("way_abbr_en")]
    [StringLength(50)]
    public string WayAbbrEn { get; set; } = null!;

    [Column("capacity")]
    public int? Capacity { get; set; }

    [Column("deadlock")]
    public bool? Deadlock { get; set; }

    [Column("crossing_uz")]
    public bool? CrossingUz { get; set; }

    [Column("crossing_amkr")]
    public bool? CrossingAmkr { get; set; }

    [Column("id_devision")]
    public int? IdDevision { get; set; }

    [Column("dissolution")]
    public bool? Dissolution { get; set; }

    [Column("output_dissolution")]
    public bool? OutputDissolution { get; set; }

    [Column("way_close", TypeName = "datetime")]
    public DateTime? WayClose { get; set; }

    [Column("way_delete", TypeName = "datetime")]
    public DateTime? WayDelete { get; set; }

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

    [InverseProperty("IdWayNavigation")]
    public virtual ICollection<ArrivalSostav> ArrivalSostavs { get; } = new List<ArrivalSostav>();

    [InverseProperty("IdWayFromNavigation")]
    public virtual ICollection<DirectoryOuterWay> DirectoryOuterWayIdWayFromNavigations { get; } = new List<DirectoryOuterWay>();

    [InverseProperty("IdWayOnNavigation")]
    public virtual ICollection<DirectoryOuterWay> DirectoryOuterWayIdWayOnNavigations { get; } = new List<DirectoryOuterWay>();

    [ForeignKey("IdDevision")]
    [InverseProperty("DirectoryWays")]
    public virtual DirectoryDivision? IdDevisionNavigation { get; set; }

    [ForeignKey("IdPark")]
    [InverseProperty("DirectoryWays")]
    public virtual DirectoryParkWay IdParkNavigation { get; set; } = null!;

    [ForeignKey("IdStation")]
    [InverseProperty("DirectoryWays")]
    public virtual DirectoryStation IdStationNavigation { get; set; } = null!;

    [InverseProperty("IdWayFromNavigation")]
    public virtual ICollection<OutgoingSostav> OutgoingSostavs { get; } = new List<OutgoingSostav>();

    [InverseProperty("IdWayNavigation")]
    public virtual ICollection<ParkStateWay> ParkStateWays { get; } = new List<ParkStateWay>();

    [InverseProperty("IdWayNavigation")]
    public virtual ICollection<WagonInternalMovement> WagonInternalMovements { get; } = new List<WagonInternalMovement>();
}
