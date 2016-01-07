using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Interfaces
{
    public interface ICustomer
    {
        List<CustomerViewModel> GetAllCustomer();
        CustomerViewModel GetCustomer(int id);
        int? CreateUpdateCustomer(CustomerViewModel customerViewModel);
        int DeleteCustomer(int id, int UserId);
        List<CustomerViewModel> GetCustomersForReport(string startDate, string endDate);
        bool validateCustomerEmailAddress(int customerId, string email);
        //void LogException(Exception e, string extraInfo);
    }
}
