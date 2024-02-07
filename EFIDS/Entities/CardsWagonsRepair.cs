using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("CardsWagonsRepairs", Schema = "IDS")]
public partial class CardsWagonsRepair
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_type_repair_wagon")]
    public int IdTypeRepairWagon { get; set; }

    [Column("date_repair", TypeName = "datetime")]
    public DateTime? DateRepair { get; set; }

    [Column("id_internal_railroad")]
    public int? IdInternalRailroad { get; set; }

    [Column("code_depo")]
    public int? CodeDepo { get; set; }

    [Column("date_non_working", TypeName = "datetime")]
    public DateTime? DateNonWorking { get; set; }

    [Column("id_wagons_condition")]
    public int? IdWagonsCondition { get; set; }

    [Column("note")]
    [StringLength(500)]
    public string? Note { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;

    [Column("change", TypeName = "datetime")]
    public DateTime Change { get; set; }

    [Column("change_user")]
    [StringLength(50)]
    public string ChangeUser { get; set; } = null!;

    [ForeignKey("CodeDepo")]
    [InverseProperty("CardsWagonsRepairs")]
    public virtual DirectoryDepo? CodeDepoNavigation { get; set; }

    [ForeignKey("IdTypeRepairWagon")]
    [InverseProperty("CardsWagonsRepairs")]
    public virtual DirectoryTypesRepairsWagon IdTypeRepairWagonNavigation { get; set; } = null!;

    [ForeignKey("IdWagonsCondition")]
    [InverseProperty("CardsWagonsRepairs")]
    public virtual DirectoryWagonsCondition? IdWagonsConditionNavigation { get; set; }

    [ForeignKey("Num")]
    [InverseProperty("CardsWagonsRepairs")]
    public virtual CardsWagon NumNavigation { get; set; } = null!;
}
