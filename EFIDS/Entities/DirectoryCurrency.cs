using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Currency", Schema = "IDS")]
public partial class DirectoryCurrency
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("currency_ru")]
    [StringLength(50)]
    public string CurrencyRu { get; set; } = null!;

    [Column("currency_en")]
    [StringLength(50)]
    public string CurrencyEn { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("code_cc")]
    [StringLength(3)]
    public string CodeCc { get; set; } = null!;

    [InverseProperty("IdCurrencyDerailmentNavigation")]
    public virtual ICollection<UsageFeePeriod> UsageFeePeriodIdCurrencyDerailmentNavigations { get; } = new List<UsageFeePeriod>();

    [InverseProperty("IdCurrencyNavigation")]
    public virtual ICollection<UsageFeePeriod> UsageFeePeriodIdCurrencyNavigations { get; } = new List<UsageFeePeriod>();
}
