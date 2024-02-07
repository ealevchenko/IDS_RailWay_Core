using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("WagonInternalMovement", Schema = "IDS")]
[Index("ParentId", Name = "NCI_Parent_id")]
[Index("IdOuterWay", Name = "NCI_id_outer_way")]
[Index("IdWay", "WayEnd", Name = "NCI_id_way_way_end")]
[Index("IdStation", "OuterWayEnd", "WayEnd", "OuterWayStart", Name = "NCI_station_way")]
[Index("IdStation", Name = "NCI_station_wim")]
[Index("IdStation", Name = "NCI_ststion")]
[Index("WayEnd", Name = "NCI_way_end")]
[Index("IdWagonInternalRoutes", Name = "NonClusteredIndex-20211021-130747")]
[Index("IdWio", Name = "NonClusteredIndex-20211021-130817")]
[Index("NumSostav", Name = "Num_sostava")]
public partial class WagonInternalMovement
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_wagon_internal_routes")]
    public long IdWagonInternalRoutes { get; set; }

    [Column("id_station")]
    public int IdStation { get; set; }

    [Column("id_way")]
    public int IdWay { get; set; }

    [Column("way_start", TypeName = "datetime")]
    public DateTime WayStart { get; set; }

    [Column("way_end", TypeName = "datetime")]
    public DateTime? WayEnd { get; set; }

    [Column("id_outer_way")]
    public int? IdOuterWay { get; set; }

    [Column("outer_way_start", TypeName = "datetime")]
    public DateTime? OuterWayStart { get; set; }

    [Column("outer_way_end", TypeName = "datetime")]
    public DateTime? OuterWayEnd { get; set; }

    [Column("position")]
    public int Position { get; set; }

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

    [Column("id_wio")]
    public long? IdWio { get; set; }

    [Column("num_sostav")]
    [StringLength(50)]
    public string? NumSostav { get; set; }

    [ForeignKey("IdOuterWay")]
    [InverseProperty("WagonInternalMovements")]
    public virtual DirectoryOuterWay? IdOuterWayNavigation { get; set; }

    [ForeignKey("IdStation")]
    [InverseProperty("WagonInternalMovements")]
    public virtual DirectoryStation IdStationNavigation { get; set; } = null!;

    [ForeignKey("IdWagonInternalRoutes")]
    [InverseProperty("WagonInternalMovements")]
    public virtual WagonInternalRoute IdWagonInternalRoutesNavigation { get; set; } = null!;

    [ForeignKey("IdWay")]
    [InverseProperty("WagonInternalMovements")]
    public virtual DirectoryWay IdWayNavigation { get; set; } = null!;

    [ForeignKey("IdWio")]
    [InverseProperty("WagonInternalMovements")]
    public virtual WagonInternalOperation? IdWioNavigation { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<WagonInternalMovement> InverseParent { get; } = new List<WagonInternalMovement>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual WagonInternalMovement? Parent { get; set; }
}
