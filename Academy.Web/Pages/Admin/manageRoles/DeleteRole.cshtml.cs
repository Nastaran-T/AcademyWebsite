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
    [PermissionChecker(9)]
    public class DeleteRoleModel : PageModel
    {
        private IPermissionService _permissionService;
        public DeleteRoleModel(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role = _permissionService.GetRoleByRoleId(id);
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //delete role
            _permissionService.DeleteRole(Role);

            return RedirectToPage("Index");
        }
    }
}
