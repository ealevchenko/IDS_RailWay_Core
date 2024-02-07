using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("OutgoingCars", Schema = "IDS")]
[Index("IdOutgoing", "PositionOutgoing", Name = "NCI_id_position_outgoing")]
[Index("ParentWirId", "IdOutgoingDetention", Name = "NCI_parent_wir_id_id_outgoing_detention")]
[Index("PositionOutgoing", "ParentWirId", Name = "NCI_position_outgoing_parent_wir_id")]
public partial class OutgoingCar
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_outgoing")]
    public long? IdOutgoing { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("position")]
    public int Position { get; set; }

    [Column("position_outgoing")]
    public int? PositionOutgoing { get; set; }

    [Column("num_doc")]
    [StringLength(50)]
    public string? NumDoc { get; set; }

    [Column("note")]
    [StringLength(200)]
    public string? Note { get; set; }

    [Column("date_outgoing_act", TypeName = "datetime")]
    public DateTime? DateOutgoingAct { get; set; }

    [Column("outgoing", TypeName = "datetime")]
    public DateTime? Outgoing { get; set; }

    [Column("outgoing_user")]
    [StringLength(50)]
    public string? OutgoingUser { get; set; }

    [Column("id_outgoing_uz_vagon")]
    public long? IdOutgoingUzVagon { get; set; }

    [Column("id_outgoing_detention")]
    public int? IdOutgoingDetention { get; set; }

    [Column("id_reason_discrepancy_amkr")]
    public int? IdReasonDiscrepancyAmkr { get; set; }

    [Column("id_reason_discrepancy_uz")]
    public int? IdReasonDiscrepancyUz { get; set; }

    [Column("id_outgoing_return_start")]
    public int? IdOutgoingReturnStart { get; set; }

    [Column("id_outgoing_return_stop")]
    public int? IdOutgoingReturnStop { get; set; }

    [Column("parent_wir_id")]
    public long? ParentWirId { get; set; }

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

    [Column("note_vagonnik")]
    [StringLength(100)]
    public string? NoteVagonnik { get; set; }

    [Column("vagonnik", TypeName = "datetime")]
    public DateTime? Vagonnik { get; set; }

    [Column("vagonnik_user")]
    [StringLength(50)]
    public string? VagonnikUser { get; set; }

    [ForeignKey("IdOutgoingDetention")]
    [InverseProperty("OutgoingCarIdOutgoingDetentionNavigations")]
    public virtual OutgoingDetentionReturn? IdOutgoingDetentionNavigation { get; set; }

    [ForeignKey("IdOutgoing")]
    [InverseProperty("OutgoingCars")]
    public virtual OutgoingSostav? IdOutgoingNavigation { get; set; }

    [ForeignKey("IdOutgoingReturnStart")]
    [InverseProperty("OutgoingCarIdOutgoingReturnStartNavigations")]
    public virtual OutgoingDetentionReturn? IdOutgoingReturnStartNavigation { get; set; }

    [ForeignKey("IdOutgoingReturnStop")]
    [InverseProperty("OutgoingCarIdOutgoingReturnStopNavigations")]
    public virtual OutgoingDetentionReturn? IdOutgoingReturnStopNavigation { get; set; }

    [ForeignKey("IdOutgoingUzVagon")]
    [InverseProperty("OutgoingCars")]
    public virtual OutgoingUzVagon? IdOutgoingUzVagonNavigation { get; set; }

    [ForeignKey("IdReasonDiscrepancyAmkr")]
    [InverseProperty("OutgoingCarIdReasonDiscrepancyAmkrNavigations")]
    public virtual DirectoryReasonDiscrepancy? IdReasonDiscrepancyAmkrNavigation { get; set; }

    [ForeignKey("IdReasonDiscrepancyUz")]
    [InverseProperty("OutgoingCarIdReasonDiscrepancyUzNavigations")]
    public virtual DirectoryReasonDiscrepancy? IdReasonDiscrepancyUzNavigation { get; set; }

    [ForeignKey("NumDoc")]
    [InverseProperty("OutgoingCars")]
    public virtual UzDocOut? NumDocNavigation { get; set; }

    [InverseProperty("IdOutgoingCarNavigation")]
    public virtual ICollection<SapoutgoingSupply> SapoutgoingSupplies { get; } = new List<SapoutgoingSupply>();

    [InverseProperty("IdOutgoingCarNavigation")]
    public virtual ICollection<WagonInternalRoute> WagonInternalRoutes { get; } = new List<WagonInternalRoute>();
}
