using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("SAPIncomingSupply", Schema = "IDS")]
[Index("IdArrivalCar", Name = "NCI_sap")]
public partial class SapincomingSupply
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_arrival_car")]
    public long IdArrivalCar { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("num_doc_uz")]
    [StringLength(35)]
    public string NumDocUz { get; set; } = null!;

    [Column("date_doc_uz", TypeName = "datetime")]
    public DateTime? DateDocUz { get; set; }

    [Column("code_border_checkpoint")]
    [StringLength(6)]
    public string? CodeBorderCheckpoint { get; set; }

    [Column("name_border_checkpoint")]
    [StringLength(35)]
    public string? NameBorderCheckpoint { get; set; }

    [Column("cross_time", TypeName = "datetime")]
    public DateTime? CrossTime { get; set; }

    [Column("VBELN")]
    [StringLength(10)]
    public string? Vbeln { get; set; }

    [Column("NUM_VBELN")]
    [StringLength(10)]
    public string? NumVbeln { get; set; }

    [Column("WERKS")]
    [StringLength(4)]
    public string? Werks { get; set; }

    [Column("LGORT")]
    [StringLength(4)]
    public string? Lgort { get; set; }

    [Column("LGOBE")]
    [StringLength(16)]
    public string? Lgobe { get; set; }

    [Column("ERDAT", TypeName = "date")]
    public DateTime? Erdat { get; set; }

    [Column("ETIME")]
    public TimeSpan? Etime { get; set; }

    [Column("LGORT_10")]
    [StringLength(4)]
    public string? Lgort10 { get; set; }

    [Column("LGOBE_10")]
    [StringLength(16)]
    public string? Lgobe10 { get; set; }

    [Column("MATNR")]
    [StringLength(18)]
    public string? Matnr { get; set; }

    [Column("MAKTX")]
    [StringLength(40)]
    public string? Maktx { get; set; }

    [Column("NAME_SH")]
    [StringLength(35)]
    public string? NameSh { get; set; }

    [Column("KOD_R_10")]
    [StringLength(4)]
    public string? KodR10 { get; set; }

    [Column("note")]
    [StringLength(250)]
    public string? Note { get; set; }

    [Column("term", TypeName = "datetime")]
    public DateTime? Term { get; set; }

    [Column("attempt")]
    public int? Attempt { get; set; }

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

    [Column("close", TypeName = "datetime")]
    public DateTime? Close { get; set; }

    [Column("close_user")]
    [StringLength(50)]
    public string? CloseUser { get; set; }

    [ForeignKey("IdArrivalCar")]
    [InverseProperty("SapincomingSupplies")]
    public virtual ArrivalCar IdArrivalCarNavigation { get; set; } = null!;

    [InverseProperty("IdSapIncomingSupplyNavigation")]
    public virtual ICollection<WagonInternalRoute> WagonInternalRoutes { get; } = new List<WagonInternalRoute>();
}
