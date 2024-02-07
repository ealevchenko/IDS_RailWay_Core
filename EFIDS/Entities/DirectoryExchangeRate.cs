using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_ExchangeRate", Schema = "IDS")]
public partial class DirectoryExchangeRate
{
    [Key]
    [Column("re")]
    public int Re { get; set; }

    [Column("txt")]
    [StringLength(100)]
    public string? Txt { get; set; }

    [Column("rate")]
    public float? Rate { get; set; }

    [Column("cc")]
    [StringLength(100)]
    public string? Cc { get; set; }

    [Column("exchangedate", TypeName = "datetime")]
    public DateTime? Exchangedate { get; set; }
}
