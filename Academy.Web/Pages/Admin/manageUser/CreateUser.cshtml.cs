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
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public CreateUserModel(IUserService userService,IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        //این خصوصیت که در فرم های ایجاد اضافه می شود برای این است که اگر مدل را تغییر دهیم خودش اتوماتیک این تغیییر را اعمال میکند
        [BindProperty]
        public CreateUserViewModel createUserViewModel{ get; set; }


        public void OnGet()
        {
            ViewData["Roles"]= _permissionService.GetRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles) 
        {
            if (!ModelState.IsValid)
                return Page();

            int userId = _userService.AddUserFromAdmin(createUserViewModel);

            //add in RoleUser table
            _permissionService.AddRolesForUser(userId,SelectedRoles);

            return Redirect("/Admin/manageUser");

        }
    }
}