using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("SAPOutgoingSupply", Schema = "IDS")]
public partial class SapoutgoingSupply
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_out_supply")]
    public int IdOutSupply { get; set; }

    [Column("id_outgoing_car")]
    public long? IdOutgoingCar { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("VBELN")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Vbeln { get; set; }

    [Column("ERDAT", TypeName = "date")]
    public DateTime? Erdat { get; set; }

    [Column("ZBEZEI")]
    [StringLength(160)]
    public string? Zbezei { get; set; }

    [Column("STAWN")]
    [StringLength(17)]
    [Unicode(false)]
    public string? Stawn { get; set; }

    [Column("NAME1_AG")]
    [StringLength(150)]
    public string? Name1Ag { get; set; }

    [Column("KUNNR_AG")]
    [StringLength(10)]
    [Unicode(false)]
    public string? KunnrAg { get; set; }

    [Column("ZRWNAME")]
    [StringLength(30)]
    public string? Zrwname { get; set; }

    [Column("ZENDSTAT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Zendstat { get; set; }

    [Column("ZCRSTNAME")]
    [StringLength(30)]
    public string? Zcrstname { get; set; }

    [Column("ZCROSSSTAT")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Zcrossstat { get; set; }

    [Column("ZZVES_NETTO")]
    public double? ZzvesNetto { get; set; }

    [Column("ABTNR")]
    [StringLength(4)]
    [Unicode(false)]
    public string? Abtnr { get; set; }

    [Column("VTEXT")]
    [StringLength(20)]
    public string? Vtext { get; set; }

    [Column("ZZDOLG")]
    [StringLength(50)]
    public string? Zzdolg { get; set; }

    [Column("ZZFIO")]
    [StringLength(50)]
    public string? Zzfio { get; set; }

    [Column("ZZPLATEL")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Zzplatel { get; set; }

    [Column("ZZNAME_PLATEL")]
    [StringLength(50)]
    public string? ZznamePlatel { get; set; }

    [Column("note")]
    [StringLength(250)]
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

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_user")]
    [StringLength(50)]
    public string? CloseUser { get; set; }

    [ForeignKey("IdOutgoingCar")]
    [InverseProperty("SapoutgoingSupplies")]
    public virtual OutgoingCar? IdOutgoingCarNavigation { get; set; }

    [InverseProperty("IdSapOutboundSupplyNavigation")]
    public virtual ICollection<WagonInternalRoute> WagonInternalRoutes { get; } = new List<WagonInternalRoute>();
}
