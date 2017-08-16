using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WTMS.Backend.App_Start;
using WTMS.Common;
using WTMS.DataAccess.ViewModel;
using WTMS.Service;

namespace WTMS.Backend.Controllers
{
    [CustomAuthorize(Roles = WTMSRoles.ADMIN)]
    public class ClientUserController : Controller
    {
        private readonly ClientUserService _clientUserService;

        public ClientUserController()
        {
            _clientUserService = new ClientUserService();
        }

        [HttpGet]
        public JsonResult Children(int parentId)
        {
            var parentViewModel = new ParentChildrenViewModel
            {
                Parent = _clientUserService.GetParentById(parentId),
                Children = _clientUserService.GetChildrenByParent(parentId)
            };
            if (parentViewModel.Parent == null || parentViewModel.Children == null || parentViewModel.Children.ToList().Count < 1)
            {
                return Json(null);
            }
            return Json(parentViewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateParentChildren(ParentChildrenViewModel parentChildrenViewModel)
        {
            // validate model
            if (parentChildrenViewModel.Parent == null)
            {
                return Json(null);
            }

            if (parentChildrenViewModel.Children.ToList().Count == 0)
            {
                return Json(null);
            }

            // Update Parent
            var parentSuccess = _clientUserService.UpdateParent(parentChildrenViewModel.Parent);

            // Update Child
            var childSuccess = _clientUserService.UpdateChildList(parentChildrenViewModel.Children.ToList());

            return Json(parentSuccess && childSuccess);
        }
    }
}