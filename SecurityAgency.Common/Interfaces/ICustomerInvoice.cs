using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Interfaces
{
    public interface ICustomerInvoice
    {        
        List<CustomerInvoiceViewModel> GetAllCustomerInvoice();
        CustomerInvoiceViewModel GetCustomerInvoice(int id);
        int? CreateCustomerInvoice(CustomerInvoiceViewModel CustomerInvoiceViewModel);
        int DeleteCustomerInvoice(int id, int UserId);
        int GetNewInvoiceNumber();
        CustomerInvoiceViewModel GetCustomerHoursAndAmount(int customerId, string type, string beginDate, string endDate, int inoviceId);
        CustomerInvoiceViewModel GenerateInvoice(int invoiceId);
        List<CustomerInvoiceViewModel> GetCustomersInvoiceForReport(string startDate, string endDate, int customerId, string invoiceType);
        List<CustomerInvoiceViewModel> GetCustomerInvoiceByCustomerId(int customerId);
        bool CheckDailyLogOverlap(int customerId, string beginDate);
        int CheckInvoiceOverlap(int customerId, string beginDate, string endDate, int inoviceId);
    }
}
