using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[PrimaryKey("KodOp", "PrOp")]
[Table("Directory_WagonOperationsUZ", Schema = "IDS")]
public partial class DirectoryWagonOperationsUz
{
    [Key]
    public int KodOp { get; set; }

    [Key]
    [Column("PrOP")]
    public bool PrOp { get; set; }

    [StringLength(100)]
    public string NameOp { get; set; } = null!;

    [StringLength(10)]
    public string MnkOp { get; set; } = null!;
}
