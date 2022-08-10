using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class BroadCastController : Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [Route("happy-five"), HttpGet]
        public ActionResult HappyFive()
        {
            return View();
        }

        [Route("number-game"), HttpGet]
        public ActionResult NumberGame()
        {
            return View();
        }
    }
}