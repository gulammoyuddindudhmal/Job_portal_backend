using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class Role
{
    public DateTime? DtCreated { get; set; }

    public DateTime? DtUpdated { get; set; }

    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Descr { get; set; }

    public string? Req { get; set; }

    public string Package { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public virtual ICollection<PreferedRole> PreferedRoles { get; set; } = new List<PreferedRole>();

    public virtual ICollection<WalkinJobRole> WalkinJobRoles { get; set; } = new List<WalkinJobRole>();
}
