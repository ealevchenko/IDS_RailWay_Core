using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewOperatorsAndGenus
{
    [Key]
    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }
    [Key]
    [Column("id_genus")]
    public int? IdGenus { get; set; }

    [Column("operators_ru")]
    [StringLength(100)]
    public string? OperatorsRu { get; set; }

    [Column("operators_en")]
    [StringLength(100)]
    public string? OperatorsEn { get; set; }

    [Column("operators_abbr_ru")]
    [StringLength(20)]
    public string? OperatorsAbbrRu { get; set; }

    [Column("operators_abbr_en")]
    [StringLength(20)]
    public string? OperatorsAbbrEn { get; set; }

    [Column("rod_uz")]
    public int? RodUz { get; set; }

    [Column("genus_ru")]
    [StringLength(50)]
    public string? GenusRu { get; set; }

    [Column("genus_en")]
    [StringLength(50)]
    public string? GenusEn { get; set; }

    [Column("genus_abbr_ru")]
    [StringLength(5)]
    public string? GenusAbbrRu { get; set; }

    [Column("genus_abbr_en")]
    [StringLength(5)]
    public string? GenusAbbrEn { get; set; }
}
