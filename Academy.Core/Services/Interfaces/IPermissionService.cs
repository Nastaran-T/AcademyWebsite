using System;
using System.Collections.Generic;
using System.Text;
using Academy.DataLayer.Entities.Permission;
using Academy.DataLayer.Entities.User;

namespace Academy.Core.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Role
        List<Role> GetRoles();
        Role GetRoleByRoleId(int roleId);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        int AddRole(Role role);
        void AddRolesForUser(int userId, List<int> roleIds);
        void EditRolesUser(int userId,List<int> roleIds);
        #endregion
        #region Permission
        List<Permission> GetPermissions();
        void AddPermissionToRole(int roleId,List<int> permissions);
        List<int> GetRolPermissions(int roleId);
        void UpdatePermisionsRole(int roleId,List<int> permissions);
        bool checkPermissionUser(int permissionId,string userName);
        #endregion
    }
}
