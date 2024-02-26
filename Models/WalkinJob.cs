using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class WalkinJob
{
    public int Id { get; set; }

    public int Location { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public string? ExtraInfo { get; set; }

    public string? GenIns { get; set; }

    public string? ExamIns { get; set; }

    public string? SysReq { get; set; }

    public string? Process { get; set; }

    public virtual Location LocationNavigation { get; set; } = null!;

    public virtual ICollection<WalkinJobRole> WalkinJobRoles { get; set; } = new List<WalkinJobRole>();

    public virtual ICollection<WalkinJobTimeslot> WalkinJobTimeslots { get; set; } = new List<WalkinJobTimeslot>();
}
