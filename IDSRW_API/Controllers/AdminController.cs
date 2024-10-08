using EF_IDS.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IDSRW_API.Controllers
{
    public class UserInfo
    {
        public string Name { get; set; }
        public bool IsAuthenticated { get; set; }
        public string TypeServer { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        public AdminController() { }

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
                    TypeServer = Environment.MachineName == "krr-app-paweb01" ? "IDS" : "Test"
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
