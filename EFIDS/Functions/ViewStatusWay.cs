using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Functions;

public class ViewStatusWay
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

    //[Column("capacity")]
    //public int? Capacity { get; set; }

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
    [Column("count_all_wagons")]
    public int? countAllWagons { get; set; }
    [Column("count_amkr_wagons")]
    public int? countAmkrWagons { get; set; }

    [Column("capacity_wagons")]
    public int? capacityWagons { get; set; }
}
