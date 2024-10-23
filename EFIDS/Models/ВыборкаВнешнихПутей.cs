using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class ВыборкаВнешнихПутей
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name_outer_way_ru")]
    [StringLength(150)]
    public string NameOuterWayRu { get; set; } = null!;

    [Column("name_outer_way_en")]
    [StringLength(150)]
    public string NameOuterWayEn { get; set; } = null!;

    public int? Expr1 { get; set; }

    [Column("station_name_ru")]
    [StringLength(50)]
    public string? StationNameRu { get; set; }

    public int? Expr2 { get; set; }

    [Column("park_name_ru")]
    [StringLength(100)]
    public string? ParkNameRu { get; set; }

    public int? Expr3 { get; set; }

    [Column("way_num_ru")]
    [StringLength(20)]
    public string? WayNumRu { get; set; }

    [Column("way_name_ru")]
    [StringLength(100)]
    public string? WayNameRu { get; set; }

    public int? Expr4 { get; set; }

    [StringLength(50)]
    public string? Expr5 { get; set; }

    public int? Expr7 { get; set; }

    [StringLength(100)]
    public string? Expr8 { get; set; }

    public int? Expr6 { get; set; }

    [StringLength(20)]
    public string? Expr9 { get; set; }

    [StringLength(100)]
    public string? Expr10 { get; set; }
}
