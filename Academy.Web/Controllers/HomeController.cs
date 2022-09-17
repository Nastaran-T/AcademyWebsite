using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Academy.Core.Services.Interfaces;

namespace Academy.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService;
        private ICourseService _courseService;
        public HomeController(IUserService userService,ICourseService courseService)
        {
            _userService = userService;
            _courseService = courseService;
        }

        //نمایش دوره ها
        public IActionResult Index()
        {
            /*این متد چون به یک tuple تبدیل شده 
             * بخاطر همین ارور میده باید به حالت تاپل کنیمش
             * یا سمت ویو مدل را به صورت زیر میکنیم
             * @model Tuple<List<Academy.Core.DTOs.Course.ShowListCourseViewModel>,int>
             * و یه حالت دیگه ام که اینجا من نوشتم
             * _courseService.GetCourse() .Item1
             * چونکه در این بخش نیاز به صفحه بندی نداشتم این جوری نوشتم و در سمت ویو هم همون جوری بمونه
             */
            ViewBag.papular = _courseService.ShowPopularCourses();
            return View(_courseService.GetCourse() .Item1);
        }



        #region OnlinePayment
        [Route("OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"] == "OK" && HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"];

                var wallet = _userService.GetWalletById(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    //کد پیگیری 
                    ViewBag.code = res.RefId;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    _userService.UpdateWallet(wallet);

                }

            }
            return View();
        }
        #endregion

        #region load subgroup with ajax 
        //برای کلیک روی هر گروه یتوانیم زیر گروه ها را با ایجکس لود کنیم بنابراین نیاز به اکشنی داریم که اطلاعات بهصورت جی سون بهمون برگردونه

        public IActionResult GetSubGroups(int id) 
        {
            List<SelectListItem> selectLists = new List<SelectListItem>()
            {
                new SelectListItem(){Text="انتخاب کنید..." ,Value=""}
            };

            selectLists.AddRange(_courseService.GetSubGroupForManageCourse(id));
            return Json(new SelectList(selectLists,"Value","Text"));
        }

        #endregion


        #region Save Images that choise in ckEditor
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MyImages", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }

        #endregion
    }
}