using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class ВыборкаПрибытий
{
    [Column("id")]
    public long Id { get; set; }

    [Column("train")]
    public int Train { get; set; }

    [Column("composition_index")]
    [StringLength(50)]
    public string CompositionIndex { get; set; } = null!;

    [Column("date_arrival", TypeName = "datetime")]
    public DateTime DateArrival { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("position")]
    public int Position { get; set; }

    [Column("consignee")]
    public int Consignee { get; set; }

    [Column("num_doc")]
    [StringLength(50)]
    public string? NumDoc { get; set; }

    [Column("revision")]
    public int? Revision { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("code_from")]
    [StringLength(4)]
    public string? CodeFrom { get; set; }

    [Column("code_on")]
    [StringLength(4)]
    public string? CodeOn { get; set; }

    [Column("dt", TypeName = "datetime")]
    public DateTime? Dt { get; set; }

    [StringLength(50)]
    public string Cargo { get; set; } = null!;

    public int CargoCode { get; set; }
}
