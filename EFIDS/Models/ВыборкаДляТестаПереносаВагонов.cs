using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class ВыборкаДляТестаПереносаВагонов
{
    public int? Expr1 { get; set; }

    public int Num { get; set; }

    public int Consignee { get; set; }
}
