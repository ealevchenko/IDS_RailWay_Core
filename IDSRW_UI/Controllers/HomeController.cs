using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCore_UI.Models;


namespace WebCore_UI.Controllers
{
    //[Route("")]
    //[Route("{controller}")]
    //[Route("{controller}/{action}")]
    //[Route("{controller}/{action}/{id?}")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        [Route("Home/Index/{id?}")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Home/Privacy")]
        [Route("Home/Privacy/{id?}")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}