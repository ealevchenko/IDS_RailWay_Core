using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

[Keyless]
public class ViewOperatorsOuterWay
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("name_outer_way_ru")]
    [StringLength(150)]
    public string? NameOuterWayRu { get; set; }

    [Column("name_outer_way_en")]
    [StringLength(150)]
    public string? NameOuterWayEn { get; set; }

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
