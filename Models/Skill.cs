using System;
using System.Collections.Generic;

namespace job_portal.Models;

public partial class Skill
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string SkillType { get; set; } = null!;

    public string SkillName { get; set; } = null!;

    public DateTime DtCreated { get; set; }

    public DateTime DtUpdtaed { get; set; }

    public virtual User User { get; set; } = null!;
}
