using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class Location
{
    public int Id { get; set; }

    public string VenueName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string PinCode { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public DateTime? DtCreated { get; set; }

    public DateTime? DtModified { get; set; }

    public virtual ICollection<WalkinJob> WalkinJobs { get; set; } = new List<WalkinJob>();
}
