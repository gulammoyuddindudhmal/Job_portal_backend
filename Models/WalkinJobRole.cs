using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class WalkinJobRole
{
    public int Id { get; set; }

    public int WalkinJob { get; set; }

    public int Role { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; } = new List<ApplicationRole>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual WalkinJob WalkinJobNavigation { get; set; } = null!;
}
