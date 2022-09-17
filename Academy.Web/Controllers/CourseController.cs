using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Academy.Core.Services;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Course;
using SharpCompress.Archives;

namespace Academy.Web.Controllers
{
    public class CourseController : Controller
    {
        private ICourseService _courseService;
        private IOrderService _orderService;
        private IUserService _userService;
        public CourseController(ICourseService courseService, IOrderService orderService,IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;

        }
        public IActionResult Index(int pageId = 1, string filter = ""
          , string getType = "all", string orderByType = "date",
          int startPrice = 0, int endPrice = 0, List<int> selectedGroups = null)
        {
            //برای نمایش گروه ها نیاز داریم گروه ها را بدست بیاریم
            ViewData["groups"] = _courseService.GetAllCourses();
            ViewData["selected"] = selectedGroups;
            ViewData["pagging"] = pageId;

            return View(_courseService.GetCourse(pageId, filter, getType, orderByType, startPrice, endPrice, selectedGroups, 9));
        }

        public IActionResult ShowCourse(int id,int episode=0)
        {
            var course = _courseService.GetCourseForShow(id);
            if (course == null)
            {
                return NotFound();
            }

            //بررسی اپیزد
            if (episode != 0 && User.Identity.IsAuthenticated)
            {
                if (course.CourseEpisodes.Any(e=>e.EpisodeId!=episode))
                {
                    return NotFound();
                }
            }
            //اگر اپیزود ما رایگان نبود
            if (!course.CourseEpisodes.First(e=>e.EpisodeId==episode).IsFree)
            {
                if (!_orderService.IsUserInCourse(User.Identity.Name, id))
                {
                    return NotFound();
                }
            }

            var Episode = course.CourseEpisodes.SingleOrDefault(c => c.EpisodeId == episode);
            ViewBag.fileEpisode = Episode;
            string filePath = "";
            string checkFilePath = "";
            if (Episode.IsFree)
            {
                //در اینجا برای دخیره ویدیو اومدیم از دل فایل زیپ شذه ویدیو کشیدیم بیرون با متد replace
                //filePath = Path.Combine(filePath, "/wwwroot/CourseOnlineFree", Episode.EpisodeFileName.Replace(".rer", ".mp4"));
                filePath = "/CourseOnlineFree/"+Episode.EpisodeFileName.Replace(".rar",".mp4");
                checkFilePath = Path.Combine(Directory.GetCurrentDirectory(),"/wwwroot/"+filePath);
            }
            else
            {
                filePath = "/CourseOnline/" + Episode.EpisodeFileName.Replace(".rar",".mp4");
                checkFilePath = Path.Combine(Directory.GetCurrentDirectory(),"/wwwroot/"+filePath);
            }

            if (!System.IO.File.Exists(checkFilePath))
            {
                //اگر فایل هم وجود نداشت بره بسازه
                //استخراج فیلم از داخل rar

                string targetPath = Directory.GetCurrentDirectory();
                if (Episode.IsFree)
                {
                    //برو فایل را تو این مسیر ایجاد کن
                    targetPath = Path.Combine(targetPath, "wwwroot/CourseOnlineFree");
                }
                else
                {
                    targetPath = Path.Combine(targetPath, "wwwroot/CourseOnline");
                }

                //اینجا فایل rar 
                //را از قسمت فایل ها پیدا می کنیم
                string rarFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles",Episode.EpisodeFileName);
                //حالا با دستور زیر فایل رر را از زیپ خارج کرده
                var archive = ArchiveFactory.Open(rarFile);
                //حالا اینجا فایل راز شده را مرتب میکنیم براساس یه چیزی که ما اینجا براساس طول نام فایل اینکارو کردیم میشه بر اساس چیز دیگه ام مرتب کرد
                var entries = archive.Entries.OrderBy(a=>a.Key.Length);
                //اینجا در اون فایل باید فقط فایل فیلم را جدا کنیم چون امکان داره به غیر فیلم فایل های دیگه ای باشه
                foreach (var ep in entries)
                {
                    if (Path.GetExtension(ep.Key)==".mp4")
                    {
                        ep.WriteTo(System.IO.File.Create(Path.Combine( targetPath,Episode.EpisodeFileName.Replace(".rar",".mp4"))));
                    }
                }
            }

            ViewBag.filePath = filePath;





            return View(course);
        }

        [Authorize]
        public IActionResult BuyCourse(int id)
        {
            int orderId = _orderService.AddOrder(User.Identity.Name, id);
            return Redirect("/UserPanel/MyOrder/ShowOrder/" + orderId);
        }

        [Route("Download/{episodeId}")]
        public IActionResult DownloadFile(int episodeId)
        {
            var episode = _courseService.GetCourseEpisodeById(episodeId);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles", episode.EpisodeFileName);
            string fileName = episode.EpisodeFileName;
            if (episode.IsFree)
            {
                //کد دانلود فایل
                byte[] file = System.IO.File.ReadAllBytes(filePath);
                return File(file, "application/force-download", fileName);

            }

            if (User.Identity.IsAuthenticated)
            {
                if (_orderService.IsUserInCourse(User.Identity.Name, episode.CourseId))
                {
                    byte[] file = System.IO.File.ReadAllBytes(filePath);
                    return File(file, "application/force-download", fileName);
                }

            }

            return Forbid();

        }

        [HttpPost]
        public IActionResult AddComment(CourseComment comment) 
        {
            //چون اون سمت فقط قسمت کامنت توسط کاربر پر می شود و فرستاده میشه بنابراین موارد دیگه رو خودمون اینجا پر میکنیم
            comment.IsDelete = false;
            comment.IsAdminRead = false;
            comment.CreateDate = DateTime.Now;
            comment.UserId = _userService.GetUserIdByUserName(User.Identity.Name);
            _courseService.AddComment(comment);

            return View("ShowComment",_courseService.GetCourseComment(comment.CourseId,1));
        }

        public IActionResult ShowComment(int id,int pageId=1) 
        {
            return View(_courseService.GetCourseComment(id,pageId));
        }

        public IActionResult CourseVote(int id) 
        {
            if (!_courseService.IsFree(id))
            {
                if (!_orderService.IsUserInCourse(User.Identity.Name,id))
                {
                    ViewBag.NotAccess = true;
                }
            }
            return PartialView(_courseService.GetCourseVote(id));
        }

        [Authorize]
        public IActionResult AddVote(int id,bool vote) 
        {
            int userId = _userService.GetUserIdByUserName(User.Identity.Name);
            _courseService.AddCourseVote(userId,id,vote);
            return PartialView("CourseVote",_courseService.GetCourseVote(id));
        }
    }
}
