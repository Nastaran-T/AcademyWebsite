using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Security;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.User;

namespace Academy.Web.Pages.Admin.manageRoles
{
    [PermissionChecker(8)]
    public class EditRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public EditRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleByRoleId(id);
            ViewData["Permissions"] = _permissionService.GetPermissions();
            ViewData["SelectedPermissions"] = _permissionService.GetRolPermissions(id);
        }

        public IActionResult OnPost(List<int> SelectedPermissions) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //edit role
            _permissionService.UpdateRole(Role);

            //edit permission for role
            _permissionService.UpdatePermisionsRole(Role.RoleID,SelectedPermissions);

            return RedirectToPage("Index");
        }
    }
}
