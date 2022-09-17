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
    [PermissionChecker(5)]
    public class DeleteUserModel : PageModel
    {
        private IUserService _userService;
        public DeleteUserModel(IUserService userService)
        {
            _userService = userService;
        }

        public InformationUserViewModel informationUserViewModel{get;set;}
    

        public void OnGet(int id)
        {
            informationUserViewModel = _userService.GetInformationUserPanel(id);
            ViewData["UserId"] = id;
        }
        public IActionResult OnPost(int UserId) 
        {
            _userService.DeleteUser(UserId);
            return RedirectToPage("Index");
        }
    }
}
