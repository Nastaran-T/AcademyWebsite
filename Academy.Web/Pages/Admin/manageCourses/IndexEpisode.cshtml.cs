using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Course;

namespace Academy.Web.Pages.Admin.manageCourses
{
    public class IndexEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public IndexEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        //کاربر وقتی که روی دکمه کلیک میکنه باید آی دی درس ارسال میشه اینجا
        public void OnGet(int id)
        {
            ViewData["CourseId"] = id;

            CourseEpisodes = _courseService.GetListEpisode(id);
        }
    }
}
