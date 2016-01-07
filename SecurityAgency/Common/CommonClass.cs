using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecurityAgency.Common
{
    public class CommonClass
    {
        public static bool GetPermission(Utility.EnumUtility.Permissions permissions)
        {
            //Validate User Pemissions
            if(HttpContext.Current.Session["result"]==null)
            {
                HttpContext
                    .Current
                    .Response
                    .RedirectToRoute(new { controller = "Account", action = "Login" });
                return false;
            }
            List<PermissionViewModel> permissionList =HttpContext.Current.Session["result"] as List<PermissionViewModel>;
            if (permissionList.FirstOrDefault(i => i.PermissionId == Convert.ToInt32(permissions)) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    
}