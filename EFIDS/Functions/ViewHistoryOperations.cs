using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EFIDS.Functions;

[Keyless]
public class ViewHistoryOperations
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("id_filing")]
    public long? IdFiling { get; set; }

    [Column("wio_parent_id")]
    public long? WioParentId { get; set; }

    [Column("wio_parent_id_operation")]
    public int? WioParentIdOperation { get; set; }

    [Column("wio_parent_operation_start", TypeName = "datetime")]
    public DateTime? WioParentOperationStart { get; set; }

    [Column("wio_parent_operation_end", TypeName = "datetime")]
    public DateTime? WioParentOperationEnd { get; set; }

    [Column("wio_id")]
    public long? WioId { get; set; }

    [Column("wio_id_operation")]
    public int? WioIdOperation { get; set; }

    [Column("wio_operation_start", TypeName = "datetime")]
    public DateTime? WioOperationStart { get; set; }

    [Column("wio_operation_end", TypeName = "datetime")]
    public DateTime? WioOperationEnd { get; set; }

    [Column("wio_next_id")]
    public long? WioNextId { get; set; }

    [Column("wio_next_id_operation")]
    public int? WioNextIdOperation { get; set; }

    [Column("wio_next_operation_start", TypeName = "datetime")]
    public DateTime? WioNextOperationStart { get; set; }

    [Column("wio_next_operation_end", TypeName = "datetime")]
    public DateTime? WioNextOperationEnd { get; set; }
}
