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
   public class PermissionComponent:IPermission
    {
        /// <summary>
        /// Initilize Referance of IDbRepository
        /// </summary>
        IDbRepository _repository = null;
       /// <summary>
        /// Assign  IDbRepository
        /// </summary>
        /// <param name="repositry">Refrence of IDbRepository</param>
        public PermissionComponent(IDbRepository repository)
        {
            _repository = repository;
        }


       public List<PermissionViewModel> GetAllRoles()
       {
           List<Role> Rolelist = _repository.GetAll<Role>().ToList();

           if (Rolelist == null)
               return null;

           Mapper.CreateMap<Role, PermissionViewModel>();

           return Mapper.Map<List<Role>, List<PermissionViewModel>>(Rolelist);
       }

       

    }
}
