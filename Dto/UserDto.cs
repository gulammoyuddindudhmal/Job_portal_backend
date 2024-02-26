namespace job_portal.Dto
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string? Portfolio { get; set; }
        public string? Resume { get; set; }
        public string? Photo { get; set; }
        public string? Referrer { get; set; }
        public Boolean SendUpdates { get; set; }
        public int Percentage { get; set; }
        public int YearOfPassing { get; set; }
        public string Qualification { get; set; }
        public string Stream { get; set; }
        public string College { get; set; }
        public string College_loc { get; set; }
        public Boolean IsExperienced { get; set; }
        public int? YearOfExperience { get; set; }
        public string? CurrentCTC { get; set; }
        public string? ExpectedCTC { get; set; }
        public Boolean OnNotice { get; set; }
        public string? EndOfNotice { get; set; }
        public int? PeriodOfNotice { get; set; }
        public Boolean HaveAppeared { get; set; }
        public string? RoleAppered { get; set; }
        public List<string> Familiar { get; set; }
        public List<string>? Expertise { get; set; }
        public List<int> Role_id { get; set; }
    
}
}
