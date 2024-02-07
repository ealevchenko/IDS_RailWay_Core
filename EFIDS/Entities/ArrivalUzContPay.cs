using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Arrival_UZ_Cont_Pay", Schema = "IDS")]
public partial class ArrivalUzContPay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_cont")]
    public long IdCont { get; set; }

    [Column("kod")]
    [StringLength(3)]
    public string Kod { get; set; } = null!;

    [Column("summa")]
    public long Summa { get; set; }

    [ForeignKey("IdCont")]
    [InverseProperty("ArrivalUzContPays")]
    public virtual ArrivalUzVagonCont IdContNavigation { get; set; } = null!;
}
