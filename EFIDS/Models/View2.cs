using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class View2
{
    [Column("id_wt")]
    public int IdWt { get; set; }

    [Column("cycle")]
    public int Cycle { get; set; }

    [Column("route")]
    public int Route { get; set; }

    [Column("station_from")]
    public int StationFrom { get; set; }

    [Column("station_end")]
    public int StationEnd { get; set; }

    [Column("nvagon")]
    public int Nvagon { get; set; }

    [Column("st_disl")]
    public int? StDisl { get; set; }

    [Column("nst_disl")]
    [StringLength(50)]
    public string? NstDisl { get; set; }

    [Column("kodop")]
    public int? Kodop { get; set; }

    [Column("nameop")]
    [StringLength(50)]
    public string? Nameop { get; set; }

    [Column("full_nameop")]
    [StringLength(100)]
    public string? FullNameop { get; set; }

    [Column("dt", TypeName = "datetime")]
    public DateTime? Dt { get; set; }

    [Column("st_form")]
    public int? StForm { get; set; }

    [Column("nst_form")]
    [StringLength(50)]
    public string? NstForm { get; set; }

    [Column("idsost")]
    public int? Idsost { get; set; }

    [Column("nsost")]
    [StringLength(50)]
    public string? Nsost { get; set; }

    [Column("st_nazn")]
    public int? StNazn { get; set; }

    [Column("nst_nazn")]
    [StringLength(50)]
    public string? NstNazn { get; set; }

    [Column("ntrain")]
    public int? Ntrain { get; set; }

    [Column("st_end")]
    public int? StEnd { get; set; }

    [Column("nst_end")]
    [StringLength(50)]
    public string? NstEnd { get; set; }

    [Column("kgr")]
    public int? Kgr { get; set; }

    [Column("nkgr")]
    [StringLength(500)]
    public string? Nkgr { get; set; }

    [Column("id_cargo")]
    public int IdCargo { get; set; }

    [Column("kgrp")]
    public int? Kgrp { get; set; }

    [Column("ves", TypeName = "numeric(18, 3)")]
    public decimal? Ves { get; set; }

    [Column("updated", TypeName = "datetime")]
    public DateTime? Updated { get; set; }

    [Column("kgro")]
    public int? Kgro { get; set; }

    [Column("km")]
    public int? Km { get; set; }
}
