using System.Web.Mvc;
using WebRequestRateLimit.ActionFilter;

namespace WebRequestRateLimit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [RateLimitActionFilter]
        public ActionResult RateLimit()
        {
            return View();
        }
    }
}