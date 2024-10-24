using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackendExamAPI.Models;

[Keyless]
[Table("Myoffice_ExcuteionLog")]
[Index("DeLogAuthId", Name = "IX_Myoffice_ExcuteionLog", IsUnique = true)]
public partial class MyofficeExcuteionLog
{
    [Column("DeLog_AuthID")]
    public int DeLogAuthId { get; set; }

    [Column("DeLog_StoredPrograms")]
    [StringLength(120)]
    public string? DeLogStoredPrograms { get; set; }

    [Column("DeLog_GroupID")]
    public Guid? DeLogGroupId { get; set; }

    [Column("DeLog_isCustomDebug")]
    public bool? DeLogIsCustomDebug { get; set; }

    [Column("DeLog_ExecutionProgram")]
    [StringLength(120)]
    public string? DeLogExecutionProgram { get; set; }

    [Column("DeLog_ExecuteionInfo")]
    public string? DeLogExecuteionInfo { get; set; }

    [Column("DeLog_VerifyNeeded")]
    public bool? DeLogVerifyNeeded { get; set; }

    [Column("exelog_nowdatetime", TypeName = "datetime")]
    public DateTime? ExelogNowdatetime { get; set; }
}
