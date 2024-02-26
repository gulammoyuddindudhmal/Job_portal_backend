using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? ProfilePhoto { get; set; }

    public string? Resume { get; set; }

    public string? PortfolioUrl { get; set; }

    public string? Referrers { get; set; }

    public sbyte? SendUpdates { get; set; }

    public int Percentage { get; set; }

    public int YearOfPassing { get; set; }

    public string Qualification { get; set; } = null!;

    public string Stream { get; set; } = null!;

    public string CollegeName { get; set; } = null!;

    public string CollegeLocation { get; set; } = null!;

    public sbyte IsExperienced { get; set; }

    public int? Yoe { get; set; }

    public string? CurrentCtc { get; set; }

    public string? ExpectedCtc { get; set; }

    public sbyte? IsOnNotice { get; set; }

    public DateTime? EndOfNotice { get; set; }

    public int? PeriodOfNotice { get; set; }

    public sbyte? HaveAppearedBefore { get; set; }

    public string? RoleAppearedBefore { get; set; }

    public DateTime DtCreated { get; set; }

    public DateTime DtModified { get; set; }

    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();

    public virtual ICollection<PreferedRole> PreferedRoles { get; set; } = new List<PreferedRole>();

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
