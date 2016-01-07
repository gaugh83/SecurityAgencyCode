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
    public class CustomerPaymentController : Controller
    {
        //
        // GET: /Customer/
 //
       
        ICustomerPayment _customerPaymentComponent;
        ICustomer _customerComponent;
        ICustomerInvoice _customerInvoiceComponent;

        public CustomerPaymentController(ICustomerPayment customerPaymentComponent, ICustomer customerComponent, ICustomerInvoice customerInvoiceComponent)
        {
            _customerPaymentComponent = customerPaymentComponent;
            _customerComponent = customerComponent;
            _customerInvoiceComponent = customerInvoiceComponent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewCustomerPayment)]      
        public ActionResult CustomerPayment()
        {
            //Validate User Pemissions
            @ViewBag.CreatePermission = CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddCustomerPayment);
            
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult GetCustomersPayment()
        {
            //Validate User Pemissions
            string action = string.Empty;
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditCustomerPayment))
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateUpdateCustomerPaymentPopup($$CustomerPaymentId$$)'></span>&nbsp;";
            }
            if (CommonClass.GetPermission(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteCustomerPayment))
            {
                action += "&nbsp;<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteCustomerPayment($$CustomerPaymentId$$)'></span>";
            }

            var customers = _customerPaymentComponent.GetAllCustomerPayment();
                if (customers == null)
                    return null;

                List<string[]> data = new List<string[]>();
                var totalRecords = customers.Count();
                foreach (var customer in customers)
                {
                    var row = new string[]
                {   
            customer.CustomerName,
            customer.InvoiceNo.ToString(),
            customer.Amount.ToString(),
            customer.Comments,
            Convert.ToDateTime(customer.PaymentDate).ToShortDateString(),
            action.Replace("$$CustomerPaymentId$$",customer.CustomerPaymentId.ToString())
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
        public ActionResult CreateUpdateCustomerPaymentPopup(int id)
        {
            var customerPayment = _customerPaymentComponent.GetCustomerPayment(id);
            if (customerPayment == null)
                customerPayment = new CustomerPaymentViewModel();
            customerPayment.CustomerList = new SelectList(_customerComponent.GetAllCustomer(), "CustomerId", "NameEmail");
            //customerPayment.InvoiceList = new SelectList(_customerInvoiceComponent.GetAllCustomerInvoice(), "InvoiceId", "InvoiceNo");
            return PartialView("/Views/Shared/Partials/_CustomerPayment.cshtml", customerPayment);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleError]
        public ActionResult DeleteCustomerPayment(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _customerPaymentComponent.DeleteCustomerPayment(id, activeUser.UserId);
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
        public ActionResult CreateUpdateCustomerPayment(CustomerPaymentViewModel customerPaymentViewModel)
        {
           
                ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
                customerPaymentViewModel.CreatedBy = activeUser.UserId;
                customerPaymentViewModel.ModifiedBy = activeUser.UserId;

                var result = _customerPaymentComponent.CreateUpdateCustomerPayment(customerPaymentViewModel);

                return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        [HttpGet]
        public ActionResult GetInvoiceAmount(int id)
        {
            CustomerInvoiceViewModel objectCustomerInvoiceViewModel = _customerInvoiceComponent.GetCustomerInvoice(id);
            return Json(new
            {
                TotalHours = objectCustomerInvoiceViewModel.TotalHours,
                Amount = objectCustomerInvoiceViewModel.Amount
            }, JsonRequestBehavior.AllowGet);
            return null;
        }

        [HttpGet]
        public ActionResult GetCustomerInvoice(int id)
        {
            List<CustomerInvoiceViewModel> objectCustomerInvoiceViewModel = _customerInvoiceComponent.GetCustomerInvoiceByCustomerId(id);
            return Json(objectCustomerInvoiceViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}