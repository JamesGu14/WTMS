using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTMS.Backend.App_Start;
using WTMS.Common;

namespace WTMS.Backend.Controllers
{
    [CustomAuthorize(Roles = WTMSRoles.ADMIN)]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.UserName = User.Identity.Name;
            return View();
        }
    }
}