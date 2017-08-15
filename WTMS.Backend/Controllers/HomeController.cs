using System.Web.Mvc;
using WTMS.Backend.App_Start;
using WTMS.Common;
using WTMS.Service;

namespace WTMS.Backend.Controllers
{
    [CustomAuthorize(Roles = WTMSRoles.ADMIN)]
    public class HomeController : Controller
    {
        private readonly SystemUserService _systemUserService;

        public HomeController ()
        {
            _systemUserService = new SystemUserService();
        }

        public ActionResult Index()
        {
            ViewBag.CurrentPage = "HOME";
            ViewBag.UserName = User.Identity.Name;
            return View();
        }

        public ActionResult TrialUser()
        {
            ViewBag.CurrentPage = "TRIAL";
            ViewBag.UserName = User.Identity.Name;
            var parentList = _systemUserService.GetParentList();
            return View(parentList);
        }
    }
}