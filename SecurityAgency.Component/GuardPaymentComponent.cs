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
using System.Web.Mvc;

namespace SecurityAgency.Component
{
    public class GuardPaymentComponent : IGuardPayment
    {
        /// <summary>
        /// Initilize Referance of IDbRepository
        /// </summary>
        IDbRepository _repository = null;

        /// <summary>
        /// Assign  IDbRepository
        /// </summary>
        /// <param name="repositry">Refrence of IDbRepository</param>
        public GuardPaymentComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        public List<GuardPaymentViewModel> GetAllGuardPayment()
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                List<GuardPaymentViewModel> guardPaymentViewModel = (from guardPayment in objContext.GuardPayments
                                                                     join guard in objContext.Guards on guardPayment.GuardId equals guard.GuardId
                                                                     where guardPayment.IsDeleted==false
                                                                     select new GuardPaymentViewModel
                                                                        {
                                                                            GuardPaymentId = guardPayment.GuardPaymentId,
                                                                            GuardId = guardPayment.GuardId,
                                                                            PaymentDate = guardPayment.PaymentDate,
                                                                            HourlyRate=guardPayment.HourlyRate,
                                                                            StartDate = guardPayment.StartDate,
                                                                            EndDate = guardPayment.EndDate,
                                                                            Amount = guardPayment.Amount,
                                                                            GuardName = guard.Name
                                                                        }).ToList();
                if (guardPaymentViewModel == null)
                    return null;

                return guardPaymentViewModel;
            }
        }

        public GuardPaymentViewModel GetGuardPayment(int id)
        {
            GuardPayment customer = _repository.Find<GuardPayment>(x => x.GuardPaymentId == id);

            if (customer == null)
                return null;

            Mapper.CreateMap<GuardPayment, GuardPaymentViewModel>();
            return Mapper.Map<GuardPayment, GuardPaymentViewModel>(customer);
        }

        public int? CreateUpdateGuardPayment(GuardPaymentViewModel guardPaymentViewModel)
        {
            GuardPayment guardPayment = null;

            if (guardPaymentViewModel.GuardPaymentId > 0)
            {
                guardPayment = _repository.Find<GuardPayment>(x => x.GuardPaymentId == guardPaymentViewModel.GuardPaymentId);
                if (guardPayment == null)
                    return null;

                guardPayment.GuardId = guardPaymentViewModel.GuardId;
                guardPayment.PaymentDate = guardPaymentViewModel.PaymentDate;
                guardPayment.StartDate = guardPaymentViewModel.StartDate;
                guardPayment.EndDate = guardPaymentViewModel.EndDate;
                guardPayment.Amount = guardPaymentViewModel.Amount;
                guardPayment.TotalHours = guardPaymentViewModel.TotalHours;
                guardPayment.HourlyRate = guardPaymentViewModel.HourlyRate;
                guardPayment.Comments = guardPaymentViewModel.Comments;
                guardPayment.ModifiedBy = guardPaymentViewModel.ModifiedBy;
                guardPayment.ModifiedDate = DateTime.Now;

                _repository.Modify<GuardPayment>(guardPayment);
                return guardPayment.GuardPaymentId;
            }

            Mapper.CreateMap<GuardPaymentViewModel, GuardPayment>();
            guardPayment = Mapper.Map<GuardPaymentViewModel, GuardPayment>(guardPaymentViewModel);

            guardPayment.CreatedDate = DateTime.Now;
            guardPayment.CreatedBy = guardPaymentViewModel.CreatedBy;
            guardPayment.IsDeleted = false;
            return _repository.Insert<GuardPayment>(guardPayment);
        }

        public int DeleteGuardPayment(int id, int UserId)
        {
            GuardPayment guardPayment = _repository.Find<GuardPayment>(x => x.GuardPaymentId == id);
            if (guardPayment == null)
                return 0;
            guardPayment.IsDeleted = true;
            guardPayment.DeletedBy = UserId;
            guardPayment.DeletedDate = DateTime.Now;
            return _repository.Modify<GuardPayment>(guardPayment);
        }

        public GuardPaymentViewModel GetPaymentAmount(string startDate, string endDate, int guardId)
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                DateTime _startDate = Convert.ToDateTime(startDate);
                DateTime _endDate = Convert.ToDateTime(endDate);
                //Get Guard Hourly Rate
                //GuardHourlyRate objectGuardHourlyRate = _repository.GetAll<GuardHourlyRate>().Where(x => x.GuardId == guardId).OrderByDescending(i => i.HourlyRateId).FirstOrDefault();
                //Get Last Start and End Date
                GuardPayment objectGuardPayment = objContext.GuardPayments.Where(i => i.GuardId == guardId && i.IsDeleted == false).OrderByDescending(i => i.EndDate).FirstOrDefault();
                Guard objectGuard = objContext.Guards.Where(i => i.GuardId == guardId).FirstOrDefault();
                if (objectGuardPayment != null)
                {
                    _startDate = objectGuardPayment.EndDate;
                    _endDate = objectGuardPayment.EndDate.AddDays(7);
                }
                List<GuardPaymentViewModel> guardPaymentViewModel = (from dailyLog in objContext.DailyLogs                                                                
                                                                 where (dailyLog.Dated > _startDate && dailyLog.Dated < _endDate)
                                                                 && (dailyLog.IsDeleted == false)
                                                                 && dailyLog.GuardId == guardId
                                                                 select new GuardPaymentViewModel
                                                                {
                                                                    TotalHours = dailyLog.Hours,
                                                                }).ToList();

                if (guardPaymentViewModel == null)
                    return null;

                GuardPaymentViewModel objectGuardPaymentViewModel = new GuardPaymentViewModel();
                objectGuardPaymentViewModel.HourlyRate = objectGuard.HourlyRate;
                objectGuardPaymentViewModel.TotalHours = guardPaymentViewModel.Sum(i => i.TotalHours);
                objectGuardPaymentViewModel.Amount = objectGuardPaymentViewModel.TotalHours * objectGuard.HourlyRate;
                objectGuardPaymentViewModel.StartDate = _startDate;
                objectGuardPaymentViewModel.EndDate = _endDate;
                return objectGuardPaymentViewModel;
            }


        }
        public List<GuardPaymentViewModel> GetGuardPaymentReport(int GuardId)
        {
            using (SecurityAgencyEntities objSecurityAgencyEntities = new SecurityAgencyEntities())
            {
                List<getGuardPayment_Result> objectGuard = objSecurityAgencyEntities.getGuardPayment().ToList();

                if (objectGuard == null)
                    return null;

                if (GuardId!=0)
                {
                    objectGuard = objectGuard.Where(i => i.Guardid == GuardId).ToList();
                }
                

                Mapper.CreateMap<getGuardPayment_Result, GuardPaymentViewModel>();
                return Mapper.Map<List<getGuardPayment_Result>, List<GuardPaymentViewModel>>(objectGuard);
            }
        }
    }
}