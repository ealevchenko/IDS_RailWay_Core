using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

[Keyless]
public class ViewOperatorsStation
{
    [Column("id_park")]
    public int? IdPark { get; set; }

    [Column("park_abbr_ru")]
    [StringLength(50)]
    public string? ParkAbbrRu { get; set; }

    [Column("park_abbr_en")]
    [StringLength(50)]
    public string? ParkAbbrEn { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("operator_abbr_ru")]
    [StringLength(20)]
    public string? OperatorAbbrRu { get; set; }

    [Column("operator_abbr_en")]
    [StringLength(20)]
    public string? OperatorAbbrEn { get; set; }

    [Column("count_operators")]
    public int? CountOperators { get; set; }
}
