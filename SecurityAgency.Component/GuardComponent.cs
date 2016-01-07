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
    public class GuardComponent : IGuard
    {

         /// <summary>
        /// Initilize Referance of IDbRepository
        /// </summary>
        IDbRepository _repository = null;

        /// <summary>
        /// Assign  IDbRepository
        /// </summary>
        /// <param name="repositry">Refrence of IDbRepository</param>
        public GuardComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<GuardViewModel> GetAllGaurd()
        {
            List<Guard> gaurds = _repository.GetAll<Guard>().Where(x => x.Active==true).ToList();

            if (gaurds == null)
                return null;

            Mapper.CreateMap<Guard, GuardViewModel>().ForMember(dest => dest.NameSSN,
               opts => opts.MapFrom(
                   src => string.Format("{0} ({1})",
                       src.Name,
                       src.SSN)));

            return Mapper.Map<List<Guard>, List<GuardViewModel>>(gaurds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GuardViewModel GetGaurd(int id)
        {
            Guard guard =  _repository.Find<Guard>(x => x.GuardId == id);

            if (guard == null)
                return null;

            Mapper.CreateMap<Guard, GuardViewModel>();
            return Mapper.Map<Guard, GuardViewModel>(guard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gaurdViewModel"></param>
        /// <returns></returns>
        public int? CreateUpdateGuard(GuardViewModel gaurdViewModel)
        {
            Guard guard = null;
            if (gaurdViewModel.GuardId > 0)
            {
                guard = _repository.Find<Guard>(x => x.GuardId == gaurdViewModel.GuardId);
                if (guard == null)
                    return null;
                
                guard.Name = gaurdViewModel.Name;
                guard.SSN = gaurdViewModel.SSN;
                guard.Address = gaurdViewModel.Address;
                guard.ContactNo = gaurdViewModel.ContactNo;
                guard.HourlyRate = gaurdViewModel.HourlyRate;
                guard.Comments = gaurdViewModel.Comments;
                guard.Zip = gaurdViewModel.Zip;
                guard.ModifiedBy = gaurdViewModel.ModifiedBy;
                guard.ModifiedDate= DateTime.Now;
              
               _repository.Modify<Guard>(guard);
                return guard.GuardId;
            }

            Mapper.CreateMap<GuardViewModel, Guard>();
            guard = Mapper.Map<GuardViewModel, Guard>(gaurdViewModel);

            guard.CreatedDate = DateTime.Now;
            guard.CreatedBy = gaurdViewModel.CreatedBy;
            guard.Active= true;
            guard.IsDeleted = false;
            return  _repository.Insert<Guard>(guard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteGuard(int id,int UserId)
        {
            Guard guard =  _repository.Find<Guard>(x => x.GuardId == id);
            if (guard == null)
                return 0;
            guard.IsDeleted = true;
            guard.DeletedBy = UserId;
            guard.Active = false;
            guard.DeletedDate = DateTime.Now;
            return _repository.Modify<Guard>(guard);

            
        }
        public List<GuardViewModel> GetGuardForReport(string startDate, string endDate)
        {
            DateTime? _startDate = null;
            DateTime? _endDate = null;
            if (startDate != "") { _startDate = Convert.ToDateTime(startDate); };
            if (endDate != "") { _endDate = Convert.ToDateTime(endDate); };
            SecurityAgencyEntities objEntities = new SecurityAgencyEntities();
            List<getAllGuards_Result> guards = objEntities.getAllGuards(_startDate, _endDate).ToList();

            if (guards == null)
                return null;

            Mapper.CreateMap<getAllGuards_Result, GuardViewModel>();
            return Mapper.Map<List<getAllGuards_Result>, List<GuardViewModel>>(guards);
        }
        public bool validateGuardSSN(int guardId, string SSN)
        {
            Guard customer = _repository.Find<Guard>(x => x.GuardId != guardId && x.SSN == SSN);
            if (customer == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
