using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Outgoing_UZ_Document_Pay", Schema = "IDS")]
public partial class OutgoingUzDocumentPay
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_document")]
    public long IdDocument { get; set; }

    [Column("code_payer")]
    public int CodePayer { get; set; }

    [Column("type_payer")]
    public int TypePayer { get; set; }

    [Column("kod")]
    [StringLength(3)]
    public string Kod { get; set; } = null!;

    [Column("summa")]
    public long Summa { get; set; }

    [ForeignKey("IdDocument")]
    [InverseProperty("OutgoingUzDocumentPays")]
    public virtual OutgoingUzDocument IdDocumentNavigation { get; set; } = null!;
}
