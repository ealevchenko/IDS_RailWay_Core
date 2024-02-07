using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_WagonsRent", Schema = "IDS")]
[Index("RentEnd", Name = "NCI_num")]
[Index("Num", "RentEnd", Name = "num_rent_end")]
public partial class DirectoryWagonsRent
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("num")]
    public int Num { get; set; }

    [Column("id_operator")]
    public int? IdOperator { get; set; }

    [Column("id_limiting")]
    public int? IdLimiting { get; set; }

    [Column("rent_start", TypeName = "datetime")]
    public DateTime? RentStart { get; set; }

    [Column("rent_end", TypeName = "datetime")]
    public DateTime? RentEnd { get; set; }

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

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [InverseProperty("IdWagonsRentArrivalNavigation")]
    public virtual ICollection<ArrivalUzVagon> ArrivalUzVagons { get; } = new List<ArrivalUzVagon>();

    [ForeignKey("IdLimiting")]
    [InverseProperty("DirectoryWagonsRents")]
    public virtual DirectoryLimitingLoading? IdLimitingNavigation { get; set; }

    [ForeignKey("IdOperator")]
    [InverseProperty("DirectoryWagonsRents")]
    public virtual DirectoryOperatorsWagon? IdOperatorNavigation { get; set; }

    [InverseProperty("Parent")]
    public virtual ICollection<DirectoryWagonsRent> InverseParent { get; } = new List<DirectoryWagonsRent>();

    [ForeignKey("Num")]
    [InverseProperty("DirectoryWagonsRents")]
    public virtual DirectoryWagon NumNavigation { get; set; } = null!;

    [InverseProperty("IdWagonsRentArrivalNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagonIdWagonsRentArrivalNavigations { get; } = new List<OutgoingUzVagon>();

    [InverseProperty("IdWagonsRentOutgoingNavigation")]
    public virtual ICollection<OutgoingUzVagon> OutgoingUzVagonIdWagonsRentOutgoingNavigations { get; } = new List<OutgoingUzVagon>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual DirectoryWagonsRent? Parent { get; set; }
}
