using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Security;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Permission;
using Academy.DataLayer.Entities.User;

namespace Academy.Web.Pages.Admin.manageRoles
{ 
    [PermissionChecker(7)]
    public class CreateRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public CreateRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
       
        [BindProperty]
        public Role Roles { get; set; }

        public void OnGet()
        {
            ViewData["Permissions"] = _permissionService.GetPermissions();
        }

        public IActionResult OnPost(List<int> SelectedPermissions) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //add a role in table
            Roles.IsDelete = false;
            int roleId = _permissionService.AddRole(Roles);

            //add permission for role
            _permissionService.AddPermissionToRole(roleId, SelectedPermissions);

            return RedirectToPage("Index");



        }
    }
}
