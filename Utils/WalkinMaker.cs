using job_portal.Dto;
using job_portal.Models;
using System.Globalization;

namespace job_portal.Utils
{
    public class WalkinMaker
    {
        public string GetDates(DateTime sd, DateTime ed)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            string sM = dtfi.GetMonthName(sd.Month);
            string eM = dtfi.GetMonthName(ed.Month);
            return sd.Day.ToString() + "-" + sM + "-" + sd.Year.ToString() + " to " + ed.Day.ToString() + "-" + eM + "-" + ed.Year.ToString();
        }
        public string GetTitle(List<RoleDto> a)
        {
            if (a.Count == 0)
            {
                return "";
            }
            string res = "Walk In for ";
            if (a.Count == 1)
            {
                res = res + a[0].type + " Job Role";
            }
            else if (a.Count == 2)
            {
                res = res + a[0].type + " and " + a[1].type + " Job Role";
            }
            else
            {
                res = "Walk In for Multiple Job Roles";
            }
            return res;
        }
        public string GetTitle(List<RoleDetailDto> a)
        {
            if (a.Count == 0)
            {
                return "";
            }
            string res = "Walk In for ";
            if (a.Count == 1)
            {
                res = res + a[0].type + " Job Role";
            }
            else if (a.Count == 2)
            {
                res = res + a[0].type + " and " + a[1].type + " Job Role";
            }
            else
            {
                res = "Walk In for Multiple Job Roles";
            }
            return res;
        }
    }
}
