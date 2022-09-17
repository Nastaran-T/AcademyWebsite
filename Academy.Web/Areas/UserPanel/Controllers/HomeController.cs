using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Academy.Core.DTOs;
using Academy.Core.Services.Interfaces;

namespace Academy.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]  //یعنی کاربر من لاگین باید باشه تا به این صفحه دسترسی داشته باشه
    public class HomeController : Controller
    {
        private IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

      

        public IActionResult Index()
        {
            return View(_userService.GetInformationUserPanel(User.Identity.Name));
        }

        #region Edit UserProfile
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetDataforEditProfile(User.Identity.Name));
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditUserPanelViewModel editUserPanel)
        {
            if (!ModelState.IsValid)
            
                return View(editUserPanel);

           
            _userService.EditProfile(User.Identity.Name, editUserPanel);

          //  ViewBag.isSuccess = true;

            //Logout
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //برای اینکه یک علامتی باشه نمایش بدیم به کاربر.. که ویرایش شده و کاربر باید مجدد لاگین کند
            return Redirect("/Login?EditProfile=true");

        }
        #endregion

        #region ChangePassword UserPanel
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordUserPanelViewModel changePassword)
        {
            if (!ModelState.IsValid)
                return View(changePassword);

            if (!_userService.CompareOldPassword(User.Identity.Name,changePassword.OldPassword))
            {
                ModelState.AddModelError("OldPassword","کلمه عبور فعلی درست نمی باشد");
                return View(changePassword);
            }

            _userService.ChangeUserPassword(User.Identity.Name,changePassword.NewPassword);
            ViewBag.IsSuccess = true;
            return View();
        }
        #endregion
    }
}