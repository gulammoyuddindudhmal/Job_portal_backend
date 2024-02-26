using job_portal.Dto;
using job_portal.Models;
using job_portal.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace job_portal.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WalkinController : ControllerBase
    {
        private readonly JobPortalContext _context;
        WalkinMaker wk;
        public WalkinController(JobPortalContext jb)
        {
            _context = jb;
            wk = new WalkinMaker(); 
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<WalkinJobDto> walkinJobDtos = new List<WalkinJobDto>();
            var query = _context.WalkinJobs.Include(t => t.LocationNavigation).Include(t => t.WalkinJobRoles).ThenInclude(t => t.RoleNavigation);
            var walkins =await query.ToListAsync();
            foreach (var item in walkins)
            {
                List<RoleDto> rls = new List<RoleDto>();
                foreach (var item1 in item.WalkinJobRoles)
                {
                    rls.Add(new RoleDto()
                    {
                        id = item1.RoleNavigation.Id,
                        title = item1.RoleNavigation.Title,
                        type = item1.RoleNavigation.Type,
                        img = item1.RoleNavigation.ImageUrl
                    });
                }
                walkinJobDtos.Add(new WalkinJobDto()
                {
                    Id = item.Id,
                    Extra_info = item.ExtraInfo,
                    City = item.LocationNavigation.City,
                    Roles = rls,
                    Title = wk.GetTitle(rls),
                    Date = wk.GetDates(item.StartDate, item.EndDate)
                });
            }


            return  Ok(walkinJobDtos);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var walkin =await _context.WalkinJobs.Where(t => t.Id == id)
                .Include(t => t.LocationNavigation)
                .Include(t => t.WalkinJobTimeslots.OrderBy(t => t.StartTime))
                .Include(t => t.WalkinJobRoles)
                .ThenInclude(t => t.RoleNavigation).FirstOrDefaultAsync();
            List<RoleDetailDto> roles = new List<RoleDetailDto>();
            List<TimeslotDto> times = new List<TimeslotDto>();
            foreach (var t in walkin.WalkinJobTimeslots)
            {
                times.Add(new TimeslotDto()
                {
                    id = t.Id,
                    time = t.StartTime.ToString("hh:mm tt") + " to " + t.EndTime.ToString("hh:mm tt")
                });
            }
            foreach (var t in walkin.WalkinJobRoles)
            {
                roles.Add(new RoleDetailDto()
                {
                    id = t.Id,
                    title = t.RoleNavigation.Title,
                    type = t.RoleNavigation.Type,
                    img = t.RoleNavigation.ImageUrl,
                    description = t.RoleNavigation.Descr,
                    requirements = t.RoleNavigation.Req,
                    package = t.RoleNavigation.Package
                });
            }
            WalkinJobDetailsDto wj = new WalkinJobDetailsDto()
            {
                Id = walkin.Id,
                Title = wk.GetTitle(roles),
                Date = wk.GetDates(walkin.StartDate, walkin.EndDate),
                City = walkin.LocationNavigation.City,
                Extra_info = walkin.ExtraInfo,
                genIns = walkin.GenIns,
                examIns = walkin.ExamIns,
                sysReq = walkin.SysReq,
                process = walkin.Process,
                Roles = roles,
                timeslots = times
            };

            return Ok(wj);
        }
    }
}
