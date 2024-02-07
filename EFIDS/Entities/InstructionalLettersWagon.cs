using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("InstructionalLettersWagon", Schema = "IDS")]
public partial class InstructionalLettersWagon
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_instructional_letters")]
    public int IdInstructionalLetters { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_status")]
    public int? CloseStatus { get; set; }

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

    [ForeignKey("IdInstructionalLetters")]
    [InverseProperty("InstructionalLettersWagons")]
    public virtual InstructionalLetter IdInstructionalLettersNavigation { get; set; } = null!;
}
