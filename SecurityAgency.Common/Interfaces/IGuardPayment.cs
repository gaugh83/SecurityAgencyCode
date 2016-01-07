using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Interfaces
{
    public interface IGuardPayment
    {
        List<GuardPaymentViewModel> GetAllGuardPayment();
        GuardPaymentViewModel GetGuardPayment(int id);
        int? CreateUpdateGuardPayment(GuardPaymentViewModel guardPaymentViewModel);
        int DeleteGuardPayment(int id, int UserId);
        GuardPaymentViewModel GetPaymentAmount(string startDate, string endDate, int guardId);
        List<GuardPaymentViewModel> GetGuardPaymentReport(int GuardId);
    }
}
