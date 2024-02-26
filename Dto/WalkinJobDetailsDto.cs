namespace job_portal.Dto
{
    public class WalkinJobDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Date { get; set; }

        public string City { get; set; }
        public string? Extra_info { get; set; }

        public List<RoleDetailDto> Roles { get; set; }

        public List<TimeslotDto> timeslots { get; set; }
        public string genIns { get; set; }
        public string examIns { get; set; }
        public string sysReq { get; set; }
        public string process { get; set; }
    }
}
