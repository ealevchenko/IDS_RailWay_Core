using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Functions;

public class ViewStatusParkWay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("position")]
    [StringLength(100)]
    public int Position { get; set; }

    [Column("park_name_ru")]
    [StringLength(100)]
    public string ParkNameRu { get; set; } = null!;

    [Column("park_name_en")]
    [StringLength(100)]
    public string ParkNameEn { get; set; } = null!;

    [Column("park_abbr_ru")]
    [StringLength(50)]
    public string ParkAbbrRu { get; set; } = null!;

    [Column("park_abbr_en")]
    [StringLength(50)]
    public string ParkAbbrEn { get; set; } = null!;
    [Column("count_all_wagons")]
    public int? countAllWagons { get; set; }
    [Column("count_amkr_wagons")]
    public int? countAmkrWagons { get; set; }

    [Column("capacity_wagons")]
    public int? capacityWagons { get; set; }
}
