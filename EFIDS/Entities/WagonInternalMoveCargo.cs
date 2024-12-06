using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("WagonInternalMoveCargo", Schema = "IDS")]
public partial class WagonInternalMoveCargo
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_wagon_internal_routes")]
    public long IdWagonInternalRoutes { get; set; }

    [Column("internal_doc_num")]
    [StringLength(20)]
    public string? InternalDocNum { get; set; }

    [Column("id_weighing_num")]
    public int? IdWeighingNum { get; set; }

    [Column("doc_received", TypeName = "datetime")]
    public DateTime? DocReceived { get; set; }

    [Column("id_cargo")]
    public int? IdCargo { get; set; }

    [Column("id_internal_cargo")]
    public int? IdInternalCargo { get; set; }

    [Column("vesg")]
    public int? Vesg { get; set; }

    [Column("id_station_from_amkr")]
    public int? IdStationFromAmkr { get; set; }

    [Column("id_division_from")]
    public int? IdDivisionFrom { get; set; }

    [Column("id_wim_load")]
    public long? IdWimLoad { get; set; }

    [Column("id_wim_redirection")]
    public long? IdWimRedirection { get; set; }

    [Column("code_external_station")]
    public int? CodeExternalStation { get; set; }

    [Column("id_station_on_amkr")]
    public int? IdStationOnAmkr { get; set; }

    [Column("id_division_on")]
    public int? IdDivisionOn { get; set; }

    [Column("id_wim_unload")]
    public long? IdWimUnload { get; set; }

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

    [Column("parent_id")]
    public long? ParentId { get; set; }

    [ForeignKey("CodeExternalStation")]
    [InverseProperty("WagonInternalMoveCargos")]
    public virtual DirectoryExternalStation? CodeExternalStationNavigation { get; set; }

    [ForeignKey("IdCargo")]
    [InverseProperty("WagonInternalMoveCargos")]
    public virtual DirectoryCargo? IdCargoNavigation { get; set; }

    [ForeignKey("IdDivisionFrom")]
    [InverseProperty("WagonInternalMoveCargoIdDivisionFromNavigations")]
    public virtual DirectoryDivision? IdDivisionFromNavigation { get; set; }

    [ForeignKey("IdDivisionOn")]
    [InverseProperty("WagonInternalMoveCargoIdDivisionOnNavigations")]
    public virtual DirectoryDivision? IdDivisionOnNavigation { get; set; }

    [ForeignKey("IdStationFromAmkr")]
    [InverseProperty("WagonInternalMoveCargoIdStationFromAmkrNavigations")]
    public virtual DirectoryStation? IdStationFromAmkrNavigation { get; set; }

    [ForeignKey("IdStationOnAmkr")]
    [InverseProperty("WagonInternalMoveCargoIdStationOnAmkrNavigations")]
    public virtual DirectoryStation? IdStationOnAmkrNavigation { get; set; }

    [ForeignKey("IdWagonInternalRoutes")]
    [InverseProperty("WagonInternalMoveCargos")]
    public virtual WagonInternalRoute IdWagonInternalRoutesNavigation { get; set; } = null!;

    [ForeignKey("IdWimLoad")]
    [InverseProperty("WagonInternalMoveCargoIdWimLoadNavigations")]
    public virtual WagonInternalMovement? IdWimLoadNavigation { get; set; }

    [ForeignKey("IdWimRedirection")]
    [InverseProperty("WagonInternalMoveCargoIdWimRedirectionNavigations")]
    public virtual WagonInternalMovement? IdWimRedirectionNavigation { get; set; }

    [ForeignKey("IdWimUnload")]
    [InverseProperty("WagonInternalMoveCargoIdWimUnloadNavigations")]
    public virtual WagonInternalMovement? IdWimUnloadNavigation { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<WagonInternalMoveCargo> InverseParent { get; set; } = new List<WagonInternalMoveCargo>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual WagonInternalMoveCargo? Parent { get; set; }

    [ForeignKey("Vesg")]
    [InverseProperty("WagonInternalMoveCargos")]
    public virtual DirectoryInternalCargo? VesgNavigation { get; set; }
}
