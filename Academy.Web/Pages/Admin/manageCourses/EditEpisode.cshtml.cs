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
    public class EditEpisodeModel : PageModel
    {
        private ICourseService _courseService;
        public EditEpisodeModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [BindProperty]
        public CourseEpisode CourseEpisode { get; set; }
        public void OnGet(int id)
        {
            CourseEpisode = _courseService.GetCourseEpisodeById(id);
        }

        public IActionResult OnPost(IFormFile fileEpisode)
        {
            if (!ModelState.IsValid)
                return Page();

            if (fileEpisode != null)
            {
                if (_courseService.IsExistFile(fileEpisode.FileName))
                {
                    ViewData["IsExistFile"] = true;
                    return Page();
                }
            }


            _courseService.EditEpisode(CourseEpisode, fileEpisode);

            return Redirect("/Admin/manageCourses/IndexEpisode/" + CourseEpisode.CourseId);
        }
    }
}
