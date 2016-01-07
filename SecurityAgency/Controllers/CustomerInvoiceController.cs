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
    public class CustomerInvoiceController : Controller
    {
        //
        // GET: /CustomerInvoice/
        ICustomerInvoice _customerInvoiceComponent;
        ICustomer _customerComponent;
        public CustomerInvoiceController(ICustomerInvoice customerInvoiceComponent, ICustomer customerComponent)
        {
            _customerInvoiceComponent = customerInvoiceComponent;
            _customerComponent = customerComponent;
        }

        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewInvoice)]      
        public ActionResult CustomerInvoice()
        {
            //Validate User Pemissions
            @ViewBag.CreatePermission = CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddInvoice);
            return View();
        }

        public ActionResult PrintInvoice()
        {
            CustomerInvoiceViewModel objectCustomerInvoiceViewModel = _customerInvoiceComponent.GenerateInvoice(Convert.ToInt32(Request.QueryString["invoiceId"]));
            return View(objectCustomerInvoiceViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult GetCustomerInvoice()
        {
            //Validate User Pemissions
            string action = string.Empty;
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.PrintInvoice))
            {
                action += "<span class='glyphicon glyphicon-print create-print' onclick='PrintInvoice($$CustomerInvoiceId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditInvoice))
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateCustomerPopup($$CustomerInvoiceId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteInvoice))
            {
                action += "<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteCustomerInvoice($$CustomerInvoiceId$$)'></span>";
            }

            var customerInvoice = _customerInvoiceComponent.GetAllCustomerInvoice();
            if (customerInvoice == null)
                return null;

            List<string[]> data = new List<string[]>();
            var totalRecords = customerInvoice.Count();
            foreach (var customer in customerInvoice)
            {
                var row = new string[]
                {   
            customer.InvoiceNo.ToString(),
            customer.CustomerName,
            customer.Amount.ToString(),
            customer.BeginDate.ToShortDateString(),
            customer.EndDate.ToShortDateString(),
            customer.InvoiceDate.ToShortDateString(),
            action.Replace("$$CustomerInvoiceId$$",customer.InvoiceId.ToString())
            
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
        public ActionResult CreateCustomerInvoicePopup(int id)
        {
            var customer = _customerInvoiceComponent.GetCustomerInvoice(id);
            if (customer == null)
                customer = new CustomerInvoiceViewModel();
            customer.CustomerList = new SelectList(_customerComponent.GetAllCustomer(), "CustomerId", "NameEmail");
            return PartialView("/Views/Shared/Partials/_CustomerInvoice.cshtml", customer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult CreateCustomerInvoice(CustomerInvoiceViewModel customerViewModel)
        {

            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            customerViewModel.CreatedBy = activeUser.UserId;
            customerViewModel.ModifiedBy = activeUser.UserId;

            var result = _customerInvoiceComponent.CreateCustomerInvoice(customerViewModel);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleError]
        public ActionResult DeleteCustomerInvoice(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _customerInvoiceComponent.DeleteCustomerInvoice(id, activeUser.UserId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public int GetNewInvoiceNumber()
        {
            return _customerInvoiceComponent.GetNewInvoiceNumber();
        }

        [HttpGet]
        public ActionResult GetCustomerHoursAndAmount(int customerId, string type, string beginDate, string endDate, int inoviceId)
        {
            CustomerInvoiceViewModel objectCustomerInvoiceViewModel = _customerInvoiceComponent.GetCustomerHoursAndAmount(customerId, type, beginDate, endDate, inoviceId);
            string beginDateDay = (objectCustomerInvoiceViewModel.BeginDate.Day.ToString().Length == 1) ? "0" + objectCustomerInvoiceViewModel.BeginDate.Day.ToString() : objectCustomerInvoiceViewModel.BeginDate.Day.ToString();
            string beginDateMonth = (objectCustomerInvoiceViewModel.BeginDate.Month.ToString().Length == 1) ? "0" + objectCustomerInvoiceViewModel.BeginDate.Month.ToString() : objectCustomerInvoiceViewModel.BeginDate.Month.ToString();
            string endDateDay = (objectCustomerInvoiceViewModel.EndDate.Day.ToString().Length == 1) ? "0" + objectCustomerInvoiceViewModel.EndDate.Day.ToString() : objectCustomerInvoiceViewModel.EndDate.Day.ToString();
            string endDateMonth = (objectCustomerInvoiceViewModel.EndDate.Month.ToString().Length == 1) ? "0" + objectCustomerInvoiceViewModel.EndDate.Month.ToString() : objectCustomerInvoiceViewModel.EndDate.Month.ToString();
            return Json(new
            {
                beginDate = objectCustomerInvoiceViewModel.BeginDate.Year + "-" + beginDateMonth + "-"+ beginDateDay,
                endDate = objectCustomerInvoiceViewModel.EndDate.Year + "-"+ endDateMonth +"-"+ endDateDay,
                totalHours = objectCustomerInvoiceViewModel.TotalHours,
                amount = objectCustomerInvoiceViewModel.Amount,
                hourlyRate = objectCustomerInvoiceViewModel.HourlyRate
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public bool CheckDailyLogOverlap(int customerId, string LogDate)
        {
            return _customerInvoiceComponent.CheckDailyLogOverlap(customerId, LogDate);
        }

        [HttpGet]
        public int CheckInvoiceOverlap(int customerId, string beginDate, string endDate, int inoviceId)
        {
            return _customerInvoiceComponent.CheckInvoiceOverlap(customerId, beginDate, endDate, inoviceId);
        }
    }
}
