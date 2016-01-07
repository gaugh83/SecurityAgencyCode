using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Interfaces
{
    public interface IGuard
    {
        List<GuardViewModel> GetAllGaurd();
        GuardViewModel GetGaurd(int id);
        int? CreateUpdateGuard(GuardViewModel gaurdViewModel);
        int DeleteGuard(int id,int UserId);
        List<GuardViewModel> GetGuardForReport(string startDate, string endDate);
        bool validateGuardSSN(int guardId, string SSN);
    }
}
