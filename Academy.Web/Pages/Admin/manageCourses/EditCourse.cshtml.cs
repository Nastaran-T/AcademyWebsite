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
    public class EditCourseModel : PageModel
    {
        private ICourseService _courseService;
        public EditCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        [BindProperty]
        public Course Course { get; set; }

        public void OnGet(int id)
        {
            Course = _courseService.GetCourseById(id);

            //برای اینکه بنونیم اون دراپ هامونم پر کنیم باید مثل زیر بریم
            var groups = _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text",Course.CourseId);


            List<SelectListItem> subGroup = new List<SelectListItem>()
            {
                new SelectListItem(){ Text="انتخاب کنید..."}
            };
            subGroup.AddRange(_courseService.GetSubGroupForManageCourse(Course.GroupId));
            string SelectedSubGroup = null;
            if (SelectedSubGroup!=null)
            {
                SelectedSubGroup = Course.CourseGroupSub.ToString();
            }
            ViewData["SubGroups"] = new SelectList(subGroup, "Value", "Text",SelectedSubGroup);


            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text",Course.TeacherId);

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text",Course.LevelId);

            var status = _courseService.GetStatus();
            ViewData["Statues"] = new SelectList(status, "Value", "Text",Course.StatusId);
        }


        public IActionResult OnPost(IFormFile imgCourseUp, IFormFile demoUp) 
        {
            if (!ModelState.IsValid)
                return Page();

            _courseService.UpdateCourse(Course,imgCourseUp,demoUp);

            return RedirectToPage("Index");
                
        }
    }
}
