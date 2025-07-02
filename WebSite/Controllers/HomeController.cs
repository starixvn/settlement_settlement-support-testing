using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebSite.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Normal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Soccerway()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LeaguesTest()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ExternalResults()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MarketJackTime()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}