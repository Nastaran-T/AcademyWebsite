using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Course;

namespace Academy.Web.Pages.Admin.manageCourses
{
   
    public class CreateCourseModel : PageModel
    {
        private ICourseService _courseService;
        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Course Course{ get; set; }

        public void OnGet()
        {
            var groups = _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = _courseService.GetSubGroupForManageCourse(int.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers,"Value","Text");

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels,"Value","Text");

            var status = _courseService.GetStatus();
            ViewData["Statues"] = new SelectList(status,"Value","Text");
        }

        public IActionResult OnPost(IFormFile imgCourseUp,IFormFile demoUp) 
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.AddCourse(Course, imgCourseUp, demoUp);

            return RedirectToPage("Index");
        }

    }
}
