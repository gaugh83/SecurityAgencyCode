using SecurityAgency.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecurityAgency.Common.ViewModels;
using SecurityAgency.Common.Models;
using System.Web.Script.Serialization;
using SecurityAgency.Repository.DbServices;
using SecurityAgency.Filter;
using SecurityAgency.Common;
using SecurityAgency.Common.Utility;
namespace SecurityAgency.Controllers
{
    [ValidateSession]
    public class UserController : Controller
    {
        //Initilize Referance of IUser
        IUser _userComponent = null;
        //Initilize Referance of IRole
        IPermission _permissioncomponent;

        /// <summary>
        /// Assign ICategory
        /// </summary>
        /// <param name="categoryComponent"></param>
        public UserController(IUser userComponent, IPermission PermissionComponent)
        {
            _userComponent = userComponent;
            _permissioncomponent = PermissionComponent;
        }



        /// <summary>

        /// </summary>
        /// <returns></returns>
        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewUser)]
        public ActionResult User()
        {
            //Validate User Pemissions
            @ViewBag.CreatePermission = CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddUser);
            return View();
        }

        /// <summary>

        /// </summary>
        /// <returns></returns>
        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewRole)]
        public ActionResult Roles()
        {
            //Validate User Pemissions
            @ViewBag.CreatePermission = CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddRole);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]

        public ActionResult GetUsers()
        {
            //Validate User Pemissions
            string action = string.Empty;
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditUser))
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateUpdateUserPopup($$UserId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteUser))
            {
                action += "&nbsp;<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteUser($$UserId$$)'></span>&nbsp;&nbsp;";
            }
            
            var users = _userComponent.GetAllUser();
            if (users == null)
                return null;

            List<string[]> data = new List<string[]>();
            var totalRecords = users.Count();
            foreach (var user in users)
            {
                var row = new string[]
                {   
                    user.UserName,
                    user.Email,
                    user.Address,
                    user.ContactNo,
                    user.Zip,
                    user.CreatedDate.ToShortDateString(),
                    action.Replace("$$UserId$$",user.UserId.ToString())
                };
                data.Add(row);
            }

            return Json(new
            {
                aaData = data,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]

        public ActionResult GetRoles()
        {
            //Validate User Pemissions
            string action = string.Empty;
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditRole))
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateUpdateRolePopup($$RoleId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteRole))
            {
                action += "&nbsp;<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteRole($$RoleId$$)'></span>&nbsp;&nbsp;";
            }
            var users = _userComponent.GetAllRole().Where(i=>i.RoleId!=Convert.ToInt32(EnumUtility.Role.Admin)).ToList();
            if (users == null)
                return null;

            List<string[]> data = new List<string[]>();
            var totalRecords = users.Count();
            foreach (var user in users)
            {
                var row = new string[]
                {   
            user.Role1,            
            action.Replace("$$RoleId$$",user.RoleId.ToString())
            };
                data.Add(row);
            }

            return Json(new
            {
                aaData = data,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult CreateUpdateUserPopup(int id)
        {
            var user = _userComponent.GetUser(id);
            var rolelist = _userComponent.GetAllRole();
            if (user == null)
                user = new UserViewModel();
            user.Rolelist = new SelectList(_userComponent.GetAllRole(), "RoleId", "Role1");
            return PartialView("/Views/Shared/Partials/_User.cshtml", user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleError]
        public ActionResult DeleteUser(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _userComponent.DeleteUser(id, activeUser.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleError]
        public ActionResult DeleteRole(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _userComponent.DeleteRole(id, activeUser.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult CreateUpdateUser(UserViewModel userViewModel)
        {
            if (userViewModel.UserId == Convert.ToInt32(EnumUtility.Role.Admin))
            {
                userViewModel.RoleId = Convert.ToInt32(EnumUtility.Role.Admin);
            }
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            userViewModel.CreatedBy = activeUser.UserId;
            userViewModel.Last_ModifiedBy = activeUser.UserId;
            var result = _userComponent.CreateUpdateUser(userViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HandleError]
        public ActionResult PermissionUserPopup(int id)
        {
            PermissionViewModel permissionviewmodel = new PermissionViewModel();
            permissionviewmodel.Rolelist = new SelectList(_userComponent.GetAllRole(), "RoleId", "Role1");
            permissionviewmodel.Modulelist = new SelectList(_userComponent.GetAllModules(), "ModuleId", "ModuleName");
            //permissionviewmodel .PermissionList=new SelectList(_userComponent.GetAllPermissions(),"PermissionId","Name");
            SecurityAgencyEntities dbcontext = new SecurityAgencyEntities();
            var query = (from m in dbcontext.Modules
                         join p in dbcontext.Permissions on m.ModuleId equals p.ModuleId
                         select new PermissionViewModel
                           {
                               ModuleId = m.ModuleId,
                               ModuleName = m.ModuleName,
                               PermissionId = p.PermissionId,
                               Name = p.Name

                           });

            permissionviewmodel.PermissionList = query.ToList();


            return PartialView("/Views/Permission/Permission.cshtml", permissionviewmodel);

        }
        //[HandleError]
        //public ActionResult CreateUpdatePermissions(PermissionViewModel permissionviewmodel)
        //{
        //    ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
        //    permissionviewmodel.CreatedBy = activeUser.UserId;
        //    permissionviewmodel.ModifiedBy = activeUser.UserId;
        //    var result = _userComponent.CreateUpdatePermission(permissionviewmodel);
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetAllPermissions()
        //{

        //    SecurityAgencyEntities dbcontext = new SecurityAgencyEntities();
        //    var query = (from m in dbcontext.Modules
        //                 join p in dbcontext.Permissions on m.ModuleId equals p.ModuleId
        //                 select new
        //                 {
        //                     m.ModuleId,
        //                     m.ModuleName,
        //                     p.PermissionId,
        //                     p.Name
        //                 }).ToList();


        //return View(query);


        //}


        [HandleError]
        public ActionResult RolePermissions()
        {
            int RoleId = Convert.ToInt32(Request.QueryString["id"]);
            ViewBag.RoleDetail = new RoleViewModel();
            if (RoleId!=0)
            {
                ViewBag.RoleDetail = _userComponent.GetAllRole().FirstOrDefault(i => i.RoleId == RoleId);
            }
            List<PermissionViewModel> objectPermissionViewModel = _userComponent.GetAllPermissions();
            ViewBag.RoleList = new SelectList(_userComponent.GetAllRole(), "RoleId", "Role1");
            return View(objectPermissionViewModel);
        }
        [HttpPost]
        public ActionResult RolePermissions(PermissionViewModel objPermissionViewModel, FormCollection formCollection)
        {
            List<string> chkPermissionList = formCollection.AllKeys.Where(c => c.StartsWith("chk_")).ToList();
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);

            _userComponent.AddRolePermissions(objPermissionViewModel.RoleId, objPermissionViewModel.Role, chkPermissionList, activeUser.UserId);
            return RedirectToAction("Roles");
        }
        [HandleError]
        public ActionResult GetPermissionForRoles(int RoleId)
        {
            var result = _userComponent.GetPermissionForRoles(RoleId);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}

