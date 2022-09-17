using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Academy.Core.Convertor;
using Academy.Core.Convertor.SendEmail;
using Academy.Core.DTOs;
using Academy.Core.Generator;
using Academy.Core.Sender.SendEmail;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.User;
using Academy.Core.Security;

namespace Academy.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;
        private IViewRenderService _viewRenderService;
        public AccountController(IUserService userService, IViewRenderService viewRenderService)
        {
            _userService = userService;
            _viewRenderService = viewRenderService;
        }

        #region Register

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            //چک کردن یوزر نیم که تکراری نباشد
            if (_userService.IsExistUserName(register.UserName))
            {
                ModelState.AddModelError("UserName", "نام کاربری معتبر نمی باشد");
                return View(register);
            }

            //چک کردن ایمیل کاربر که تکراری نباشد
            if (_userService.IsExistEmail(FixedText.FixEmail(register.Email)))
            {
                ModelState.AddModelError("Email", "ایمیل معتبر نمی باشد");
                return View(register);
            }

            User user = new User()
            {
                ActiveCode = NameGenerator.CodeGenarator(),
                Email = FixedText.FixEmail(register.Email),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                RegisterDate = DateTime.Now,
                UserAvatar = "Default.png",
                UserName = register.UserName


            };
            _userService.AddorUpdateUser(user);

            #region Send Email Activation
            string body = _viewRenderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی", body);

            #endregion

            return View("SuccessRegister", user);
            /*اینجا این user
             * مدلی است که دارم برای ویو میفرستم 
            */
        }
        #endregion

        #region ActiveAccount
        /*این آی دی ورودی در واقع همون اکتیو کد است که از یو آز ال دریافت میشه
         * بخاطر همین اسمشو گذاشتم 
         * id
         */
        public IActionResult ActiveAccount(string id)
        {
            ViewBag.IsActiveAccount = _userService.ActiveAccount(id);
            return View();
        }
        #endregion

        #region Login
        
        [Route("Login")]
        public IActionResult Login(bool EditProfile = false)
        {
            ViewBag.EditProfile = EditProfile;
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login,string returnUrl="/")
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            //اینجا باید حتما کاربر فعال باشد وگرنه نمیتونه ورود به سایت کنه
            User user = _userService.LoginUser(login);

            if (user != null)
            {
                if (user.IsActive)
                {
                    //کد های بخش لاگین
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier,user.UserID.ToString()),
                        new Claim(ClaimTypes.Name,user.UserName)
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties()
                    {
                        //فقط من را به خاطر بسپار در نظر داشته باش
                        IsPersistent = login.RememberMe
                    };

                    //این خط عمل لاگین انجام میده
                    HttpContext.SignInAsync(principal, properties);

                    //این ویو بگ در واقع علامتی است برای ما که سمت کاربر میخواهیم پیغامی نمایش دهیم و بعد از چند ثانیه به صفحه اصلی منتقل بشیم
                    ViewBag.IsSuccess = true;
                    if (returnUrl!="/")
                    {
                        return Redirect(returnUrl);
                    }
                    // return Redirect("/UserPanel");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("Email", "حساب کاربری فعال نمی باشد");
                    return View(login);
                }

            }
            else
            {
                ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
                return View(login);
            }

        }
        #endregion

        #region Logout
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Login");
        }
        #endregion

        #region Forgot Password
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }

            //ایمیل وارد شده توسط کاربر فیکس بشه
            string FixedEmail = FixedText.FixEmail(forgot.Email);
            User user = _userService.GetUserByEmail(FixedEmail);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربری یافت نشد");
                return View(forgot);
            }

            //ارسال ایمیل برای کاربر
            string bodyEmail = _viewRenderService.RenderToStringAsync("_ForgotPassword", user);
            SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);

            ViewBag.IsActive = true;

            return View();
        }
        #endregion

        #region Resert Password
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel() { ActiveCode = id });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)

                return View(reset);


            User user = _userService.GetUserByActiveCode(reset.ActiveCode);

            if (user == null)
            {
                return NotFound();
            }

            string HashPassword = PasswordHelper.EncodePasswordMd5(reset.Password);

            user.Password = HashPassword;
            _userService.UpdateUser(user);

            return Redirect("/Login");



        }
        #endregion
    }
}