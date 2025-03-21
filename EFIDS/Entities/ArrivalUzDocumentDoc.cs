using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Arrival_UZ_Document_Docs", Schema = "IDS")]
public partial class ArrivalUzDocumentDoc
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_document")]
    public long IdDocument { get; set; }

    [Column("id_doc")]
    [StringLength(25)]
    public string? IdDoc { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("doc_date", TypeName = "datetime")]
    public DateTime? DocDate { get; set; }

    [Column("doc_type")]
    [StringLength(3)]
    public string? DocType { get; set; }

    [Column("doc_type_name")]
    [StringLength(150)]
    public string? DocTypeName { get; set; }

    [Column("doc")]
    [MaxLength(1)]
    public byte[]? Doc { get; set; }

    [ForeignKey("IdDocument")]
    [InverseProperty("ArrivalUzDocumentDocs")]
    public virtual ArrivalUzDocument IdDocumentNavigation { get; set; } = null!;
}
