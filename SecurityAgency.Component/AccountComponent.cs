using SecurityAgency.Common.Interfaces;
using SecurityAgency.Common.Models;
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

    public class AccountComponent: IAccount
    {
        /// <summary>
        /// Initilize Referance of IDbRepository
        /// </summary>
        IDbRepository _repository;

        public AccountComponent(IDbRepository repository)
        {
            _repository = repository;
        }

        public ActiveUser CheckLogin(UserViewModel userModel)
        {

            var userData = _repository.Find<User>(x => x.UserName == userModel.UserName && x.Password== userModel.Password && x.Active == true);
            if (userData == null)
                return null;


            

            ActiveUser activeUser = new ActiveUser()
            {
                UserId = userData.UserID,
                Role = userData.RoleId,
                UserName = userData.UserName,
               
            };

            return activeUser;
        }
    }
}
