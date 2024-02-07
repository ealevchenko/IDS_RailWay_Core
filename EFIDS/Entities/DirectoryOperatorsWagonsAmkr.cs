using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_OperatorsWagons_AMKR", Schema = "IDS")]
public partial class DirectoryOperatorsWagonsAmkr
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_operator")]
    public int IdOperator { get; set; }
}
