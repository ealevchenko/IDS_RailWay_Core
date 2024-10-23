using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class View1
{
    [Column("cycle")]
    public int Cycle { get; set; }

    [Column("route")]
    public int Route { get; set; }

    [Column("station_end")]
    public int StationEnd { get; set; }

    public int? Expr1 { get; set; }

    [Column("nvagon")]
    public int Nvagon { get; set; }
}
