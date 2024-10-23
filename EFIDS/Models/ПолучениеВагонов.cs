using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class ПолучениеВагонов
{
    [Column("position")]
    public int? Position { get; set; }

    [Column("id")]
    public int Id { get; set; }

    [Column("id_sostav")]
    public int IdSostav { get; set; }

    [Column("id_arrival")]
    public int IdArrival { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("dt_uz", TypeName = "datetime")]
    public DateTime? DtUz { get; set; }

    [Column("dt_inp_amkr", TypeName = "datetime")]
    public DateTime? DtInpAmkr { get; set; }

    [Column("dt_out_amkr", TypeName = "datetime")]
    public DateTime? DtOutAmkr { get; set; }

    [Column("natur_kis")]
    public int? NaturKis { get; set; }

    [Column("natur")]
    public int? Natur { get; set; }

    [Column("dt_create", TypeName = "datetime")]
    public DateTime DtCreate { get; set; }

    [Column("user_create")]
    [StringLength(50)]
    public string? UserCreate { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    public int Expr1 { get; set; }

    [Column("group_cars_ru")]
    [StringLength(50)]
    public string GroupCarsRu { get; set; } = null!;

    [Column("group_cars_en")]
    [StringLength(50)]
    public string GroupCarsEn { get; set; } = null!;

    public int Expr2 { get; set; }

    [Column("type_cars_ru")]
    [StringLength(50)]
    public string TypeCarsRu { get; set; } = null!;

    [Column("type_cars_en")]
    [StringLength(50)]
    public string TypeCarsEn { get; set; } = null!;

    [Column("lifting_capacity", TypeName = "numeric(18, 3)")]
    public decimal? LiftingCapacity { get; set; }

    [Column("tare", TypeName = "numeric(18, 3)")]
    public decimal? Tare { get; set; }

    public int Expr3 { get; set; }

    [Column("country_ru")]
    [StringLength(100)]
    public string CountryRu { get; set; } = null!;

    [Column("country_en")]
    [StringLength(100)]
    public string CountryEn { get; set; } = null!;

    [Column("code")]
    public int Code { get; set; }

    [Column("count_axles")]
    public int? CountAxles { get; set; }

    [Column("is_output_uz")]
    public bool? IsOutputUz { get; set; }

    public int Expr4 { get; set; }

    [Column("status_ru")]
    [StringLength(50)]
    public string? StatusRu { get; set; }

    [Column("status_en")]
    [StringLength(50)]
    public string? StatusEn { get; set; }

    [Column("id_status_next")]
    public int? IdStatusNext { get; set; }

    [Column("order")]
    public int? Order { get; set; }

    public int Expr5 { get; set; }

    [Column("name_ru")]
    [StringLength(50)]
    public string NameRu { get; set; } = null!;

    [Column("name_en")]
    [StringLength(50)]
    public string NameEn { get; set; } = null!;
}
