using SecurityAgency.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;



namespace SecurityAgency.Common.Interfaces
{
   public interface IUser
    {
      
        List<UserViewModel> GetAllUser();
        UserViewModel GetUser(int id);
        int? CreateUpdateUser(UserViewModel userViewModel);
        int DeleteUser(int id,int UserId);
        int DeleteRole(int id, int UserId);
        List<RoleViewModel> GetAllRole();
        List<PermissionViewModel> GetAllModules();
        List<PermissionViewModel> GetAllPermissions();
        List<PermissionViewModel> GetPermissionForRoles(int RoleId);
        void AddRolePermissions(int RoleId, string Role, List<string> Permissions, int UserId);
    }
}
