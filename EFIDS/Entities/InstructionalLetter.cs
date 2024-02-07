using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("InstructionalLetters", Schema = "IDS")]
public partial class InstructionalLetter
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("num")]
    [StringLength(20)]
    public string Num { get; set; } = null!;

    [Column("dt", TypeName = "datetime")]
    public DateTime Dt { get; set; }

    [Column("owner")]
    [StringLength(200)]
    public string Owner { get; set; } = null!;

    [Column("destination_station")]
    public int DestinationStation { get; set; }

    [Column("note")]
    [StringLength(500)]
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

    [InverseProperty("IdInstructionalLettersNavigation")]
    public virtual ICollection<InstructionalLettersWagon> InstructionalLettersWagons { get; } = new List<InstructionalLettersWagon>();
}
