using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Functions;

public class ViewStatusAllStation
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

    [Column("count_arrives_wagons")]
    public int? countArrivesWagons { get; set; }

    [Column("count_sent_wagons")]
    public int? countSentWagons { get; set; }
    [Column("count_all_wagons")]
    public int? countAllWagons { get; set; }
    [Column("count_amkr_wagons")]
    public int? countAmkrWagons { get; set; }

    [Column("capacity_wagons")]
    public int? capacityWagons { get; set; }
}
