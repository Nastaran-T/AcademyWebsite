using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.DTOs;
using Academy.Core.Security;
using Academy.Core.Services.Interfaces;

namespace Academy.Web.Pages.Admin.manageUser
{
    [PermissionChecker(4)]
    public class EditUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public EditUserModel(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        [BindProperty]
        public EditUserViewModel editUserViewModel { get; set; }

        public void OnGet(int id)
        {
            //چیدمان المان ها 
            ViewData["Roles"] = _permissionService.GetRoles();
            editUserViewModel = _userService.GetUserForEditMode(id);
        }

        public IActionResult OnPost(List<int> SelectedRoles) 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Edit User
            _userService.EditUserFromAdmin(editUserViewModel);

            //EditRole
            _permissionService.EditRolesUser(editUserViewModel.UserId, SelectedRoles);

            return RedirectToPage("Index");
            
        }
    }
}
