using job_portal.auth;
using job_portal.Dto;
using job_portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace job_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private JobPortalContext _context;
        private IConfiguration _config;
        private TokenMaker tk;
        public LoginController(JobPortalContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            tk = new TokenMaker(_config);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginDto login)
        {
            if(!await _context.Users.AnyAsync(t=>t.Email==login.email&&t.Password == login.password))
            {
                return BadRequest();
            }
            var user =await _context.Users.Where(t => t.Email == login.email&& t.Password == login.password).FirstOrDefaultAsync();
            string token = tk.MakeJwtToken(user.Id, user.Email);
            return Ok(token);
        }
    }
}
