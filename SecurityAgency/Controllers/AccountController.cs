using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.Utility;
using SecurityAgency.Common.ViewModels;
using SecurityAgency.Repository.DbServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace SecurityAgency.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        IAccount _accountComponent;

        public AccountController(IAccount accountComponent)
        {
            _accountComponent = accountComponent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult LogIn()
        {

            return View();
        }


        /// <summary>
        /// Method To Login
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LogIn(UserViewModel userModel)
        {
            var result = _accountComponent.CheckLogin(userModel);
            if (result == null)
            {
                ModelState.AddModelError("", "User name or password is incorrect.");
                return View("LogIn");
            }
            else
            {
                Session["UserDetail"] = result;
                GetAllPermissions(result.Role);
                FormsAuthentication.SetAuthCookie(new JavaScriptSerializer().Serialize(result), false);

                if (result.Role == Convert.ToInt32(EnumUtility.Role.Admin))
                {
                    return RedirectToAction("Customer", "Customer");
                }
                else
                {
                    return RedirectToAction("Customer", "Customer");
                }
               
            }
        }
        public void GetAllPermissions(int RoleId)
        {

            SecurityAgencyEntities dbcontext = new SecurityAgencyEntities();
            List<PermissionViewModel> permission = (from u in dbcontext.Users
                                                    join rp in dbcontext.RolePermissions on u.RoleId equals rp.RoleId
                                                    join p in dbcontext.Permissions on new { a = rp.PermissionId } equals new { a = p.PermissionId }
                                                    where u.RoleId == RoleId
                                                    select new PermissionViewModel
                            {
                                ModuleId = p.ModuleId,
                                UserId = u.UserID,
                                RoleId = u.RoleId,
                                PermissionId = rp.PermissionId,
                                Name = p.Name,

                            }).ToList();

            Session["result"] = permission;

        }


        /// <summary>
        /// Method to logout
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public RedirectToRouteResult LogOut()
        {
            //ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            //if (activeUser == null)
             //   return RedirectToAction("LogIn", "Account");

            //UserLogModel userLogModel = new UserLogModel()
            //{
            //    LogId = activeUser.LogId,
            //    LogOutTime = DateTime.Now
            //};
            //_accountComponent.HandleUserLog(userLogModel);

            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn", "Account");
        }

    }
}

