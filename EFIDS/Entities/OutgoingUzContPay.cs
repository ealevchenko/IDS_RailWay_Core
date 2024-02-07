using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Outgoing_UZ_Cont_Pay", Schema = "IDS")]
public partial class OutgoingUzContPay
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
    [InverseProperty("OutgoingUzContPays")]
    public virtual OutgoingUzVagonCont IdContNavigation { get; set; } = null!;
}
