using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Station", Schema = "IDS")]
public partial class DirectoryStation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("station_name_ru")]
    [StringLength(50)]
    public string StationNameRu { get; set; } = null!;

    [Column("station_name_en")]
    [StringLength(50)]
    public string StationNameEn { get; set; } = null!;

    [Column("station_abbr_ru")]
    [StringLength(50)]
    public string StationAbbrRu { get; set; } = null!;

    [Column("station_abbr_en")]
    [StringLength(50)]
    public string StationAbbrEn { get; set; } = null!;

    [Column("exit_uz")]
    public bool ExitUz { get; set; }

    [Column("station_uz")]
    public bool StationUz { get; set; }

    [Column("default_side")]
    public bool? DefaultSide { get; set; }

    [Column("code")]
    public int? Code { get; set; }

    [Column("idle_time")]
    public int? IdleTime { get; set; }

    [Column("station_delete", TypeName = "datetime")]
    public DateTime? StationDelete { get; set; }

    [InverseProperty("IdStationFromNavigation")]
    public virtual ICollection<ArrivalSostav> ArrivalSostavIdStationFromNavigations { get; } = new List<ArrivalSostav>();

    [InverseProperty("IdStationOnNavigation")]
    public virtual ICollection<ArrivalSostav> ArrivalSostavIdStationOnNavigations { get; } = new List<ArrivalSostav>();

    [InverseProperty("IdStationOnAmkrNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdStationFromNavigation")]
    public virtual ICollection<DirectoryOuterWay> DirectoryOuterWayIdStationFromNavigations { get; } = new List<DirectoryOuterWay>();

    [InverseProperty("IdStationOnNavigation")]
    public virtual ICollection<DirectoryOuterWay> DirectoryOuterWayIdStationOnNavigations { get; } = new List<DirectoryOuterWay>();

    [InverseProperty("IdStationNavigation")]
    public virtual ICollection<DirectoryWay> DirectoryWays { get; } = new List<DirectoryWay>();

    [InverseProperty("IdStationFromNavigation")]
    public virtual ICollection<OutgoingSostav> OutgoingSostavIdStationFromNavigations { get; } = new List<OutgoingSostav>();

    [InverseProperty("IdStationOnNavigation")]
    public virtual ICollection<OutgoingSostav> OutgoingSostavIdStationOnNavigations { get; } = new List<OutgoingSostav>();

    [InverseProperty("IdStationNavigation")]
    public virtual ICollection<ParkStateStation> ParkStateStations { get; } = new List<ParkStateStation>();

    [InverseProperty("IdStationNavigation")]
    public virtual ICollection<WagonInternalMovement> WagonInternalMovements { get; } = new List<WagonInternalMovement>();
}
