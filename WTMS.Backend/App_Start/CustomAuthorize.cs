using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WTMS.Backend.App_Start
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                //if not logged, it will work as normal Authorize and redirect to the Login
                // base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "Index" }));
            }
            else
            {
                //logged and wihout the role to access it - redirect to the custom controller action
                // base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}