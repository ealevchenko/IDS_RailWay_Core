using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Outgoing_UZ_Vagon_Cont", Schema = "IDS")]
public partial class OutgoingUzVagonCont
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_vagon")]
    public long IdVagon { get; set; }

    [Column("nom_cont")]
    [StringLength(11)]
    public string NomCont { get; set; } = null!;

    [Column("kod_tiporazmer")]
    [StringLength(4)]
    public string? KodTiporazmer { get; set; }

    [Column("gruzp")]
    public int? Gruzp { get; set; }

    [Column("ves_tary_arc")]
    public int? VesTaryArc { get; set; }

    [Column("id_cargo")]
    public int? IdCargo { get; set; }

    [Column("id_cargo_gng")]
    public int? IdCargoGng { get; set; }

    [Column("kol_pac")]
    public int? KolPac { get; set; }

    [Column("pac")]
    [StringLength(3)]
    public string? Pac { get; set; }

    [Column("vesg")]
    public int? Vesg { get; set; }

    [Column("nom_zpu")]
    [StringLength(20)]
    public string? NomZpu { get; set; }

    [ForeignKey("IdCargoGng")]
    [InverseProperty("OutgoingUzVagonConts")]
    public virtual DirectoryCargoGng? IdCargoGngNavigation { get; set; }

    [ForeignKey("IdCargo")]
    [InverseProperty("OutgoingUzVagonConts")]
    public virtual DirectoryCargo? IdCargoNavigation { get; set; }

    [ForeignKey("IdVagon")]
    [InverseProperty("OutgoingUzVagonConts")]
    public virtual OutgoingUzVagon IdVagonNavigation { get; set; } = null!;

    [InverseProperty("IdContNavigation")]
    public virtual ICollection<OutgoingUzContPay> OutgoingUzContPays { get; } = new List<OutgoingUzContPay>();
}
