using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EF_IDS.Entities;

[Table("GIVC_requests", Schema = "IDS")]
public partial class GivcRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("dt_requests", TypeName = "datetime")]
    public DateTime DtRequests { get; set; }

    [Column("type_requests")]
    [StringLength(20)]
    public string TypeRequests { get; set; } = null!;

    [Column("result_requests")]
    public string? ResultRequests { get; set; }

    [Column("count_line")]
    public int? CountLine { get; set; }

    [Column("create", TypeName = "datetime")]
    public DateTime Create { get; set; }

    [Column("create_user")]
    [StringLength(50)]
    public string CreateUser { get; set; } = null!;
}
