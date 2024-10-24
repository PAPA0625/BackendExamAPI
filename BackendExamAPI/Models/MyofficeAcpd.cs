using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace BackendExamAPI.Models;

[Table("Myoffice_ACPD")]
public partial class MyofficeAcpd
{
    [Key]
    [Column("acpd_sid")]
    [StringLength(20)]
    [Unicode(false)]
    [JsonIgnore]
    public string? AcpdSid { get; set; } = null!;

    [Column("acpd_cname")]
    [StringLength(60)]
    public string? AcpdCname { get; set; }

    [Column("acpd_ename")]
    [StringLength(40)]
    public string? AcpdEname { get; set; }

    [Column("acpd_sname")]
    [StringLength(40)]
    public string? AcpdSname { get; set; }

    [Column("acpd_email")]
    [StringLength(60)]
    public string? AcpdEmail { get; set; }

    [Column("acpd_status")]
    public byte? AcpdStatus { get; set; }

    [Column("acpd_stop")]
    public bool? AcpdStop { get; set; }

    [Column("acpd_stopMemo")]
    [StringLength(600)]
    public string? AcpdStopMemo { get; set; }

    [Column("acpd_LoginID")]
    [StringLength(30)]
    public string? AcpdLoginId { get; set; }

    [Column("acpd_LoginPW")]
    [StringLength(60)]
    public string? AcpdLoginPw { get; set; }

    [Column("acpd_memo")]
    [StringLength(120)]
    public string? AcpdMemo { get; set; }

    [Column("acpd_nowdatetime", TypeName = "datetime")]
    public DateTime? AcpdNowdatetime { get; set; }

    [Column("appd_nowid")]
    [StringLength(20)]
    public string? AppdNowid { get; set; }

    [Column("acpd_upddatetitme", TypeName = "datetime")]
    public DateTime? AcpdUpddatetitme { get; set; }

    [Column("acpd_updid")]
    [StringLength(20)]
    public string? AcpdUpdid { get; set; }
}
