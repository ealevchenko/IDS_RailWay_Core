using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("OutgoingSostav", Schema = "IDS")]
public partial class OutgoingSostav
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("num_doc")]
    public int NumDoc { get; set; }

    [Column("id_station_from")]
    public int IdStationFrom { get; set; }

    [Column("id_way_from")]
    public int IdWayFrom { get; set; }

    [Column("id_station_on")]
    public int? IdStationOn { get; set; }

    [Column("date_readiness_amkr", TypeName = "datetime")]
    public DateTime DateReadinessAmkr { get; set; }

    [Column("date_end_inspection_acceptance_delivery", TypeName = "datetime")]
    public DateTime? DateEndInspectionAcceptanceDelivery { get; set; }

    [Column("date_end_inspection_loader", TypeName = "datetime")]
    public DateTime? DateEndInspectionLoader { get; set; }

    [Column("date_end_inspection_vagonnik", TypeName = "datetime")]
    public DateTime? DateEndInspectionVagonnik { get; set; }

    [Column("date_show_wagons", TypeName = "datetime")]
    public DateTime? DateShowWagons { get; set; }

    [Column("date_readiness_uz", TypeName = "datetime")]
    public DateTime? DateReadinessUz { get; set; }

    [Column("date_outgoing", TypeName = "datetime")]
    public DateTime? DateOutgoing { get; set; }

    [Column("date_outgoing_act", TypeName = "datetime")]
    public DateTime? DateOutgoingAct { get; set; }

    [Column("date_departure_amkr", TypeName = "datetime")]
    public DateTime? DateDepartureAmkr { get; set; }

    [Column("composition_index")]
    [StringLength(50)]
    public string? CompositionIndex { get; set; }

    [Column("status")]
    public int Status { get; set; }

    [Column("route_sign")]
    public bool? RouteSign { get; set; }

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

    [Column("vagonnik_user")]
    [StringLength(50)]
    public string? VagonnikUser { get; set; }

    [ForeignKey("IdStationFrom")]
    [InverseProperty("OutgoingSostavIdStationFromNavigations")]
    public virtual DirectoryStation IdStationFromNavigation { get; set; } = null!;

    [ForeignKey("IdStationOn")]
    [InverseProperty("OutgoingSostavIdStationOnNavigations")]
    public virtual DirectoryStation? IdStationOnNavigation { get; set; }

    [ForeignKey("IdWayFrom")]
    [InverseProperty("OutgoingSostavs")]
    public virtual DirectoryWay IdWayFromNavigation { get; set; } = null!;

    [InverseProperty("IdOutgoingNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCars { get; } = new List<OutgoingCar>();

    [InverseProperty("IdOutgoingNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();
}
