using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityAgency.Filter
{
    public class ValidateSession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.Session.Contents["result"] == null || System.Web.HttpContext.Current==null)
            {
                filterContext
                    .HttpContext
                    .Response
                    .RedirectToRoute(new { controller = "Account", action = "Login" });
            }

            base.OnActionExecuting(filterContext);
        }
    }
}