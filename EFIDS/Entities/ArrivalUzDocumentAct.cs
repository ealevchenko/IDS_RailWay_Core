using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Arrival_UZ_Document_Acts", Schema = "IDS")]
public partial class ArrivalUzDocumentAct
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_document")]
    public long IdDocument { get; set; }

    [Column("date_akt", TypeName = "datetime")]
    public DateTime? DateAkt { get; set; }

    [Column("date_dved", TypeName = "datetime")]
    public DateTime? DateDved { get; set; }

    [Column("nom_akt")]
    [StringLength(9)]
    public string? NomAkt { get; set; }

    [Column("nom_dved")]
    public int? NomDved { get; set; }

    [Column("prichina_akt")]
    [StringLength(70)]
    public string? PrichinaAkt { get; set; }

    [Column("stn_akt")]
    public int? StnAkt { get; set; }

    [Column("stn_name_akt")]
    [StringLength(50)]
    public string? StnNameAkt { get; set; }

    [Column("type")]
    public int? Type { get; set; }

    [Column("vagon_nom")]
    public int? VagonNom { get; set; }

    [ForeignKey("IdDocument")]
    [InverseProperty("ArrivalUzDocumentActs")]
    public virtual ArrivalUzDocument IdDocumentNavigation { get; set; } = null!;
}
