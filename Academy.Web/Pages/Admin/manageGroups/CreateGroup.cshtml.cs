using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Academy.Core.Services;
using Academy.Core.Services.Interfaces;
using Academy.DataLayer.Entities.Course;

namespace Academy.Web.Pages.Admin.manageGroups
{
    public class CreateGroupModel : PageModel
    {
        private ICourseService _courseService;
        public CreateGroupModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseGroup CourseGroup { get; set; }
        public void OnGet(int ?id)
        {
            CourseGroup = new CourseGroup()
            {
                ParentId = id
            };
        }

        public IActionResult OnPost() 
        {
            if (!ModelState.IsValid)
                return Page();
            _courseService.AddCourseGroup(CourseGroup);
            return RedirectToPage("Index");
        }
    }
}
