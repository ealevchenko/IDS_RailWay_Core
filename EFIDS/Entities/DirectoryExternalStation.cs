﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_ExternalStation", Schema = "IDS")]
public partial class DirectoryExternalStation
{
    [Key]
    [Column("code")]
    public int Code { get; set; }

    [Column("station_name_ru")]
    [StringLength(50)]
    public string StationNameRu { get; set; } = null!;

    [Column("station_name_en")]
    [StringLength(50)]
    public string StationNameEn { get; set; } = null!;

    [Column("code_inlandrailway")]
    public int CodeInlandrailway { get; set; }

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

    [Column("port")]
    public bool? Port { get; set; }

    [InverseProperty("CodeStnFromNavigation")]
    public virtual ICollection<ArrivalUzDocument> ArrivalUzDocumentCodeStnFromNavigations { get; set; } = new List<ArrivalUzDocument>();

    [InverseProperty("CodeStnToNavigation")]
    public virtual ICollection<ArrivalUzDocument> ArrivalUzDocumentCodeStnToNavigations { get; set; } = new List<ArrivalUzDocument>();

    [ForeignKey("CodeInlandrailway")]
    [InverseProperty("DirectoryExternalStations")]
    public virtual DirectoryInlandRailway CodeInlandrailwayNavigation { get; set; } = null!;

    [InverseProperty("CodeStnFromNavigation")]
    public virtual ICollection<OutgoingUzDocument> OutgoingUzDocumentCodeStnFromNavigations { get; set; } = new List<OutgoingUzDocument>();

    [InverseProperty("CodeStnToNavigation")]
    public virtual ICollection<OutgoingUzDocument> OutgoingUzDocumentCodeStnToNavigations { get; set; } = new List<OutgoingUzDocument>();

    [InverseProperty("CodeStnToNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagons { get; set; } = new List<OutgoingUzVagon>();

    [InverseProperty("CodeStnFromNavigation")]
    public virtual ICollection<UsageFeePeriodDetali> UsageFeePeriodDetaliCodeStnFromNavigations { get; set; } = new List<UsageFeePeriodDetali>();

    [InverseProperty("CodeStnToNavigation")]
    public virtual ICollection<UsageFeePeriodDetali> UsageFeePeriodDetaliCodeStnToNavigations { get; set; } = new List<UsageFeePeriodDetali>();

    [InverseProperty("CodeExternalStationNavigation")]
    public virtual ICollection<WagonInternalMoveCargo> WagonInternalMoveCargos { get; set; } = new List<WagonInternalMoveCargo>();
}
