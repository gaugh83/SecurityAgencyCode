using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityAgency.Filter
{
    public class UserPermission :ActionFilterAttribute
    {
        public int userid { get; set; }
        public string ModuleName { get; set; }
        public int Permission { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {            
            List<PermissionViewModel> permissionList = filterContext.HttpContext.Session.Contents["result"] as List<PermissionViewModel>;
            if ((permissionList != null) && (permissionList.Count() > 0))
            {
                if (permissionList.FirstOrDefault(i => i.PermissionId == Permission) == null)
                {
                    filterContext
                        .HttpContext
                        .Response
                        .RedirectToRoute(new { controller = "Account", action = "Login" });
                }
            }
            else
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