using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Consignee", Schema = "IDS")]
public partial class DirectoryConsignee
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("description")]
    [StringLength(200)]
    public string Description { get; set; } = null!;

    [Column("auxiliary")]
    public bool Auxiliary { get; set; }

    [Column("id_division")]
    public int? IdDivision { get; set; }

    [InverseProperty("CodeConsigneeNavigation")]
    public virtual ICollection<ArrivalUzDocument> ArrivalUzDocuments { get; } = new List<ArrivalUzDocument>();

    [ForeignKey("IdDivision")]
    [InverseProperty("DirectoryConsignees")]
    public virtual DirectoryDivision? IdDivisionNavigation { get; set; }

    [InverseProperty("CodeShipperNavigation")]
    public virtual ICollection<OutgoingUzDocument> OutgoingUzDocuments { get; } = new List<OutgoingUzDocument>();
}
