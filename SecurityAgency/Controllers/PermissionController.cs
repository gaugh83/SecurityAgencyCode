using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityAgency.Controllers
{
    public class PermissionController : Controller
    {
       
      IPermission _permissioncomponent;

        public PermissionController(IPermission PermissionComponent)
        {
            _permissioncomponent = PermissionComponent;
        }

        public ActionResult Permission(PermissionViewModel permissionviewmodel)
        {
            permissionviewmodel.Rolelist = new SelectList(_permissioncomponent.GetAllRoles(), "RoleId", "Role");
            return View(permissionviewmodel);
        }

    }
}
