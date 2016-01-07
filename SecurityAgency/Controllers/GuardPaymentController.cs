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
    public class GuardPaymentController : Controller
    {
        //
        // GET: /Customer/
 //
       
        IGuardPayment _guardPaymentComponent;
        IGuard _guardComponent;
        IDailyLog _dailyLogComponent;

        public GuardPaymentController(IGuardPayment guardPaymentComponent, IGuard guardComponent, IDailyLog dailyLogComponent)
        {
            _guardPaymentComponent = guardPaymentComponent;
            _guardComponent = guardComponent;
            _dailyLogComponent = dailyLogComponent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewGuardPayment)]      
        public ActionResult GuardPayment()
        {
            //Validate User Pemissions
            @ViewBag.CreatePermission = CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddGuardPayment);
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult GetGuardPayment()
        {
            //Validate User Pemissions
            string action = string.Empty;
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditGuardPayment))
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateUpdateGuardPaymentPopup($$GuardPaymentId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteGuardPayment))
            {
                action += "&nbsp;<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteGuardPayment($$GuardPaymentId$$)'></span>";
            }
            var guards = _guardPaymentComponent.GetAllGuardPayment();
            if (guards == null)
                    return null;

                List<string[]> data = new List<string[]>();
                var totalRecords = guards.Count();
                foreach (var guard in guards)
                {
                    var row = new string[]
                {   
            guard.GuardName,
            guard.PaymentDate.ToShortDateString(),
            guard.Amount.ToString(),
            guard.StartDate.ToShortDateString(),
            guard.EndDate.ToShortDateString(),
             action.Replace("$$GuardPaymentId$$",guard.GuardPaymentId.ToString())
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
        public ActionResult CreateUpdateGuardPaymentPopup(int id)
        {
            var guardPayment = _guardPaymentComponent.GetGuardPayment(id);
            if (guardPayment == null)
                guardPayment = new GuardPaymentViewModel();
            guardPayment.GuardList = new SelectList(_guardComponent.GetAllGaurd(), "GuardId", "NameSSN");
            //customerPayment.InvoiceList = new SelectList(_customerInvoiceComponent.GetAllCustomerInvoice(), "InvoiceId", "InvoiceNo");
            return PartialView("/Views/Shared/Partials/_GuardPayment.cshtml", guardPayment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleError]
        public ActionResult DeleteGuardPayment(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _guardPaymentComponent.DeleteGuardPayment(id, activeUser.UserId);
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
        public ActionResult CreateUpdateGuardPayment(GuardPaymentViewModel guardPaymentViewModel)
        {
           
                ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
                guardPaymentViewModel.CreatedBy = activeUser.UserId;
                guardPaymentViewModel.ModifiedBy = activeUser.UserId;

                var result = _guardPaymentComponent.CreateUpdateGuardPayment(guardPaymentViewModel);

                return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPaymentAmount(string startDate, string endDate, int guardId)
        {
            GuardPaymentViewModel objectGuardPaymentViewModel = _guardPaymentComponent.GetPaymentAmount(startDate, endDate, guardId);
            string beginDateDay = (objectGuardPaymentViewModel.StartDate.Day.ToString().Length == 1) ? "0" + objectGuardPaymentViewModel.StartDate.Day.ToString() : objectGuardPaymentViewModel.StartDate.Day.ToString();
            string beginDateMonth = (objectGuardPaymentViewModel.StartDate.Month.ToString().Length == 1) ? "0" + objectGuardPaymentViewModel.StartDate.Month.ToString() : objectGuardPaymentViewModel.StartDate.Month.ToString();
            string endDateDay = (objectGuardPaymentViewModel.EndDate.Day.ToString().Length == 1) ? "0" + objectGuardPaymentViewModel.EndDate.Day.ToString() : objectGuardPaymentViewModel.EndDate.Day.ToString();
            string endDateMonth = (objectGuardPaymentViewModel.EndDate.Month.ToString().Length == 1) ? "0" + objectGuardPaymentViewModel.EndDate.Month.ToString() : objectGuardPaymentViewModel.EndDate.Month.ToString();
            return Json(new
            {
                StartDate = objectGuardPaymentViewModel.StartDate.Year + "-" + beginDateMonth + "-" + beginDateDay,
                EndDate = objectGuardPaymentViewModel.EndDate.Year + "-" + endDateMonth + "-" + endDateDay,
                TotalHours = objectGuardPaymentViewModel.TotalHours,
                Amount = objectGuardPaymentViewModel.Amount,
                HourlyRate = objectGuardPaymentViewModel.HourlyRate
            }, JsonRequestBehavior.AllowGet);
        }
    }
}