using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("WagonInternalRoutes", Schema = "IDS")]
[Index("ParentId", Name = "NCI_WIR_parent_id")]
[Index("IdArrivalCar", Name = "NCI_id_arrival_car_id_sap_incoming_supply")]
[Index("IdOutgoingCar", Name = "NCI_id_outgoing_car")]
public partial class WagonInternalRoute
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_arrival_car")]
    public long? IdArrivalCar { get; set; }

    [Column("id_sap_incoming_supply")]
    public long? IdSapIncomingSupply { get; set; }

    [Column("doc_outgoing_car")]
    public bool? DocOutgoingCar { get; set; }

    [Column("id_outgoing_car")]
    public long? IdOutgoingCar { get; set; }

    [Column("id_sap_outbound_supply")]
    public long? IdSapOutboundSupply { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_user")]
    [StringLength(50)]
    public string? CloseUser { get; set; }

    [Column("parent_id")]
    public long? ParentId { get; set; }

    [Column("highlight_color")]
    [StringLength(10)]
    public string? HighlightColor { get; set; }

    [Column("id_usage_fee")]
    public int? IdUsageFee { get; set; }

    [ForeignKey("IdArrivalCar")]
    [InverseProperty("WagonInternalRoutes")]
    public virtual ArrivalCar? IdArrivalCarNavigation { get; set; }

    [ForeignKey("IdOutgoingCar")]
    [InverseProperty("WagonInternalRoutes")]
    public virtual OutgoingCar? IdOutgoingCarNavigation { get; set; }

    [ForeignKey("IdSapIncomingSupply")]
    [InverseProperty("WagonInternalRoutes")]
    public virtual SapincomingSupply? IdSapIncomingSupplyNavigation { get; set; }

    [ForeignKey("IdSapOutboundSupply")]
    [InverseProperty("WagonInternalRoutes")]
    public virtual SapoutgoingSupply? IdSapOutboundSupplyNavigation { get; set; }

    [ForeignKey("IdUsageFee")]
    [InverseProperty("WagonInternalRoutes")]
    public virtual WagonUsageFee? IdUsageFeeNavigation { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<WagonInternalRoute> InverseParent { get; } = new List<WagonInternalRoute>();

    [ForeignKey("Num")]
    [InverseProperty("WagonInternalRoutes")]
    public virtual DirectoryWagon NumNavigation { get; set; } = null!;

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual WagonInternalRoute? Parent { get; set; }

    [InverseProperty("IdWagonInternalRoutesNavigation")]
    public virtual ICollection<WagonInternalMovement> WagonInternalMovements { get; } = new List<WagonInternalMovement>();

    [InverseProperty("IdWagonInternalRoutesNavigation")]
    public virtual ICollection<WagonInternalOperation> WagonInternalOperations { get; } = new List<WagonInternalOperation>();
}
