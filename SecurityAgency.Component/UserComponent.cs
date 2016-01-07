using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityAgency.Common.Interfaces;
using SecurityAgency.Repository;
using SecurityAgency.Repository.DbServices;
using SecurityAgency.Common.ViewModels;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace SecurityAgency.Component
{
    public class UserComponent : IUser
    {
        IDbRepository _repository = null;

        public UserComponent(IDbRepository repository)
        {
            _repository = repository;

        }


        public List<UserViewModel> GetAllUser()
        {
            List<User> user = _repository.GetAll<User>().Where(x => x.Active == true).ToList();

            if (user == null)
                return null;

            Mapper.CreateMap<User, UserViewModel>();

            return Mapper.Map<List<User>, List<UserViewModel>>(user);
        }

        public UserViewModel GetUser(int id)
        {
            User user = _repository.Find<User>(x => x.UserID == id);

            if (user == null)
                return null;

            Mapper.CreateMap<User, UserViewModel>();
            return Mapper.Map<User, UserViewModel>(user);
        }

        public int? CreateUpdateUser(UserViewModel userViewModel)
        {
            User user = null;

            if (userViewModel.UserId > 0)
            {
                user = _repository.Find<User>(x => x.UserID == userViewModel.UserId);
                if (user == null)
                    return null;

                user.UserName = userViewModel.UserName;
                user.Email = userViewModel.Email;
                user.Address = userViewModel.Address;
                user.Password = userViewModel.Password;
                user.ContactNo = userViewModel.ContactNo;
                user.Zip = userViewModel.Zip;
                user.RoleId = userViewModel.RoleId;
                user.ModifiedBy = userViewModel.Last_ModifiedBy;
                user.ModifiedDate = DateTime.Now;

                _repository.Modify<User>(user);
                return user.UserID;
            }

            Mapper.CreateMap<UserViewModel, User>();
            user = Mapper.Map<UserViewModel, User>(userViewModel);

            user.CreatedDate = DateTime.Now;
            user.CreatedBy = userViewModel.CreatedBy;
            user.Active = true;
            user.IsDeleted = false;

            return _repository.Insert<User>(user);
        }

        public int DeleteUser(int id, int UserId)
        {
            User user = _repository.Find<User>(x => x.UserID == id);
            if (user == null)
                return 0;
            user.IsDeleted = true;
            user.DeletedBy = UserId;
            user.Active = false;
            user.DeletedDate = DateTime.Now;
            return _repository.Modify<User>(user);
        }

        public int DeleteRole(int id, int UserId)
        {
            Role user = _repository.Find<Role>(x => x.RoleId == id);
            if (user == null)
                return 0;
            user.IsDeleted = true;
            user.DeletedBy = UserId;
            user.Active = false;
            user.DeletedDate = DateTime.Now;
            return _repository.Modify<Role>(user);
        }
        public List<RoleViewModel> GetAllRole()
        {
            List<Role> roleList = _repository.GetAll<Role>().Where(i=>i.IsDeleted==false).ToList();

            if (roleList == null)
                return null;

            Mapper.CreateMap<Role, RoleViewModel>();

            return Mapper.Map<List<Role>, List<RoleViewModel>>(roleList);
        }
        public List<PermissionViewModel> GetAllModules()
        {
            List<Module> modulelist = _repository.GetAll<Module>().ToList();

            if (modulelist == null)
                return null;

            Mapper.CreateMap<Module, PermissionViewModel>();

            return Mapper.Map<List<Module>, List<PermissionViewModel>>(modulelist);
        }
        public List<PermissionViewModel> GetAllPermissions()
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                List<getRolePermissions_Result> permissionlist = objContext.getRolePermissions().ToList();

                if (permissionlist == null)
                    return null;

                Mapper.CreateMap<getRolePermissions_Result, PermissionViewModel>();

                return Mapper.Map<List<getRolePermissions_Result>, List<PermissionViewModel>>(permissionlist);
            }
        }
        public List<PermissionViewModel> GetPermissionForRoles(int RoleId)
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                List<RolePermission> permissionlist = _repository.GetAll<RolePermission>().Where(i=>i.RoleId==RoleId && i.IsDeleted==false).ToList();

                if (permissionlist == null)
                    return null;

                Mapper.CreateMap<RolePermission, PermissionViewModel>();

                return Mapper.Map<List<RolePermission>, List<PermissionViewModel>>(permissionlist);
            }
        }

        public int CreateRole(RoleViewModel roleViewModel)
        {
            Role role = null;

            if (roleViewModel.RoleId > 0)
            {
                role = _repository.Find<Role>(x => x.RoleId == roleViewModel.RoleId);
                if (role == null)
                    return 0;

                role.Role1 = roleViewModel.Role1;
                role.ModifiedBy = roleViewModel.Last_ModifiedBy;
                role.ModifiedDate = DateTime.Now;

                _repository.Modify<Role>(role);
                return role.RoleId;
            }

            Mapper.CreateMap<RoleViewModel, Role>();
            role = Mapper.Map<RoleViewModel, Role>(roleViewModel);

            role.CreatedDate = DateTime.Now;
            role.CreatedBy = roleViewModel.CreatedBy;
            role.Active = true;
            role.IsDeleted = false;

            _repository.Insert<Role>(role);
            return role.RoleId;
        }
        public void AddRolePermissions(int RoleId, string Role, List<string> Permissions, int UserId)
        {
            using (SecurityAgencyEntities objContext = new SecurityAgencyEntities())
            {
                RoleViewModel objectRoleViewModel = new RoleViewModel();

                objectRoleViewModel.RoleId = RoleId;
                objectRoleViewModel.Role1 = Role;
                objectRoleViewModel.Last_ModifiedBy = objectRoleViewModel.CreatedBy = UserId;
                objectRoleViewModel.Last_ModifiedDate = objectRoleViewModel.CreatedDate = DateTime.Now;
                objectRoleViewModel.IsDeleted = false;
                objectRoleViewModel.Active = true;
                RoleId = CreateRole(objectRoleViewModel);

                //Removing all previous permissions
                List<RolePermission> objectRolePermission = objContext.RolePermissions.Where(i => i.RoleId == RoleId).ToList();
                objContext.RolePermissions.RemoveRange(objectRolePermission);

                foreach (string strPermission in Permissions)
                {
                    RolePermission objectRolePermissionNew = new RolePermission();
                    objectRolePermissionNew.RoleId = RoleId;
                    objectRolePermissionNew.PermissionId = Convert.ToInt32(strPermission.Replace("chk_", ""));
                    objectRolePermissionNew.CreatedBy = UserId;
                    objectRolePermissionNew.CreatedDate = DateTime.Now;
                    objectRolePermissionNew.IsDeleted = false;
                    objContext.RolePermissions.Add(objectRolePermissionNew);
                }
                objContext.SaveChanges();
            }
        }
    }
}


