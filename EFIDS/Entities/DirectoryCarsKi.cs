using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Cars_KIS", Schema = "IDS")]
public partial class DirectoryCarsKi
{
    [Key]
    [Column("num")]
    public int Num { get; set; }

    [Column("id_sob_kis")]
    public int? IdSobKis { get; set; }

    [Column("id_genus")]
    public int? IdGenus { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("id_limiting")]
    public int? IdLimiting { get; set; }

    [ForeignKey("IdGenus")]
    [InverseProperty("DirectoryCarsKis")]
    public virtual DirectoryGenusWagon? IdGenusNavigation { get; set; }

    [ForeignKey("IdLimiting")]
    [InverseProperty("DirectoryCarsKis")]
    public virtual DirectoryLimitingLoading? IdLimitingNavigation { get; set; }

    [ForeignKey("IdOperator")]
    [InverseProperty("DirectoryCarsKis")]
    public virtual DirectoryOperatorsWagon? IdOperatorNavigation { get; set; }
}
