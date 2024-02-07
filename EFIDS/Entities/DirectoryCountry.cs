using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Countrys", Schema = "IDS")]
public partial class DirectoryCountry
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("code_sng")]
    public int? CodeSng { get; set; }

    [Column("code_europe")]
    public int? CodeEurope { get; set; }

    [Column("code_iso")]
    public int? CodeIso { get; set; }

    [Column("countrys_name_ru")]
    [StringLength(100)]
    public string CountrysNameRu { get; set; } = null!;

    [Column("countrys_name_en")]
    [StringLength(100)]
    public string CountrysNameEn { get; set; } = null!;

    [Column("country_abbr_ru")]
    [StringLength(10)]
    public string CountryAbbrRu { get; set; } = null!;

    [Column("country_abbr_en")]
    [StringLength(10)]
    public string CountryAbbrEn { get; set; } = null!;

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

    [InverseProperty("IdCountrysNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdCountrysNavigation")]
    public virtual ICollection<DirectoryRailway> DirectoryRailways { get; } = new List<DirectoryRailway>();

    [InverseProperty("IdCountrysNavigation")]
    public virtual ICollection<DirectoryWagon> DirectoryWagons { get; } = new List<DirectoryWagon>();

    [InverseProperty("IdCountrysNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();
}
