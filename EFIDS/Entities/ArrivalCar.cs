using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("ArrivalCars", Schema = "IDS")]
[Index("PositionArrival", Name = "NCI_arrival_position")]
[Index("IdArrival", Name = "NCI_id_arrival")]
public partial class ArrivalCar
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_arrival")]
    public long? IdArrival { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("position")]
    public int Position { get; set; }

    [Column("position_arrival")]
    public int? PositionArrival { get; set; }

    [Column("consignee")]
    public int Consignee { get; set; }

    [Column("num_doc")]
    [StringLength(50)]
    public string? NumDoc { get; set; }

    [Column("id_transfer")]
    public long? IdTransfer { get; set; }

    [Column("note")]
    [StringLength(200)]
    public string? Note { get; set; }

    [Column("date_adoption_act", TypeName = "datetime")]
    public DateTime? DateAdoptionAct { get; set; }

    [Column("arrival", TypeName = "datetime")]
    public DateTime? Arrival { get; set; }

    [Column("arrival_user")]
    [StringLength(50)]
    public string? ArrivalUser { get; set; }

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

    [Column("id_arrival_uz_vagon")]
    public long? IdArrivalUzVagon { get; set; }

    [ForeignKey("IdArrival")]
    [InverseProperty("ArrivalCars")]
    public virtual ArrivalSostav? IdArrivalNavigation { get; set; }

    [ForeignKey("IdArrivalUzVagon")]
    [InverseProperty("ArrivalCars")]
    public virtual ArrivalUzVagon? IdArrivalUzVagonNavigation { get; set; }

    [ForeignKey("NumDoc")]
    [InverseProperty("ArrivalCars")]
    public virtual UzDoc? NumDocNavigation { get; set; }

    [InverseProperty("IdArrivalCarNavigation")]
    public virtual ICollection<SapincomingSupply> SapincomingSupplies { get; } = new List<SapincomingSupply>();

    [InverseProperty("IdArrivalCarNavigation")]
    public virtual ICollection<WagonInternalRoute> WagonInternalRoutes { get; } = new List<WagonInternalRoute>();
}
