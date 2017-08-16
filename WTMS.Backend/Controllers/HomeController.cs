using System.Linq;
using System.Web.Mvc;
using WTMS.Backend.App_Start;
using WTMS.Common;
using WTMS.DataAccess.ViewModel;
using WTMS.Service;

namespace WTMS.Backend.Controllers
{
    [CustomAuthorize(Roles = WTMSRoles.ADMIN)]
    public class HomeController : Controller
    {
        private readonly SystemUserService _systemUserService;
        private readonly ClientUserService _clientUserService;
        private readonly ReferenceDataService _referenceDataService;

        public HomeController ()
        {
            _systemUserService = new SystemUserService();
            _referenceDataService = new ReferenceDataService();
            _clientUserService = new ClientUserService();
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

            var parentList = _clientUserService.GetParentList().ToList();
            var statusList = _referenceDataService.GetAllUserStatus();

            return View(new ParentListViewModel
            {
                Parents = parentList,
                UserStatus = statusList
            });
        }
    }
}