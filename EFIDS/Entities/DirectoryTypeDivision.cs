using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_TypeDivision", Schema = "IDS")]
public partial class DirectoryTypeDivision
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type_devisions_ru")]
    [StringLength(250)]
    public string TypeDevisionsRu { get; set; } = null!;

    [Column("type_devisions_en")]
    [StringLength(250)]
    public string TypeDevisionsEn { get; set; } = null!;

    [InverseProperty("IdTypeDevisionNavigation")]
    public virtual ICollection<DirectoryDivision> DirectoryDivisions { get; } = new List<DirectoryDivision>();
}
