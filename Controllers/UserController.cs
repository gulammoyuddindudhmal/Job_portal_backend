using job_portal.Dto;
using job_portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace job_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        JobPortalContext _context;
        public UserController(JobPortalContext jb)
        {
            _context = jb;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDto us)
        {
            var user = new User
            {
                Email = us.Email,
                FirstName = us.FirstName,
                LastName = us.LastName,
                Password = "pass",
                PhoneNumber = us.Phone,
                ProfilePhoto = us.Photo,
                PortfolioUrl = us.Portfolio,
                Resume = us.Resume,
                Referrers = us.Referrer,
                SendUpdates = Convert.ToSByte(us.SendUpdates),
                Percentage = us.Percentage,
                YearOfPassing = us.YearOfPassing,
                Qualification = us.Qualification,
                Stream = us.Stream,
                CollegeName = us.College,
                CollegeLocation = us.College_loc,
                IsExperienced = Convert.ToSByte(us.IsExperienced),
                Yoe = us.YearOfExperience,
                CurrentCtc = us.CurrentCTC,
                ExpectedCtc = us.ExpectedCTC,
                IsOnNotice = Convert.ToSByte(us.OnNotice),
                EndOfNotice = us.OnNotice ? DateTime.Parse(us.EndOfNotice) : null,
                PeriodOfNotice = us.PeriodOfNotice,
                HaveAppearedBefore = Convert.ToSByte(us.HaveAppeared),
                RoleAppearedBefore = us.RoleAppered
            };
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                var u =await _context.Users.Where(t => t.Email == us.Email).FirstOrDefaultAsync();
                foreach (var s in us.Familiar)
                {
                    u.Skills.Add(new Skill
                    {
                        UserId = u.Id,
                        SkillType = "familiar",
                        SkillName = s
                    });
                }
                if (us.IsExperienced)
                {
                    foreach (var s in us.Expertise)
                    {
                        u.Skills.Add(new Skill
                        {
                            SkillName = s,
                            SkillType = "expert",
                            UserId = u.Id,
                        });
                    }
                }
                foreach (var i in us.Role_id)
                {
                    if (!await _context.Roles.AnyAsync(t => t.Id == i))
                    {
                        return BadRequest();
                    }
                    var r =await _context.Roles.Where(t => t.Id == i).FirstOrDefaultAsync();
                    PreferedRole pf = new PreferedRole()
                    {
                        UserId = u.Id,
                        Role = i,
                        RoleNavigation = r,
                        User = u
                    };
                    u.PreferedRoles.Add(pf);
                }
                await _context.SaveChangesAsync();
                return Created();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal server error");

            }
        }
        [HttpPut,Authorize]
        public async Task<ActionResult> put([FromBody]string resume)
        {
            int user = int.Parse(HttpContext.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier).Value);
            var u =await _context.Users.FindAsync(user);

            u.Resume= resume;
            try
            {
               await _context.SaveChangesAsync();
                return Created();
            }catch (Exception e)
            {
                Debug.WriteLine(e);
                return StatusCode(500);
            }
        }
    }
}
