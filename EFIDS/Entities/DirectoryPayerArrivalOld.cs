﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Keyless]
[Table("Directory_PayerArrival_old", Schema = "IDS")]
public partial class DirectoryPayerArrivalOld
{
    [Column("code")]
    public int Code { get; set; }

    [Column("payer_name_ru")]
    [StringLength(100)]
    public string PayerNameRu { get; set; } = null!;

    [Column("payer_name_en")]
    [StringLength(100)]
    public string PayerNameEn { get; set; } = null!;

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
}
