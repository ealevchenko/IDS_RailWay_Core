using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("UZ_DOC_OUT", Schema = "IDS")]
public partial class UzDocOut
{
    [Key]
    [Column("num_doc")]
    [StringLength(50)]
    public string NumDoc { get; set; } = null!;

    [Column("revision")]
    public int Revision { get; set; }

    [Column("status")]
    public int? Status { get; set; }

    [Column("code_from")]
    [StringLength(4)]
    public string CodeFrom { get; set; } = null!;

    [Column("code_on")]
    [StringLength(4)]
    public string CodeOn { get; set; } = null!;

    [Column("dt", TypeName = "datetime")]
    public DateTime? Dt { get; set; }

    [Column("xml_doc", TypeName = "xml")]
    public string? XmlDoc { get; set; }

    [Column("num_uz")]
    public int? NumUz { get; set; }

    [InverseProperty("NumDocNavigation")]
    public virtual ICollection<OutgoingCar> OutgoingCars { get; } = new List<OutgoingCar>();
}
