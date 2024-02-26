using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class ApplicationRole
{
    public int Id { get; set; }

    public int AppId { get; set; }

    public int Role { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public virtual Application App { get; set; } = null!;

    public virtual WalkinJobRole RoleNavigation { get; set; } = null!;
}
