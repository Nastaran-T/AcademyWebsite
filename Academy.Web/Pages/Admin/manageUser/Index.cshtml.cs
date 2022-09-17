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
    [PermissionChecker(2)]
    public class IndexModel : PageModel
    {
        //injection
        private IUserService _userService;
        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserForAdminViewModel userForAdminViewModel{ get; set; }
        public void OnGet(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            userForAdminViewModel = _userService.GetAllUser(pageId, filterEmail, filterUserName);
        }
    }
}