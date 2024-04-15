using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

public class ViewTotalBalance
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("all")]
    public int? All { get; set; }
    [Column("amkr")]
    public int? Amkr { get; set; }

}
