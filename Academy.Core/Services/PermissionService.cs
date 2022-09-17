using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Context;
using Academy.DataLayer.Entities.Permission;
using Academy.DataLayer.Entities.User;

namespace Academy.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private AcademyContext _context;
        public PermissionService(AcademyContext context)
        {
            _context = context;
        }

        #region CreateUser
        public void AddRolesForUser(int userId, List<int> roleIds)
        {
            //به ازای هر یوزر آی دی امکان دارد بیش از یک نقش وجود داشته باشد بنابراین از حلقه استفاده میکنیم
            foreach (var Roles in roleIds)
            {
                _context.UserRoles.Add(new UserRole
                {

                    UserID = userId,
                    RoleID = Roles
                });
            }
            _context.SaveChanges();
        }
        #endregion
        public void AddPermissionToRole(int roleId, List<int> permissions)
        {
            //ما اینجا برای اضافه کردن کلا هر چیزی پونکه یه نقش چند تا رول داره بنابراین از foreach استفاده میکنیم

            foreach (var p in permissions)
            {
                _context.RolePermissions.Add(new RolePermission()
                {

                    RoleID = roleId,
                    PermissionId = p
                });

                _context.SaveChanges();
            }
        }

        public int AddRole(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
            return role.RoleID;
        }


        public bool checkPermissionUser(int permissionId, string userName)
        {
            int userId = _context.Users.Single(u => u.UserName == userName).UserID;
            //نقش های یوزر رو میخوام
            List<int> userRole = _context.UserRoles.Where(r => r.UserID == userId).Select(r => r.RoleID).ToList();

            //یعنی اگه کاربر نقشی نداشت
            if (!userRole.Any())
                return false;


            //دسترسی های هر نقش میخوام
            List<int> permissionRole = _context.RolePermissions.Where(p => p.PermissionId == permissionId).Select(p => p.RoleID).ToList();

            //حالا باید چک کنیم کنیم که این رول های پرمیشن شامل رول های یوزر میشن
            return permissionRole.Any(p=>userRole.Contains(p));
            //return permissionRole.Any(p=> EF.Functions.Like(userRole, "%" + p + "%"));

        }

        public void DeleteRole(Role role)
        {
            role.IsDelete = true;
            UpdateRole(role);
        }

        public void EditRolesUser(int userId, List<int> roleIds)
        {
            //Delete All Role User
            _context.UserRoles.Where(r => r.UserID == userId).ToList().ForEach(r => _context.UserRoles.Remove(r));

            //Add new Role
            AddRolesForUser(userId, roleIds);
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public Role GetRoleByRoleId(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public List<int> GetRolPermissions(int roleId)
        {
            return _context.RolePermissions.Where(r => r.RoleID == roleId).Select(r=>r.PermissionId).ToList();
        }

        public void UpdatePermisionsRole(int roleId, List<int> permissions)
        {
            _context.RolePermissions.Where(r => r.RoleID == roleId).ToList()
                .ForEach(p => _context.RolePermissions.Remove(p));

            AddPermissionToRole(roleId, permissions);
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        }
    }
}
