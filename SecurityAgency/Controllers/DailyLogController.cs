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
    public class DailyLogController : Controller
    {
        //Initilize Referance of IDailyLog
       IDailyLog _dailylogComponent = null;

       
         ///<summary>
        /// Assign ICategory
        /// </summary>
        /// <param name="categoryComponent"></param>
          public DailyLogController(IDailyLog dailylogComponent)
        {
            _dailylogComponent = dailylogComponent;
        }

         
      
        /// <summary>
     
        /// </summary>
        /// <returns></returns>
          [HandleError]
          [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewDailyLog)]      
          public ActionResult DailyLog()
          {
              //Validate User Pemissions
              @ViewBag.CreatePermission = CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddDailyLog);
              return View();
          }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult GetDailyLog()
        {
            //Validate User Pemissions
            string action = string.Empty;
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditDailyLog))
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateUpdateDailyLogPopup($$DailyLogId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteDailyLog))
            {
                action += "&nbsp;<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteDailyLog($$DailyLogId$$)'></span>";
            }

            var dailyLog = _dailylogComponent.GetAllDailyLog();
            if (dailyLog == null)
                return null;

            List<string[]> data = new List<string[]>();
            var TotalRecords = dailyLog.Count();

            foreach (var dailylog in dailyLog)
            {
                var row = new string[]
                {   
            dailylog.CustomerName.ToString(),
            dailylog.GuardName.ToString(),
            dailylog.Hours.ToString(),
            dailylog.Dated.ToString(),
            dailylog.Comments,
            dailylog.CreatedDate.ToShortDateString(),
            action.Replace("$$DailyLogId$$",dailylog.DailyLogId.ToString())
            };
                data.Add(row);
            }

            return Json(new { aaData = data,
            iTotalRecords=TotalRecords,
            iTotalDisplayRecords=TotalRecords,}, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult CreateUpdateDailyLogPopup(int id)
        {
            var dailylog = _dailylogComponent.GetDailyLog(id);
            var customerList = _dailylogComponent.GetAllCustomer();
            var guardlist = _dailylogComponent.GetAllGuard();
            if (dailylog == null)
                dailylog = new DailyLogViewModel();
            dailylog.CustomerList = new SelectList(_dailylogComponent.GetAllCustomer(), "CustomerId", "NameEmail");
            dailylog.GuardList = new SelectList(_dailylogComponent.GetAllGuard(), "GuardId", "NameSSN");
            return PartialView("/Views/Shared/Partials/_DailyLog.cshtml", dailylog);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleError]
        public ActionResult DeleteDailyLog(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _dailylogComponent.DeleteDailyLog(id, activeUser.UserId);
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
        public ActionResult CreateUpdateDailyLog(DailyLogViewModel dailylogViewModel)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
           dailylogViewModel.CreatedBy = activeUser.UserId;
            dailylogViewModel.ModifiedBy= activeUser.UserId;
            var result = _dailylogComponent.CreateUpdateDailyLog(dailylogViewModel);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}

 
