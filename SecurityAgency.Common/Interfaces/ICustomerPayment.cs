using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Interfaces
{
    public interface ICustomerPayment
    {
        List<CustomerPaymentViewModel> GetAllCustomerPayment();
        CustomerPaymentViewModel GetCustomerPayment(int id);
        int? CreateUpdateCustomerPayment(CustomerPaymentViewModel customerPaymentViewModel);
        int DeleteCustomerPayment(int id, int UserId);
    }
}
