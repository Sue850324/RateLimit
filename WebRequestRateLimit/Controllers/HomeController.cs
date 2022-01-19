using PagedList;
using System.Web.Mvc;
using WebRequestRateLimit.ActionFilter;
using WebRequestRateLimit.Models;
using WebRequestRateLimit.Service;

namespace WebRequestRateLimit.Controllers
{
    public class HomeController : Controller
    {
        GetStudentDataService GetDataService = new GetStudentDataService();
        public ActionResult Index()
        {
            return View();
        }

        [RateLimitActionFilter]
        public ActionResult RateLimit()
        {
            return View();
        }

        public ActionResult All(StudentViewModel model, string dataType, int page = 1, int pageSize = 10)
        {
            model.GetList = GetDataService.GetAllStudent().ToPagedList(page, pageSize);

            return View(model);
        }
        public ActionResult Female(StudentViewModel model, string dataType, int page = 1, int pageSize = 10)
        {
            model.GetList = GetDataService.GirlsStudent().ToPagedList(page, pageSize);

            return View(model);
        }
        public ActionResult Male(StudentViewModel model, string dataType, int page = 1, int pageSize = 10)
        {
            model.GetList = GetDataService.BoysStudent().ToPagedList(page, pageSize);

            return View(model);
        }
    }
}