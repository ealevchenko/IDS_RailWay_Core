using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_OperatorsWagonsGroup", Schema = "IDS")]
public partial class DirectoryOperatorsWagonsGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("group")]
    [StringLength(20)]
    public string Group { get; set; } = null!;

    [Column("id_operator")]
    public int IdOperator { get; set; }

    [Column("description")]
    [StringLength(200)]
    public string? Description { get; set; }
}
