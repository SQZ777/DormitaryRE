using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace Dormitary_Re.Filter
{
    public class FilterPermission : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session["account"] ==null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                {
                    {"controller","Home" },
                    {"action","LoginIndex"}
                });
            }
            base.OnActionExecuting(filterContext);
        }
    }
}