using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class View3
{
    [Column("id_wagon_internal_routes")]
    public long IdWagonInternalRoutes { get; set; }

    [Column("id_way")]
    public int IdWay { get; set; }

    [Column("way_end", TypeName = "datetime")]
    public DateTime? WayEnd { get; set; }

    [Column("id")]
    public long Id { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_arrival_car")]
    public long? IdArrivalCar { get; set; }

    [Column("num_doc")]
    [StringLength(50)]
    public string? NumDoc { get; set; }

    [Column("date_adoption_act", TypeName = "datetime")]
    public DateTime? DateAdoptionAct { get; set; }

    [Column("date_arrival", TypeName = "datetime")]
    public DateTime DateArrival { get; set; }

    [Column("date_adoption", TypeName = "datetime")]
    public DateTime? DateAdoption { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Expr1 { get; set; }

    [Column("id_countrys")]
    public int IdCountrys { get; set; }

    [Column("id_genus")]
    public int IdGenus { get; set; }
}
