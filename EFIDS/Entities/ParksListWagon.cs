using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("ParksListWagons", Schema = "IDS")]
public partial class ParksListWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_park_wagon")]
    public int IdParkWagon { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [ForeignKey("IdParkWagon")]
    [InverseProperty("ParksListWagons")]
    public virtual ParksWagon IdParkWagonNavigation { get; set; } = null!;

    [ForeignKey("Num")]
    [InverseProperty("ParksListWagons")]
    public virtual CardsWagon NumNavigation { get; set; } = null!;
}
