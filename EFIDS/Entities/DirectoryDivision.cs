using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Divisions", Schema = "IDS")]
public partial class DirectoryDivision
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("position")]
    public int Position { get; set; }

    [Column("name_division_ru")]
    [StringLength(250)]
    public string NameDivisionRu { get; set; } = null!;

    [Column("name_division_en")]
    [StringLength(250)]
    public string NameDivisionEn { get; set; } = null!;

    [Column("division_abbr_ru")]
    [StringLength(50)]
    public string DivisionAbbrRu { get; set; } = null!;

    [Column("division_abbr_en")]
    [StringLength(50)]
    public string DivisionAbbrEn { get; set; } = null!;

    [Column("id_type_devision")]
    public int IdTypeDevision { get; set; }

    [Column("code")]
    [StringLength(5)]
    public string? Code { get; set; }

    [Column("old")]
    public bool? Old { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [InverseProperty("IdDivisionOnAmkrNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("IdDivisionNavigation")]
    public virtual ICollection<DirectoryConsignee> DirectoryConsignees { get; } = new List<DirectoryConsignee>();

    [InverseProperty("IdDevisionNavigation")]
    public virtual ICollection<DirectoryWay> DirectoryWays { get; } = new List<DirectoryWay>();

    [ForeignKey("IdTypeDevision")]
    [InverseProperty("DirectoryDivisions")]
    public virtual DirectoryTypeDivision IdTypeDevisionNavigation { get; set; } = null!;

    [InverseProperty("Parent")]
    public virtual ICollection<DirectoryDivision> InverseParent { get; } = new List<DirectoryDivision>();

    [InverseProperty("IdDivisionNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; } = new List<OutgoingUzVagon>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual DirectoryDivision? Parent { get; set; }
}
