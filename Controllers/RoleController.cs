using job_portal.Dto;
using job_portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace job_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        JobPortalContext _context;
        public RoleController(JobPortalContext jb)
        {
            _context = jb;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var roles =await _context.Roles.ToListAsync();
            List<RoleDto> rolesDto = new List<RoleDto>();
            foreach (var role in roles) {
                rolesDto.Add(new RoleDto {
                    id = role.Id,
                    title = role.Title,
                    type = role.Type,
                    img=role.ImageUrl
                });
            }
            return Ok(rolesDto);
        }
    }
}
