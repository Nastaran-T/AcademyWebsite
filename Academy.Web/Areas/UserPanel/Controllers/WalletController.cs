using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Academy.Core.DTOs;
using Academy.Core.Services.Interfaces;

namespace Academy.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }


        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            //در اینجا ویو ما یک مدل دارد برای شارژ کیف پول بنابراین اینجا این یکی مدل را به صورت ویو بگ در آوردیم
            ViewBag.ListWalletUser = _userService.GetWalletUser(User.Identity.Name);
            return View();
        }



        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(ShargeWalletViewModel sharge)
        {
            if (!ModelState.IsValid)
            {
                //بخاطره وجود لیست تراکنش ها
                ViewBag.ListWallet = _userService.GetWalletUser(User.Identity.Name);
                return View(sharge);
            }

            //اضافه کردن شارژ به جدول
           int walletId= _userService.ShargeWallet(User.Identity.Name, sharge.Amount, "شارژ حساب");

            #region ZarinPal
            //درگاه پرداخت
            var payment = new ZarinpalSandbox.Payment(sharge.Amount);
            //باید از درگاه یه درخواست بگیریم یعنی اوکی بده به ما
            var res = payment.PaymentRequest("شارژ کیف پول", "https://localhost:44340/OnlinePayment/" + walletId, "nilootouzandejani2020@gmail.com", "09354879855");
            //حالا باید وضعیت ارسالی درگاه را بررسی کنید بعد کاربر را به سمت درگاه بفرستیم
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            #endregion

            return null;
            
        }
    }
}