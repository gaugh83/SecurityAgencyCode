using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Common.Interfaces
{
   public interface IDailyLog
    {
       List<DailyLogViewModel> GetAllDailyLog();
       DailyLogViewModel GetDailyLog(int id);
       int? CreateUpdateDailyLog(DailyLogViewModel dailyViewModel);
        int DeleteDailyLog(int id, int UserId);
        List<CustomerViewModel> GetAllCustomer();
        List<GuardViewModel> GetAllGuard();
    }
    }

