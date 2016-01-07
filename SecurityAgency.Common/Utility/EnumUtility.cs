using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Utility
{
    public class EnumUtility
    {
        public enum Role
        {
            Admin = 1,
            Supervisor = 2,
            User = 3
        }

        public enum Permissions
        {
            AddCustomer = 2,
            EditCustomer = 3,
            DeleteCustomer = 4,
            ViewCustomer = 5,
            AddGuard = 6,
            EditGuard = 7,
            DeleteGuard = 8,
            ViewGuard = 9,
            AddUser = 10,
            EditUser = 11,
            DeleteUser = 13,
            ViewUser = 14,
            AddCustomerPayment = 18,
            EditCustomerPayment = 19,
            DeleteCustomerPayment = 21,
            ViewCustomerPayment = 22,
            AddGuardPayment = 30,
            EditGuardPayment = 31,
            DeleteGuardPayment = 32,
            ViewGuardPayment = 33,
            AddDailyLog = 35,
            EditDailyLog = 36,
            DeleteDailyLog = 37,
            ViewDailyLog = 38,
            AddInvoice = 39,
            DeleteInvoice = 40,
            ViewInvoice = 41,
            PrintInvoice = 43,            
            UserPermission = 44,
            ViewCustomerReports = 45,
            ViewGuardReports = 46,
            ViewCustomerInvoiceReports = 47,
            ViewGuardPaymentReport = 48,
            AddRole = 49,
            EditRole = 50,
            DeleteRole = 51,
            ViewRole = 52,
            EditInvoice = 53
        }
        public enum InvoiceType
        {
            All = 0,
            Paid = 1,
            Unpaid = -1
        }
 }
    }
