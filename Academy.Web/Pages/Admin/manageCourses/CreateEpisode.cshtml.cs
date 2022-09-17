using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Course;

namespace Academy.Web.Pages.Admin.manageCourses
{
    public class CreateEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public CreateEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }

        //ما در اینجا نیاز  به آی دی درس داریم که تو آدرس اومدهکه ما باید اون کد رو در هنگام افزودن بخش جدید بفرستیم 
        public void OnGet(int id)
        {
            //اگر نمونه نذاریم عملا خالیه 
            CourseEpisode courseEpisode = new CourseEpisode();
            courseEpisode. CourseId = id;
        }

        public IActionResult OnPost(int id,IFormFile fileEpisode) 
        {
            if (!ModelState.IsValid && fileEpisode == null)
                return Page();

            //چک کردن اینکه فایل تکراری نباشه
            if (_courseService.IsExistFile(fileEpisode.FileName))
            {
                ViewData["IsExist"] = true;
                return Page();
            }

             CourseEpisode.CourseId = id;
            _courseService.AddCourseEpisode(CourseEpisode, fileEpisode);
           
            return Redirect("/Admin/manageCourses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}
