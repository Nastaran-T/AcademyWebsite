using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Course;

namespace Academy.Web.Pages.Admin.manageGroups
{
    public class IndexModel : PageModel
    {
        private ICourseService _courseService;
        public IndexModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

       public List<CourseGroup> CourseGroups { get; set; }
        public void OnGet()
        {
           CourseGroups= _courseService.GetAllCourses();
        }
    }
}
