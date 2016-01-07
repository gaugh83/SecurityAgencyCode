using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.ViewModels;
using SecurityAgency.Filter;
using SecurityAgency.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityAgency.Controllers
{
    [ValidateSession]
    public class ReportsController : Controller
    {
        ICustomer _customerComponent;
        IGuard _guardComponent;
        public ReportsController(ICustomer customerComponent, IGuard guardComponent)
        {
            _customerComponent = customerComponent;
            _guardComponent = guardComponent;
        }

        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewCustomerReports)]      
        public ActionResult CustomerReport()
        {
            return View();
        }

        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewGuardReports)]      
        public ActionResult GuardReport()
        {
            return View();
        }

        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewCustomerInvoiceReports)]      
        public ActionResult CustomerInvoiceReport()
        {
            CustomerInvoiceViewModel objectCustomerInvoiceViewModel = new CustomerInvoiceViewModel();
            objectCustomerInvoiceViewModel.CustomerList = new SelectList(_customerComponent.GetAllCustomer(), "CustomerId", "NameEmail");
            return View(objectCustomerInvoiceViewModel);
        }

        [HandleError]
        [UserPermission(Permission = (int)SecurityAgency.Common.Utility.EnumUtility.Permissions.ViewGuardPaymentReport)]      
        public ActionResult GuardPaymentReport()
        {
            GuardViewModel objectGuardViewModel = new GuardViewModel();
            objectGuardViewModel.Guardlist = new SelectList(_guardComponent.GetAllGaurd(), "GuardId", "NameSSN");
            return View(objectGuardViewModel);
        }
    }
}
