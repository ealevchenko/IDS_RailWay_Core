using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Outgoing_UZ_Document", Schema = "IDS")]
public partial class OutgoingUzDocument
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_doc_uz")]
    [StringLength(50)]
    public string IdDocUz { get; set; } = null!;

    [Column("nom_doc")]
    public int? NomDoc { get; set; }

    [Column("code_stn_from")]
    public int? CodeStnFrom { get; set; }

    [Column("code_stn_to")]
    public int? CodeStnTo { get; set; }

    [Column("country_nazn")]
    public int? CountryNazn { get; set; }

    [Column("code_border_checkpoint")]
    public int? CodeBorderCheckpoint { get; set; }

    [Column("cross_date", TypeName = "datetime")]
    public DateTime? CrossDate { get; set; }

    [Column("code_shipper")]
    public int? CodeShipper { get; set; }

    [Column("code_consignee")]
    public int? CodeConsignee { get; set; }

    [Column("vid")]
    [StringLength(2)]
    public string? Vid { get; set; }

    [Column("code_payer")]
    [StringLength(20)]
    public string? CodePayer { get; set; }

    [Column("distance_way")]
    public int? DistanceWay { get; set; }

    [Column("osum")]
    public long? Osum { get; set; }

    [Column("date_sozdan", TypeName = "datetime")]
    public DateTime? DateSozdan { get; set; }

    [Column("date_otpr", TypeName = "datetime")]
    public DateTime? DateOtpr { get; set; }

    [Column("date_pr", TypeName = "datetime")]
    public DateTime? DatePr { get; set; }

    [Column("date_grpol", TypeName = "datetime")]
    public DateTime? DateGrpol { get; set; }

    [Column("date_vid", TypeName = "datetime")]
    public DateTime? DateVid { get; set; }

    [Column("info_sht")]
    [StringLength(400)]
    public string? InfoSht { get; set; }

    [Column("name_gr")]
    [StringLength(800)]
    public string? NameGr { get; set; }

    [Column("note")]
    [StringLength(200)]
    public string? Note { get; set; }

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

    [ForeignKey("CodeBorderCheckpoint")]
    [InverseProperty("OutgoingUzDocuments")]
    public virtual DirectoryBorderCheckpoint? CodeBorderCheckpointNavigation { get; set; }

    [ForeignKey("CodeConsignee")]
    [InverseProperty("OutgoingUzDocuments")]
    public virtual DirectoryShipper? CodeConsigneeNavigation { get; set; }

    [ForeignKey("CodePayer")]
    [InverseProperty("OutgoingUzDocuments")]
    public virtual DirectoryPayerSender? CodePayerNavigation { get; set; }

    [ForeignKey("CodeShipper")]
    [InverseProperty("OutgoingUzDocuments")]
    public virtual DirectoryConsignee? CodeShipperNavigation { get; set; }

    [ForeignKey("CodeStnFrom")]
    [InverseProperty("OutgoingUzDocumentCodeStnFromNavigations")]
    public virtual DirectoryExternalStation? CodeStnFromNavigation { get; set; }

    [ForeignKey("CodeStnTo")]
    [InverseProperty("OutgoingUzDocumentCodeStnToNavigations")]
    public virtual DirectoryExternalStation? CodeStnToNavigation { get; set; }

    [InverseProperty("IdDocumentNavigation")]
    public virtual ICollection<OutgoingUzDocumentPay> OutgoingUzDocumentPays { get; } = new List<OutgoingUzDocumentPay>();

    [InverseProperty("IdDocumentNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();
}
