using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_BankRate", Schema = "IDS")]
public partial class DirectoryBankRate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date", TypeName = "datetime")]
    public DateTime Date { get; set; }

    [Column("code")]
    public int Code { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("rate")]
    public float Rate { get; set; }

    [Column("cc")]
    [StringLength(3)]
    public string Cc { get; set; } = null!;
}
