using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("ArrivalSostav", Schema = "IDS")]
public partial class ArrivalSostav
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_arrived")]
    public long? IdArrived { get; set; }

    [Column("id_sostav")]
    public long? IdSostav { get; set; }

    [Column("train")]
    public int Train { get; set; }

    [Column("composition_index")]
    [StringLength(50)]
    public string CompositionIndex { get; set; } = null!;

    [Column("date_arrival", TypeName = "datetime")]
    public DateTime DateArrival { get; set; }

    [Column("date_adoption", TypeName = "datetime")]
    public DateTime? DateAdoption { get; set; }

    [Column("date_adoption_act", TypeName = "datetime")]
    public DateTime? DateAdoptionAct { get; set; }

    [Column("id_station_from")]
    public int? IdStationFrom { get; set; }

    [Column("id_station_on")]
    public int? IdStationOn { get; set; }

    [Column("id_way")]
    public int? IdWay { get; set; }

    [Column("numeration")]
    public bool? Numeration { get; set; }

    [Column("num_doc")]
    public int? NumDoc { get; set; }

    [Column("count")]
    public int? Count { get; set; }

    [Column("status")]
    public int Status { get; set; }

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

    [InverseProperty("IdArrivalNavigation")]
    public virtual ICollection<ArrivalCar> ArrivalCars { get; } = new List<ArrivalCar>();

    [InverseProperty("IdArrivalNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [ForeignKey("IdStationFrom")]
    [InverseProperty("ArrivalSostavIdStationFromNavigations")]
    public virtual DirectoryStation? IdStationFromNavigation { get; set; }

    [ForeignKey("IdStationOn")]
    [InverseProperty("ArrivalSostavIdStationOnNavigations")]
    public virtual DirectoryStation? IdStationOnNavigation { get; set; }

    [ForeignKey("IdWay")]
    [InverseProperty("ArrivalSostavs")]
    public virtual DirectoryWay? IdWayNavigation { get; set; }
}
