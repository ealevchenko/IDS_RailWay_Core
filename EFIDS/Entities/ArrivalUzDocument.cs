using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Arrival_UZ_Document", Schema = "IDS")]
public partial class ArrivalUzDocument
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_doc_uz")]
    [StringLength(50)]
    public string IdDocUz { get; set; } = null!;

    [Column("nom_doc")]
    public int? NomDoc { get; set; }

    [Column("nom_main_doc")]
    public int? NomMainDoc { get; set; }

    [Column("code_stn_from")]
    public int? CodeStnFrom { get; set; }

    [Column("code_stn_to")]
    public int? CodeStnTo { get; set; }

    [Column("code_border_checkpoint")]
    public int? CodeBorderCheckpoint { get; set; }

    [Column("cross_time", TypeName = "datetime")]
    public DateTime? CrossTime { get; set; }

    [Column("code_shipper")]
    public int? CodeShipper { get; set; }

    [Column("code_consignee")]
    public int? CodeConsignee { get; set; }

    [Column("klient")]
    public bool? Klient { get; set; }

    [Column("code_payer_sender")]
    [StringLength(20)]
    public string? CodePayerSender { get; set; }

    [Column("code_payer_arrival")]
    [StringLength(20)]
    public string? CodePayerArrival { get; set; }

    [Column("distance_way")]
    public int? DistanceWay { get; set; }

    [Column("note")]
    [StringLength(200)]
    public string? Note { get; set; }

    [Column("parent_id")]
    public long? ParentId { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("change", TypeName = "datetime")]
    public DateTime? Change { get; set; }

    [Column("change_user")]
    [StringLength(50)]
    public string? ChangeUser { get; set; }

    [Column("manual")]
    public bool? Manual { get; set; }

    [Column("date_otpr", TypeName = "datetime")]
    public DateTime? DateOtpr { get; set; }

    [Column("srok_end", TypeName = "datetime")]
    public DateTime? SrokEnd { get; set; }

    [Column("date_grpol", TypeName = "datetime")]
    public DateTime? DateGrpol { get; set; }

    [Column("date_pr", TypeName = "datetime")]
    public DateTime? DatePr { get; set; }

    [Column("date_vid", TypeName = "datetime")]
    public DateTime? DateVid { get; set; }

    [InverseProperty("IdDocumentNavigation")]
    public virtual ICollection<ArrivalUzDocumentAct> ArrivalUzDocumentActs { get; } = new List<ArrivalUzDocumentAct>();

    [InverseProperty("IdDocumentNavigation")]
    public virtual ICollection<ArrivalUzDocumentDoc> ArrivalUzDocumentDocs { get; } = new List<ArrivalUzDocumentDoc>();

    [InverseProperty("IdDocumentNavigation")]
    public virtual ICollection<ArrivalUzDocumentPay> ArrivalUzDocumentPays { get; } = new List<ArrivalUzDocumentPay>();

    [InverseProperty("IdDocumentNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [ForeignKey("CodeBorderCheckpoint")]
    [InverseProperty("ArrivalUzDocuments")]
    public virtual DirectoryBorderCheckpoint? CodeBorderCheckpointNavigation { get; set; }

    [ForeignKey("CodeConsignee")]
    [InverseProperty("ArrivalUzDocuments")]
    public virtual DirectoryConsignee? CodeConsigneeNavigation { get; set; }

    [ForeignKey("CodePayerSender")]
    [InverseProperty("ArrivalUzDocuments")]
    public virtual DirectoryPayerSender? CodePayerSenderNavigation { get; set; }

    [ForeignKey("CodeShipper")]
    [InverseProperty("ArrivalUzDocuments")]
    public virtual DirectoryShipper? CodeShipperNavigation { get; set; }

    [ForeignKey("CodeStnFrom")]
    [InverseProperty("ArrivalUzDocumentCodeStnFromNavigations")]
    public virtual DirectoryExternalStation? CodeStnFromNavigation { get; set; }

    [ForeignKey("CodeStnTo")]
    [InverseProperty("ArrivalUzDocumentCodeStnToNavigations")]
    public virtual DirectoryExternalStation? CodeStnToNavigation { get; set; }

    [ForeignKey("IdDocUz")]
    [InverseProperty("ArrivalUzDocuments")]
    public virtual UzDoc IdDocUzNavigation { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<ArrivalUzDocument> InverseParent { get; } = new List<ArrivalUzDocument>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual ArrivalUzDocument? Parent { get; set; }
}
