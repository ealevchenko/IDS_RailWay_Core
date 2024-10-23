using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Models;

[Keyless]
public partial class Пятница
{
    [Column("id")]
    public int Id { get; set; }

    [Column("id_car")]
    public int IdCar { get; set; }

    [Column("id_car_conditions")]
    public int? IdCarConditions { get; set; }

    [Column("id_car_status")]
    public int? IdCarStatus { get; set; }

    [Column("id_station")]
    public int? IdStation { get; set; }

    [Column("dt_inp_station", TypeName = "datetime")]
    public DateTime? DtInpStation { get; set; }

    [Column("dt_out_station", TypeName = "datetime")]
    public DateTime? DtOutStation { get; set; }

    [Column("id_way")]
    public int? IdWay { get; set; }

    [Column("dt_inp_way", TypeName = "datetime")]
    public DateTime? DtInpWay { get; set; }

    [Column("dt_out_way", TypeName = "datetime")]
    public DateTime? DtOutWay { get; set; }

    [Column("position")]
    public int? Position { get; set; }

    [Column("send_id_station")]
    public int? SendIdStation { get; set; }

    [Column("send_id_overturning")]
    public int? SendIdOverturning { get; set; }

    [Column("send_id_shop")]
    public int? SendIdShop { get; set; }

    [Column("send_dt_inp_way", TypeName = "datetime")]
    public DateTime? SendDtInpWay { get; set; }

    [Column("send_dt_out_way", TypeName = "datetime")]
    public DateTime? SendDtOutWay { get; set; }

    [Column("send_id_position")]
    public int? SendIdPosition { get; set; }

    [Column("send_train1")]
    public int? SendTrain1 { get; set; }

    [Column("send_train2")]
    public int? SendTrain2 { get; set; }

    [Column("send_side")]
    public int? SendSide { get; set; }

    [Column("edit_user")]
    [StringLength(50)]
    public string? EditUser { get; set; }

    [Column("edit_dt", TypeName = "datetime")]
    public DateTime? EditDt { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }
}
