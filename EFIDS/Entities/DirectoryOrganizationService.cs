using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_OrganizationService", Schema = "IDS")]
public partial class DirectoryOrganizationService
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("organization_service_ru")]
    [StringLength(50)]
    public string OrganizationServiceRu { get; set; } = null!;

    [Column("organization_service_en")]
    [StringLength(50)]
    public string OrganizationServiceEn { get; set; } = null!;

    [Column("code_sap")]
    [StringLength(20)]
    public string? CodeSap { get; set; }

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

    [Column("delete", TypeName = "datetime")]
    public DateTime? Delete { get; set; }

    [Column("delete_user")]
    [StringLength(50)]
    public string? DeleteUser { get; set; }

    [InverseProperty("IdOrganizationServiceNavigation")]
    public virtual ICollection<WagonInternalOperation> WagonInternalOperations { get; set; } = new List<WagonInternalOperation>();
}
