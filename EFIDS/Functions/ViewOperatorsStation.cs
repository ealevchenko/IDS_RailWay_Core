using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

[Keyless]
public class ViewOperatorsStation
{
    [Column("id_way")]
    public int? IdWay { get; set; }

    [Column("current_way_amkr_num_ru")]
    [StringLength(20)]
    public string? CurrentWayAmkrNumRu { get; set; }

    [Column("current_way_amkr_num_en")]
    [StringLength(20)]
    public string? CurrentWayAmkrNumEn { get; set; }

    [Column("current_way_amkr_abbr_ru")]
    [StringLength(50)]
    public string? CurrentWayAmkrAbbrRu { get; set; }

    [Column("current_way_amkr_abbr_en")]
    [StringLength(50)]
    public string? CurrentWayAmkrAbbrEn { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("operator_abbr_ru")]
    [StringLength(20)]
    public string? OperatorAbbrRu { get; set; }

    [Column("operator_abbr_en")]
    [StringLength(20)]
    public string? OperatorAbbrEn { get; set; }

    [Column("operator_color")]
    [StringLength(10)]
    public string? OperatorColor { get; set; }

    [Column("count_operators")]
    public int? CountOperators { get; set; }
}
