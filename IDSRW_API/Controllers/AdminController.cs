using EF_IDS.Concrete;
using EF_IDS.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDSRW_API.Controllers
{
    public class UserInfo
    {
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }
        public string connectionString { get; set; }
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
                UserInfo result = new UserInfo()
                {
                    Name = HttpContext.User.Identity.Name,
                    IsAuthenticated = HttpContext.User.Identity.IsAuthenticated,
                    connectionString = this.db.Database.GetConnectionString()
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
