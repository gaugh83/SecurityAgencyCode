using SecurityAgency.Common;
using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.Models;
using SecurityAgency.Common.ViewModels;
using SecurityAgency.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SecurityAgency.Controllers
{
    [ValidateSession]
    public class GuardController : Controller
    {
        //
        // GET: /Guard/
        IGuard _gaurdComponent;

        public GuardController(IGuard gaurdComponent)
        {
            _gaurdComponent = gaurdComponent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewGuard)]      
        public ActionResult Guard()
        {
            //Validate User Pemissions
            @ViewBag.CreatePermission = CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddGuard);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult GetGuards()
        {
            //Validate User Pemissions
            string action = string.Empty;
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditGuard))
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateUpdateGuardPopup($$GuardId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteGuard))
            {
                action += "&nbsp;<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteGuard($$GuardId$$)'></span>";
            }
            var guards = _gaurdComponent.GetAllGaurd();
            if (guards == null)
                return null;

            List<string[]> data = new List<string[]>();
           var totalRecords = guards.Count();
            foreach (var guard in guards)
            {
                var row = new string[]
                {   
            guard.Name,
            guard.SSN,
            guard.Address,
            guard.ContactNo,
            guard.HourlyRate.ToString(),
            guard.CreatedDate.ToShortDateString(),
            action.Replace("$$GuardId$$",guard.GuardId.ToString())
            };
                data.Add(row);
            }

            return Json(new { aaData = data,
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
        public ActionResult CreateUpdateGuardPopup(int id)
        {
            var guard = _gaurdComponent.GetGaurd(id);
            if (guard == null)
                guard = new GuardViewModel();
            return PartialView("/Views/Shared/Partials/_Guard.cshtml", guard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteGuard(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _gaurdComponent.DeleteGuard(id, activeUser.UserId);
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
        public ActionResult CreateUpdateGuard(GuardViewModel guardViewModel)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            guardViewModel.CreatedBy = activeUser.UserId;
            guardViewModel.ModifiedBy = activeUser.UserId;
            var result = _gaurdComponent.CreateUpdateGuard(guardViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleError]
        public JsonResult validateGuardSSN(int guardId, string SSN)
        {
            return Json(_gaurdComponent.validateGuardSSN(guardId, SSN), JsonRequestBehavior.AllowGet);

        }
    }
}
