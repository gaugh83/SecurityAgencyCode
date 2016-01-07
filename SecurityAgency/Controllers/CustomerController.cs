using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.Models;
using SecurityAgency.Common.Utility;
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
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/
 //
       
        ICustomer _customerComponent;
        
        public CustomerController(ICustomer customerComponent)
        {
            _customerComponent = customerComponent;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewCustomer)]      
        public ActionResult Customer()
        {
            //Validate User Pemissions
            List<PermissionViewModel> permissionList = Session["result"] as List<PermissionViewModel>;
            if (permissionList.FirstOrDefault(i => i.PermissionId == Convert.ToInt32(SecurityAgency.Common.Utility.EnumUtility.Permissions.AddCustomer))!=null)
            {
                @ViewBag.CreatePermission = "create";
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult GetCustomers()
        {
            //Validate User Pemissions
            List<PermissionViewModel> permissionList = Session["result"] as List<PermissionViewModel>;
            string action=string.Empty;
            if(permissionList.FirstOrDefault(i=>i.PermissionId == Convert.ToInt32(SecurityAgency.Common.Utility.EnumUtility.Permissions.EditCustomer))!=null)
            {
                action += "<span class='glyphicon glyphicon-pencil create-pencil' onclick='CreateUpdateCustomerPopup($$CustomerId$$)'></span>&nbsp;";
            }
            if (permissionList.FirstOrDefault(i => i.PermissionId == Convert.ToInt32(SecurityAgency.Common.Utility.EnumUtility.Permissions.DeleteCustomer)) != null)
            {
                action += "&nbsp;<span class='glyphicon glyphicon-trash delete-trash'  onclick='DeleteCustomer($$CustomerId$$)'></span>";
            }
            var customers = _customerComponent.GetAllCustomer();
            if (customers == null)
                return null;

            List<string[]> data = new List<string[]>();
            var totalRecords = customers.Count();
            foreach (var customer in customers)
            {
                var row = new string[]
                {   
            customer.Name,
            customer.Email,
            customer.Address,
            customer.ContactNo,
            customer.HourlyRate.ToString(),
            customer.CreatedDate.ToShortDateString(),
            action.Replace("$$CustomerId$$",customer.CustomerId.ToString())
            };
                data.Add(row);
            }

            return Json(new
            {
                iTotalRecords = customers.Count(),
                iTotalDisplayRecords = customers.Count(),
                aaData = data
            },
             JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [HandleError]
        public ActionResult CreateUpdateCustomerPopup(int id)
        {
            var customer = _customerComponent.GetCustomer(id);
            if (customer == null)
                customer = new CustomerViewModel();
            return PartialView("/Views/Shared/Partials/_Customer.cshtml", customer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [HandleError]
        public ActionResult DeleteCustomer(int id)
        {
            ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
            var result = _customerComponent.DeleteCustomer(id, activeUser.UserId);
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
        public ActionResult CreateUpdateCustomer(CustomerViewModel customerViewModel)
        {
           
                ActiveUser activeUser = new JavaScriptSerializer().Deserialize<ActiveUser>(System.Web.HttpContext.Current.User.Identity.Name);
                customerViewModel.CreatedBy = activeUser.UserId;
                customerViewModel.ModifiedBy = activeUser.UserId;

                var result = _customerComponent.CreateUpdateCustomer(customerViewModel);

                return Json(result, JsonRequestBehavior.AllowGet);
            
        }
        [HttpPost]
        [HandleError]
        public JsonResult validateCustomerEmailAddress(int customerId, string email)
        {
            return Json(_customerComponent.validateCustomerEmailAddress(customerId, email), JsonRequestBehavior.AllowGet);

        }

    }
}