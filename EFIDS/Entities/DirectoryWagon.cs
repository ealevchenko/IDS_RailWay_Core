using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_Wagons", Schema = "IDS")]
public partial class DirectoryWagon
{
    [Key]
    [Column("num")]
    public int Num { get; set; }

    [Column("id_countrys")]
    public int IdCountrys { get; set; }

    [Column("id_genus")]
    public int IdGenus { get; set; }

    [Column("id_owner")]
    public int IdOwner { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("change_operator", TypeName = "datetime")]
    public DateTime? ChangeOperator { get; set; }

    [Column("gruzp")]
    public double Gruzp { get; set; }

    [Column("tara")]
    public double? Tara { get; set; }

    [Column("kol_os")]
    public int KolOs { get; set; }

    [Column("usl_tip")]
    [StringLength(10)]
    public string? UslTip { get; set; }

    [Column("date_rem_uz", TypeName = "datetime")]
    public DateTime? DateRemUz { get; set; }

    [Column("date_rem_vag", TypeName = "datetime")]
    public DateTime? DateRemVag { get; set; }

    [Column("id_type_ownership")]
    public int? IdTypeOwnership { get; set; }

    [Column("sign")]
    public int? Sign { get; set; }

    [Column("factory_number")]
    [StringLength(10)]
    public string? FactoryNumber { get; set; }

    [Column("inventory_number")]
    [StringLength(10)]
    public string? InventoryNumber { get; set; }

    [Column("year_built")]
    public int? YearBuilt { get; set; }

    [Column("exit_ban")]
    public bool? ExitBan { get; set; }

    [Column("note")]
    [StringLength(1000)]
    public string Note { get; set; } = null!;

    [Column("sobstv_kis")]
    public int? SobstvKis { get; set; }

    [Column("bit_warning")]
    public bool? BitWarning { get; set; }

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

    [Column("closed_route")]
    public bool? ClosedRoute { get; set; }

    [Column("new_construction")]
    [StringLength(200)]
    public string? NewConstruction { get; set; }

    [InverseProperty("NumNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [InverseProperty("NumNavigation")]
    public virtual ICollection<DirectoryWagonsRent> DirectoryWagonsRents { get; } = new List<DirectoryWagonsRent>();

    [ForeignKey("IdCountrys")]
    [InverseProperty("DirectoryWagons")]
    public virtual DirectoryCountry IdCountrysNavigation { get; set; } = null!;

    [ForeignKey("IdGenus")]
    [InverseProperty("DirectoryWagons")]
    public virtual DirectoryGenusWagon IdGenusNavigation { get; set; } = null!;

    [ForeignKey("IdOperator")]
    [InverseProperty("DirectoryWagons")]
    public virtual DirectoryOperatorsWagon? IdOperatorNavigation { get; set; }

    [ForeignKey("IdOwner")]
    [InverseProperty("DirectoryWagons")]
    public virtual DirectoryOwnersWagon IdOwnerNavigation { get; set; } = null!;

    [ForeignKey("IdTypeOwnership")]
    [InverseProperty("DirectoryWagons")]
    public virtual DirectoryTypeOwnerShip? IdTypeOwnershipNavigation { get; set; }

    [InverseProperty("NumNavigation")]
    public virtual ICollection<WagonInternalRoute> WagonInternalRoutes { get; } = new List<WagonInternalRoute>();
}
