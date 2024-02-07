using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[PrimaryKey("Areas", "Controller", "Action")]
[Table("WebAccess", Schema = "IDS")]
public partial class WebAccess
{
    [Key]
    [Column("areas")]
    [StringLength(100)]
    public string Areas { get; set; } = null!;

    [Key]
    [Column("controller")]
    [StringLength(100)]
    public string Controller { get; set; } = null!;

    [Key]
    [Column("action")]
    [StringLength(100)]
    public string Action { get; set; } = null!;

    [Column("description")]
    [StringLength(100)]
    public string? Description { get; set; }

    [Column("roles")]
    public string? Roles { get; set; }

    [Column("users")]
    public string? Users { get; set; }
}
