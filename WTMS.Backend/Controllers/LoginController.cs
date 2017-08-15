using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WTMS.DataAccess.ViewModel;
using WTMS.Service;

namespace WTMS.Backend.Controllers
{
    public class LoginController : Controller
    {
        private readonly SystemUserService _systemUserService;
        public LoginController()
        {
            _systemUserService = new SystemUserService();
        }

        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            var systemUserModel = _systemUserService.Login(loginModel);
            if (systemUserModel == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginModel);
            }

            FormsAuthentication.SetAuthCookie(systemUserModel.UserName, false);

            var authTicket = new FormsAuthenticationTicket(1, systemUserModel.Name, DateTime.Now,
                DateTime.Now.AddMinutes(60), false, systemUserModel.WtmsRole);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Response.Cookies.Add(authCookie);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}