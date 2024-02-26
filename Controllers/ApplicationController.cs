using job_portal.Dto;
using job_portal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace job_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly JobPortalContext _context;
        public ApplicationController(JobPortalContext jb)
        {
            _context = jb;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ApplicationDto app)
        {
            int user = int.Parse(HttpContext.User.Claims.FirstOrDefault(t => t.Type == ClaimTypes.NameIdentifier).Value);
            Application application = new Application()
            {
                UserId = user,
                WalkinJobId = app.WalkinTimeId,
                User = _context.Users.Where(t => t.Id == user).FirstOrDefault(),
                WalkinJob = _context.WalkinJobTimeslots.Where(t => t.Id == app.WalkinTimeId).FirstOrDefault()
            };
            try
            {
               await _context.Applications.AddAsync(application);
               await _context.SaveChangesAsync();
                var appli = _context.Applications.Where(t => t.UserId == user && t.WalkinJobId == app.WalkinTimeId).FirstOrDefault();
                foreach (var i in app.Roles)
                {

                    if (!await _context.WalkinJobRoles.AnyAsync(t => t.Id == i))
                    {
                        return BadRequest();
                    }
                    var role =await _context.WalkinJobRoles.Where(t => t.Id == i).FirstOrDefaultAsync();

                    ApplicationRole ar = new ApplicationRole()
                    {
                        AppId = appli.ApplicationId,
                        Role = i,
                        App = appli,
                        RoleNavigation = role
                    };
                     role.ApplicationRoles.Add(ar);
                    appli.ApplicationRoles.Add(ar);
                }

                await _context.SaveChangesAsync();
                return Ok(appli.ApplicationId);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationById(int id)
        {
            var application =await _context.Applications
                .Where(t => t.ApplicationId == id)
                .Include(t => t.WalkinJob)
                .ThenInclude(t => t.WalkinJob)
                .ThenInclude(t => t.LocationNavigation)
                .FirstOrDefaultAsync();
            var loc = application.WalkinJob.WalkinJob.LocationNavigation;
            var st = application.WalkinJob.StartTime;
            var et = application.WalkinJob.EndTime;
            AppDetail app = new AppDetail()
            {
                date = st.Day.ToString() + "-" + new DateTimeFormatInfo().GetMonthName(st.Month) + "-" + st.Year.ToString(),
                time = st.ToString("hh:mm tt") + " to " + et.ToString("hh:mm tt"),
                venue = loc.VenueName,
                city = loc.City,
                address = loc.Address,
                pincode = loc.PinCode,
                phone = loc.PhoneNo
            };


            return Ok(app);
        }
    }
}
