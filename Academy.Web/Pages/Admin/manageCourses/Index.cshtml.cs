using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.DTOs.Course;
using Academy.Core.Services.Interfaces;

namespace Academy.Web.Pages.Admin.manageCourses
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }


        public List<ShowCourseForAdminViewModel> showCourses { get; set; }
        public void OnGet()
        {
           showCourses=_courseService.showCourseForAdmins();
        }
    }
}
