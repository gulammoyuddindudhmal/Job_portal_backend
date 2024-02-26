using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class PreferedRole
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int Role { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtUpdated { get; set; }

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
