using EF_IDS.Concrete;
using EF_IDS.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [Route("[controller]")]
    [ApiController]
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
    }
}
