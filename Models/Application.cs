using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class Application
{
    public int ApplicationId { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public int UserId { get; set; }

    public int WalkinJobId { get; set; }

    public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; } = new List<ApplicationRole>();

    public virtual User User { get; set; } = null!;

    public virtual WalkinJobTimeslot WalkinJob { get; set; } = null!;
}
