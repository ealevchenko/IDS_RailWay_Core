using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_DEPO", Schema = "IDS")]
public partial class DirectoryDepo
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("code_station")]
    public int CodeStation { get; set; }

    [Column("depo_ru")]
    [StringLength(50)]
    public string DepoRu { get; set; } = null!;

    [Column("depo_en")]
    [StringLength(50)]
    public string DepoEn { get; set; } = null!;

    [InverseProperty("CodeDepoNavigation")]
    public virtual ICollection<CardsWagonsRepair> CardsWagonsRepairs { get; } = new List<CardsWagonsRepair>();
}
