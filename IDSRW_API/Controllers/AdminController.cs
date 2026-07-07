using EF_IDS.Concrete;
using EF_IDS.Functions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace IDSRW_API.Controllers
{
    public class UserInfo
    {
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }
        public string DataSource { get; set; }
        public string DataBase { get; set; }
        public string NameServer { get; set; }
    }
    public class RoleInfo
    {
        public string Role { get; set; }
        public bool IsRole { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    //[Authorize(Policy = "ADMIN")]
    public class AdminController : ControllerBase
    {
        private EFDbContext db;
        public AdminController(EFDbContext db)
        {
            this.db = db;
        }

        // GET: Admin/user_info
        [HttpGet("user_info")]
        public async Task<ActionResult<UserInfo>> GetUserInfo()
        {
            try
            {
                // Определим сервер и базу данных
                string ds = ""; string db = "";
                string cs = this.db.Database.GetConnectionString();
                if (!String.IsNullOrWhiteSpace(cs))
                {
                    string[] info = cs.Split(';');
                    if (info != null && info.Count() > 0)
                    {
                        string substring = "Data Source=";
                        string res = info.ToList().Find(o => o.IndexOf(substring) >= 0);
                        ds = !String.IsNullOrWhiteSpace(res) ? res.Remove(res.IndexOf(substring), res.IndexOf(substring) + substring.Length) : "";
                        substring = "Initial Catalog=";
                        res = info.ToList().Find(o => o.IndexOf(substring) >= 0);
                        db = !String.IsNullOrWhiteSpace(res) ? res.Remove(res.IndexOf(substring), res.IndexOf(substring) + substring.Length) : "";
                    }
                }

                //bool isDevelopers = HttpContext.User.IsInRole(@"EUROPE\KRR-LG-PA-RailWay_Developers"); //EUROPE\\KRR-LG-PA-Developers_DB

                UserInfo result = new UserInfo()
                {
                    Name = HttpContext.User.Identity.Name,
                    IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
                    DataSource = ds,
                    DataBase = db,
                    NameServer = Environment.MachineName
                };
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: Admin/is_role/EUROPE\KRR-LG-PA-RailWay_Developers
        [HttpGet("is_role/{role}")]
        public async Task<ActionResult> IsRole(string role)
        {
            try
            {
                bool isRole = false;
                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                string[] parts = HttpContext.User.Identity.Name.Split('\\');
                string domain = parts.Length > 0 ? parts[0] : string.Empty;
                if (IsAuthenticated)
                {
                    if (!String.IsNullOrWhiteSpace(domain))
                    {
                        isRole = HttpContext.User.IsInRole(domain + "\\" + role);
                    }
                    else
                    {
                        isRole = HttpContext.User.IsInRole(role);
                    }

                }
                return new ObjectResult(isRole);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // Admin/get_is_roles/
        [HttpGet("get_is_roles")]
        public async Task<ActionResult> GetIsRoles()
        {
            try
            {
                List<RoleInfo> result = new List<RoleInfo>();

                string[] roles = new string[] {
                    "KRR-LG_TD-IDSRW_ACCEPT",
"KRR-LG_TD-IDSRW_SEND",
"KRR-LG_TD-IDSRW_TRF_ACCEPT",
"KRR-LG_TD-IDSRW_TRF_SEND",
"KRR-LG_TD-IDSRW_DOK_ACCEPT",
"KRR-LG_TD-IDSRW_DOK_SEND",
"KRR-LG_TD-IDSRO_DOK",
"KRR-LG_TD-IDSRO_LET_WORK",
"KRR-LG_TD-IDSRW_ADMIN",
"KRR-LG_TD-IDSRW_PAY",
"KRR-LG_TD-IDSRW_LETTERS",
"KRR-LG_TD-IDSRW_DIRECTORY",
"KRR-LG_TD-IDSRW_ADDRESS",
"KRR-LG_TD-IDSRW_COM_STAT",
"KRR-LG_TD-IDSRW_COND_ARR",
"KRR-LG_TD-IDSRW_COND_SEND",
"KRR-LG_TD-IDSRW_ARM_TROP",
"KRR-LG_TD-IDSRW_ARM_ OPERATIONS",
"KRR-LG_TD-IDSRW_ARM_OR",
"KRR-LG_TD-IDSRW_ARM_ NOTE",
"KRR-LG_TD-IDSRO_ARM",
"KRR-LG_TD-IDSRW_TIME",
"KRR-LG_TD-IDSRW_CORREECT",
"KRR-LG_TD-IDSRO_ACCEPT",
"KRR-LG_TD-IDSRO_SEND",
"KRR-LG_TD-IDSRO_HISTORI",
"KRR-TD-IDSRW_PARK",
"KRR-LG_TD-IDSRO_ REPORT",
"KRR-LG_TD-IDSRO_ REPORT_ACCEPT",
"KRR-LG_TD-IDSRO_ REPORT_SEND",
"KRR-LG_TD-IDSRO_ REPORT_PLATA",
"KRR-LG_TD-IDSRO_ REPORT_REMAINDER",
"KRR-LG_TD-IDSRO_ REPORT_CROSSING"};

                bool IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
                string[] parts = HttpContext.User.Identity.Name.Split('\\');
                string domain = parts.Length > 0 ? parts[0] : string.Empty;
                if (IsAuthenticated)
                {

                    foreach (string role in roles)
                    {
                        bool isRole = false;
                        if (!String.IsNullOrWhiteSpace(domain))
                        {
                            isRole = HttpContext.User.IsInRole(domain + "\\" + role);
                        }
                        else
                        {
                            isRole = HttpContext.User.IsInRole(role);
                        }
                        result.Add(new RoleInfo { Role = role, IsRole = isRole });
                    }
                }
                return new ObjectResult(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
