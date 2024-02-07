using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Outgoing_UZ_Vagon_Pay", Schema = "IDS")]
public partial class OutgoingUzVagonPay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_vagon")]
    public long IdVagon { get; set; }

    [Column("kod")]
    [StringLength(3)]
    public string Kod { get; set; } = null!;

    [Column("summa")]
    public long Summa { get; set; }

    [ForeignKey("IdVagon")]
    [InverseProperty("OutgoingUzVagonPays")]
    public virtual OutgoingUzVagon IdVagonNavigation { get; set; } = null!;
}
