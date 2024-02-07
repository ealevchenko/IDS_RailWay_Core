using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Settings", Schema = "IDS")]
public partial class Setting
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("area")]
    [StringLength(20)]
    public string Area { get; set; } = null!;

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [Column("value")]
    [StringLength(20)]
    public string Value { get; set; } = null!;

    [Column("type")]
    public int Type { get; set; }

    [Column("description")]
    [StringLength(100)]
    public string? Description { get; set; }
}
