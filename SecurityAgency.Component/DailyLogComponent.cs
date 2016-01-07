using AutoMapper;
using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.ViewModels;
using SecurityAgency.Repository;
using SecurityAgency.Repository.DbServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAgency.Component
{
   public class DailyLogComponent :IDailyLog
    {
        IDbRepository _repository = null;

        /// <summary>
        /// Assign  IDbRepository
        /// </summary>
        /// <param name="repositry">Refrence of IDbRepository</param>
        public DailyLogComponent(IDbRepository repository)
        {
            _repository = repository;
        }
        public List<DailyLogViewModel> GetAllDailyLog()
        {
            List<DailyLog> dailylog = _repository.GetAll<DailyLog>().Where(x => x.IsDeleted == false).ToList();

            if (dailylog == null)
                return null;
            var DailyLogList = new List<DailyLogViewModel>();

            foreach (var item in dailylog)
            {
                DailyLogList.Add(new DailyLogViewModel
                {
                    DailyLogId=item.DailyLogId,
                    CustomerName= item.Customer.Name,
                    GuardName=item.Guard.Name,
                    Hours = item.Hours,
                    Dated = item.Dated,
                    Comments = item.Comments,
                    CreatedDate = item.CreatedDate,

                });
            }

            //Mapper.CreateMap<DailyLog, DailyLogViewModel>();
            //return Mapper.Map<List<DailyLog>, List<DailyLogViewModel>>(dailylog);
            return DailyLogList;
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public DailyLogViewModel GetDailyLog(int id)
        {
            DailyLog dailylog = _repository.Find<DailyLog>(x => x.DailyLogId == id);

            if (dailylog == null)
                return null;

            Mapper.CreateMap<DailyLog, DailyLogViewModel>();
            return Mapper.Map<DailyLog, DailyLogViewModel>(dailylog);
        }

        public int? CreateUpdateDailyLog(DailyLogViewModel dailyLogViewModel)
        {
            DailyLog dailyLog = null;
            if (dailyLogViewModel.DailyLogId > 0)
            {
                dailyLog = _repository.Find<DailyLog>(x => x.DailyLogId == dailyLogViewModel.DailyLogId);

                if (dailyLog == null)
                    return null;

                dailyLog.CustomerId = dailyLogViewModel.CustomerId;
                dailyLog.GuardId = dailyLogViewModel.GuardId;
                dailyLog.Comments = dailyLogViewModel.Comments;
                dailyLog.Dated = dailyLogViewModel.Dated;

                _repository.Modify<DailyLog>(dailyLog);
                return dailyLog.CustomerId;
            }

            Mapper.CreateMap<DailyLogViewModel, DailyLog>();
            dailyLog = Mapper.Map<DailyLogViewModel, DailyLog>(dailyLogViewModel);
          

            dailyLog.CreatedDate = DateTime.Now;
            dailyLog.Dated = dailyLogViewModel.Dated;
           
            dailyLog.IsDeleted = false;
            return _repository.Insert<DailyLog>(dailyLog);
        }

        public int DeleteDailyLog(int id, int UserId)
        {
            DailyLog dailylog = _repository.Find<DailyLog>(x => x.DailyLogId== id);
            if (dailylog == null)
                return 0;
            dailylog.IsDeleted = true;
            dailylog.DeletedBy = UserId;
            dailylog.DeletedDate = DateTime.Now;
            return _repository.Modify<DailyLog>(dailylog);
        }
        public List<CustomerViewModel> GetAllCustomer()
        {
            List<Customer> customerlist = _repository.GetAll<Customer>().Where(i => i.IsDeleted == false).ToList();

            if (customerlist == null)
                return null;

            Mapper.CreateMap<Customer, CustomerViewModel>();

            return Mapper.Map<List<Customer>, List<CustomerViewModel>>(customerlist);
        }
        public List<GuardViewModel> GetAllGuard()
        {
            List<Guard> guardlist = _repository.GetAll<Guard>().Where(i=>i.IsDeleted==false).ToList();

            if (guardlist == null)
                return null;

            Mapper.CreateMap<Guard, GuardViewModel>().ForMember(dest => dest.NameSSN,
               opts => opts.MapFrom(
                   src => string.Format("{0} ({1})",
                       src.Name,
                       src.SSN)));

            return Mapper.Map<List<Guard>, List<GuardViewModel>>(guardlist);
        }
    }
}
