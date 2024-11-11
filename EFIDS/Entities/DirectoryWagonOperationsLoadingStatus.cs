using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("Directory_WagonOperationsLoadingStatus", Schema = "IDS")]
public partial class DirectoryWagonOperationsLoadingStatus
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_wagon_operations")]
    public int IdWagonOperations { get; set; }

    [Column("id_wagon_loading_status")]
    public int IdWagonLoadingStatus { get; set; }

    [ForeignKey("IdWagonLoadingStatus")]
    [InverseProperty("DirectoryWagonOperationsLoadingStatuses")]
    public virtual DirectoryWagonLoadingStatus IdWagonLoadingStatusNavigation { get; set; } = null!;

    [ForeignKey("IdWagonOperations")]
    [InverseProperty("DirectoryWagonOperationsLoadingStatuses")]
    public virtual DirectoryWagonOperation IdWagonOperationsNavigation { get; set; } = null!;
}
