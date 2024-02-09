using Microsoft.AspNetCore.Mvc;

namespace IDSRW_UI.Areas.Report.Controllers
{
    [Area("Report")]
    public class HomeController : Controller
    {
        [Route("{area}")]
        [Route("{area}/{controller}")]
        [Route("{area}/{controller}/{action}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
