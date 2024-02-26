using job_portal.Models;

namespace job_portal.Dto
{
    public class WalkinJobDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Date { get; set; }

        public string City { get; set; }
        public string? Extra_info { get; set; }

        public List<RoleDto> Roles { get; set; }
    }
}
