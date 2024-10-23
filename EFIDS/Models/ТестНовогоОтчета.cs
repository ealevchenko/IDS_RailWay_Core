using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class ТестНовогоОтчета
{
    [Column("cycle")]
    public int Cycle { get; set; }

    [Column("route")]
    public int Route { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Expr2 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Expr3 { get; set; }

    public int? Expr1 { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("station")]
    [StringLength(50)]
    public string Station { get; set; } = null!;

    [Column("id_cargo")]
    public int IdCargo { get; set; }
}
