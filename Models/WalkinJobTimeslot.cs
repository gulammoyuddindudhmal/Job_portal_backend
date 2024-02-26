using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class WalkinJobTimeslot
{
    public int Id { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int WalkinJobId { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual WalkinJob WalkinJob { get; set; } = null!;
}
