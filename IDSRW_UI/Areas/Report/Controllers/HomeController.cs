using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;

namespace IDSRW_UI.Areas.Report.Controllers
{
    [Area("Report")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        // конструктор вводит зарегистрированный репозиторий
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _logger.LogDebug(1, "Create Report-> HomeController");

        }

        [Route("{area}")]
        [Route("{area}/{controller}")]
        [Route("{area}/{controller}/{action}")]
        public IActionResult Index()
        {
            _logger.LogDebug(1, "View Report-> Home/Index");
            return View();
        }
    }
}
